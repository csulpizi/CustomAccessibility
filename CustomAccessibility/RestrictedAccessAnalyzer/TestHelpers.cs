#if TEST
using Microsoft.CodeAnalysis;
using System.Text.RegularExpressions;

namespace CustomAccessibility;

partial class RestrictedAccessAnalyzer
{
    static IEnumerable<string> ExpectedSymbols(string directiveText, string diagnosticCode)
    {
        var s = directiveText.Replace("#region", "").Replace(diagnosticCode, "");
        s = Regex.Replace(s, @"\s", "");
        return s.Split(',');
    }

    static bool DirectiveMatches(string directiveText, string symbolName, string diagnosticCode)
    {
        return directiveText.Contains(diagnosticCode)
            && ExpectedSymbols(directiveText, diagnosticCode).Contains(symbolName);
    }

    static bool IsDiagnosticExpected(SyntaxNode node, string symbolName, string diagnosticCode)
    {
        var root = node.SyntaxTree.GetRoot();
        var regions = Util.GetAllDirectiveRegions(
            root,
            directiveText => DirectiveMatches(directiveText, symbolName, diagnosticCode)
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
