using Labb2LinQ2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Labb2LinQ2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Teaching> Teachings { get; set; }
    }
}
