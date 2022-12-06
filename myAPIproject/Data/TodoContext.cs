
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace myAPIproject.Data
{
    public partial class TodoContext : DbContext
    {
        public TodoContext()
        {

        }
        
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {

        }
        public virtual DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.name).HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
