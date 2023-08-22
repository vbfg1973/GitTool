using System.Collections.Immutable;

namespace GitTool.Infrastructure.Git.Commands.Abstract
{
    public abstract record AbstractCommandLineArguments
    {
        public string FileName { get; protected init; } = null!;
        public ImmutableList<string> Arguments { get; init; }
    }
}