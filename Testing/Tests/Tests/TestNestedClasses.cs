using CustomAccessibility.Attributes;

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
    #region CACC000 SomeRestrictedClass,UnrestrictedClassForInnerclass,UnrestrictedClassForInnerclassVerbose
    SomeRestrictedClass x = new();
    UnrestrictedClassForOutclass y = new();
    UnrestrictedClassForInnerclass z = new();
    UnrestrictedClassForInnerclassVerbose a = new();
    UnrestrictedClassWildCardBoth b = new();
    #endregion
    class InnerClass
    {
        #region CACC000 SomeRestrictedClass,UnrestrictedClassForOutclass
        SomeRestrictedClass x = new();
        UnrestrictedClassForOutclass y = new();
        UnrestrictedClassForInnerclass z = new();
        UnrestrictedClassForInnerclassVerbose a = new();
        UnrestrictedClassWildCardBoth b = new();
        #endregion
    }
}
