using PeekStudy.API.Models;

namespace PeekStudy.API.Repo.IRepo
{
    public interface IIslandRepo
    {
        public Task<Island?> GetByIdAsync(int id);
        public Task<List<Island>> GetByUserIdAsync(int UserId);
        public Task<List<Island>> GetAllAsync();
        public Task AddAsync(Island island);
        public Task UpdateAsync(Island island);
        public Task DeleteAsync(int id);
        public Task SaveAsync();
    }
}
