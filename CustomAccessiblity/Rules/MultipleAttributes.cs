using Microsoft.CodeAnalysis;

namespace CustomAccessiblity.Rules;

static class MultipleAttributes
{
    internal static readonly DiagnosticDescriptor Rule =
        new(
            "CACC101",
            "Invalid Attribute Usage",
            $"Cannot specify multiples of {nameof(Attributes.InternalAccessOnly)}, {nameof(Attributes.ExternalAccessOnly)}, and {nameof(Attributes.AccessibleByAll)}",
            "Usage",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );
}
