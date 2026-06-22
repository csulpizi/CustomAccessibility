#pragma warning disable CACC002 // Restricted 'using static'
using static Testing.Definitions.Internal.StaticClass;
#pragma warning restore CACC002 // Restricted 'using static'

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
