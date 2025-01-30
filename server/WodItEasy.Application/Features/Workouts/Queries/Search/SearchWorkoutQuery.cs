namespace WodItEasy.Application.Features.Workouts.Queries.Search
{
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using MediatR;

    using static Common.DefaultValues;

    public class SearchWorkoutQuery : IRequest<PaginatedOutputModel<SearchWorkoutOutputModel>>
    {
        public string? StartsAtDate { get; set; }

        public int PageIndex { get; set; } = DefaultPageIndex;

        public int PageSize { get; set; } = DefaultPageSize;

        public class SearchWorkoutQueryHandler : IRequestHandler<SearchWorkoutQuery, PaginatedOutputModel<SearchWorkoutOutputModel>>
        {
            private readonly IWorkoutRepository repository;

            public SearchWorkoutQueryHandler(IWorkoutRepository repository) 
                => this.repository = repository;

            public Task<PaginatedOutputModel<SearchWorkoutOutputModel>> Handle(SearchWorkoutQuery request, CancellationToken cancellationToken)
                => this.repository.Paginated(
                    request.StartsAtDate,
                    request.PageIndex,
                    request.PageSize,
                    cancellationToken);
        }
    }
}
