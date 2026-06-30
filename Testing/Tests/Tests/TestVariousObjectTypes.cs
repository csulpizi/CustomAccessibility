using CustomAccessibility.Attributes;

namespace Testing.Tests;

class UnrestrictedClass { }

[OnlyAccessibleBy(nameof(VariousObjectReferencesA))]
class RestrictedClass { }

enum UnrestrictedEnum
{
    Default,
}

[OnlyAccessibleBy(nameof(VariousObjectReferencesA))]
enum RestrictedEnum
{
    Default,
}

interface IUnrestrictedInterface { }

[OnlyAccessibleBy(nameof(VariousObjectReferencesA))]
[OnlyAccessibleBy(nameof(RestrictedInterfaceImplementation))]
interface IRestrictedInterface { }

class UnrestrictedInterfaceImplementation : IUnrestrictedInterface { }

class RestrictedInterfaceImplementation : IRestrictedInterface { }

struct UnrestrictedStruct { }

[OnlyAccessibleBy(nameof(VariousObjectReferencesA))]
struct RestrictedStruct { }

class VariousObjectReferencesA
{
    // No issues; all either unrestricted or allow-list this class
    UnrestrictedClass a = new();
    RestrictedClass b = new();
    UnrestrictedEnum c = UnrestrictedEnum.Default;
    RestrictedEnum d = RestrictedEnum.Default;
    IUnrestrictedInterface e = new UnrestrictedInterfaceImplementation();
    IRestrictedInterface f = new RestrictedInterfaceImplementation();
    UnrestrictedStruct g = new();
    RestrictedStruct h = new();
    Func<RestrictedClass, int> i = r =>
    {
        return r.GetHashCode();
    };
}

class VariousObjectReferencesB
{
    #region CACC000 RestrictedClass,RestrictedEnum,IRestrictedInterface,RestrictedStruct
    RestrictedClass a = new();
    RestrictedEnum b = RestrictedEnum.Default;
    IRestrictedInterface c = new RestrictedInterfaceImplementation();
    RestrictedStruct d = new();
    Func<RestrictedClass, int> i = r =>
    {
        return r.GetHashCode();
    };
    Func<int, int> j = x =>
    {
        RestrictedClass c = new();

        return x;
    };
    #endregion
}
