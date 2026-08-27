using PeekStudy.API.Models;
using PeekStudy.API.Data;
using PeekStudy.API.IRepo;
using Microsoft.EntityFrameworkCore;

namespace PeekStudy.API.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext db;
        public UserRepo(AppDbContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(User user)
        {
            await db.Users.AddAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            var d = await db.Users.FindAsync(id);
            if (d != null)
            {
                db.Users.Remove(d);
            }

        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public Task UpdateAsync(User user)
        {
            db.Users.Update(user);
            return Task.CompletedTask;
        }

        async Task<bool> IUserRepo.EmailExistsAsync(string email)
        {
            return await db.Users.AnyAsync(u => u.Email == email);

        }

        async Task<User?> IUserRepo.GetByEmailAsync(string email)
        {
            return await db.Users.FirstOrDefaultAsync(e => e.Email == email);
        }

        async Task<User?> IUserRepo.GetByIdAsync(int id)
        {
            return await db.Users.FindAsync(id);
        }
    }
}