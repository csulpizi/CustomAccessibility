using System.Collections.Immutable;
using CustomAccessiblity.Rules;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessiblity;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class RestrictedAccessAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
        [RestrictedAccess.Rule, ImplicitlyRestrictedAccess.Rule, UsingStaticUseRestricted.Rule];

    public override void Initialize(AnalysisContext ctx)
    {
        ctx.ConfigureGeneratedCodeAnalysis(
            GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics
        );
        ctx.EnableConcurrentExecution();

        ctx.RegisterSyntaxNodeAction(ValidateUsingStatic, SyntaxKind.UsingDirective);
        ctx.RegisterSyntaxNodeAction(ValidateTypeDeclaration, SyntaxKind.ClassDeclaration);
        ctx.RegisterSyntaxNodeAction(ValidateTypeDeclaration, SyntaxKind.StructDeclaration);
        ctx.RegisterSyntaxNodeAction(ValidateTypeDeclaration, SyntaxKind.InterfaceDeclaration);
    }

    static void ValidateUsingStatic(SyntaxNodeAnalysisContext ctx)
    {
        var usingNode = (UsingDirectiveSyntax)ctx.Node;
        var isStatic = usingNode.ChildTokens().Any(t => t.IsKind(SyntaxKind.StaticKeyword));
        if (!isStatic)
            return;

        var nameNode = (QualifiedNameSyntax?)
            usingNode.ChildNodes().FirstOrDefault(x => x is QualifiedNameSyntax);
        if (nameNode is null)
            return;

        var symbol = ctx.SemanticModel.GetSymbolInfo(nameNode).Symbol;
        if (symbol is null)
            return;

        var parent = usingNode.Parent;
        if (parent is null)
            return;

        foreach (
            var typeDeclaration in parent
                .DescendantNodes()
                .Where(node => node is TypeDeclarationSyntax)
                .Cast<TypeDeclarationSyntax>()
        )
        {
            var declaringSymbol =
                ctx.SemanticModel.GetDeclaredSymbol(typeDeclaration)
                ?? throw new Exception("Should not happen!");
            if (Validate(symbol, declaringSymbol) != ValidationResult.NoError)
            {
                UsingStaticUseRestricted.Report(
                    ctx,
                    symbol,
                    declaringSymbol,
                    usingNode.GetLocation()
                );
                continue;
            }
        }
    }

    static void ValidateTypeDeclaration(SyntaxNodeAnalysisContext ctx)
    {
        var node = (TypeDeclarationSyntax)ctx.Node;
        var declaringSymbol =
            ctx.SemanticModel.GetDeclaredSymbol(node) ?? throw new Exception("Should not happen!");
        ValidateAllReferencedTypes(ctx, node, declaringSymbol);
    }

    static void ValidateAllReferencedTypes(
        SyntaxNodeAnalysisContext ctx,
        TypeDeclarationSyntax node,
        INamedTypeSymbol declaringSymbol
    )
    {
        Dictionary<int, ValidationResult> cache = [];
        var descendents = node.DescendantNodes(x => node == x || x is not TypeDeclarationSyntax)
            .Where(x => x is TypeSyntax)
            .Cast<TypeSyntax>();
        foreach (var typeReference in descendents)
        {
            var symbol = ctx.SemanticModel.GetSymbolInfo(typeReference).Symbol;
            if (symbol is null)
                continue;

            var symbolHash = SymbolEqualityComparer.Default.GetHashCode(symbol);
            if (!cache.TryGetValue(symbolHash, out var validation))
            {
                validation = Validate(symbol, declaringSymbol);
                cache[symbolHash] = validation;
            }

            if (validation is ValidationResult.NoError) { }
            else if (validation is ValidationResult.ImplicitlyRestrictedAccess)
                ImplicitlyRestrictedAccess.Report(ctx, typeReference.GetLocation());
            else
                RestrictedAccess.Report(ctx, symbol, typeReference.GetLocation());
        }
    }

    enum ValidationResult
    {
        NoError,
        RestrictedAccess,
        ImplicitlyRestrictedAccess
    }

    static ValidationResult Validate(ISymbol? symbol, INamedTypeSymbol declaringSymbol)
    {
        // We only need to check non-null symbols with 'Internal' accessibility
        if (symbol is null || !Util.IsDeclaredInternal(symbol))
            return ValidationResult.NoError;

        // Always allow private or protected access
        if (Util.IsPrivateOrProtectedAccess(symbol, declaringSymbol))
            return ValidationResult.NoError;

        var isExternalAccess = Util.IsExternalAccess(declaringSymbol, symbol);
        var accessibilityType = Util.GetAccessibilityType(symbol);

        if (isExternalAccess && accessibilityType == AccessibilityType.InternalAccessOnly)
            return ValidationResult.RestrictedAccess;
        if (!isExternalAccess && accessibilityType == AccessibilityType.ExternalAccessOnly)
            return ValidationResult.RestrictedAccess;

        var classes = Util.GetClassRegexes(symbol);
        var fullyQualifiedDeclardingName = Util.FullyQualifiedClassName(declaringSymbol);

        if (
            classes.Any()
            && !classes.Any(regex => regex.IsMatch(declaringSymbol.Name))
            && !classes.Any(regex => regex.IsMatch(fullyQualifiedDeclardingName))
        )
            return ValidationResult.RestrictedAccess;

        if (isExternalAccess && accessibilityType == AccessibilityType.Default)
        {
            return ValidationResult.ImplicitlyRestrictedAccess;
        }
        return ValidationResult.NoError;
    }
}
