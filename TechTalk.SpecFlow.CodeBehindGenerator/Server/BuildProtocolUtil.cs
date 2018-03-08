﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Generic;
using TechTalk.SpecFlow.CodeBehindGenerator.Shared;
using TechTalk.SpecFlow.CodeBehindGenerator.Shared.Request;

namespace TechTalk.SpecFlow.CodeBehindGenerator.Server
{
    internal static class BuildProtocolUtil
    {
        internal static RunRequest GetRunRequest(BuildRequest req)
        {
            string currentDirectory;
            string libDirectory;
            string tempDirectory;
            string[] arguments = GetCommandLineArguments(req, out currentDirectory, out tempDirectory, out libDirectory);
            string language = "";
            return new RunRequest(language, currentDirectory, tempDirectory, libDirectory, arguments);
        }

        internal static string[] GetCommandLineArguments(BuildRequest req, out string currentDirectory, out string tempDirectory, out string libDirectory)
        {
            currentDirectory = null;
            libDirectory = null;
            tempDirectory = null;
            List<string> commandLineArguments = new List<string>();

            foreach (BuildRequest.Argument arg in req.Arguments)
            {
                if (arg.ArgumentId == BuildProtocolConstants.ArgumentId.CurrentDirectory)
                {
                    currentDirectory = arg.Value;
                }
                else if (arg.ArgumentId == BuildProtocolConstants.ArgumentId.TempDirectory)
                {
                    tempDirectory = arg.Value;
                }
                else if (arg.ArgumentId == BuildProtocolConstants.ArgumentId.LibEnvVariable)
                {
                    libDirectory = arg.Value;
                }
                else if (arg.ArgumentId == BuildProtocolConstants.ArgumentId.CommandLineArgument)
                {
                    int argIndex = arg.ArgumentIndex;
                    while (argIndex >= commandLineArguments.Count)
                        commandLineArguments.Add("");
                    commandLineArguments[argIndex] = arg.Value;
                }
            }

            return commandLineArguments.ToArray();
        }
    }
}
