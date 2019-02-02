using DFF.Common.Mongo;
using DFF.Services.Identity.Domain;
using System.Threading.Tasks;

namespace DFF.Services.Identity.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMongoRepository<RefreshToken> _repository;

        public RefreshTokenRepository(IMongoRepository<RefreshToken> repository)
        {
            _repository = repository;
        }

        public async Task<RefreshToken> GetAsync(string token)
            => await _repository.GetAsync(x => x.Token == token);

        public async Task CreateAsync(RefreshToken token)
            => await _repository.CreateAsync(token);

        public async Task UpdateAsync(RefreshToken token)
            => await _repository.UpdateAsync(token);
    }
}
