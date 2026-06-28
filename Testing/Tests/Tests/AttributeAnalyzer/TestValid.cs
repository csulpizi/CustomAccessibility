using CustomAccessibility.Attributes;

namespace Testing.Tests.AttributeAnalyzer;

[ExternalAccessAllowed]
class AttributedClass { }

[ExternalAccessAllowed]
enum AttributedEnum { }

[ExternalAccessAllowed]
interface IAttributed { }

[ExternalAccessAllowed]
struct AttributedStruct { }

[ExternalAccessAllowed]
[OnlyAccessibleBy("SomeClass")]
[OnlyAccessibleBy("SomeClass2")]
class AttributedClass2 { }

[OnlyAccessibleBy("SomeClass")]
[OnlyAccessibleBy("SomeClass2")]
class AttributedClass3 { }

class AttributedMembersOfClass
{
    [ExternalAccessAllowed]
    internal void Method() { }

    [ExternalAccessAllowed]
    internal int Field = 0;

    [ExternalAccessAllowed]
    internal int Property => 0;
}

interface IAttributedMembersOfInterface
{
    [ExternalAccessAllowed]
    internal void Method();

    [ExternalAccessAllowed]
    internal int Property { get; }
}

struct AttributedMembersOfStruct()
{
    [ExternalAccessAllowed]
    internal void Method() { }

    [ExternalAccessAllowed]
    internal readonly void ReadonlyMethod() { }

    [ExternalAccessAllowed]
    internal static void StaticMethod() { }

    [ExternalAccessAllowed]
    internal int Field = 0;

    [ExternalAccessAllowed]
    internal int Property => 0;
}

readonly struct AttributedMembersOfReadonlyStruct()
{
    [ExternalAccessAllowed]
    internal void Method() { }

    [ExternalAccessAllowed]
    internal readonly void ReadonlyMethod() { }

    [ExternalAccessAllowed]
    internal static void StaticMethod() { }

    [ExternalAccessAllowed]
    internal readonly int Field = 0;

    [ExternalAccessAllowed]
    internal readonly int Property => 0;
}
