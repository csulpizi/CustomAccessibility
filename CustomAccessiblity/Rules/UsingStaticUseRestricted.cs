using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessiblity.Rules;

static class UsingStaticUseRestricted
{
    internal static readonly DiagnosticDescriptor Rule = Create(
        "this imported static class",
        "a class declared in this file"
    );

    // FIXME: rm internal
    internal static DiagnosticDescriptor Create(string usingClass, string declaredClass) =>
        new(
            "CACC002",
            "Restricted 'using static'",
            $"Use of {usingClass} is restricted; {declaredClass} is not allowed to access it",
            "Usage",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true
        );

    internal static void Report(
        SyntaxNodeAnalysisContext ctx,
        ISymbol importedClass,
        ISymbol declaredClass,
        Location location
    )
    {
        ctx.ReportDiagnostic(
            Diagnostic.Create(Create(importedClass.Name, declaredClass.Name), location)
        );
    }
}
