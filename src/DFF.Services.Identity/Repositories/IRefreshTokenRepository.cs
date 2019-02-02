using DFF.Services.Identity.Domain;
using System.Threading.Tasks;

namespace DFF.Services.Identity.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string token);
        Task CreateAsync(RefreshToken token);
        Task UpdateAsync(RefreshToken token);
    }
}
