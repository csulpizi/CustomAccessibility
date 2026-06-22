using CustomAccessiblity.Rules;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessiblity;

partial class RestrictedAccessAnalyzer
{
    protected virtual void ReportCACC000(
        SyntaxNodeAnalysisContext ctx,
        ISymbol symbol,
        Location location
    )
    {
        ctx.ReportDiagnostic(Diagnostic.Create(RestrictedAccess.Create(symbol.Name), location));
    }

    protected virtual void ReportCACC001(
        SyntaxNodeAnalysisContext ctx,
        ISymbol symbol,
        Location location
    )
    {
        ctx.ReportDiagnostic(Diagnostic.Create(ImplicitlyRestrictedAccess.Rule, location));
    }

    protected virtual void ReportCACC002(
        SyntaxNodeAnalysisContext ctx,
        ISymbol importedType,
        INamedTypeSymbol declaredType,
        UsingDirectiveSyntax usingNode
    )
    {
        ctx.ReportDiagnostic(
            Diagnostic.Create(
                UsingStaticUseRestricted.Create(importedType.Name, declaredType.Name),
                usingNode.GetLocation()
            )
        );
    }

    protected virtual void ReportNoAccessIssueForSymbol(
        SyntaxNodeAnalysisContext ctx,
        ISymbol symbol,
        TypeSyntax typeReference
    ) { }

    protected virtual void ReportNoIssueWithStaticImport(
        SyntaxNodeAnalysisContext ctx,
        ISymbol importedType,
        UsingDirectiveSyntax node
    ) { }
}