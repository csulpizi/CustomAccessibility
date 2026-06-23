#if TEST
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CustomAccessibility;

partial class AttributeAnalyzer
{
    static bool IsDiagnosticExpected(string diagnosticCode, AttributeSyntax node)
    {
        var root = node.SyntaxTree.GetRoot();
        var regions = Util.GetAllDirectiveRegions(
            root,
            directiveText => directiveText.Contains(diagnosticCode)
        );
        foreach (var region in regions)
        {
            if (region.Contains(node.Span))
                return true;
        }
        return false;
    }
}
#endif
