﻿namespace Admission.Dictionary.Application.Constants;

internal static class RoutingKeys
{
    internal const string FacultyDeleteTimeChangedRoutingKey = "user.document.admission";
    internal const string FacultyNameChangedRoutingKey = "user..admission";
    internal const string ProgramChangedRoutingKey = ".admission";
    internal const string DocumentChangedRoutingKey = ".document.admission";
    internal const string LevelChangedRoutingKey = ".document.admission";
    internal const string NextLevelChangedRoutingKey = ".document.admission";
}