using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessiblity.Rules;

static class InvalidAttributeUsage
{
    internal static readonly DiagnosticDescriptor Rule =
        new(
            "CACC100",
            "Invalid Attribute Usage",
            "`CustomAccessiblity` attributes can only be applied to declarations with the `internal` access modifier",
            "Usage",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );

    internal static void Report(SyntaxNodeAnalysisContext ctx, Location location)
    {
        ctx.ReportDiagnostic(Diagnostic.Create(Rule, location));
    }
}
