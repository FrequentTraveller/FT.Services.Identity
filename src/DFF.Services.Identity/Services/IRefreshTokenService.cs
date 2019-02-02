using DFF.Common.Authentication;
using System;
using System.Threading.Tasks;

namespace DFF.Services.Identity.Services
{
    public interface IRefreshTokenService
    {
        Task CreateAsync(Guid userId);
        Task<JsonWebToken> CreateAccessTokenAsync(string refreshToken);
        Task RevokeAsync(string refreshToken, Guid userId);
    }
}
