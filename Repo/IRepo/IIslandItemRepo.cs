using PeekStudy.API.Models;

namespace PeekStudy.API.Repo.IRepo
{
    public interface IIslandItemRepo
    {
        public Task<IslandItem?> GetByIdAsync(int id);
        public Task<List<IslandItem>> GetByFocusSessionIdAsync(int focusSessionId);
        public Task<List<IslandItem>> GetByIslandIdAsync(int islandId);
        public Task<List<IslandItem>> GetAllAsync();
        public Task AddAsync(IslandItem islandItem);
        public Task UpdateAsync(IslandItem islandItem);
        public Task DeleteAsync(int id);
        public Task SaveAsync();
    }
}
