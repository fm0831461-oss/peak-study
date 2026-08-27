using Microsoft.EntityFrameworkCore;
using PeekStudy.API.Models;

namespace PeekStudy.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Main Entities
        public DbSet<User> Users { get; set; }
        public DbSet<StudyPlan> StudyPlans { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        // Study Content
        public DbSet<StudySource> StudySources { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

        // Study Schedule
        public DbSet<StudyTask> StudyTasks { get; set; }
        public DbSet<FocusSession> FocusSessions { get; set; }

        // Exams & Quizzes
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }

        // Quiz Attempts
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }

        // Gamification
        public DbSet<Island> Islands { get; set; }
        public DbSet<IslandItem> IslandItems { get; set; }

        // COPILOT CHANGE: Added StudySourceFile DbSet to track uploaded file metadata.
        public DbSet<StudySourceFile> StudySourceFiles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);




            modelBuilder.Entity<Subject>()
                .HasOne(sp => sp.User)
                .WithMany(u => u.Subjects)
                .HasForeignKey(sp => sp.UserId)
           .OnDelete(DeleteBehavior.NoAction);









            // ==========================================
            // StudyPlan 1 ─── 1 Subject
            // ==========================================

            modelBuilder.Entity<StudyPlan>()
                .HasOne(sp => sp.Subject)
                .WithOne(s => s.StudyPlan)
                .HasForeignKey<StudyPlan>(sp => sp.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);


            // ==========================================
            // Subject 1 ─── * StudySource
            // ==========================================

            modelBuilder.Entity<StudySource>()
                .HasOne(ss => ss.Subject)
                .WithMany(s => s.StudySources)
                .HasForeignKey(ss => ss.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);


            // ==========================================
            // StudySource 1 ─── * Unit
            // ==========================================

            modelBuilder.Entity<Unit>()
                .HasOne(u => u.StudySource)
                .WithMany(ss => ss.Units)
                .HasForeignKey(u => u.StudySourceId)
                .OnDelete(DeleteBehavior.Cascade);


            // ==========================================
            // Unit 1 ─── * Lesson
            // ==========================================

            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Unit)
                .WithMany(u => u.Lessons)
                .HasForeignKey(l => l.UnitId)
                .OnDelete(DeleteBehavior.Cascade);


            // ==========================================
            // Lesson 1 ─── * StudyTask
            // ==========================================

            modelBuilder.Entity<StudyTask>()
                .HasOne(st => st.Lesson)
                .WithMany(l => l.StudyTasks)
                .HasForeignKey(st => st.LessonId)
                .OnDelete(DeleteBehavior.Cascade);


            // ==========================================
            // StudyPlan 1 ─── * StudyTask
            // ==========================================

            modelBuilder.Entity<StudyTask>()
                .HasOne(st => st.StudyPlan)
                .WithMany(sp => sp.StudyTasks)
                .HasForeignKey(st => st.StudyPlanId)
                .OnDelete(DeleteBehavior.NoAction);


            // ==========================================
            // Lesson 1 ─── * Quiz
            // ==========================================

            modelBuilder.Entity<Quiz>()
                .HasOne(q => q.Lesson)
                .WithMany(l => l.Quizzes)
                .HasForeignKey(q => q.LessonId)
                .OnDelete(DeleteBehavior.Cascade);


            // ==========================================
            // Quiz 1 ─── * Question
            // ==========================================

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Quiz)
                .WithMany(qz => qz.Questions)
                .HasForeignKey(q => q.QuizId)
                .OnDelete(DeleteBehavior.NoAction);


            // ==========================================
            // Exam 1 ─── * Question
            // ==========================================

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Exam)
                .WithMany(e => e.Questions)
                .HasForeignKey(q => q.ExamId)
                .OnDelete(DeleteBehavior.NoAction);


            // ==========================================
            // Lesson 1 ─── * Question
            // ==========================================

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Lesson)
                .WithMany(l => l.Questions)
                .HasForeignKey(q => q.LessonId)
                .OnDelete(DeleteBehavior.NoAction);


            // ==========================================
            // Subject 1 ─── * Exam
            // ==========================================

            modelBuilder.Entity<Exam>()
                .HasOne(e => e.Subject)
                .WithMany(s => s.Exams)
                .HasForeignKey(e => e.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);


            // ==========================================
            // Question 1 ─── * Option
            // ==========================================

            modelBuilder.Entity<Option>()
                .HasOne(o => o.Question)
                .WithMany(q => q.Options)
                .HasForeignKey(o => o.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);


            // ==========================================
            // Quiz 1 ─── * QuizAttempt
            // ==========================================

            modelBuilder.Entity<QuizAttempt>()
                .HasOne(qa => qa.Quiz)
                .WithMany(q => q.QuizAttempts)
                .HasForeignKey(qa => qa.QuizId)
                .OnDelete(DeleteBehavior.Cascade);


            // ==========================================
            // QuizAttempt 1 ─── * QuizAnswer
            // ==========================================

            modelBuilder.Entity<QuizAnswer>()
                .HasOne(qa => qa.QuizAttempt)
                .WithMany(qat => qat.QuizAnswers)
                .HasForeignKey(qa => qa.QuizAttemptId)
                .OnDelete(DeleteBehavior.Cascade);


            // ==========================================
            // Question 1 ─── * QuizAnswer
            // ==========================================

            modelBuilder.Entity<QuizAnswer>()
                .HasOne(qa => qa.Question)
                .WithMany(q => q.QuizAnswers)
                .HasForeignKey(qa => qa.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);


            // ==========================================
            // Option 1 ─── * QuizAnswer
            // ==========================================

            modelBuilder.Entity<QuizAnswer>()
                .HasOne(qa => qa.Option)
                .WithMany(o => o.QuizAnswers)
                .HasForeignKey(qa => qa.OptionId)
                .OnDelete(DeleteBehavior.NoAction);


            // ==========================================
            // StudyTask 1 ─── * FocusSession
            // ==========================================

            modelBuilder.Entity<FocusSession>()
                .HasOne(fs => fs.StudyTask)
                .WithMany(st => st.FocusSessions)
                .HasForeignKey(fs => fs.StudyTaskId)
                .OnDelete(DeleteBehavior.Cascade);


            // ==========================================
            // User 1 ─── 1 Island
            // ==========================================

            modelBuilder.Entity<Island>()
                .HasOne(i => i.User)
                .WithOne(u => u.Island)
                .HasForeignKey<Island>(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            // ==========================================
            // Island 1 ─── * IslandItem
            // ==========================================

            modelBuilder.Entity<IslandItem>()
                .HasOne(ii => ii.Island)
                .WithMany(i => i.IslandItems)
                .HasForeignKey(ii => ii.IslandId)
                .OnDelete(DeleteBehavior.Cascade);


            // ==========================================
            // FocusSession 1 ─── 1 IslandItem
            // ==========================================

            modelBuilder.Entity<IslandItem>()
                .HasOne(ii => ii.FocusSession)
                .WithOne(fs => fs.IslandItem)
                .HasForeignKey<IslandItem>(ii => ii.FocusSessionId)
                .OnDelete(DeleteBehavior.Cascade);

            // COPILOT CHANGE: Configure StudySource 1 -> * StudySourceFile with cascade delete.
            modelBuilder.Entity<StudySourceFile>()
                .HasOne(f => f.StudySource)
                .WithMany(ss => ss.Files)
                .HasForeignKey(f => f.StudySourceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}