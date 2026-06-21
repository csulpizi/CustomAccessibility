using CustomAccessiblity.Attributes;

namespace Testing.Tests.Attributes;

class ClassWithNonInternalMembers
{
#pragma warning disable CACC100 // Invalid Attribute Usage
    [AccessibleByAll]
#pragma warning restore CACC100 // Invalid Attribute Usage
    void Method() { }

#pragma warning disable CACC100 // Invalid Attribute Usage
    [AccessibleByAll]
#pragma warning restore CACC100 // Invalid Attribute Usage
    int Field = 0;

#pragma warning disable CACC100 // Invalid Attribute Usage
    [AccessibleByAll]
#pragma warning restore CACC100 // Invalid Attribute Usage
    int Property => 0;
}

public interface IWithNonInternalMembers
{
#pragma warning disable CACC100 // Invalid Attribute Usage
    [AccessibleByAll]
#pragma warning restore CACC100 // Invalid Attribute Usage
    void Method();

#pragma warning disable CACC100 // Invalid Attribute Usage
    [AccessibleByAll]
#pragma warning restore CACC100 // Invalid Attribute Usage
    int Property { get; }
}

struct StructWithNonInternalMembers()
{
#pragma warning disable CACC100 // Invalid Attribute Usage
    [AccessibleByAll]
#pragma warning restore CACC100 // Invalid Attribute Usage
    void Method() { }

#pragma warning disable CACC100 // Invalid Attribute Usage
    [AccessibleByAll]
#pragma warning restore CACC100 // Invalid Attribute Usage
    readonly void ReadonlyMethod() { }

#pragma warning disable CACC100 // Invalid Attribute Usage
    [AccessibleByAll]
#pragma warning restore CACC100 // Invalid Attribute Usage
    static void StaticMethod() { }

#pragma warning disable CACC100 // Invalid Attribute Usage
    [AccessibleByAll]
#pragma warning restore CACC100 // Invalid Attribute Usage
    int Field = 0;

#pragma warning disable CACC100 // Invalid Attribute Usage
    [AccessibleByAll]
#pragma warning restore CACC100 // Invalid Attribute Usage
    int Property => 0;
}

readonly struct ReadonlyStructWithNonInternalMembers()
{
#pragma warning disable CACC100 // Invalid Attribute Usage
    [AccessibleByAll]
#pragma warning restore CACC100 // Invalid Attribute Usage
    void Method() { }

#pragma warning disable CACC100 // Invalid Attribute Usage
    [AccessibleByAll]
#pragma warning restore CACC100 // Invalid Attribute Usage
    readonly void ReadonlyMethod() { }

#pragma warning disable CACC100 // Invalid Attribute Usage
    [AccessibleByAll]
#pragma warning restore CACC100 // Invalid Attribute Usage
    static void StaticMethod() { }

#pragma warning disable CACC100 // Invalid Attribute Usage
    [AccessibleByAll]
#pragma warning restore CACC100 // Invalid Attribute Usage
    readonly int Field = 0;

#pragma warning disable CACC100 // Invalid Attribute Usage
    [AccessibleByAll]
#pragma warning restore CACC100 // Invalid Attribute Usage
    readonly int Property => 0;
}
