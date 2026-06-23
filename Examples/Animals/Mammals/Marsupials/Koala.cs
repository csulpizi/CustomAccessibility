namespace CustomAccessibility.Examples.Animals.Mammals.Marsupials;

class Koala
{
    void AccessTest(SomeClass o)
    {
        _ = o.B; // => CACC000; "Koala" != "Dog"
        _ = o.D; // => CACC000; "Koala" != "Dog" && != "SheepDog"
        _ = o.E; // => CACC000; "Koala" does not end with "Dog"
        _ = o.G; // => CACC000; Not a direct member of the CustomAccessibility.Examples.Animals.Mammals namespace
        _ = o.H; // Okay! 
    }
}