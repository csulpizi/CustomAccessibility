using CustomAccessiblity.Attributes;

namespace Testing.Tests;

[OnlyAccessibleBy("Some fake class")]
class SomeRestrictedClass;

[OnlyAccessibleBy(nameof(OuterClass))]
class UnrestrictedClassForOutclass;

[OnlyAccessibleBy("InnerClass")]
class UnrestrictedClassForInnerclass;

[OnlyAccessibleBy("Testing.Tests.OuterClass.InnerClass")]
class UnrestrictedClassForInnerclassVerbose;

[OnlyAccessibleBy("Testing.Tests.OuterClass**")]
class UnrestrictedClassWildCardBoth;

class OuterClass
{
#pragma warning disable CACC000 // Restricted Access
    SomeRestrictedClass x = new();
#pragma warning restore CACC000 // Restricted Access
    UnrestrictedClassForOutclass y = new();
#pragma warning disable CACC000 // Restricted Access
    UnrestrictedClassForInnerclass z = new();
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
    UnrestrictedClassForInnerclassVerbose a = new();
#pragma warning restore CACC000 // Restricted Access
    UnrestrictedClassWildCardBoth b = new();

    class InnerClass
    {
#pragma warning disable CACC000 // Restricted Access
        SomeRestrictedClass x = new();
#pragma warning restore CACC000 // Restricted Access
#pragma warning disable CACC000 // Restricted Access
        UnrestrictedClassForOutclass y = new();
#pragma warning restore CACC000 // Restricted Access
        UnrestrictedClassForInnerclass z = new();
        UnrestrictedClassForInnerclassVerbose a = new();
        UnrestrictedClassWildCardBoth b = new();

    }
}
