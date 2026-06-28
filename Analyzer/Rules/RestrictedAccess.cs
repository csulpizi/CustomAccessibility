using Microsoft.CodeAnalysis;

namespace CustomAccessibility.Analyzer.Rules;

static class RestrictedAccess
{
    internal static readonly DiagnosticDescriptor Rule = Create("symbol");

    internal static DiagnosticDescriptor Create(string nested) =>
        new(
            "CACC000",
            "Restricted Access",
            $"Use of {nested} is restricted and cannot be used here",
            "Usage",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            helpLinkUri: "https://github.com/csulpizi/CustomAccessibility/Documentation/DiagnosticRules.md#cacc000"
        );
}
