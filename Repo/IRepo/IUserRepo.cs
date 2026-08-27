using PeekStudy.API.Models;

namespace PeekStudy.API.IRepo
{
    public interface IUserRepo
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
        public Task AddAsync(User user);
        public Task UpdateAsync(User user);
        public Task DeleteAsync(int id);
        public Task SaveAsync();
    }
}
