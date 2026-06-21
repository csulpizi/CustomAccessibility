using CustomAccessiblity.Attributes;

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

class ClassDeclaration1
{
#pragma warning disable CACC000 // Restricted Access
    internal ClassDeclaration1(RestrictedMember x) { }
#pragma warning restore CACC000 // Restricted Access

#pragma warning disable CACC000 // Restricted Access
    readonly RestrictedMember f = new();
#pragma warning restore CACC000 // Restricted Access

#pragma warning disable CACC000 // Restricted Access
    (RestrictedMember, RestrictedMember) tf = (new(), new());
#pragma warning restore CACC000 // Restricted Access

#pragma warning disable CACC000 // Restricted Access
    (RestrictedMember a, RestrictedMember b) qtf = (new(), new());
#pragma warning restore CACC000 // Restricted Access

#pragma warning disable CACC000 // Restricted Access
    RestrictedMember P => new();
#pragma warning restore CACC000 // Restricted Access

#pragma warning disable CACC000 // Restricted Access
    (RestrictedMember, RestrictedMember) Tp => (new(), new());
#pragma warning restore CACC000 // Restricted Access
}

#pragma warning disable CACC000 // Restricted Access
class ClassWithDefaultConstructor(RestrictedMember x) { }
#pragma warning restore CACC000 // Restricted Access

class ClassWithMultiDefaultConstructor(
#pragma warning disable CACC000 // Restricted Access
    RestrictedMember x,
#pragma warning restore CACC000 // Restricted Access
    int y,
#pragma warning disable CACC000 // Restricted Access
    RestrictedMember z
#pragma warning restore CACC000 // Restricted Access
) { }

// Inheritance works for external references
class DerivedClass0 : Definitions.External.Both { }

// Inheritence works; wild card matches ns
class DerivedClass1 : Definitions.Internal.WildCard { }

// CACC000; cannot inherit inaccessible class
#pragma warning disable CACC000 // Restricted Access
class DerivedClass2 : Definitions.Internal.IncorrectWildCard { }
#pragma warning restore CACC000 // Restricted Access

// Inheritence works; unrestricted
class DerivedClass3 : Definitions.Internal.IUnrestrictedInterface { }

// Inheritence works; restricted but this class allow-listed
class DerivedClass4 : Definitions.Internal.IRestrictedInterface { }

// CACC000; cannot inherit inaccessible interface (not allow-listed)
#pragma warning disable CACC000 // Restricted Access
class DerivedClass5 : Definitions.Internal.IRestrictedInterface { }
#pragma warning restore CACC000 // Restricted Access
