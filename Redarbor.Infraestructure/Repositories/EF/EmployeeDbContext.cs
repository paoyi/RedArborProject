using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Redarbor.Infraestructure.Repositories.EF
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<EmployeeEntity> Employees { get; set; } = default!;

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeEntity>().ToTable("Employee")
                .Property(c => c.Id)
                .HasColumnName("id");
            modelBuilder.Entity<EmployeeEntity>()
                .Property(c => c.Name)
                .HasColumnName("name").IsRequired().HasMaxLength(200);
            modelBuilder.Entity<EmployeeEntity>()
               .Property(c => c.CompanyId)
               .HasColumnName("companyId").IsRequired();
            modelBuilder.Entity<EmployeeEntity>()
               .Property(c => c.CreatedOn)
               .HasColumnName("createdOn");
            modelBuilder.Entity<EmployeeEntity>()
               .Property(c => c.DeletedOn)
               .HasColumnName("deletedOn");
            modelBuilder.Entity<EmployeeEntity>()
               .Property(c => c.Email)
               .HasColumnName("email").HasMaxLength(100);
            modelBuilder.Entity<EmployeeEntity>()
               .Property(c => c.Fax)
               .HasColumnName("fax");
            modelBuilder.Entity<EmployeeEntity>()
               .Property(c => c.LastLogin)
               .HasColumnName("lastLogin");
            modelBuilder.Entity<EmployeeEntity>()
               .Property(c => c.Password)
               .HasColumnName("password").IsRequired().HasMaxLength(100);
            modelBuilder.Entity<EmployeeEntity>()
               .Property(c => c.PortalId)
               .HasColumnName("portalId").IsRequired();
            modelBuilder.Entity<EmployeeEntity>()
               .Property(c => c.RoleId)
               .HasColumnName("roleId").IsRequired();
            modelBuilder.Entity<EmployeeEntity>()
              .Property(c => c.StatusId)
              .HasColumnName("statusId");
            modelBuilder.Entity<EmployeeEntity>()
             .Property(c => c.UpdatedOn)
             .HasColumnName("updatedOn");
            modelBuilder.Entity<EmployeeEntity>()
            .Property(c => c.Username)
            .HasColumnName("username").IsRequired().HasMaxLength(100);
            modelBuilder.Entity<EmployeeEntity>().HasKey(vf => new { vf.Id });

            modelBuilder.Entity<EmployeeEntity>()
            .HasData(
                 new EmployeeEntity
                 {
                     Id = 1,
                     CompanyId = 1,
                     CreatedOn = DateTime.Now,
                     Email = "admin@gmail.com",
                     Name = "admin",
                     Password = "admin",
                     PortalId = 1,
                     RoleId = 1,
                     StatusId = 1,
                     Telephone = "3202993039",
                     Username = "admin@gmail.com",
                 }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}