using CustomAccessibility.Attributes;

namespace Testing.Tests;

class TestPrivateAccess
{
    [ExternalAccessOnly]
    internal void Foo() { }

    void Goo()
    {
        // No issue; even though Foo is marked as 'ExternalAccessOnly'
        //  it is still able to be referenced privately
        Foo();
    }
}

class OuterClassForProtectedAccessTest : TestPrivateAccess
{
    void Goo()
    {
        // No issue; even though Foo is marked as 'ExternalAccessOnly'
        //  it is still able to be referenced in a protected context
        Foo();
    }
}

[OnlyAccessibleBy("Dog")]
class SelfAccessTest
{
    // No issue. Classes are always allowed to reference themselves
    void Foo(SelfAccessTest x)
    {
        
    }
}