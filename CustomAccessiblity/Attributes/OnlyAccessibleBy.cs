namespace CustomAccessiblity.Attributes;

[AttributeUsage(
    AttributeTargets.Class
        | AttributeTargets.Enum
        | AttributeTargets.Field
        | AttributeTargets.Method
        | AttributeTargets.Property
        | AttributeTargets.Struct
        | AttributeTargets.Interface,
    AllowMultiple = true
)]
public sealed class OnlyAccessibleBy(string name) : Attribute
{
    public readonly string Name = name;
}
