using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessiblity.Rules;

static class RestrictedAccess
{
    internal static readonly DiagnosticDescriptor Rule = Create("symbol");

    static DiagnosticDescriptor Create(string nested) =>
        new(
            "CACC000",
            "Restricted Access",
            $"Use of {nested} is restricted and cannot be used here",
            "Usage",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );

    internal static void Report(SyntaxNodeAnalysisContext ctx, ISymbol symbol, Location location)
    {
        ctx.ReportDiagnostic(Diagnostic.Create(Create(symbol.Name), location));
    }
}
