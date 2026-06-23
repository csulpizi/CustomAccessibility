using CustomAccessibility.Attributes;

namespace Testing.Tests.AttributeAnalyzer;

[ExternalAccessOnly]
#region CACC101
[InternalAccessOnly]
#endregion
class OverAttributedClass { }

[ExternalAccessOnly]
#region CACC101
[InternalAccessOnly]
#endregion
enum OverAttributedEnum { }

[ExternalAccessOnly]
#region CACC101
[InternalAccessOnly]
#endregion
interface IOverAttributed { }

[ExternalAccessOnly]
#region CACC101
[InternalAccessOnly]
#endregion
struct OverAttributedStruct { }

class OverAttributedMembersOfClass
{
    [ExternalAccessOnly]
    #region CACC101
    [InternalAccessOnly]
    #endregion
    internal void Method() { }

    [ExternalAccessOnly]
    #region CACC101
    [InternalAccessOnly]
    #endregion
    internal int Field = 0;

    [ExternalAccessOnly]
    #region CACC101
    [InternalAccessOnly]
    #endregion
    internal int Property => 0;
}

interface IOverAttributedMembersOfInterface
{
    [ExternalAccessOnly]
    #region CACC101
    [InternalAccessOnly]
    #endregion
    internal void Method();

    [ExternalAccessOnly]
    #region CACC101
    [InternalAccessOnly]
    #endregion
    internal int Property { get; }
}

struct OverAttributedMembersOfStruct()
{
    [ExternalAccessOnly]
    #region CACC101
    [InternalAccessOnly]
    #endregion
    internal void Method() { }

    [ExternalAccessOnly]
    #region CACC101
    [InternalAccessOnly]
    #endregion
    internal static void StaticMethod() { }

    [ExternalAccessOnly]
    #region CACC101
    [InternalAccessOnly]
    #endregion
    internal int Field = 0;

    [ExternalAccessOnly]
    #region CACC101
    [InternalAccessOnly]
    #endregion
    internal int Property => 0;
}

readonly struct OverAttributedMembersOfReadonlyStruct()
{
    [ExternalAccessOnly]
    #region CACC101
    [InternalAccessOnly]
    #endregion
    internal void Method() { }

    [ExternalAccessOnly]
    #region CACC101
    [InternalAccessOnly]
    #endregion
    internal readonly void ReadonlyMethod() { }

    [ExternalAccessOnly]
    #region CACC101
    [InternalAccessOnly]
    #endregion
    internal static void StaticMethod() { }

    [ExternalAccessOnly]
    #region CACC101
    [InternalAccessOnly]
    #endregion
    readonly internal int Field = 0;

    [ExternalAccessOnly]
    #region CACC101
    [InternalAccessOnly]
    #endregion
    readonly internal int Property => 0;
}
