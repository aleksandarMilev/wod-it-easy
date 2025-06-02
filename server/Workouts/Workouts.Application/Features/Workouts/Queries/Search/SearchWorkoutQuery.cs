namespace WodItEasy.Workouts.Application.Features.Workouts.Queries.Search
{
    using MediatR;
    using WodItEasy.Common.Application.Models;

    using static WodItEasy.Common.Application.Constants.DefaultValues;

    public class SearchWorkoutQuery
        : IRequest<PaginatedOutputModel<SearchWorkoutOutputModel>>
    {
        public string? StartsAt { get; set; }

        public int PageIndex { get; set; } = DefaultPageIndex;

        public int PageSize { get; set; } = DefaultPageSize;

        public class SearchWorkoutQueryHandler(
            IWorkoutRepository repository)
            : IRequestHandler<SearchWorkoutQuery, PaginatedOutputModel<SearchWorkoutOutputModel>>
        {
            private readonly IWorkoutRepository repository = repository;

            public async Task<PaginatedOutputModel<SearchWorkoutOutputModel>> Handle(
                SearchWorkoutQuery request,
                CancellationToken cancellationToken)
            {
                var startsAt = request.StartsAt == null
                    ? null
                    : DateTime.Parse(request.StartsAt) as DateTime?;

                return await this.repository.Paginated(
                    startsAt,
                    request.PageIndex,
                    request.PageSize,
                    cancellationToken);
            }
        }
    }
}
