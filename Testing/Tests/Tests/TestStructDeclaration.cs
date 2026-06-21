using CustomAccessiblity.Attributes;

namespace Testing.Tests;

[OnlyAccessibleBy(nameof(StructDeclaration0))]
struct RestrictedMemberForStruct { }

struct StructDeclaration0
{
    internal StructDeclaration0(RestrictedMemberForStruct x) { }

    // Field
    readonly RestrictedMemberForStruct f = new();

    // Tuple field
    (RestrictedMemberForStruct, RestrictedMemberForStruct) tf = (new(), new());

    // Qualified tuple field
    (RestrictedMemberForStruct a, RestrictedMemberForStruct b) qtf = (new(), new());

    // Property
    RestrictedMemberForStruct P => new();

    // Tuple property
    (RestrictedMemberForStruct, RestrictedMemberForStruct) Tp => (new(), new());
}

struct StructDeclaration1
{
#pragma warning disable CACC000 // Restricted Access
    internal StructDeclaration1(RestrictedMemberForStruct x) { }
#pragma warning restore CACC000 // Restricted Access

#pragma warning disable CACC000 // Restricted Access
    readonly RestrictedMemberForStruct f = new();
#pragma warning restore CACC000 // Restricted Access

#pragma warning disable CACC000 // Restricted Access
    (RestrictedMemberForStruct, RestrictedMemberForStruct) tf = (new(), new());
#pragma warning restore CACC000 // Restricted Access

#pragma warning disable CACC000 // Restricted Access
    (RestrictedMemberForStruct a, RestrictedMemberForStruct b) qtf = (new(), new());
#pragma warning restore CACC000 // Restricted Access

#pragma warning disable CACC000 // Restricted Access
    RestrictedMemberForStruct P => new();
#pragma warning restore CACC000 // Restricted Access

#pragma warning disable CACC000 // Restricted Access
    (RestrictedMemberForStruct, RestrictedMemberForStruct) Tp => (new(), new());
#pragma warning restore CACC000 // Restricted Access
}

#pragma warning disable CACC000 // Restricted Access
struct StructWithDefaultConstructor(RestrictedMemberForStruct x) { }
#pragma warning restore CACC000 // Restricted Access

struct StructWithMultiDefaultConstructor(
#pragma warning disable CACC000 // Restricted Access
    RestrictedMemberForStruct x,
#pragma warning restore CACC000 // Restricted Access
    int y,
#pragma warning disable CACC000 // Restricted Access
    RestrictedMemberForStruct z
#pragma warning restore CACC000 // Restricted Access
) { }

// Inheritance works for external references
struct DerivedStruct0 : Definitions.Internal.IUnrestrictedInterface { }

// CACC000; cannot inherit inaccessible interface (not allow-listed)
#pragma warning disable CACC000 // Restricted Access
struct DerivedStruct1 : Definitions.Internal.IRestrictedInterface { }
#pragma warning restore CACC000 // Restricted Access
