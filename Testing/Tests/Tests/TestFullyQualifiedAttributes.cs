namespace Testing.Tests;

[CustomAccessibility.Attributes.OnlyAccessibleBy(nameof(SomeTestClassPartIV))]
class SomeRestrictedClassPartIV { }

class SomeTestClassPartIV
{
    SomeRestrictedClassPartIV a = new();
}

class SomeTestClassPartV
{
    #region CACC000 SomeRestrictedClassPartIV
    SomeRestrictedClassPartIV a = new();
    #endregion
}
