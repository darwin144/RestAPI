using Microsoft.EntityFrameworkCore;
using RestAPI.Model;
using RestAPI.Utility;
using System.Collections.Generic;

namespace RestAPI.Context
{
    public class BookingManagementContext : DbContext
    {
        public BookingManagementContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<University> Universities { get; set; }

        // menambahkan update unique key
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // set tabel role agar data tidak berubah saat migrasi
            builder.Entity<Role>().HasData(new Role
            {
                Guid = Guid.Parse("f147a695-1a4f-4960-bffc-08db60bf618f"),
                Name = nameof(RoleLevel.User),
                CreatedDate =DateTime.Now,
                ModifiedDate = DateTime.Now
            },
              new Role
              {
                  Guid = Guid.Parse("c22a20c5-0149-41fd-bffd-08db60bf618f"),
                  Name = nameof(RoleLevel.Manager),
                  CreatedDate = DateTime.Now,
                  ModifiedDate = DateTime.Now
              },
              new Role
              {
                  Guid = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                  Name = nameof(RoleLevel.Admin),
                  CreatedDate = DateTime.Now,
                  ModifiedDate = DateTime.Now
              }
            );


            // set up unique on the class
            builder.Entity<Employee>().HasIndex(e => new { e.NIK, e.Email, e.PhoneNumber }).IsUnique();

            // set foreign table
            builder.Entity<Education>().HasOne(u => u.University).WithMany(e => e.Educations)
                .HasForeignKey(e => e.UniversityGuid);

            builder.Entity<Education>().HasOne(e => e.Employee).WithOne(e => e.Education)
                .HasForeignKey<Education>(e => e.Guid);

            builder.Entity<Account>().HasOne(e => e.Employee).WithOne(e => e.Account)
                .HasForeignKey<Account>(e => e.Guid);

            builder.Entity<Account>().HasMany(e => e.Accountroles).WithOne(e => e.Account)
                .HasForeignKey(e => e.AccountGuid);

            builder.Entity<AccountRole>().HasOne(e => e.Role).WithMany(e => e.AccountRoles)
                .HasForeignKey(e => e.RoleGuid);

            builder.Entity<Room>().HasMany(e => e.Bookings).WithOne(e => e.Room).
                HasForeignKey(e => e.RoomGuid);

            builder.Entity<Booking>().HasOne(e => e.Employee).WithMany(e => e.Bookings).
                HasForeignKey(e => e.EmployeeGuid);

        }
    }
}
