using Microsoft.EntityFrameworkCore;

namespace Student_Web_API.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {

        }
        public DbSet<Model.Student> Students { get; set; }
    }
}
