using CustomAccessibility.Attributes;
using CustomAccessibility.Examples.Animals.Mammals;

namespace CustomAccessibility.Examples;

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

    [OnlyAccessibleBy("CustomAccessibility.Examples.Animals.Mammals.Dog")]
    internal int F; // Accessible by the exact type "Animals.Mammals.Dog"

    [OnlyAccessibleBy("CustomAccessibility.Examples.Animals.Mammals.*")]
    internal int G; // Accessible by any type in the "Animals.Mammals" namespace

    [OnlyAccessibleBy("CustomAccessibility.Examples.Animals.Mammals.**")]
    internal int H; // Accessible by any type in the "Animals.Mammals" namespace or its descendents

    [OnlyAccessibleBy("CustomAccessibility.Examples.Animals.Mammals.Dog.**")]
    internal int I; // Accessible by any types defined within the "Animals.Mammals.Dog" class
}
