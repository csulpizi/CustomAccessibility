`[RestrictAllExternalAccess]` will always be assumed, even for symbols with no CIA attributes specified. If an external module attempts to access a symbol with this implicit assumption, it will return `CIA002` instead of `CIA001`. You can mute `CIA002` to ignore this behaviour.

If `[AllowAccessForClass(...)]` or `[AllowAccessForNamespace(...)]` comes before any other CIA attributes, `[RestrictAllInternalAccess]` and `[RestrictAllExternalAccess]` will both be assumed. To override this assumption, specify internal or external access before the `AllowAccess...` attributes. 

If you are unsure about the implicit assumptions, explicitly mention all access attributes.







|Code Snippet|Equivalent Snippet|Description|
|:--|:--|:--|
|void Foo() {}|[AllowAllInternalAccess]<br/>[RestrictAllExternalAccess]<br/>void Foo() {}| No restrictions on internal access; restricted external access (diagnosticId CIA003 can be muted to allow external by default) |
|[AllowAccessForClass(nameof(Apple))]<br/>void Foo() {}|[RestrictAllInternalAccess]<br/>[RestrictAllExternalAccess]<br/>[AllowAccessForClass(nameof(Apple))]<br/>void Foo() {}| Internal access and external access are both restricted with the exception of access by any class named 'Apple' |
|[RestrictAllExternalAccess]<br/>[AllowAccessForClass(nameof(Apple))]<br/>void Foo() {}|[AllowAllInternalAccess]<br/>[RestrictAllExternalAccess]<br/>[AllowAccessForClass(nameof(Apple))]<br/>void Foo() {}| No restrictions on internal access; external access is restricted with the exception of access by any class named 'Apple' |
|[AllowAccessForClass(nameof(Apple))]<br/>[RestrictAllExternalAccess]<br/>void Foo() {}|[RestrictAllInternalAccess]<br/>[AllowAccessForClass(nameof(Apple))]<br/>[RestrictAllExternalAccess]<br/>void Foo() {}| Internal access is both restricted with the exception of access by any class named 'Apple'; external access completely restricted |