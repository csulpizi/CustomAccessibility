namespace CustomAccessibility.Examples.Animals.Mammals;

class SheepDog
{
    void AccessTest(SomeClass o)
    {
        _ = o.B; // => CACC000; "SheepDog" != "Dog"
        _ = o.C; // => CACC000; "SheepDog" != "Dog"
        _ = o.D; // Okay! Not a dog but is a "SheepDog"
        _ = o.E; // Okay! Matches "*Dog"
        _ = o.F; // => CACC000; Not == Dog class
        _ = o.G; // Okay! Is a direct member of the Mammals namespace
        _ = o.H; // Okay! Is in the Mammals namespace
    }
}
