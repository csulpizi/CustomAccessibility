using Testing.Definitions.External;

namespace Testing.Tests;

class QualifiedNameTest
{
    // These are all accessible, no problem
    Testing.Definitions.Internal.Default a = new();
    Definitions.Internal.Default b = new();
    ExternalAllowed c = new();
    Testing.Definitions.External.ExternalAllowed d = new();

    // These are all restricted, CACC000
    #region CACC000 ByDog,IncorrectWildCard
    Testing.Definitions.Internal.ByDog e = new();
    Definitions.Internal.ByDog f = new();
    IncorrectWildCard g = new();
    Testing.Definitions.External.IncorrectWildCard h = new();
    #endregion
}
