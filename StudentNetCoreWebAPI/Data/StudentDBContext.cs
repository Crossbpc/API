using Microsoft.EntityFrameworkCore;
using StudentNetCoreWebAPI.Entities;

namespace StudentNetCoreWebAPI.Data
{
    public class StudentDBContext : DbContext
    {
        public StudentDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<StudentEntity> Students { get; set; }
    }
}
