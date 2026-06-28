using Microsoft.CodeAnalysis;

namespace CustomAccessibility.Analyzer.Rules;

static class UsingStaticUseRestricted
{
    internal static readonly DiagnosticDescriptor Rule = Create(
        "this imported static class",
        "a type declared in this file",
        ""
    );

    internal static DiagnosticDescriptor Create(string usingClass, string declaredClass, string append) =>
        new(
            "CACC002",
            "Restricted 'using static'",
            $"Use of {usingClass} is restricted; {declaredClass} is not allowed to access it{append}",
            "Usage",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            helpLinkUri: "https://github.com/csulpizi/CustomAccessibility/blob/main/Documentation/DiagnosticRules.md#cacc002"
        );
}
