using CustomAccessiblity.Attributes;

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

[AccessibleByAll]
class Both { }

[AccessibleByAll]
[OnlyAccessibleBy("Testing.Tests.ExternalReferencesA")]
class BothClassSpecified { }

[AccessibleByAll]
[OnlyAccessibleBy("Testing.Tests.ExternalReferences*")]
class WildCard { }

[AccessibleByAll]
[OnlyAccessibleBy("Testing.Tests.Shamwow.*")]
class IncorrectWildCard { }
