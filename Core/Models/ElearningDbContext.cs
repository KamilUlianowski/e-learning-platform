using System.Data.Entity;

namespace E_LearningWeb.Models
{
    public class ElearningDbContext : DbContext
    {
        public ElearningDbContext() : base("MDalConnectionCloud")
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}