using Microsoft.CodeAnalysis;

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
}
