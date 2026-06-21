using CustomAccessiblity.Attributes;

namespace Testing.Tests;

class UnrestrictedClass { }

[OnlyAccessibleBy(nameof(VariousObjectReferencesA))]
class RestrictedClass { }

enum UnrestrictedEnum
{
    Default
}

[OnlyAccessibleBy(nameof(VariousObjectReferencesA))]
enum RestrictedEnum
{
    Default
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
}

class VariousObjectReferencesB
{
    // CACC000; not in allow-list
#pragma warning disable CACC000 // Restricted Access
    RestrictedClass a = new();
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
    RestrictedEnum b = RestrictedEnum.Default;
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
    IRestrictedInterface c = new RestrictedInterfaceImplementation();
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
    RestrictedStruct d = new();
#pragma warning restore CACC000 // Restricted Access
}
