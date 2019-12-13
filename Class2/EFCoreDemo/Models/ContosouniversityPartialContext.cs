using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EFCoreDemo.Models
{
    public partial class ContosouniversityContext
    {
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var courseEntries = ChangeTracker.Entries<Course>();
            foreach (var courseEntry in courseEntries)
            {
                if (courseEntry.State == EntityState.Deleted)
                {
                    courseEntry.Property(p => p.IsDelete).CurrentValue = true;
                    courseEntry.State = EntityState.Modified;
                }
            }

            var departmentEntries = ChangeTracker.Entries<Department>();
            foreach (var departmentEntrie in departmentEntries)
            {
                if (departmentEntrie.State == EntityState.Deleted)
                {
                    departmentEntrie.Property(p => p.IsDelete).CurrentValue = true;
                    departmentEntrie.State = EntityState.Modified;
                }
            }

            var personEntries = ChangeTracker.Entries<Person>();
            foreach (var personEntrie in personEntries)
            {
                if (personEntrie.State == EntityState.Deleted)
                {
                    personEntrie.Property(p => p.IsDelete).CurrentValue = true;
                    personEntrie.State = EntityState.Modified;
                }
            }

            return (await base.SaveChangesAsync(true, cancellationToken));
        }

        public override int SaveChanges()
        {
            var courseEntries = ChangeTracker.Entries<Course>();
            foreach (var courseEntry in courseEntries)
            {
                if (courseEntry.State == EntityState.Deleted)
                {
                    courseEntry.Property(p => p.IsDelete).CurrentValue = true;
                    courseEntry.State = EntityState.Modified;
                }
            }

            var departmentEntries = ChangeTracker.Entries<Department>();
            foreach (var departmentEntrie in departmentEntries)
            {
                if (departmentEntrie.State == EntityState.Deleted)
                {
                    departmentEntrie.Property(p => p.IsDelete).CurrentValue = true;
                    departmentEntrie.State = EntityState.Modified;
                }
            }

            var personEntries = ChangeTracker.Entries<Person>();
            foreach (var personEntrie in personEntries)
            {
                if (personEntrie.State == EntityState.Deleted)
                {
                    personEntrie.Property(p => p.IsDelete).CurrentValue = true;
                    personEntrie.State = EntityState.Modified;
                }
            }

            return base.SaveChanges();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasQueryFilter(c => !c.IsDelete);
            modelBuilder.Entity<Department>().HasQueryFilter(d => !d.IsDelete);
            modelBuilder.Entity<Person>().HasQueryFilter(p => !p.IsDelete);
        }
    }
}
