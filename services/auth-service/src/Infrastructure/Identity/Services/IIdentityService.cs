using Domain.Aggregates.User.Entities;
using Domain.Common;

namespace Infrastructure.Identity.Services
{
    public interface IIdentityService
    {
        Task<Result<string>> CreateUserIdentityAsync(User user, string password);
        Task<Result> ValidateUserCredentialsAsync(string username, string password);
        Task<Result<string>> GenerateTokenAsync(User user);
        Task<Result> UpdateUserIdentityAsync(User user);
        Task<Result> DeleteUserIdentityAsync(string userId);
    }
}