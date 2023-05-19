using Microsoft.EntityFrameworkCore;
using RestAPI.Context;
using RestAPI.Contracts;
using RestAPI.Model;
using RestAPI.Repository;

namespace RestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // add dbcontext to database server
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<BookingManagementContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IUniversityRepository<University>, UniversityRepository>();
            builder.Services.AddScoped<IUniversityRepository<Room>, RoomRepository>();
            builder.Services.AddScoped<IUniversityRepository<Role>, RoleRepository>();
            builder.Services.AddScoped<IUniversityRepository<Employee>, EmployeeRepository>();
            builder.Services.AddScoped<IUniversityRepository<Education>, EducationRepository>();
            builder.Services.AddScoped<IUniversityRepository<Booking>, BookingRepository>();
            builder.Services.AddScoped<IUniversityRepository<AccountRole>, AccountRoleRepository>();
            builder.Services.AddScoped<IUniversityRepository<Account>, AccountRepository>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}