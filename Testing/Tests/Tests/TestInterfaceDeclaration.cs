using CustomAccessibility.Attributes;

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
    #region CACC000 RestrictedClassForInterface
    void Foo(RestrictedClassForInterface x);

    // Property
    RestrictedClassForInterface P { get; set; }

    // Tuple property
    (RestrictedClassForInterface, RestrictedClassForInterface) Tp { get; set; }
    #endregion
}

interface IInternalInterfaceWithMembers
{
    // Does not work; attribute requires implicit 'internal'
    #region CACC100
    [OnlyAccessibleBy(nameof(InterfaceAccessingClass))]
    #endregion
    void Foo();

    [OnlyAccessibleBy(nameof(InterfaceAccessingClass))]
    internal void Goo();

    [OnlyAccessibleBy(nameof(InterfaceAccessingClass))]
    internal int Field { get; }
}

public interface IPublicInterfaceWithMembers
{
    // Does not work; attribute requires implicit 'internal'
    #region CACC100
    [OnlyAccessibleBy(nameof(InterfaceAccessingClass))]
    #endregion
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
        #region CACC000 Goo,Field
        a.Goo();
        _ = a.Field;
        b.Goo();
        _ = b.Field;
        #endregion
    }
}
