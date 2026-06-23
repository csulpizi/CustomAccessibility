using CustomAccessibility.Attributes;

namespace Testing.Definitions.Internal;

[OnlyAccessibleBy("Dog")]
class ByDog { }

[OnlyAccessibleBy("**.Dog")]
class ByDogAlt { }

[OnlyAccessibleBy("*Dog")]
class ByPrefixDog { }

[OnlyAccessibleBy("Testing.Tests.RestrictedAccessAnalyzer.AccessibleBy.*")]
class ByOuterNamespace { }

[OnlyAccessibleBy("Testing.Tests.RestrictedAccessAnalyzer.AccessibleBy.**")]
class ByEntireNamespace { }

[OnlyAccessibleBy("Testing.Tests.RestrictedAccessAnalyzer.AccessibleBy.OuterDog.Dog")]
class ByFullyQualified { }

[OnlyAccessibleBy("**")]
class ByCompleteWildCard { }
