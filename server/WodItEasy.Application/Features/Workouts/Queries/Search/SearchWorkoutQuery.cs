namespace WodItEasy.Application.Features.Workouts.Queries.Search
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using MediatR;

    using static DefaultValues;

    public class SearchWorkoutQuery : IRequest<PaginatedOutputModel<SearchWorkoutOutputModel>>
    {
        public DateTime? StartsAtDate { get; }

        public int PageSize { get; } = DefaultPageSize;

        public int PageIndex { get; } = DefaultPageIndex;

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
