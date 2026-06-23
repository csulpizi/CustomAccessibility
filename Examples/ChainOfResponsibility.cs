using CustomAccessibility.Attributes;

namespace CustomAccessibility.Examples;

static class ChainOfResponsibility
{
    [OnlyAccessibleBy("Sargeant")]
    internal class Soldier
    {
        void IssueCommand(Sargeant sargeant) {} // => CACC000 Use of Sargeant is restricted and cannot be used here
        void IssueCommand(Lieutenant lieutenant) {} // => CACC000 Use of Lieutenant is restricted and cannot be used here
    }

    [OnlyAccessibleBy("Lieutenant")]
    internal class Sargeant 
    {
        void IssueCommand(Soldier soldier) {} // => Okay!
        void IssueCommand(Lieutenant lieutenant) {} // => CACC000 Use of Lieutenant is restricted and cannot be used here
    }

    [OnlyAccessibleBy("Main")]
    internal class Lieutenant
    {
        void IssueCommand(Soldier soldier) {} // => CACC000 Use of Soldier is restricted and cannot be used here
        void IssueCommand(Sargeant sargeant) {} // => Okay!
    }
}
