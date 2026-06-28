using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessibility.Analyzer;

partial class AttributeAnalyzer
{
    void ValidateMemberDeclaration(SyntaxNodeAnalysisContext ctx, MemberDeclarationSyntax node)
    {
        bool isInternalAccessibility;
        if (node is TypeDeclarationSyntax || node is EnumDeclarationSyntax)
        {
            var declaredSymbol =
                ctx.SemanticModel.GetDeclaredSymbol(node)
                ?? throw new Exception("TypeDeclarationSyntax somehow has no DeclaredSymbol");
            isInternalAccessibility = Util.IsDeclaredInternal(declaredSymbol);
        }
        else
            isInternalAccessibility = node.Modifiers.Any(SyntaxKind.InternalKeyword);
        var attributes = node.AttributeLists.Select(x => x.Attributes).SelectMany(x => x);
        ValidateAttributes(ctx, attributes, isInternalAccessibility);
    }

    void ValidateAttributes(
        SyntaxNodeAnalysisContext ctx,
        IEnumerable<AttributeSyntax> attributes,
        bool isInternalAccessibility
    )
    {
        foreach (var attribute in attributes)
        {
            var attributeName = attribute.Name.ToString().Split('.').Last();
            if (
                nameof(Attributes.ExternalAccessAllowed) == attributeName
                || nameof(Attributes.OnlyAccessibleBy) == attributeName
            )
            {
                if (!isInternalAccessibility)
                {
                    ReportCACC100(ctx, attribute);
                    return;
                }
                else
                {
                    ReportNoIssueForAttribute(ctx, attribute);
                }
            }
        }
    }
}
