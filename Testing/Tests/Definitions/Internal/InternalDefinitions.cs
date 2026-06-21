using CustomAccessiblity.Attributes;

namespace Testing.Definitions.Internal;

class Default { }

[OnlyAccessibleBy("Testing.Tests.InternalReferencesA")]
class DefaultClassSpecified { }

[InternalAccessOnly]
class InternalOnly { }

[InternalAccessOnly]
[OnlyAccessibleBy("Testing.Tests.InternalReferencesA")]
class InternalOnlyClassSpecified { }

[ExternalAccessOnly]
class ExternalOnly { }

[ExternalAccessOnly]
[OnlyAccessibleBy("Testing.Tests.InternalReferencesA")]
class ExternalOnlyClassSpecified { }

[AccessibleByAll]
class Both { }

[AccessibleByAll]
[OnlyAccessibleBy("Testing.Tests.InternalReferencesA")]
class BothClassSpecified { }

[OnlyAccessibleBy("Testing.Tests.**")]
class WildCard { }

[OnlyAccessibleBy("Testing.Tests.Shamwow.*")]
class IncorrectWildCard { }

interface IUnrestrictedInterface { }

[OnlyAccessibleBy("Testing.Tests.DerivedClass4")]
interface IRestrictedInterface { }
