using Microsoft.EntityFrameworkCore;
using PeekStudy.API.Data;
using PeekStudy.API.Models;
using PeekStudy.API.Repo.IRepo;

namespace PeekStudy.API.Repo
{
    public class QuizRepo : IQuizRepo
    {
        private readonly AppDbContext db;
        public QuizRepo(AppDbContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(Quiz quiz)
        {
          await db.Quizzes.AddAsync(quiz);
        }

        public async Task DeleteAsync(int id)
        {
            var d = await db.Quizzes.FindAsync(id);
            if (d != null)
            {
                db.Quizzes.Remove(d);
            }
        }

        public async Task<List<Quiz>> GetAllAsync()
        {
            return await db.Quizzes.ToListAsync();
        }

        public async Task<Quiz?> GetByIdAsync(int id)
        {
          return  await db.Quizzes.FindAsync(id);
        }

        public async Task<List<Quiz>> GetByLessonIdAsync(int LessonId)
        {
            return await db.Quizzes.Where(e => e.LessonId == LessonId).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public Task UpdateAsync(Quiz quiz)
        {
            db.Quizzes.Update(quiz);
            return Task.CompletedTask;
        }
    }
}
