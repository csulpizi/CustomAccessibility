using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessiblity;

partial class AttributeAnalyzer
{
    void ValidateMemberDeclaration(SyntaxNodeAnalysisContext ctx, MemberDeclarationSyntax node)
    {
        bool isInternalAccessibility;
        if (node is TypeDeclarationSyntax || node is EnumDeclarationSyntax)
        {
            var declaredSymbol =
                ctx.SemanticModel.GetDeclaredSymbol(node)
                ?? throw new Exception("Should never happen!");
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
        int nAccessibilityAttributes = 0;
        foreach (var attribute in attributes)
        {
            Location location = attribute.GetLocation();
            //FIXME: Could this be less jank?
            var attributeName = attribute.Name.ToString().Split('.').Last();
            if (
                nameof(Attributes.InternalAccessOnly) == attributeName
                || nameof(Attributes.ExternalAccessOnly) == attributeName
                || nameof(Attributes.AccessibleByAll) == attributeName
                || nameof(Attributes.OnlyAccessibleBy) == attributeName
            )
            {
                if (nameof(Attributes.OnlyAccessibleBy) != attributeName)
                    nAccessibilityAttributes++;
                if (!isInternalAccessibility)
                {
                    ReportCACC100(ctx, attributes, location);
                    return;
                }
                if (nAccessibilityAttributes > 1)
                {
                    ReportCACC101(ctx, attributes, location);
                    return;
                }
            }
        }
        ReportNoIssueForAttributes(ctx, attributes);
    }
}
