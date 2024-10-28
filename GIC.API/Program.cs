using GICApp.ApplicationCore.Application.Repositories;
using GICApp.ApplicationCore.Application.Services;
using GICApp.Infrastructure.Persistence;
using GICApp.Infrastructure.Persistence.Repositories;
using GICApp.Infrastructure.Persistence.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace GIC.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container. 
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connectionstring")));
             
            builder.Services.AddScoped<IBankAccountRepository, BankAccountRepository>(); 
            builder.Services.AddScoped<IBankAccountService, BankAccountService>();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
            builder.Services.AddControllers();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
             
            app.UseHttpsRedirection();

              app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        
    }
}