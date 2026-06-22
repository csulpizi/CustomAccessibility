using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessiblity.Rules;

static class ImplicitlyRestrictedAccess
{
    internal static readonly DiagnosticDescriptor Rule =
        new(
            "CACC001",
            "Restricted Access",
            "External access is restricted unless explicitly specified",
            "Usage",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );

    internal static void Report(SyntaxNodeAnalysisContext ctx, Location location)
    {
        ctx.ReportDiagnostic(Diagnostic.Create(Rule, location));
    }
}
