using System.Text.RegularExpressions;
using CustomAccessiblity.Attributes;
using CustomAccessiblity.Rules;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessiblity;

[AccessibleByAll]
static class Util
{
    internal static void Validate(
        SyntaxNodeAnalysisContext ctx,
        ISymbol? symbol,
        INamedTypeSymbol? declaringSymbol,
        Func<Location> getLocation
    )
    {
        if (symbol is null)
            return;

        if (symbol.DeclaredAccessibility != Accessibility.Internal)
            return;

        var isPrivateAccess =
            symbol.ContainingType is not null
            && SymbolEqualityComparer.Default.Equals(symbol.ContainingType, declaringSymbol);

        // Always allow private access
        if (isPrivateAccess)
            return;

        var isExternalAccess = IsExternalAccess(declaringSymbol, symbol);
        var accessibilityType = GetAccessibilityType(symbol);

        if (isExternalAccess && accessibilityType == AccessibilityType.InternalAccessOnly)
        {
            ctx.ReportDiagnostic(
                Diagnostic.Create(RestrictedAccess.Create(symbol.Name), getLocation())
            );
            return;
        }
        if (!isExternalAccess && accessibilityType == AccessibilityType.ExternalAccessOnly)
        {
            ctx.ReportDiagnostic(
                Diagnostic.Create(RestrictedAccess.Create(symbol.Name), getLocation())
            );
            return;
        }

        var classes = GetClassRegexes(symbol);

        if (classes.Any() && !classes.Any(regex => CheckClassRegex(declaringSymbol, regex)))
        {
            ctx.ReportDiagnostic(
                Diagnostic.Create(RestrictedAccess.Create(symbol.Name), getLocation())
            );
            return;
        }

        if (isExternalAccess && accessibilityType == AccessibilityType.Default)
        {
            ctx.ReportDiagnostic(Diagnostic.Create(ImplicitlyRestrictedAccess.Rule, getLocation()));
            return;
        }
    }

    static AccessibilityType GetAccessibilityType(ISymbol symbol)
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

    static string ConstructorArgFromAttributeData(AttributeData data)
    {
        var s =
            data.ConstructorArguments[0].Value ?? throw new Exception("This should never happen!");
        return (string)s;
    }

    static IEnumerable<Regex> GetClassRegexes(ISymbol symbol)
    {
        return symbol
            .GetAttributes()
            .Where(x => nameof(OnlyAccessibleBy) == x.AttributeClass?.Name)
            .Select(x => GetRegex(ConstructorArgFromAttributeData(x)));
    }

    [ExternalAccessOnly]
    internal static bool IsExternalAccess(INamedTypeSymbol? declaringClassSymbol, ISymbol subject)
    {
        return !SymbolEqualityComparer.Default.Equals(
            subject.ContainingModule,
            declaringClassSymbol?.ContainingModule
        );
    }

    static bool CheckClassRegex(INamedTypeSymbol? declaringSymbol, Regex regex) =>
        CheckRegex(declaringSymbol?.Name, regex)
        || CheckRegex(FullyQualifiedClassName(declaringSymbol), regex);

    static string? FullyQualifiedClassName(INamedTypeSymbol? declaringSymbol)
    {
        if (declaringSymbol is null)
            return null;

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

    [ExternalAccessOnly]
    internal static bool CheckRegex(string? name, Regex regex)
    {
        if (name is null)
            return false;
        return regex.IsMatch(name);
    }

    [ExternalAccessOnly]
    internal static Regex GetRegex(string s)
    {
        s = "^" + s + "$";
        s = s.Replace(".", "\\.").Replace("**", "[^\\s]{0,}").Replace("*", "[^.\\s]{0,}");
        return new(s);
    }
}
