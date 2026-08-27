using Microsoft.EntityFrameworkCore;
using PeekStudy.API.Data;
using PeekStudy.API.Models;
using PeekStudy.API.Repo.IRepo;

namespace PeekStudy.API.Repo
{
    public class ExamRepo : IExamRepo
    {
        private readonly AppDbContext db;
        public ExamRepo(AppDbContext db)
        {
            this.db= db;
        }
        public async Task AddAsync(Exam exam)
        {
            await db.Exams.AddAsync(exam);
                }

        public async Task DeleteAsync(int id)
        {
            var d = await db.Exams.FindAsync(id);
            if (d != null)
            {
                db.Remove(d);
            }
        }

        public async Task<List<Exam>> GetAllAsync()
        {
            return await db.Exams.ToListAsync();
        }

        public async Task<Exam?> GetByIdAsync(int id)
        {
            return await db.Exams.FindAsync(id);
        }

        public async Task<List<Exam>> GetBySubjectIdAsync(int SubjectId)
        {
            return await db.Exams.Where(e => e.SubjectId == SubjectId).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public Task UpdateAsync(Exam exam)
        {
            db.Exams.Update(exam);
            return Task.CompletedTask;
        }
    }
}
