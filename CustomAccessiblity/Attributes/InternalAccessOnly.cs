namespace CustomAccessiblity.Attributes;

[AttributeUsage(
    AttributeTargets.Class
        | AttributeTargets.Enum
        | AttributeTargets.Field
        | AttributeTargets.Method
        | AttributeTargets.Property
        | AttributeTargets.Struct
        | AttributeTargets.Interface,
    AllowMultiple = false
)]
public sealed class InternalAccessOnly() : Attribute { }
