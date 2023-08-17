﻿using GitTool.Infrastructure.Git.Commands.Abstract;

namespace GitTool.Infrastructure.Git.Tests.Helpers
{
    /// <summary>
    ///     A fake "process runner" which mimics the running of processes by reading their output from a file
    /// </summary>
    public class FileReaderProcessRunner : IProcessCommandRunner
    {
        private readonly string _path;

        public FileReaderProcessRunner(string path)
        {
            _path = path;
        }

        public IEnumerable<string> Runner(AbstractCommandLineArguments commandLineArguments)
        {
            return File.ReadLines(_path).Select(NormaliseLineEndings);
        }

        private static string NormaliseLineEndings(string str)
        {
            return str.Replace("\\r", "");
        }
    }
}