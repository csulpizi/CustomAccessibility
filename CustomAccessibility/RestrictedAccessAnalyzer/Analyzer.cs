using System.Collections.Immutable;
using CustomAccessibility.Rules;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessibility;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public partial class RestrictedAccessAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
    {
        get
        {
#if TEST
            return
            [
                RestrictedAccess.Rule,
                ImplicitlyRestrictedAccess.Rule,
                UsingStaticUseRestricted.Rule,
                DiagnosticExpected.Rule
            ];
#else
            return
            [
                RestrictedAccess.Rule,
                ImplicitlyRestrictedAccess.Rule,
                UsingStaticUseRestricted.Rule
            ];
#endif
        }
    }

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

    void ValidateUsingStatic(SyntaxNodeAnalysisContext ctx)
    {
        var usingNode = (UsingDirectiveSyntax)ctx.Node;
        var isStatic = usingNode.ChildTokens().Any(t => t.IsKind(SyntaxKind.StaticKeyword));
        if (!isStatic)
            return;

        var nameNode = (QualifiedNameSyntax?)
            usingNode.ChildNodes().FirstOrDefault(x => x is QualifiedNameSyntax);
        if (nameNode is null)
            return;

        var importedType = ctx.SemanticModel.GetSymbolInfo(nameNode).Symbol;
        if (importedType is null)
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
            var declaredType =
                ctx.SemanticModel.GetDeclaredSymbol(typeDeclaration)
                ?? throw new Exception("Should not happen!");
            if (ValidateAccessibility(importedType, declaredType) != ValidationResult.NoError)
            {
                ReportCACC002(ctx, importedType, declaredType, usingNode);
                return;
            }
        }
        ReportNoIssueWithStaticImport(ctx, importedType, usingNode);
    }

    void ValidateTypeDeclaration(SyntaxNodeAnalysisContext ctx)
    {
        var node = (TypeDeclarationSyntax)ctx.Node;
        var declaringType =
            ctx.SemanticModel.GetDeclaredSymbol(node) ?? throw new Exception("Should not happen!");
        ValidateAllReferencedTypes(ctx, node, declaringType);
    }

    void ValidateAllReferencedTypes(
        SyntaxNodeAnalysisContext ctx,
        TypeDeclarationSyntax node,
        INamedTypeSymbol enclosingType
    )
    {
        Dictionary<int, ValidationResult> cache = [];
        var descendents = node.DescendantNodes(x =>
                node == x || (x is not TypeDeclarationSyntax && x is not AttributeSyntax)
            )
            .Where(x => x is TypeSyntax)
            .Where(node => node is not QualifiedNameSyntax)
            .Cast<TypeSyntax>();
        foreach (var typeReference in descendents)
        {
            var symbol = ctx.SemanticModel.GetSymbolInfo(typeReference).Symbol;
            if (symbol is null)
                continue;

            var symbolHash = SymbolEqualityComparer.Default.GetHashCode(symbol);
            if (!cache.TryGetValue(symbolHash, out var validation))
            {
                validation = ValidateAccessibility(symbol, enclosingType);
                cache[symbolHash] = validation;
            }

            if (validation is ValidationResult.NoError)
                ReportNoAccessIssueForReferencedType(ctx, typeReference);
            else if (validation is ValidationResult.ImplicitlyRestrictedAccess)
                ReportCACC001(ctx, typeReference);
            else
                ReportCACC000(ctx, typeReference);
        }
    }
}
