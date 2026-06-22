using CustomAccessiblity.Attributes;

namespace Testing.Tests;

[OnlyAccessibleBy(nameof(IDeclaration0))]
class RestrictedClassForInterface { }

interface IDeclaration0
{
    void Foo(RestrictedClassForInterface x);

    // Property
    RestrictedClassForInterface P { get; set; }

    // Tuple property
    (RestrictedClassForInterface, RestrictedClassForInterface) Tp { get; set; }
}

interface IDeclaration1
{
#pragma warning disable CACC000 // Restricted Access
    void Foo(RestrictedClassForInterface x);
#pragma warning restore CACC000 // Restricted Access

    // Property
#pragma warning disable CACC000 // Restricted Access
    RestrictedClassForInterface P { get; set; }
#pragma warning restore CACC000 // Restricted Access

    // Tuple property
#pragma warning disable CACC000 // Restricted Access
    (RestrictedClassForInterface,
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
        RestrictedClassForInterface
#pragma warning restore CACC000 // Restricted Access
    ) Tp { get; set; }
}

interface IInternalInterfaceWithMembers
{
    // Works; 'internal' not required when implicit
    [OnlyAccessibleBy(nameof(InterfaceAccessingClass))]
    internal void Foo();

    [OnlyAccessibleBy(nameof(InterfaceAccessingClass))]
    internal void Goo();

    [OnlyAccessibleBy(nameof(InterfaceAccessingClass))]
    internal int Field { get; }
}

public interface IPublicInterfaceWithMembers
{
    // Does not work; implicitly 'public'
#pragma warning disable CACC100 // Invalid Attribute Usage
    [OnlyAccessibleBy(nameof(InterfaceAccessingClass))]
#pragma warning restore CACC100 // Invalid Attribute Usage
    void Foo();

    [OnlyAccessibleBy(nameof(InterfaceAccessingClass))]
    internal void Goo();

    [OnlyAccessibleBy(nameof(InterfaceAccessingClass))]
    internal int Field { get; }
}

class InterfaceAccessingClass
{
    void Foo(IInternalInterfaceWithMembers a, IPublicInterfaceWithMembers b)
    {
        a.Foo();
        a.Goo();
        _ = a.Field;

        b.Foo();
        b.Goo();
        _ = b.Field;
    }
}

class InterfaceAccessingClassNotAllowed
{
    void Foo(IInternalInterfaceWithMembers a, IPublicInterfaceWithMembers b)
    {
#pragma warning disable CACC000 // Restricted Access
        a.Goo();
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
        _ = a.Field;
#pragma warning restore CACC000 // Restricted Access

#pragma warning disable CACC000 // Restricted Access
        b.Goo();
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
        _ = b.Field;
#pragma warning restore CACC000 // Restricted Access
    }
}
