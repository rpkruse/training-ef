using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingEfBE.Models
{
    public partial class DataContext : DbContext
    {
        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<Post> Post { get; set; }

        public virtual DbSet<Room> Room { get; set; }

        public virtual DbSet<UserRoom> UserRoom { get; set; }

        public virtual DbSet<UpvotePost> UpvotePost { get; set; }

        public virtual DbSet<DownvotePost> DownvotePost { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(e =>
            {
                e.HasOne(u => u.User).WithMany(p => p.Posts).HasForeignKey(p => p.CreatedBy).HasConstraintName("fk_createdBy_userID");
                e.HasOne(c => c.Category).WithMany().HasForeignKey(p => p.CategoryID).HasConstraintName("fk_categoryID_categoryID");

            });

        }
    }
}
