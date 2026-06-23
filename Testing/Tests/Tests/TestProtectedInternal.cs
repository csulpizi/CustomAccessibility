using CustomAccessibility.Attributes;

namespace Testing.Tests;

class Alpha
{
    [OnlyAccessibleBy(nameof(AlphaAccessorA))]
    protected internal int Field = 4;

    [OnlyAccessibleBy(nameof(AlphaAccessorA))]
    protected internal int Property => 9;

    [OnlyAccessibleBy(nameof(AlphaAccessorA))]
    protected internal void Foo() { }
}

class Beta : Alpha
{
    // Should be able to reach base class's definitions
    void Goo()
    {
        Foo();
        _ = Field;
        _ = Property;
    }
}

class AlphaAccessorA
{
    void Foo(Alpha x)
    {
        _ = x.Field;
        _ = x.Property;
        x.Foo();
    }
}

class AlphaAccessorB
{
    void Foo(Alpha x)
    {
        #region CACC000 Field,Property,Foo
        _ = x.Field;
        _ = x.Property;
        x.Foo();
        #endregion
    }
}
