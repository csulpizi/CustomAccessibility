using System.Text.RegularExpressions;
using CustomAccessiblity.Attributes;
using Microsoft.CodeAnalysis;

namespace CustomAccessiblity;

[AccessibleByAll]
static class Util
{
    internal static AccessibilityType GetAccessibilityType(ISymbol symbol)
    {
        var xs = symbol.GetAttributes();
        if (xs.Any(x => nameof(InternalAccessOnly) == x.AttributeClass?.Name))
            return AccessibilityType.InternalAccessOnly;
        if (xs.Any(x => nameof(ExternalAccessOnly) == x.AttributeClass?.Name))
            return AccessibilityType.ExternalAccessOnly;
        if (xs.Any(x => nameof(AccessibleByAll) == x.AttributeClass?.Name))
            return AccessibilityType.AccessibleByAll;
        return AccessibilityType.Default;
    }

    internal static bool IsDeclaredInternal(ISymbol symbol)
    {
        return symbol.DeclaredAccessibility == Accessibility.Internal
            || symbol.DeclaredAccessibility == Accessibility.ProtectedOrInternal;
    }

    internal static IEnumerable<Regex> GetClassRegexes(ISymbol symbol)
    {
        return symbol
            .GetAttributes()
            .Where(x => nameof(OnlyAccessibleBy) == x.AttributeClass?.Name)
            .Select(CoerceAttributeToRegex);
    }

    internal static bool IsExternalAccess(INamedTypeSymbol declaringClassSymbol, ISymbol subject)
    {
        return !SymbolEqualityComparer.Default.Equals(
            declaringClassSymbol.ContainingModule,
            subject.OriginalDefinition.ContainingModule
        );
    }

    internal static string FullyQualifiedClassName(INamedTypeSymbol declaringSymbol)
    {
        var s = declaringSymbol.Name;

        var parent = declaringSymbol.ContainingType;
        while (parent is not null && parent.Name != "")
        {
            s = parent.Name + "." + s;
            parent = parent.ContainingType;
        }

        var ns = (parent ?? declaringSymbol).ContainingNamespace;
        while (ns is not null && ns.Name != "")
        {
            s = ns.Name + "." + s;
            ns = ns.ContainingNamespace;
        }
        return s;
    }

    static Regex CoerceAttributeToRegex(AttributeData attribute)
    {
        var s = (string)(
            attribute.ConstructorArguments[0].Value ?? throw new Exception("Should never happen")
        );
        return CoerceWildCardStringToRegex(s);
    }

    // Allow external for unit testing
    [AccessibleByAll]
    internal static Regex CoerceWildCardStringToRegex(string s)
    {
        s = "^" + s + "$";
        s = s.Replace(".", "\\.").Replace("**", "[^\\s]{0,}").Replace("*", "[^.\\s]{0,}");
        return new(s);
    }

    internal static bool IsPrivateOrProtectedAccess(ISymbol symbol, ITypeSymbol declaringSymbol)
    {
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
}
