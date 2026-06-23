#region CACC002 Testing.Definitions.Internal.StaticClass
using static Testing.Definitions.Internal.StaticClass;
#endregion

namespace Testing.Tests;

// Allowed to access Testing.Definitions.Internal.StaticClass
class TestClassUsingStaticA
{
    void Method()
    {
        Foo();
        var x = A;
        var y = B;
        var z = C;
    }
}

// NOT allowed to access Testing.Definitions.Internal.StaticClass
class TestClassUsingStaticB
{
    void Method()
    {
        Foo();
        var x = A;
        var y = B;
        var z = C;
    }
}
