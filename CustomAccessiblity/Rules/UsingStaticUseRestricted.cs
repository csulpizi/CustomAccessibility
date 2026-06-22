using Microsoft.CodeAnalysis;

namespace CustomAccessiblity.Rules;

static class UsingStaticUseRestricted
{
    internal static readonly DiagnosticDescriptor Rule = Create(
        "this imported static class",
        "a class declared in this file"
    );

    internal static DiagnosticDescriptor Create(string usingClass, string declaredClass) =>
        new(
            "CACC002",
            "Restricted 'using static'",
            $"Use of {usingClass} is restricted; {declaredClass} is not allowed to access it",
            "Usage",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );
}
