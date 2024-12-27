namespace WodItEasy.Application.Contracts
{
    using System.Threading.Tasks;
    using Application.Common;
    using Features.Identity;

    public interface IIdentity
    {
        Task<Result> Register(UserInputModel userInput);

        Task<Result<LoginOutputModel>> Login(UserInputModel userInput);
    }
}
