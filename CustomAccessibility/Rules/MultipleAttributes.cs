using Microsoft.CodeAnalysis;

namespace CustomAccessibility.Rules;

static class MultipleAttributes
{
    internal static readonly DiagnosticDescriptor Rule =
        new(
            "CACC101",
            "Invalid Attribute Usage",
            $"Cannot specify multiples of {nameof(Attributes.InternalAccessOnly)}, {nameof(Attributes.ExternalAccessOnly)}, and {nameof(Attributes.AccessibleByInternalAndExternal)}",
            "Usage",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );
}
