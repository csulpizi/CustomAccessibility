using System.Collections.Immutable;
using CustomAccessibility.Rules;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessibility;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public partial class AttributeAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
    {
        get
        {
#if TEST
            return [InvalidAttributeUsage.Rule, MultipleAttributes.Rule, DiagnosticExpected.Rule];
#else
            return [InvalidAttributeUsage.Rule, MultipleAttributes.Rule];
#endif
        }
    }

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(
            GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics
        );
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeEnumDeclaration, SyntaxKind.EnumDeclaration);
        context.RegisterSyntaxNodeAction(AnalyzeTypeDeclaration, SyntaxKind.ClassDeclaration);
        context.RegisterSyntaxNodeAction(AnalyzeTypeDeclaration, SyntaxKind.InterfaceDeclaration);
        context.RegisterSyntaxNodeAction(AnalyzeTypeDeclaration, SyntaxKind.StructDeclaration);
    }

    void AnalyzeEnumDeclaration(SyntaxNodeAnalysisContext ctx) =>
        ValidateMemberDeclaration(ctx, (EnumDeclarationSyntax)ctx.Node);

    void AnalyzeTypeDeclaration(SyntaxNodeAnalysisContext ctx)
    {
        var node = (TypeDeclarationSyntax)ctx.Node;
        ValidateMemberDeclaration(ctx, node);

        var members = node.ChildNodes()
            .Where(child => child is MemberDeclarationSyntax)
            .Cast<MemberDeclarationSyntax>();
        foreach (var member in members)
            ValidateMemberDeclaration(ctx, member);
    }
}
