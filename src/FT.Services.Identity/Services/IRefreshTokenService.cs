using FT.Common.Authentication;
using System;
using System.Threading.Tasks;

namespace FT.Services.Identity.Services
{
    public interface IRefreshTokenService
    {
        Task CreateAsync(Guid userId);
        Task<JsonWebToken> CreateAccessTokenAsync(string refreshToken);
        Task RevokeAsync(string refreshToken, Guid userId);
    }
}
