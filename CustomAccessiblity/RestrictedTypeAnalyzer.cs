using System.Collections.Immutable;
using CustomAccessiblity.Rules;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessiblity;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class RestrictedTypeAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
        [RestrictedAccess.Rule, ImplicitlyRestrictedAccess.Rule];

    public override void Initialize(AnalysisContext ctx)
    {
        ctx.ConfigureGeneratedCodeAnalysis(
            GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics
        );
        ctx.EnableConcurrentExecution();

        ctx.RegisterSyntaxNodeAction(ValidateAllReferencedTypes, SyntaxKind.ClassDeclaration);
        ctx.RegisterSyntaxNodeAction(ValidateAllReferencedTypes, SyntaxKind.StructDeclaration);
    }

    static void ValidateAllReferencedTypes(SyntaxNodeAnalysisContext ctx)
    {
        var node = (TypeDeclarationSyntax)ctx.Node;
        var declaringSymbol =
            ctx.SemanticModel.GetDeclaredSymbol(node) ?? throw new Exception("Should not happen!");
        var descendents = node.DescendantNodes().Where(x => x is TypeSyntax).Cast<TypeSyntax>();
        foreach (var typeReference in descendents)
        {
            var symbol = ctx.SemanticModel.GetSymbolInfo(typeReference).Symbol;
            Validate(ctx, symbol, declaringSymbol, typeReference.GetLocation);
        }
    }

    static void Validate(
        SyntaxNodeAnalysisContext ctx,
        ISymbol? symbol,
        INamedTypeSymbol declaringSymbol,
        Func<Location> getLocation
    )
    {
        // We only need to check non-null symbols with 'Internal' accessibility
        if (symbol is null || symbol.DeclaredAccessibility != Accessibility.Internal)
            return;

        // Always allow private access
        var isPrivateAccess =
            symbol.ContainingType is not null
            && SymbolEqualityComparer.Default.Equals(symbol.ContainingType, declaringSymbol);
        if (isPrivateAccess)
            return;

        var isExternalAccess = Util.IsExternalAccess(declaringSymbol, symbol);
        var accessibilityType = Util.GetAccessibilityType(symbol);

        if (isExternalAccess && accessibilityType == AccessibilityType.InternalAccessOnly)
        {
            ctx.ReportDiagnostic(
                Diagnostic.Create(RestrictedAccess.Create(symbol.Name), getLocation())
            );
            return;
        }
        if (!isExternalAccess && accessibilityType == AccessibilityType.ExternalAccessOnly)
        {
            ctx.ReportDiagnostic(
                Diagnostic.Create(RestrictedAccess.Create(symbol.Name), getLocation())
            );
            return;
        }

        var classes = Util.GetClassRegexes(symbol);
        var fullyQualifiedDeclardingName = Util.FullyQualifiedClassName(declaringSymbol);

        if (
            classes.Any()
            && !classes.Any(regex => regex.IsMatch(declaringSymbol.Name))
            && !classes.Any(regex => regex.IsMatch(fullyQualifiedDeclardingName))
        )
        {
            ctx.ReportDiagnostic(
                Diagnostic.Create(RestrictedAccess.Create(symbol.Name), getLocation())
            );
            return;
        }

        if (isExternalAccess && accessibilityType == AccessibilityType.Default)
        {
            ctx.ReportDiagnostic(Diagnostic.Create(ImplicitlyRestrictedAccess.Rule, getLocation()));
            return;
        }
    }
}
