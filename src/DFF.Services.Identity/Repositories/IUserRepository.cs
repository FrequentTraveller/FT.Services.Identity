using DFF.Services.Identity.Domain;
using System;
using System.Threading.Tasks;

namespace DFF.Services.Identity.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
    }
}
