using CustomAccessiblity.Attributes;

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
