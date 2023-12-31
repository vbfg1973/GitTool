﻿using CodeTool.Infrastructure.Git.ProcessRunner.Commands.Abstract;
using CodeTool.Infrastructure.Git.ProcessRunner.Commands.Parameters;

namespace CodeTool.Infrastructure.Git.ProcessRunner.Commands
{
    public class GitLogBasicPaging : GitLogCommandLineArguments
    {
        private readonly bool _includeFiles;

        public GitLogBasicPaging
        (
            RepositoryDetails repositoryDetails,
            GitPageParameters gitPaging,
            bool includeFiles = false
        ) :
            base(repositoryDetails, gitPaging)
        {
            _includeFiles = includeFiles;
        }

        public override IEnumerable<string> Arguments()
        {
            foreach (var argument in Preamble()) yield return argument;

            if (_includeFiles) yield return "--name-status";

            foreach (var argument in Paging()) yield return argument;
        }
    }
}