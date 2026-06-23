namespace CustomAccessibility.Attributes;

/// <summary>
/// This attribute tells the CustomAccessibility.RestrictedAccessAnalyzer that
///   external projects are not allowed to access the attributed definition 
/// </summary>
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
