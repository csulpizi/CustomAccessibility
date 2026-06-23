using Microsoft.CodeAnalysis;

namespace CustomAccessibility;

partial class RestrictedAccessAnalyzer
{
    enum ValidationResult
    {
        NoError,
        RestrictedAccess,
        ImplicitlyRestrictedAccess
    }

    static ValidationResult ValidateAccessibility(ISymbol? symbol, INamedTypeSymbol enclosingType)
    {
        // We only need to check non-null symbols with 'Internal' accessibility
        if (symbol is null || !Util.IsDeclaredInternal(symbol))
            return ValidationResult.NoError;

        // Always allow private or protected access
        if (Util.IsPrivateOrProtectedAccess(symbol, enclosingType))
            return ValidationResult.NoError;

        var isExternalAccess = Util.IsExternalAccess(enclosingType, symbol);
        var accessibilityType = Util.GetAccessibilityType(symbol);

        if (isExternalAccess && accessibilityType == AccessibilityType.InternalAccessOnly)
            return ValidationResult.RestrictedAccess;
        if (!isExternalAccess && accessibilityType == AccessibilityType.ExternalAccessOnly)
            return ValidationResult.RestrictedAccess;

        var classes = Util.GetClassRegexes(symbol);

        if (
            classes.Any()
            && !classes.Any(regex => regex.IsMatch(enclosingType.Name))
            && !classes.Any(regex => regex.IsMatch(enclosingType.FullName()))
        )
            return ValidationResult.RestrictedAccess;

        if (isExternalAccess && accessibilityType == AccessibilityType.Default)
        {
            return ValidationResult.ImplicitlyRestrictedAccess;
        }
        return ValidationResult.NoError;
    }
}
