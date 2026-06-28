# Diagnostic Rules

## CACC000

### Description

Restricted Access

Use of \<symbol\> is restricted and cannot be used here

### Cause

The referenced symbol is not accessible to the type that is attempting to reference it. 

### Fixes

- Mark the original symbol with the `CustomAccessibility.Attributes.OnlyAccessibleBy` attribute to specify that the type is allowed to reference it. See [here](https://github.com/csulpizi/CustomAccessibility#onlyaccessibleby) for more details.

## CACC001

### Description

Restricted External Access

External access is restricted unless explicitly specified

### Cause

The referenced symbol does not specify that external projects are allowed to access it. By default, external access is restricted if there are no attributes specifying access restrictions.

### Fixes

- Mark the original symbol with the `ExternalAccessAllowed` attribute; or
- Suppress `CACC001` to assume external access is allowed by default

See [here](https://github.com/csulpizi/CustomAccessibility#external-access) for more details

## CACC002

### Description

Restricted 'using static'

Use of \<imported static class\> is restricted; \<type\> is not allowed to access it",

### Cause

A `using static <some static class>;` statement in the namespace caused this error. That static class is not accessible to one or more of the types declared in the namespace.

### Fixes

- Remove the `using static` expression and fully qualify any necessary references to it; or
- Mark the static class with the `CustomAccessibility.Attributes.OnlyAccessibleBy` attribute to specify that the declared type is allowed to reference it. See [here](https://github.com/csulpizi/CustomAccessibility#onlyaccessibleby) for more details; or
- Use an alias for the `using static` expression instead (e.g., `using SomeAlias = StaticClassYouWantToImport`)

## CACC100

Invalid attribute usage

`CustomAccessibility` attributes can only be applied to declarations with the `internal` access modifier

### Cause

One or more `CustomAccessibility` attributes were attached to a non-`internal` definition.

### Fixes

- Change the accessibility of the definition to `internal`; or
- Remove the problematic attribute