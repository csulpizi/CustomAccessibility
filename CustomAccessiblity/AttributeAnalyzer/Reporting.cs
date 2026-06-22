using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessiblity;

partial class AttributeAnalyzer
{
    protected virtual void ReportCACC100(SyntaxNodeAnalysisContext ctx,
                                         IEnumerable<AttributeSyntax> attributes,
                                         Location location)
    {
        ctx.ReportDiagnostic(Diagnostic.Create(Rules.InvalidAttributeUsage.Rule, location));
    }

    protected virtual void ReportCACC101(SyntaxNodeAnalysisContext ctx,
                                         IEnumerable<AttributeSyntax> attributes,
                                         Location location)
    {

        ctx.ReportDiagnostic(Diagnostic.Create(Rules.MultipleAttributes.Rule, location));
    }

    protected virtual void ReportNoIssueForAttributes(SyntaxNodeAnalysisContext ctx,
                            IEnumerable<AttributeSyntax> attributes) {}
}