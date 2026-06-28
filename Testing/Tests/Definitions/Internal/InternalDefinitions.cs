using CustomAccessibility.Attributes;

namespace Testing.Definitions.Internal;

class Default { }

[OnlyAccessibleBy("Testing.Tests.InternalReferencesA")]
class DefaultClassSpecified { }

[ExternalAccessAllowed]
class ExternalAllowed { }

[ExternalAccessAllowed]
[OnlyAccessibleBy("Testing.Tests.InternalReferencesA")]
class ExternalAllowedClassSpecified { }

[OnlyAccessibleBy("Testing.Tests.**")]
class WildCard { }

[OnlyAccessibleBy("Testing.Tests.Shamwow.*")]
class IncorrectWildCard { }

interface IUnrestrictedInterface { }

[OnlyAccessibleBy("Testing.Tests.DerivedClass4")]
interface IRestrictedInterface { }

[OnlyAccessibleBy(nameof(Tests.TestClassUsingStaticA))]
[OnlyAccessibleBy(nameof(Tests.TestClassUsingStaticC))]
[OnlyAccessibleBy(nameof(Tests.TestClassUsingStaticD))]
static class StaticClass
{
    internal static void Foo() { }

    internal static int A = 4;
    internal static bool C => false;

    static string b = "attt";
    internal static string B
    {
        get { return b; }
        set { b = value; }
    }
}
