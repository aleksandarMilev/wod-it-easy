namespace WodItEasy.Application.Features.Workouts.Queries.Search
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common;
    using MediatR;

    using static Application.Common.DefaultValues;

    public class SearchWorkoutQuery : IRequest<PaginatedOutputModel<SearchWorkoutOutputModel>>
    {
        public string? StartsAt { get; set; }

        public int PageIndex { get; set; } = DefaultPageIndex;

        public int PageSize { get; set; } = DefaultPageSize;

        public class SearchWorkoutQueryHandler : IRequestHandler<SearchWorkoutQuery, PaginatedOutputModel<SearchWorkoutOutputModel>>
        {
            private readonly IWorkoutRepository repository;

            public SearchWorkoutQueryHandler(IWorkoutRepository repository) 
                => this.repository = repository;

            public async Task<PaginatedOutputModel<SearchWorkoutOutputModel>> Handle(
                SearchWorkoutQuery request,
                CancellationToken cancellationToken)
            {
                var startsAt = request.StartsAt == null 
                    ? null
                    : DateTime.Parse(request.StartsAt).ToUniversalTime() as DateTime?;

                 return await this.repository.Paginated(
                    startsAt,
                    request.PageIndex,
                    request.PageSize,
                    cancellationToken);
            }
        }
    }
}
