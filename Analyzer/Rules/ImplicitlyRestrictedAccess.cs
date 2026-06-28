using Microsoft.CodeAnalysis;

namespace CustomAccessibility.Analyzer.Rules;

static class ImplicitlyRestrictedAccess
{
    internal static readonly DiagnosticDescriptor Rule =
        new(
            "CACC001",
            "Restricted External Access",
            "External access is restricted unless explicitly specified",
            "Usage",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            helpLinkUri: "https://github.com/csulpizi/CustomAccessibility/blob/main/Documentation/DiagnosticRules.md#cacc001"
        );
}
