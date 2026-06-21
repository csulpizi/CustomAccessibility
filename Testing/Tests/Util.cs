using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Testing;

static class Util
{
    internal static Compilation CreateCompilation(string source)
    {
        string fileName = "Test.cs";
        string projectName = "Tests";

        var syntaxTree = CSharpSyntaxTree.ParseText(source, path: fileName);

        return CSharpCompilation.Create(projectName, syntaxTrees: [syntaxTree]);
    }

    [AttributeUsage(
        AttributeTargets.Class
            | AttributeTargets.Enum
            | AttributeTargets.Field
            | AttributeTargets.Method
            | AttributeTargets.Property
            | AttributeTargets.Struct,
        AllowMultiple = false
    )]
    internal class ExpectedPass : Attribute { }

    [AttributeUsage(
        AttributeTargets.Class
            | AttributeTargets.Enum
            | AttributeTargets.Field
            | AttributeTargets.Method
            | AttributeTargets.Property
            | AttributeTargets.Struct,
        AllowMultiple = false
    )]
    internal class ExpectedImplicitFail : Attribute { }

    internal interface ITestClass { }
}
