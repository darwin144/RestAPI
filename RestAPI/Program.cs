using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestAPI.Context;
using RestAPI.Contracts;
using RestAPI.Controllers;
using RestAPI.Model;
using RestAPI.Repository;
using RestAPI.Utility;
using RestAPI.ViewModels.Accounts;
using RestAPI.ViewModels.Universities;
using System.Text;

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

            // add service token
            builder.Services.AddScoped<ITokenService, TokenService>();

            builder.Services.AddSingleton(typeof(IMapper<,>),typeof(Mapper<,>));


            // add service email to the container
            builder.Services.AddTransient<IEmailService, EmailService>(_ => new EmailService(
                smtpServer : builder.Configuration["Email:SmtpServer"],
                smtpPort : Convert.ToInt32(builder.Configuration["Email:SmtpPort"]),
                fromEmailAddress : builder.Configuration["Email:FromEmailAddress"]                
                ));


            // Add authentication to the container.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options => {
                       options.RequireHttpsMetadata = false;
                       options.SaveToken = true;
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateAudience = false,
                           ValidAudience = builder.Configuration["JWT:Audience"],
                           ValidateIssuer = false,
                           ValidIssuer = builder.Configuration["JWT:Issuer"],
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                           ValidateLifetime = true,
                           ClockSkew = TimeSpan.Zero
                       };
                   });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x => {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MCC 2023",
                    Description = "ASP.NET Core API 6.0"
                });
                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                     }
                });
            });

            // set up cors default
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin();
                    //policy.WithOrigins("http://localhost:3000", "https://localhost:3223");
                    policy.AllowAnyHeader();
                    //policy.WithHeaders("content-type", "authorization");
                    policy.AllowAnyMethod();
                    //policy.WithMethods("GET", "POST", "PUT", "DELETE");
                });

                /*options.AddPolicy("Tokopedia", policy => {
                    policy.WithOrigins("http://www.tokopedia.co.id");
                    policy.AllowAnyHeader();
                    policy.WithMethods("GET", "POST");
                });

                options.AddPolicy("GoPay", policy => {
                    policy.WithOrigins("http://www.tokopedia.co.id");
                    policy.AllowAnyHeader();
                    policy.WithMethods("PUT", "POST");
                });*/
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            // update add autentication
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors();

            app.MapControllers();

            app.Run();
        }
    }
}