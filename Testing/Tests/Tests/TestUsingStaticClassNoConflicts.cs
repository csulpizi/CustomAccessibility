using static Testing.Definitions.Internal.StaticClass;

namespace Testing.Tests;

// Allowed to access Testing.Definitions.Internal.StaticClass
class TestClassUsingStaticC
{
    void Method()
    {
        Foo();
        var x = A;
        var y = B;
        var z = C;
    }
}
