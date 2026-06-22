using System.Collections.Immutable;
using CustomAccessiblity.Rules;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessiblity;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class AttributeAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
        [InvalidAttributeUsage.Rule, MultipleAttributes.Rule];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(
            GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics
        );
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeDeclaration, SyntaxKind.EnumDeclaration);
        context.RegisterSyntaxNodeAction(AnalyzeTypeDeclaration, SyntaxKind.ClassDeclaration);
        context.RegisterSyntaxNodeAction(AnalyzeTypeDeclaration, SyntaxKind.InterfaceDeclaration);
        context.RegisterSyntaxNodeAction(AnalyzeTypeDeclaration, SyntaxKind.StructDeclaration);
    }

    static void AnalyzeDeclaration(SyntaxNodeAnalysisContext ctx) =>
        ValidateSymbol(ctx, ctx.Node, ctx.SemanticModel.GetDeclaredSymbol(ctx.Node));

    static void AnalyzeTypeDeclaration(SyntaxNodeAnalysisContext ctx)
    {
        // Type itself
        var node = (TypeDeclarationSyntax)ctx.Node;
        var declared = ctx.SemanticModel.GetDeclaredSymbol(node);
        if (declared is null)
            return;
        ValidateSymbol(ctx, node, declared);

        var members = node.ChildNodes()
            .Where(child => child is MemberDeclarationSyntax)
            .Cast<MemberDeclarationSyntax>();
        foreach (var member in members)
            ValidateMemberDeclaration(ctx, member);
    }

    //FIXME: FACTOR OUT!!!!!@
    static void ValidateMemberDeclaration(
        SyntaxNodeAnalysisContext ctx,
        MemberDeclarationSyntax node
    )
    {
        var isInternalAccessibility = node.Modifiers.Any(SyntaxKind.InternalKeyword);
        var attributes = node.AttributeLists.Select(x => x.Attributes).SelectMany(x => x);
        int nAccessibilityAttributes = 0;

        foreach (var attribute in attributes)
        {
            Location location = attribute.GetLocation();
            var attributeName = attribute.Name.ToString();
            if (
                nameof(Attributes.InternalAccessOnly) == attributeName
                || nameof(Attributes.ExternalAccessOnly) == attributeName
                || nameof(Attributes.AccessibleByAll) == attributeName
                || nameof(Attributes.OnlyAccessibleBy) == attributeName
            )
            {
                if (nameof(Attributes.OnlyAccessibleBy) != attributeName)
                    nAccessibilityAttributes++;
                if (nAccessibilityAttributes > 1)
                {
                    MultipleAttributes.Report(ctx, location);
                }
                if (!isInternalAccessibility)
                {
                    InvalidAttributeUsage.Report(ctx, location);
                }
            }
        }
    }

    static void ValidateSymbol(SyntaxNodeAnalysisContext ctx, SyntaxNode node, ISymbol? symbol)
    {
        if (symbol is null)
            return;
        var isInternalAccessibility = false;
        if (symbol.DeclaredAccessibility == Accessibility.Internal)
            isInternalAccessibility = true;

        var attributes = symbol.GetAttributes();
        var attributeSyntaxes = node.ChildNodes()
            .Where(n => n is AttributeListSyntax)
            .Cast<AttributeListSyntax>();

        int nAccessibilityAttributes = 0;
        for (int i = 0; i < attributes.Length; i++)
        {
            Location location = node.GetLocation();

            if (attributeSyntaxes.Count() > i)
                location = attributeSyntaxes.ElementAt(i).GetLocation();

            if (
                !isInternalAccessibility
                && (
                    nameof(Attributes.InternalAccessOnly) == attributes[i].AttributeClass?.Name
                    || nameof(Attributes.ExternalAccessOnly) == attributes[i].AttributeClass?.Name
                    || nameof(Attributes.AccessibleByAll) == attributes[i].AttributeClass?.Name
                    || nameof(Attributes.OnlyAccessibleBy) == attributes[i].AttributeClass?.Name
                )
            )
            {
                InvalidAttributeUsage.Report(ctx, location);
            }

            if (
                nameof(Attributes.InternalAccessOnly) == attributes[i].AttributeClass?.Name
                || nameof(Attributes.ExternalAccessOnly) == attributes[i].AttributeClass?.Name
                || nameof(Attributes.AccessibleByAll) == attributes[i].AttributeClass?.Name
            )
            {
                nAccessibilityAttributes++;
                if (nAccessibilityAttributes > 1)
                {
                    MultipleAttributes.Report(ctx, location);
                }
            }
        }
    }
}
