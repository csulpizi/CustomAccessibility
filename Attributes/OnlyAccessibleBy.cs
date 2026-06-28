namespace CustomAccessibility.Attributes;

/// <summary>
/// This attribute specifies what type(s) are allowed to access
///   the attached definition.
/// If no `OnlyAccessibleBy` attribute is attached to a definition,
///   it is assumed that there are no restrictions for what types
///   are allowed to reference that definition
/// See https://github.com/csulpizi/CustomAccessibility#onlyaccessibleby
///   for more information
/// </summary>
/// <param name="name">
/// Either the unqualified name, fully qualified name, or a wildcard string
///   of the type(s) that are allowed to access this definition
/// </param>
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
