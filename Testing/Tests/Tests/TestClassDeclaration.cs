using CustomAccessibility.Attributes;

namespace Testing.Tests;

[OnlyAccessibleBy(nameof(ClassDeclaration0))]
class RestrictedMember { }

class ClassDeclaration0
{
    internal ClassDeclaration0(RestrictedMember x) { }

    // Field
    readonly RestrictedMember f = new();

    // Tuple field
    (RestrictedMember, RestrictedMember) tf = (new(), new());

    // Qualified tuple field
    (RestrictedMember a, RestrictedMember b) qtf = (new(), new());

    // Property
    RestrictedMember P => new();

    // Tuple property
    (RestrictedMember, RestrictedMember) Tp => (new(), new());
}

#region CACC000 RestrictedMember
class ClassDeclaration1
{
    internal ClassDeclaration1(RestrictedMember x) { }

    readonly RestrictedMember f = new();
    (RestrictedMember, RestrictedMember) tf = (new(), new());
    (RestrictedMember a, RestrictedMember b) qtf = (new(), new());
    RestrictedMember P => new();
    (RestrictedMember, RestrictedMember) Tp => (new(), new());
}

class ClassWithDefaultConstructor(RestrictedMember x) { }

class ClassWithMultiDefaultConstructor(RestrictedMember x, int y, RestrictedMember z) { }
#endregion

// Inheritance works for external references
class DerivedClass0 : Definitions.External.ExternalAllowed { }

// Inheritence works; wild card matches ns
class DerivedClass1 : Definitions.Internal.WildCard { }

// CACC000; cannot inherit inaccessible class
#region CACC000 IncorrectWildCard
class DerivedClass2 : Definitions.Internal.IncorrectWildCard { }
#endregion

// Inheritence works; unrestricted
class DerivedClass3 : Definitions.Internal.IUnrestrictedInterface { }

// Inheritence works; restricted but this class allow-listed
class DerivedClass4 : Definitions.Internal.IRestrictedInterface { }

// CACC000; cannot inherit inaccessible interface (not allow-listed)
#region CACC000 IRestrictedInterface
class DerivedClass5 : Definitions.Internal.IRestrictedInterface { }
#endregion
