using Microsoft.CodeAnalysis;

namespace CustomAccessibility.Analyzer;

partial class RestrictedAccessAnalyzer
{
    enum ValidationResult
    {
        NoError,
        RestrictedAccess,
        ImplicitlyRestrictedAccess,
    }

    static (ValidationResult result, IEnumerable<string> allowlist) ValidateAccessibility(
        ISymbol? symbol,
        INamedTypeSymbol enclosingType
    )
    {
        // We only need to check non-null symbols with 'Internal' accessibility
        if (symbol is null || !Util.IsDeclaredInternal(symbol))
            return (ValidationResult.NoError, []);

        // Always allow private or protected access
        if (Util.IsPrivateOrProtectedAccess(symbol, enclosingType))
            return (ValidationResult.NoError, []);

        var isExternalAccess = Util.IsExternalAccess(enclosingType, symbol);
        var isExternalAllowed = Util.IsExternalAllowed(symbol);

        var classes = Util.GetAllowListRegexes(symbol);

        if (
            classes.Any()
            && !classes.Any(regex => regex.IsMatch(enclosingType.Name))
            && !classes.Any(regex => regex.IsMatch(enclosingType.FullName()))
        )
            return (ValidationResult.RestrictedAccess, Util.GetAllowList(symbol));

        if (isExternalAccess && !isExternalAllowed)
        {
            return (ValidationResult.ImplicitlyRestrictedAccess, []);
        }
        return (ValidationResult.NoError, []);
    }
}
