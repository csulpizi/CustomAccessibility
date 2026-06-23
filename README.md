# CustomAccessibility

Provides type, namespace, and module-specific accessibility constraints. Packaged with a code analyzer that enforces those constraints in wer IDE and during compilation.

## Install

[![NuGet version (csulpizi.CustomAccessibility)](https://img.shields.io/nuget/v/csulpizi.CustomAccessibility.svg?style=flat-square)](https://www.nuget.org/packages/csulpizi.CustomAccessibility/)

```
<ItemGroup>
    <PackageReference Include="csulpizi.CustomAccessibility" OutputItemType="Analyzer" />
</ItemGroup>
```

Make sure to specify `OutputItemType="Analyzer"` or else the analyzer will not work.

## Rationale

Accessibility is an important part of strongly-typed languages to provide well-defined boundaries and abstractions for individual systems, preventing components from accessing items that are not intended to be exposed. `public`, `internal`, and `private` provide adequate support in most use cases, but are not flexible enough to cover every design pattern. 

A good example is the "Chain-of-Responsibility" design pattern. In this pattern, we expect classes to only interact with their subordinate. A Lieutenant should never issue commands directly to a Soldier.

![Chain of Responsibility](Documentation/ChainOfResponsibility.png)

If we define this same structure in a given project, we would need to make the `Soldier` class and all relevant members `internal` so that the `Sargeant` can access them. But this exposes `Soldier` and its members to every class in the project, not just `Sargeant`, breaking the pattern we are trying to achieve.

A similar problem appears when testing. We may want a method to be `private`, but in order to unit test the method in a test project we need to use `[assembly: InternalsVisibleTo("TestProject")]` and mark the method as `internal`. This again breaks the desired pattern, since now any type in the project can access the method we wanted to be `private`.

## Usage

Using the `OnlyAccessibleBy` attribute on any definition allows us to constrain which types and namespaces are allowed to access that definition.

```
using CustomAccessibility.Attributes;

[OnlyAccessibleBy("Sargeant")]
class Soldier
{
    ...
}

[OnlyAccessibleBy("Lieutenant")]
class Sargeant
{
    ...
}

[OnlyAccessibleBy("Main")]
class Lieutenant
{
    ...
}
```

The `OnlyAccessibleBy` attribute can be used on classes, interfaces, structs, enums, methods, fields, and properties. Any reference to a definition by a type that is not allow-listed will report a `CACC000 - Restricted Access` diagnostic error.

Internals not marked with `OnlyAccessibleBy` are not constrained and can be referenced anywhere in a project.

Accessibility contraints will never prevent a type from accessing its own members (ie, private access is maintained) or a type from accessing its base class's members (ie, protected access is maintained).

## OnlyAccessibleBy

The `OnlyAccessibleBy` attribute works for both unqualified names and fully qualified names. Multiple `OnlyAccessibleBy` attributes are treated as "or", allowing access by multiple types. `*` and `**` can be used for wildcard matching.

```
using CustomAccessibility.Attributes;

class SomeClass
{
    internal int A; // No restrictions; accessible by every type

    [OnlyAccessibleBy("Dog")]
    internal int B; // Accessible by any type named "Dog"

    [OnlyAccessibleBy(nameof(Dog))]
    internal int C; // Accessible by any type named "Dog"

    [OnlyAccessibleBy("Dog")]
    [OnlyAccessibleBy("SheepDog")]
    internal int D; // Accessible by any type named "Dog" or "SheepDog"

    [OnlyAccessibleBy("*Dog")]
    internal int E; // Accessible by any type whose name ends in "Dog" (e.g. BlackDog, SheepDog, Dog)

    [OnlyAccessibleBy("Animals.Mammals.Dog")]
    internal int F; // Accessible by the exact type "Animals.Mammals.Dog"

    [OnlyAccessibleBy("Animals.Mammals.*")]
    internal int G; // Accessible by any type in the "Animals.Mammals" namespace

    [OnlyAccessibleBy("Animals.Mammals.**")]
    internal int H; // Accessible by any type in the "Animals.Mammals" namespace or its descendents

    [OnlyAccessibleBy("Animals.Mammals.Dog.**")]
    internal int I; // Accessible by any types defined within the "Animals.Mammals.Dog" class
}
```

## External Access

When we want to give a project access to another project's internals (e.g. `[assembly: InternalsVisibleTo("TestProject")]`), we can specify which definitions we want the external project to have access to.

```
using CustomAccessibility.Attributes;

// This method is only accessible by this project (unless CACC001 is supressed)
void Apple() {}

// This method is only accessible by this project
[InternalAccessOnly]
void Pear() {}

// This method is only accessible by an external project
[ExternalAccessOnly]
void Orange() {}

// This method is accessible by both
[AccessibleByInternalAndExternal]
void Grape() {}
```

Access to external definitions is restricted by default, and will report a `CACC001 - Restricted Access` diagnostic error. To override this behaviour, suppress the `CACC001` diagnostic.

External references will still be constrained by `OnlyAccessibleBy` attributes. e.g:

```
using CustomAccessibility.Attributes;

// This class can only be referenced by classes named "Dog",
//   whether or not that class is internal or external
[AccessibleByInternalAndExternal]
[OnlyAccessibleBy("Dog")]
class Trainer
{
    ...
}
```
