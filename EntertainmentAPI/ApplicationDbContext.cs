using EntertainmentAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntertainmentAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
        : base(options)
        {
        }

        public DbSet<User> Users { set; get; }
        public DbSet<Topic> Topics { set; get; }
        public DbSet<Question> Questions { set; get; }
        public DbSet<Answer> Answers { set; get; }
        public DbSet<Quiz> Quizs { set; get; }
        public DbSet<QuizQuestion> QuizQuestions { set; get; }
        public DbSet<QuizQuestionUser> QuizQuestionUsers { set; get; }
        public DbSet<QuizAnswerUser> QuizAnswerUsers { set; get; }


        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
