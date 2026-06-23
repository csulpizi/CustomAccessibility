using StaticClass = Testing.Definitions.Internal.StaticClass;

namespace Testing.Tests;

// Allowed to access Testing.Definitions.Internal.StaticClass
class TestClassUsingStaticD
{
    void Method()
    {
        StaticClass.Foo();
        var x = StaticClass.A;
        var y = StaticClass.B;
        var z = StaticClass.C;
    }
}

// NOT allowed to access Testing.Definitions.Internal.StaticClass
class TestClassUsingStaticE
{
    void Method()
    {
        #region CACC000 StaticClass
        StaticClass.Foo();
        var x = StaticClass.A;
        var y = StaticClass.B;
        var z = StaticClass.C;
        #endregion
    }
}
