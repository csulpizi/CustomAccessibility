using CustomAccessibility.Attributes;

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
    #region CACC000 StaticClassWithRestrictions
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
    #endregion
}
