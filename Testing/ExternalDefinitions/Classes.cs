using CustomAccessibility.Attributes;

namespace Testing.Definitions.External;

class Default { }

[OnlyAccessibleBy("Testing.Tests.ExternalReferencesA")]
class DefaultClassSpecified { }

[ExternalAccessAllowed]
class ExternalAllowed { }

[ExternalAccessAllowed]
[OnlyAccessibleBy("Testing.Tests.ExternalReferencesA")]
class BothClassSpecified { }

[ExternalAccessAllowed]
[OnlyAccessibleBy("Testing.Tests.ExternalReferences*")]
class WildCard { }

[ExternalAccessAllowed]
[OnlyAccessibleBy("Testing.Tests.Shamwow.*")]
class IncorrectWildCard { }

[ExternalAccessAllowed]
class ClassWithMembers
{
    internal void RestrictedFoo() { }

    internal int RestrictedField = 0;
    internal int RestrictedProperty => 4;

    [ExternalAccessAllowed]
    internal void UnrestrictedFoo() { }

    [ExternalAccessAllowed]
    internal int UnrestrictedField = 0;

    [ExternalAccessAllowed]
    internal int UnrestrictedProperty => 4;
}

public class ClassForProtectedAccessTests
{
    protected int A => 8;

    protected internal int B => 7;

    internal int C => 9;
}
