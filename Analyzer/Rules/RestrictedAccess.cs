using Microsoft.CodeAnalysis;

namespace CustomAccessibility.Analyzer.Rules;

static class RestrictedAccess
{
    internal static readonly DiagnosticDescriptor Rule = Create("symbol", "");

    internal static DiagnosticDescriptor Create(string nested, string append) =>
        new(
            "CACC000",
            "Restricted Access",
            $"Use of {nested} is restricted and cannot be used here{append}",
            "Usage",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            helpLinkUri: "https://github.com/csulpizi/CustomAccessibility/blob/main/Documentation/DiagnosticRules.md#cacc000"
        );
}
