using CustomAccessiblity.Attributes;

namespace Testing.Tests;

class ClassWithMembers
{
    [OnlyAccessibleBy(nameof(ClassAccessesMembersA))]
    internal int A;

    [OnlyAccessibleBy(nameof(ClassAccessesMembersA))]
    internal int B => 0;

    [OnlyAccessibleBy(nameof(ClassAccessesMembersA))]
    internal void Foo() { }

     string s = "some string";
    [OnlyAccessibleBy(nameof(ClassAccessesMembersA))]
    internal string S
    {
        get => s;
        set => s = value;
    }
}

static class StaticClassWithMembers
{
    [OnlyAccessibleBy(nameof(ClassAccessesMembersA))]
    internal static int A;

    [OnlyAccessibleBy(nameof(ClassAccessesMembersA))]
    internal static int B => 0;

    [OnlyAccessibleBy(nameof(ClassAccessesMembersA))]
    internal static void Foo() { }
}

[OnlyAccessibleBy(nameof(ClassAccessesMembersA))]
static class RestrictedStaticClassWithMembers
{
    internal static int A;
    internal static int B => 0;

    internal static void Foo() { }
}

class ClassAccessesMembersA
{
    // No issues; this class is allow-listed
    void Foo(ClassWithMembers obj)
    {
        _ = obj.A;
        _ = obj.B;
        obj.Foo();
        _ = obj.S;
        obj.S = "Hello!";

        _ = StaticClassWithMembers.A;
        _ = StaticClassWithMembers.B;
        StaticClassWithMembers.Foo();

        _ = RestrictedStaticClassWithMembers.A;
        _ = RestrictedStaticClassWithMembers.B;
        RestrictedStaticClassWithMembers.Foo();
    }
}

class ClassAccessesMembersB
{
    // CACC000; this class is NOT allow-listed
    void Foo(ClassWithMembers obj)
    {
#pragma warning disable CACC000 // Restricted Access
        _ = obj.A;
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
        _ = obj.B;
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
        obj.Foo();
#pragma warning restore CACC000 // Restricted Access

#pragma warning disable CACC000 // Restricted Access
        _ = obj.S;
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
        obj.S = "Hello!";
#pragma warning restore CACC000 // Restricted Access

#pragma warning disable CACC000 // Restricted Access
        _ = StaticClassWithMembers.A;
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
        _ = StaticClassWithMembers.B;
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
        StaticClassWithMembers.Foo();
#pragma warning restore CACC000 // Restricted Access

#pragma warning disable CACC000 // Restricted Access
        _ = RestrictedStaticClassWithMembers.A;
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
        _ = RestrictedStaticClassWithMembers.B;
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
        RestrictedStaticClassWithMembers.Foo();
#pragma warning restore CACC000 // Restricted Access
    }
}
