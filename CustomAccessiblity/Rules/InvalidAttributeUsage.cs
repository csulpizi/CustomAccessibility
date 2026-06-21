using Microsoft.CodeAnalysis;

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
}
