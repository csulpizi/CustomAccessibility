using System.Collections.Immutable;
using CustomAccessiblity.Attributes;
using CustomAccessiblity.Rules;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace CustomAccessiblity;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class RestrictedTypeAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics =>
        [RestrictedAccess.Rule, ImplicitlyRestrictedAccess.Rule];

    public override void Initialize(AnalysisContext ctx)
    {
        ctx.ConfigureGeneratedCodeAnalysis(
            GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics
        );
        ctx.EnableConcurrentExecution();

        //ctx.RegisterSyntaxNodeAction(AnalyzeMemberAccess, SyntaxKind.SimpleMemberAccessExpression);
        //ctx.RegisterSyntaxNodeAction(AnalyzeTupleElement, SyntaxKind.TupleElement);
        //ctx.RegisterSyntaxNodeAction(AnalyzeVariableDeclaration, SyntaxKind.VariableDeclaration);

        //ctx.RegisterSyntaxNodeAction(AnalyzeTypeDeclaration, SyntaxKind.ClassDeclaration);
        //ctx.RegisterSyntaxNodeAction(AnalyzeTypeDeclaration, SyntaxKind.StructDeclaration);

        ctx.RegisterSyntaxNodeAction(ValidateAllReferencedTypes, SyntaxKind.ClassDeclaration);
        ctx.RegisterSyntaxNodeAction(ValidateAllReferencedTypes, SyntaxKind.StructDeclaration);
    }

    static void AnalyzeMemberAccess(SyntaxNodeAnalysisContext ctx)
    {
        var node = (MemberAccessExpressionSyntax)ctx.Node;
        var declaringClass = node.FirstAncestorOrSelf<TypeDeclarationSyntax>();
        var declaringClassSymbol = declaringClass is not null
            ? ctx.SemanticModel.GetDeclaredSymbol(declaringClass)
            : null;
        var symbol = ctx.SemanticModel.GetSymbolInfo(node).Symbol;
        Util.Validate(ctx, symbol, declaringClassSymbol, node.Name.GetLocation);
    }

    static void AnalyzeVariableDeclaration(SyntaxNodeAnalysisContext ctx)
    {
        var node = (VariableDeclarationSyntax)ctx.Node;
        var type = node.Type;
        var symbol = ctx.SemanticModel.GetSymbolInfo(type).Symbol;
        var declaringClass = node.FirstAncestorOrSelf<TypeDeclarationSyntax>();
        var declaringClassSymbol = declaringClass is not null
            ? ctx.SemanticModel.GetDeclaredSymbol(declaringClass)
            : null;
        Util.Validate(ctx, symbol, declaringClassSymbol, type.GetLocation);
    }

    static void AnalyzeTupleElement(SyntaxNodeAnalysisContext ctx)
    {
        var node = (TupleElementSyntax)ctx.Node;
        var declaringClass = node.FirstAncestorOrSelf<TypeDeclarationSyntax>();
        var declaringClassSymbol = declaringClass is not null
            ? ctx.SemanticModel.GetDeclaredSymbol(declaringClass)
            : null;
        var symbol = ctx.SemanticModel.GetSymbolInfo(node.Type).Symbol;
        Util.Validate(ctx, symbol, declaringClassSymbol, node.GetLocation);
    }

    static void AnalyzeTypeDeclaration(SyntaxNodeAnalysisContext ctx)
    {
        var node = (TypeDeclarationSyntax)ctx.Node;
        var declaringClassSymbol = ctx.SemanticModel.GetDeclaredSymbol(node);
        if (declaringClassSymbol is null)
            return;

        AnalyzePropertyDeclaration(ctx, node, declaringClassSymbol);
        AnalyzeMethodDeclaration(ctx, node, declaringClassSymbol);
        AnalyzeInheritance(ctx, node, declaringClassSymbol);
        AnalyzeConstructors(ctx, node, declaringClassSymbol);
    }

    static void AnalyzeInheritance(
        SyntaxNodeAnalysisContext ctx,
        TypeDeclarationSyntax node,
        INamedTypeSymbol declaringClassSymbol
    )
    {
        Location getLocation(ISymbol s)
        {
            var baseList = node.BaseList;
            if (baseList is null)
                return node.GetLocation();
            var types = baseList.Types.Where(t =>
                SymbolEqualityComparer.Default.Equals(
                    s,
                    ctx.SemanticModel.GetSymbolInfo(t.Type).Symbol
                )
            );
            if (!types.Any())
                return node.GetLocation();
            return types.First().GetLocation();
        }

        var baseClass = declaringClassSymbol.BaseType;
        if (baseClass is not null)
        {
            Util.Validate(ctx, baseClass, declaringClassSymbol, () => getLocation(baseClass));
        }
        for (int i = 0; i < declaringClassSymbol.Interfaces.Length; i++)
        {
            var o = declaringClassSymbol.Interfaces[i];
            Util.Validate(ctx, o, declaringClassSymbol, () => getLocation(o));
        }
    }

    static void AnalyzePropertyDeclaration(
        SyntaxNodeAnalysisContext ctx,
        TypeDeclarationSyntax node,
        INamedTypeSymbol declaringClassSymbol
    )
    {
        foreach (
            var type in node.ChildNodes()
                .Where(x => x is PropertyDeclarationSyntax)
                .Cast<PropertyDeclarationSyntax>()
                .Select(property => property.Type)
        )
            ValidateTypeUnrestricted(ctx, declaringClassSymbol, type);
    }

    static void AnalyzeMethodDeclaration(
        SyntaxNodeAnalysisContext ctx,
        TypeDeclarationSyntax node,
        INamedTypeSymbol declaringClassSymbol
    )
    {
        foreach (
            var method in node.ChildNodes()
                .Where(x => x is MethodDeclarationSyntax)
                .Cast<MethodDeclarationSyntax>()
        )
        {
            ValidateTypeUnrestricted(ctx, declaringClassSymbol, method.ReturnType);
            foreach (
                var type in method.ParameterList.Parameters.Select(parameter => parameter.Type)
            )
            {
                if (type is not null)
                    ValidateTypeUnrestricted(ctx, declaringClassSymbol, type);
            }
        }
    }

    static void AnalyzeConstructors(
        SyntaxNodeAnalysisContext ctx,
        TypeDeclarationSyntax node,
        INamedTypeSymbol declaringClassSymbol
    )
    {
        // Handle primary constructors
        var ps = node.ParameterList;
        if (ps is not null)
        {
            foreach (var p in ps.Parameters)
            {
                if (p.ChildNodes().First() is IdentifierNameSyntax type)
                {
                    if (type is not null)
                        ValidateTypeUnrestricted(ctx, declaringClassSymbol, type);
                }
            }
        }

        // Handle explicit constructors
        foreach (
            var method in node.ChildNodes()
                .Where(x => x is ConstructorDeclarationSyntax)
                .Cast<ConstructorDeclarationSyntax>()
        )
        {
            foreach (
                var type in method.ParameterList.Parameters.Select(parameter => parameter.Type)
            )
            {
                if (type is not null)
                    ValidateTypeUnrestricted(ctx, declaringClassSymbol, type);
            }
        }
    }

    static void ValidateTypeUnrestricted(
        SyntaxNodeAnalysisContext ctx,
        INamedTypeSymbol declaringSymbol,
        TypeSyntax referencedType
    )
    {
        var symbol = ctx.SemanticModel.GetSymbolInfo(referencedType).Symbol;
        Util.Validate(ctx, symbol, declaringSymbol, referencedType.GetLocation);
    }

    static void ValidateAllReferencedTypes(SyntaxNodeAnalysisContext ctx)
    {
        var node = (TypeDeclarationSyntax)ctx.Node;
        var declaringSymbol = ctx.SemanticModel.GetDeclaredSymbol(node);
        var descendents = node.DescendantNodes().Where(x => x is TypeSyntax).Cast<TypeSyntax>();
        foreach (var typeReference in descendents)
        {
            var symbol = ctx.SemanticModel.GetSymbolInfo(typeReference).Symbol;
            Util.Validate(ctx, symbol, declaringSymbol, typeReference.GetLocation);
        }
    }
}
