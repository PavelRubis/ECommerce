using ECommerce.Core.Aggregates;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.DAL;
using ECommerce.DAL.DTOs;
using ECommerce.DAL.Repositories;
using ECommerce.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            builder.Services
                .AddControllers()
                .AddNewtonsoftJson(); ;
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ECommerceDbContext>
            (
                options =>
                {
                    options.UseNpgsql(config.GetConnectionString(nameof(ECommerceDbContext)));
                }
            );
            builder.Services.AddScoped<IOrdersRepository, OrdersRepository>();
            builder.Services.AddScoped<ICRUDRepository<Item>, ItemsRepository>();
            builder.Services.AddScoped<ICRUDRepository<Customer>, CustomersRepository>();
            builder.Services.AddScoped<AccountsRepository, AccountsRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.MapControllers();
            app.Run();
        }
    }
}
