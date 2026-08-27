using Microsoft.EntityFrameworkCore;
using PeekStudy.API.Data;
using PeekStudy.API.Models;
using PeekStudy.API.Repo.IRepo;

namespace PeekStudy.API.Repo
{
    public class QuestionRepo : IQuestionRepo
    {
        private readonly AppDbContext db;
        public QuestionRepo(AppDbContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(Question question)
        {
            await db.Questions.AddAsync(question);
        }

        public async Task DeleteAsync(int id)
        {
            var d = await db.Questions.FindAsync(id);
            if (d != null)
            {
                db.Questions.Remove(d);
            }
        }

        public async Task<List<Question>> GetAllAsync()
        {
            return await db.Questions.ToListAsync();
        }

        public async Task<Question?> GetByIdAsync(int id)
        {
            return await db.Questions.FindAsync(id);
        }

        public async Task<List<Question>> GetByQuizIdAsync(int quizId)
        {
            return await db.Questions.Where(e => e.QuizId == quizId).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public Task UpdateAsync(Question question)
        {
            db.Questions.Update(question);
            return Task.CompletedTask;
        }
    }
}
