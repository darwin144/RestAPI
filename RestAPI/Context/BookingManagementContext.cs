using Microsoft.EntityFrameworkCore;
using RestAPI.Model;
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
    }
}
