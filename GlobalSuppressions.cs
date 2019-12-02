// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "Ну вот ещё, глупости какие")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "Иногда очень хочется")]

//// [assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1404:Code analysis suppression must have justification")]

[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1642:Constructor summary documentation should begin with standard text", Justification = "Не подходит для русского языка")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:Property summary documentation should match accessors", Justification = "Не подходит для русского языка")]

[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:The file header XML is invalid", Justification = "Мы не используем хедеры")]

[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Иначе JSON-сериализация не видит эту коллекцию и не заполняет", Scope = "member", Target = "~P:Olekstra.LikePharma.Client.GetDiscountRequest.Orders")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Иначе JSON-сериализация не видит эту коллекцию и не заполняет", Scope = "member", Target = "~P:Olekstra.LikePharma.Client.GetDiscountResponse.Orders")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Иначе JSON-сериализация не видит эту коллекцию и не заполняет", Scope = "member", Target = "~P:Olekstra.LikePharma.Client.ConfirmPurchaseRequest.Transactions")]
[assembly: SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Иначе JSON-сериализация не видит эту коллекцию и не заполняет", Scope = "member", Target = "~P:Olekstra.LikePharma.Client.ConfirmPurchaseRequest.Skus")]
[assembly: SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Иногда очень хочется", Scope = "type", Target = "~T:Olekstra.LikePharma.Client.GetDiscountRequest.Order")]
[assembly: SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Иногда очень хочется", Scope = "type", Target = "~T:Olekstra.LikePharma.Client.GetDiscountResponse.Order")]
[assembly: SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Иногда очень хочется", Scope = "type", Target = "~T:Olekstra.LikePharma.Client.ConfirmPurchaseRequest.Sku")]

// [assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA0001:XML comment analysis is disabled due to project configuration")]
// [assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented")]
// [assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:Documentation for parameter X is missing")]
// [assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1615:Document return value")]
// [assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:Property summary documentation must match accessors")]
// [assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1652:Enable XML documentation output")]
