using System.Text.RegularExpressions;
using CustomAccessibility.Attributes;
using Microsoft.CodeAnalysis;
#if TEST
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
#endif

namespace CustomAccessibility.Analyzer;

[ExternalAccessAllowed]
static class Util
{
    internal static bool IsExternalAllowed(ISymbol symbol) =>
        symbol.GetAttributes().Any(x => nameof(ExternalAccessAllowed) == x.AttributeClass?.Name);

    internal static bool IsDeclaredInternal(ISymbol symbol)
    {
        return symbol.DeclaredAccessibility == Accessibility.Internal
            || symbol.DeclaredAccessibility == Accessibility.ProtectedOrInternal;
    }

    static IEnumerable<AttributeData> GetOnlyAccessibleAttributes(ISymbol symbol)
    {
        return symbol
            .GetAttributes()
            .Where(x => nameof(OnlyAccessibleBy) == x.AttributeClass?.Name);
    }

    internal static IEnumerable<string> GetAllowList(ISymbol symbol)
    {
        return GetOnlyAccessibleAttributes(symbol).Select(CoerceAttributeToOnlyAccessibleString);
    }

    internal static IEnumerable<Regex> GetAllowListRegexes(ISymbol symbol)
    {
        return GetAllowList(symbol).Select(CoerceWildCardStringToRegex);
    }

    internal static bool IsExternalAccess(INamedTypeSymbol declaringClassSymbol, ISymbol subject)
    {
        return !SymbolEqualityComparer.Default.Equals(
            declaringClassSymbol.ContainingModule,
            subject.OriginalDefinition.ContainingModule
        );
    }

    internal static string FullName(this ISymbol @this)
    {
        var s = @this.Name;
        var parent = @this.ContainingType;
        while (parent is not null && parent.Name != "")
        {
            s = parent.Name + "." + s;
            parent = parent.ContainingType;
        }

        var ns = (parent ?? @this).ContainingNamespace;
        while (ns is not null && ns.Name != "")
        {
            s = ns.Name + "." + s;
            ns = ns.ContainingNamespace;
        }
        return s;
    }

    static string CoerceAttributeToOnlyAccessibleString(AttributeData attribute)
    {
        var val = attribute.ConstructorArguments.FirstOrDefault().Value;
        if (val is null)
            return "";
        return (string)val;
    }

    // Allow external for unit testing
    [ExternalAccessAllowed]
    internal static Regex CoerceWildCardStringToRegex(string s)
    {
        s = "^" + s + "$";
        s = s.Replace(".", "\\.").Replace("**", "[^\\s]{0,}").Replace("*", "[^.\\s]{0,}");
        return new(s);
    }

    internal static bool IsPrivateOrProtectedAccess(ISymbol symbol, ITypeSymbol declaringSymbol)
    {
        if (SymbolEqualityComparer.Default.Equals(symbol, declaringSymbol))
            return true;

        var ogDeclarer = symbol.ContainingType;
        if (ogDeclarer is null)
            return false;

        // Is symbol declared in this class?
        if (SymbolEqualityComparer.Default.Equals(ogDeclarer, declaringSymbol))
            return true;

        // Is symbol inherited?
        var baseType = declaringSymbol.BaseType;
        while (baseType is not null)
        {
            if (SymbolEqualityComparer.Default.Equals(ogDeclarer, baseType))
                return true;
            baseType = baseType.BaseType;
        }
        return false;
    }

#if TEST
    static TextSpan GetRegionForDirective(DirectiveTriviaSyntax directive)
    {
        var end = directive.GetNextDirective(x => x.IsKind(SyntaxKind.EndRegionDirectiveTrivia));
        if (end is null)
            return new(0, 0);
        return new TextSpan(directive.FullSpan.Start, end.FullSpan.End - directive.FullSpan.Start);
    }

    internal static IEnumerable<TextSpan> GetAllDirectiveRegions(
        SyntaxNode node,
        Func<string, bool> predicate
    )
    {
        List<TextSpan> regions = [];
        var directive = node.GetFirstDirective();
        while (directive is not null)
        {
            if (
                directive.IsKind(SyntaxKind.RegionDirectiveTrivia)
                && predicate(directive.ToFullString())
            )
                regions.Add(GetRegionForDirective(directive));
            directive = directive.GetNextDirective();
        }
        return regions;
    }
#endif
}
