using DFF.Common.Authentication;
using DFF.Services.Identity.Domain;
using System;
using System.Threading.Tasks;

namespace DFF.Services.Identity.Services
{
    public interface IIdentityService
    {
        Task SignUpAsync(Guid id, string email, string password, string role = Role.User);
        Task<JsonWebToken> SignInAsync(string email, string password);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }
}
