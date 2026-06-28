using CustomAccessibility.Attributes;

namespace Testing.Tests.AttributeAnalyzer;

class ClassWithNonInternalMembers
{
    #region CACC100
    [ExternalAccessAllowed]
    #endregion
    void Method() { }

    #region CACC100
    [ExternalAccessAllowed]
    #endregion
    int Field = 0;

    #region CACC100
    [ExternalAccessAllowed]
    #endregion
    int Property => 0;

    #region  CACC100
    [OnlyAccessibleBy("Dog")]
    #endregion
    int Property2 = 0;
}

interface IInternalWithNonInternalMembers
{
    #region CACC100
    [ExternalAccessAllowed]
    #endregion
    void Method();

    #region CACC100
    [ExternalAccessAllowed]
    #endregion
    int Property { get; }
}

public interface IPublicWithNonInternalMembers
{
    #region CACC100
    [ExternalAccessAllowed]
    #endregion
    void Method();

    #region CACC100
    [ExternalAccessAllowed]
    #endregion
    int Property { get; }
}

struct StructWithNonInternalMembers()
{
    #region CACC100
    [ExternalAccessAllowed]
    #endregion
    void Method() { }

    #region CACC100
    [ExternalAccessAllowed]
    #endregion
    readonly void ReadonlyMethod() { }

    #region CACC100
    [ExternalAccessAllowed]
    #endregion
    static void StaticMethod() { }

    #region CACC100
    [ExternalAccessAllowed]
    #endregion
    int Field = 0;

    #region CACC100
    [ExternalAccessAllowed]
    #endregion
    int Property => 0;
}

readonly struct ReadonlyStructWithNonInternalMembers()
{
    #region CACC100
    [ExternalAccessAllowed]
    #endregion
    void Method() { }

    #region CACC100
    [ExternalAccessAllowed]
    #endregion
    readonly void ReadonlyMethod() { }

    #region CACC100
    [ExternalAccessAllowed]
    #endregion
    static void StaticMethod() { }

    #region CACC100
    [ExternalAccessAllowed]
    #endregion
    readonly int Field = 0;

    #region CACC100
    [ExternalAccessAllowed]
    #endregion
    readonly int Property => 0;
}
