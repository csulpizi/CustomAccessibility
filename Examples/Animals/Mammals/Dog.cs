namespace CustomAccessibility.Examples.Animals.Mammals;

class Dog
{
    void AccessTest(SomeClass o)
    {
        _ = o.A;
        _ = o.B;
        _ = o.C;
        _ = o.D;
        _ = o.E;
        _ = o.F;
        _ = o.G;
        _ = o.H;
        _ = o.I; // => CACC000; I is only accessible subtypes of this class
    }

    class Collar
    {
        void AccessTest(SomeClass o)
        {
            _ = o.I; // => Okay!
        }
    }
}
