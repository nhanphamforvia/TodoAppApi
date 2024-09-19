using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TodoAppApi.Entity;

namespace TodoAppApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
              new Category { Id = 1, Name = "Work" },
              new Category { Id = 2, Name = "Personal" }
            );

            modelBuilder.Entity<Todo>().HasData(
                new Todo { Id = 1, Title = "Task 1", IsCompleted = false, CategoryId = 1 },
                new Todo { Id = 2, Title = "Task 2", IsCompleted = true, CategoryId = 2 },
                new Todo { Id = 3, Title = "Task 3", IsCompleted = false, CategoryId = 1 }
            );
        }
    }
}