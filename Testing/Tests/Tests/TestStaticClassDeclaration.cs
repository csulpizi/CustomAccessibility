using CustomAccessiblity.Attributes;

namespace Testing.Tests;

[OnlyAccessibleBy(nameof(StaticClassAccessorA))]
class StaticClassWithRestrictions { }

static class StaticClassAccessorA
{
    static StaticClassWithRestrictions Foo() => throw new NotImplementedException();

    static readonly StaticClassWithRestrictions f = new();

    // Tuple field
    static (StaticClassWithRestrictions, StaticClassWithRestrictions) tf = (new(), new());

    // Qualified tuple field
    static (StaticClassWithRestrictions a, StaticClassWithRestrictions b) qtf = (new(), new());

    // Property
    static StaticClassWithRestrictions P => new();

    // Tuple property
    static (StaticClassWithRestrictions, StaticClassWithRestrictions) Tp => (new(), new());
}

static class StaticClassAccessorB
{
#pragma warning disable CACC000 // Restricted Access
    static StaticClassWithRestrictions Foo() => throw new NotImplementedException();
#pragma warning restore CACC000 // Restricted Access

#pragma warning disable CACC000 // Restricted Access
    static readonly StaticClassWithRestrictions f = new();
#pragma warning restore CACC000 // Restricted Access

    // Tuple field
#pragma warning disable CACC000 // Restricted Access
    static (StaticClassWithRestrictions,
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
        StaticClassWithRestrictions) tf = (new(), new());
#pragma warning restore CACC000 // Restricted Access

    // Qualified tuple field
#pragma warning disable CACC000 // Restricted Access
    static (StaticClassWithRestrictions a,
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
        StaticClassWithRestrictions b) qtf = (new(), new());
#pragma warning restore CACC000 // Restricted Access

    // Property
#pragma warning disable CACC000 // Restricted Access
    static StaticClassWithRestrictions P => new();
#pragma warning restore CACC000 // Restricted Access

    // Tuple property
#pragma warning disable CACC000 // Restricted Access
    static (StaticClassWithRestrictions,
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
        StaticClassWithRestrictions) Tp => (new(), new());
#pragma warning restore CACC000 // Restricted Access
}
