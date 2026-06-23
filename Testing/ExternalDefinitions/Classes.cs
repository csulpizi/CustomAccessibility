using CustomAccessibility.Attributes;

namespace Testing.Definitions.External;

class Default { }

[OnlyAccessibleBy("Testing.Tests.ExternalReferencesA")]
class DefaultClassSpecified { }

[InternalAccessOnly]
class InternalOnly { }

[InternalAccessOnly]
[OnlyAccessibleBy("Testing.Tests.ExternalReferencesA")]
class InternalOnlyClassSpecified { }

[ExternalAccessOnly]
class ExternalOnly { }

[ExternalAccessOnly]
[OnlyAccessibleBy("Testing.Tests.ExternalReferencesA")]
class ExternalOnlyClassSpecified { }

[AccessibleByInternalAndExternal]
class Both { }

[AccessibleByInternalAndExternal]
[OnlyAccessibleBy("Testing.Tests.ExternalReferencesA")]
class BothClassSpecified { }

[AccessibleByInternalAndExternal]
[OnlyAccessibleBy("Testing.Tests.ExternalReferences*")]
class WildCard { }

[AccessibleByInternalAndExternal]
[OnlyAccessibleBy("Testing.Tests.Shamwow.*")]
class IncorrectWildCard { }

[AccessibleByInternalAndExternal]
class ClassWithMembers
{
    internal void RestrictedFoo() { }

    internal int RestrictedField = 0;
    internal int RestrictedProperty => 4;

    [AccessibleByInternalAndExternal]
    internal void UnrestrictedFoo() { }

    [AccessibleByInternalAndExternal]
    internal int UnrestrictedField = 0;

    [AccessibleByInternalAndExternal]
    internal int UnrestrictedProperty => 4;
}


public class ClassForProtectedAccessTests
{
    protected int A => 8;

    protected internal int B => 7;

    internal int C => 9;
}