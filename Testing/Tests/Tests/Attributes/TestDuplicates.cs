using CustomAccessiblity.Attributes;

namespace Testing.Tests.Attributes;

[ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
[InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
class OverAttributedClass { }

[ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
[InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
enum OverAttributedEnum { }

[ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
[InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
interface IOverAttributed { }

[ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
[InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
struct OverAttributedStruct { }

class OverAttributedMembersOfClass
{
    [ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
    [InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
    internal void Method() { }

    [ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
    [InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
    internal int Field = 0;

    [ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
    [InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
    internal int Property => 0;
}

interface IOverAttributedMembersOfInterface
{
    [ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
    [InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
    internal void Method();

    [ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
    [InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
    internal int Property { get; }
}

struct OverAttributedMembersOfStruct()
{
    [ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
    [InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
    internal void Method() { }

    [ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
    [InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
    internal static void StaticMethod() { }

    [ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
    [InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
    internal int Field = 0;

    [ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
    [InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
    internal int Property => 0;
}

readonly struct OverAttributedMembersOfReadonlyStruct()
{
    [ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
    [InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
    internal void Method() { }

    [ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
    [InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
    internal readonly void ReadonlyMethod() { }

    [ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
    [InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
    internal static void StaticMethod() { }

    [ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
    [InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
    readonly internal int Field = 0;

    [ExternalAccessOnly]
#pragma warning disable CACC003 // Invalid Attribute Usage
    [InternalAccessOnly]
#pragma warning restore CACC003 // Invalid Attribute Usage
    readonly internal int Property => 0;
}
