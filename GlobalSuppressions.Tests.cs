﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1404:Code analysis suppression must have justification")]

[assembly: SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Ну не в тестах же")]
[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Ну в тестах для удобочитаемости можно")]

[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA0001:XML comment analysis is disabled due to project configuration")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:Documentation for parameter X is missing")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1615:Document return value")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:Property summary documentation must match accessors")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1652:Enable XML documentation output")]
