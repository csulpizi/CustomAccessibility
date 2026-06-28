#if TEST
using Microsoft.CodeAnalysis;

namespace CustomAccessibility.Analyzer.Rules;

static class DiagnosticExpected
{
    static DiagnosticDescriptor Create_(string codeExpected, string nested) =>
        new(
            "CACCXXX",
            "Diagnostic Expected",
            $"Expected {codeExpected} to be reported{nested}",
            "Testing",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );

    internal static DiagnosticDescriptor Create(string codeExpected, string symbol) =>
        Create_(codeExpected, " for " + symbol);

    internal static readonly DiagnosticDescriptor Rule = Create_("diagnostic", "");
}
#endif
