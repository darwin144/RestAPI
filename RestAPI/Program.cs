using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestAPI.Context;
using RestAPI.Contracts;
using RestAPI.Controllers;
using RestAPI.Model;
using RestAPI.Repository;
using RestAPI.Utility;
using RestAPI.ViewModels.Accounts;
using RestAPI.ViewModels.Universities;

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

            builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEducationRepository, EducationRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();


            builder.Services.AddSingleton(typeof(IMapper<,>),typeof(Mapper<,>));


            // add service email to the container
            builder.Services.AddTransient<IEmailService, EmailService>(_ => new EmailService(
                smtpServer : builder.Configuration["Email : SmtpServer"],
                smtpPort : int.Parse(builder.Configuration["Email : SmtpPort"]),
                fromEmailAddress : builder.Configuration["Email : FromEmailAddress"]                
                ));



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