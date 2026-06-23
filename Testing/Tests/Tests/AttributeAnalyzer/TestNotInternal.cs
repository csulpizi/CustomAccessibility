using CustomAccessibility.Attributes;

namespace Testing.Tests.AttributeAnalyzer;

class ClassWithNonInternalMembers
{
    #region CACC100
    [AccessibleByInternalAndExternal]
    #endregion
    void Method() { }

    #region CACC100
    [AccessibleByInternalAndExternal]
    #endregion
    int Field = 0;

    #region CACC100
    [AccessibleByInternalAndExternal]
    #endregion
    int Property => 0;
}

interface IInternalWithNonInternalMembers
{
    #region CACC100
    [AccessibleByInternalAndExternal]
    #endregion
    void Method();

    #region CACC100
    [AccessibleByInternalAndExternal]
    #endregion
    int Property { get; }
}

public interface IPublicWithNonInternalMembers
{
    #region CACC100
    [AccessibleByInternalAndExternal]
    #endregion
    void Method();

    #region CACC100
    [AccessibleByInternalAndExternal]
    #endregion
    int Property { get; }
}

struct StructWithNonInternalMembers()
{
    #region CACC100
    [AccessibleByInternalAndExternal]
    #endregion
    void Method() { }

    #region CACC100
    [AccessibleByInternalAndExternal]
    #endregion
    readonly void ReadonlyMethod() { }

    #region CACC100
    [AccessibleByInternalAndExternal]
    #endregion
    static void StaticMethod() { }

    #region CACC100
    [AccessibleByInternalAndExternal]
    #endregion
    int Field = 0;

    #region CACC100
    [AccessibleByInternalAndExternal]
    #endregion
    int Property => 0;
}

readonly struct ReadonlyStructWithNonInternalMembers()
{
    #region CACC100
    [AccessibleByInternalAndExternal]
    #endregion
    void Method() { }

    #region CACC100
    [AccessibleByInternalAndExternal]
    #endregion
    readonly void ReadonlyMethod() { }

    #region CACC100
    [AccessibleByInternalAndExternal]
    #endregion
    static void StaticMethod() { }

    #region CACC100
    [AccessibleByInternalAndExternal]
    #endregion
    readonly int Field = 0;

    #region CACC100
    [AccessibleByInternalAndExternal]
    #endregion
    readonly int Property => 0;
}
