using CustomAccessibility.Attributes;

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

#region CACC000 RestrictedMemberForStruct
struct StructDeclaration1
{
    internal StructDeclaration1(RestrictedMemberForStruct x) { }

    readonly RestrictedMemberForStruct f = new();
    (RestrictedMemberForStruct, RestrictedMemberForStruct) tf = (new(), new());
    (RestrictedMemberForStruct a, RestrictedMemberForStruct b) qtf = (new(), new());
    RestrictedMemberForStruct P => new();
    (RestrictedMemberForStruct, RestrictedMemberForStruct) Tp => (new(), new());
}

struct StructWithDefaultConstructor(RestrictedMemberForStruct x) { }

struct StructWithMultiDefaultConstructor(
    RestrictedMemberForStruct x,
    int y,
    RestrictedMemberForStruct z
) { }
#endregion

// Inheritance works for external references
struct DerivedStruct0 : Definitions.Internal.IUnrestrictedInterface { }

#region CACC000 IRestrictedInterface
struct DerivedStruct1 : Definitions.Internal.IRestrictedInterface { }
#endregion
