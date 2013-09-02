// Guids.cs
// MUST match guids.h
using System;

namespace Digiexnet.VSGithubConnectorPkg
{
    static class GuidList
    {
        public const string guidVSGithubConnectorPkgPkgString = "407d6b9e-3999-4ad5-93ab-134f9f9fc43d";
        public const string guidVSGithubConnectorPkgCmdSetString = "b9bdcdea-8db1-49f6-9d3a-cc703862d33a";
        public const string guidToolWindowPersistanceString = "e3bc9e16-1474-4e57-8c07-17b15dd1312d";

        public static readonly Guid guidVSGithubConnectorPkgCmdSet = new Guid(guidVSGithubConnectorPkgCmdSetString);
    };
}