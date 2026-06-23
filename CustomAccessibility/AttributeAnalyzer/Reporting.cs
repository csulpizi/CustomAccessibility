using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessibility;

partial class AttributeAnalyzer
{
    protected virtual void ReportCACC100(SyntaxNodeAnalysisContext ctx, AttributeSyntax attribute)
    {
#if TEST
        if (IsDiagnosticExpected("CACC100", attribute))
            return;
#endif
        ctx.ReportDiagnostic(
            Diagnostic.Create(Rules.InvalidAttributeUsage.Rule, attribute.GetLocation())
        );
    }

    protected virtual void ReportCACC101(SyntaxNodeAnalysisContext ctx, AttributeSyntax attribute)
    {
#if TEST
        if (IsDiagnosticExpected("CACC101", attribute))
            return;
#endif
        ctx.ReportDiagnostic(
            Diagnostic.Create(Rules.MultipleAttributes.Rule, attribute.GetLocation())
        );
    }

    protected virtual void ReportNoIssueForAttribute(
        SyntaxNodeAnalysisContext ctx,
        AttributeSyntax attribute
    )
    {
#if TEST
        if (IsDiagnosticExpected("CACC100", attribute))
            ctx.ReportDiagnostic(
                Diagnostic.Create(
                    Rules.DiagnosticExpected.Create("CACC100", "attribute"),
                    attribute.GetLocation()
                )
            );
        if (IsDiagnosticExpected("CACC101", attribute))
            ctx.ReportDiagnostic(
                Diagnostic.Create(
                    Rules.DiagnosticExpected.Create("CACC101", "attribute"),
                    attribute.GetLocation()
                )
            );
#endif
    }
}
