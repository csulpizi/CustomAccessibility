using System.Collections.Immutable;
using CustomAccessiblity.Rules;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessiblity;

public partial class AttributeAnalyzer
{
    public virtual ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
        [InvalidAttributeUsage.Rule, MultipleAttributes.Rule];

    public void AnalyzeEnumDeclaration(SyntaxNodeAnalysisContext ctx) =>
        ValidateMemberDeclaration(ctx, (EnumDeclarationSyntax)ctx.Node);

    public void AnalyzeTypeDeclaration(SyntaxNodeAnalysisContext ctx)
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

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class AttributeAnalyzer_ : DiagnosticAnalyzer
{
    readonly AttributeAnalyzer analyzer = new();

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => analyzer.SupportedDiagnostics;

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(
            GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics
        );
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(analyzer.AnalyzeEnumDeclaration, SyntaxKind.EnumDeclaration);
        context.RegisterSyntaxNodeAction(analyzer.AnalyzeTypeDeclaration, SyntaxKind.ClassDeclaration);
        context.RegisterSyntaxNodeAction(analyzer.AnalyzeTypeDeclaration, SyntaxKind.InterfaceDeclaration);
        context.RegisterSyntaxNodeAction(analyzer.AnalyzeTypeDeclaration, SyntaxKind.StructDeclaration);
    }
}