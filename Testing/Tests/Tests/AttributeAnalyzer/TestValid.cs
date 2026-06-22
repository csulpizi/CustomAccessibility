using CustomAccessiblity.Attributes;

namespace Testing.Tests.AttributeAnalyzer;

[ExternalAccessOnly]
class AttributedClass { }

[ExternalAccessOnly]
enum AttributedEnum { }

[ExternalAccessOnly]
interface IAttributed { }

[ExternalAccessOnly]
struct AttributedStruct { }

[ExternalAccessOnly]
[OnlyAccessibleBy("SomeClass")]
[OnlyAccessibleBy("SomeClass2")]
class AttributedClass2 { }

class AttributedMembersOfClass
{
    [ExternalAccessOnly]
    internal void Method() { }

    [ExternalAccessOnly]
    internal int Field = 0;

    [ExternalAccessOnly]
    internal int Property => 0;
}

interface IAttributedMembersOfInterface
{
    [ExternalAccessOnly]
    internal void Method();

    [ExternalAccessOnly]
    internal int Property { get; }
}

struct AttributedMembersOfStruct()
{
    [ExternalAccessOnly]
    internal void Method() { }

    [ExternalAccessOnly]
    internal readonly void ReadonlyMethod() { }

    [ExternalAccessOnly]
    internal static void StaticMethod() { }

    [ExternalAccessOnly]
    internal int Field = 0;

    [ExternalAccessOnly]
    internal int Property => 0;
}

readonly struct AttributedMembersOfReadonlyStruct()
{
    [ExternalAccessOnly]
    internal void Method() { }

    [ExternalAccessOnly]
    internal readonly void ReadonlyMethod() { }

    [ExternalAccessOnly]
    internal static void StaticMethod() { }

    [ExternalAccessOnly]
    internal readonly int Field = 0;

    [ExternalAccessOnly]
    internal readonly int Property => 0;
}
