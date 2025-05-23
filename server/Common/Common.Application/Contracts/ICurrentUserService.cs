namespace WodItEasy.Common.Application.Contracts
{
    public interface ICurrentUserService
    {
        string? UserId { get; }

        string? Username { get; }

        bool IsAdmin { get; }
    }
}
