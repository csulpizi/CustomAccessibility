using Microsoft.CodeAnalysis;

namespace CustomAccessibility.Analyzer.Rules;

static class InvalidAttributeUsage
{
    internal static readonly DiagnosticDescriptor Rule =
        new(
            "CACC100",
            "Invalid Attribute Usage",
            "`CustomAccessibility` attributes can only be applied to declarations with the `internal` access modifier",
            "Usage",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            helpLinkUri: "https://github.com/csulpizi/CustomAccessibility/blob/main/Documentation/DiagnosticRules.md#cacc100"
        );
}
