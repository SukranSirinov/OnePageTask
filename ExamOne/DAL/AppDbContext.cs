using ExamOne.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamOne.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Slider> sliders { get; set; }
        public DbSet<WhyChoose> whychooses { get; set; }
    }
}
