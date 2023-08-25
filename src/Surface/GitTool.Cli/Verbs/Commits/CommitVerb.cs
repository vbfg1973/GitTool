using System.Text.Json;
using GitTool.Cli.Verbs.Options;
using GitTool.Domain.Features.Repositories.GitLogs;
using GitTool.Domain.Features.Repositories.GitLogs.Csv;
using GitTool.Infrastructure.Git;
using MediatR;

namespace GitTool.Cli.Verbs.Commits
{
    public class CommitVerb
    {
        private readonly IGitService _gitService;
        private readonly IMediator _mediator;

        public CommitVerb(IMediator mediator, IGitService gitService)
        {
            _mediator = mediator;
            _gitService = gitService;
        }

        public async Task Run(CommitOptions options, CancellationToken ctx)
        {
            // var repoDetails = options.GetRepositoryDetails();
            // var pageParameters = new GitPageParameters { Page = 1, PageSize = 100 };
            // var request = new GetGitLogPage(repoDetails, pageParameters);
            //
            // Console.WriteLine(JsonSerializer.Serialize(request));
            //
            // var page = _mediator.CreateStream(request, ctx).ToBlockingEnumerable().ToList();
            // Console.WriteLine(page.Count());
            // Console.WriteLine();
            //
            // var handler = new GetGitLogPageHandler(_gitService);
            // var a = handler.Handle(request, ctx).ToBlockingEnumerable(ctx).ToList();
            // Console.WriteLine(a.Count);
            // Console.WriteLine();
            //
            // var servicePage = _gitService.GetLogsWithFiles(repoDetails, pageParameters, ctx).ToBlockingEnumerable()
            //     .ToList();
            // Console.WriteLine(servicePage.Count());
            // Console.WriteLine();
            //
            // foreach (var gitLog in page) Console.WriteLine(gitLog.Author);

            await _mediator.Send(new SaveGitLogsToCsv(options.GetRepositoryDetails(), options.CsvFile), ctx);
        }
    }
}