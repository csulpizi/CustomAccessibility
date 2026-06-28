using CustomAccessibility.Analyzer.Rules;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessibility.Analyzer;

partial class RestrictedAccessAnalyzer
{
    protected virtual void ReportCACC000(SyntaxNodeAnalysisContext ctx, TypeSyntax typeReference)
    {
#if TEST
        if (IsDiagnosticExpected(typeReference, typeReference.ToString(), "CACC000"))
            return;
#endif
        ctx.ReportDiagnostic(
            Diagnostic.Create(
                RestrictedAccess.Create(typeReference.ToString()),
                typeReference.GetLocation()
            )
        );
    }

    protected virtual void ReportCACC001(SyntaxNodeAnalysisContext ctx, TypeSyntax typeReference)
    {
#if TEST
        if (IsDiagnosticExpected(typeReference, typeReference.ToString(), "CACC001"))
            return;
#endif
        ctx.ReportDiagnostic(
            Diagnostic.Create(ImplicitlyRestrictedAccess.Rule, typeReference.GetLocation())
        );
    }

    protected virtual void ReportCACC002(
        SyntaxNodeAnalysisContext ctx,
        ISymbol importedType,
        INamedTypeSymbol declaredType,
        UsingDirectiveSyntax usingNode
    )
    {
#if TEST
        if (IsDiagnosticExpected(usingNode, importedType.ToString(), "CACC002"))
            return;
#endif
        ctx.ReportDiagnostic(
            Diagnostic.Create(
                UsingStaticUseRestricted.Create(importedType.Name, declaredType.Name),
                usingNode.GetLocation()
            )
        );
    }

    protected virtual void ReportNoAccessIssueForReferencedType(
        SyntaxNodeAnalysisContext ctx,
        TypeSyntax typeReference
    )
    {
#if TEST
        if (IsDiagnosticExpected(typeReference, typeReference.ToString(), "CACC000"))
            ctx.ReportDiagnostic(
                Diagnostic.Create(
                    DiagnosticExpected.Create("CACC000", typeReference.ToString()),
                    typeReference.GetLocation()
                )
            );
        if (IsDiagnosticExpected(typeReference, typeReference.ToString(), "CACC001"))
            ctx.ReportDiagnostic(
                Diagnostic.Create(
                    DiagnosticExpected.Create("CACC001", typeReference.ToString()),
                    typeReference.GetLocation()
                )
            );
#endif
    }

    protected virtual void ReportNoIssueWithStaticImport(
        SyntaxNodeAnalysisContext ctx,
        ISymbol importedType,
        UsingDirectiveSyntax usingNode
    )
    {
#if TEST
        if (IsDiagnosticExpected(usingNode, importedType.ToString(), "CACC002"))
            ctx.ReportDiagnostic(
                Diagnostic.Create(
                    DiagnosticExpected.Create("CACC002", importedType.ToString()),
                    usingNode.GetLocation()
                )
            );
#endif
    }
}
