namespace WodItEasy.Profile.Application.Features.Profile.Queries.Details
{
    using Common.Application.Mapping;
    using Domain.Models.Profile;

    public class ProfileDetailsOutputModel : IMapFrom<Profile>
    {
        public int Id { get; set; }

        public string? AvatarUrl { get; set; }

        public string? Bio { get; set; }
    }
}
