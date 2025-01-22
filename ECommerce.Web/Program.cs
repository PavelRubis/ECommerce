using ECommerce.Application.Interfaces;
using ECommerce.Application.RepositoryInterfaces;
using ECommerce.Application.Services;
using ECommerce.Core.Aggregates;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.ServiceInterfaces;
using ECommerce.DAL;
using ECommerce.DAL.Repositories;
using ECommerce.DAL.UnitOfWork;
using ECommerce.Web.Extensions;
using ECommerce.Web.Infrastructure.Auth;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;

namespace Web
{
    public class Program
    {
        public const string MANAGER_ROLE = "Manager";
        public const string CUSTOMER_ROLE = "Customer";
        public const string AUTH_REQUIREMENT = "AUTH_REQUIREMENT";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var config = builder.Configuration;

            services.Configure<JwtOptions>(config.GetSection(nameof(JwtOptions)));
            services.AddJwtAuthentication(config);

            services
                .AddControllers()
                .AddNewtonsoftJson();

            services.AddSwaggerGen();
            
            services.AddDbContext<ECommerceDbContext>
            (
                options =>
                {
                    options.UseNpgsql(config.GetConnectionString(nameof(ECommerceDbContext)));
                }
            );

            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddScoped<ICRUDRepository<Item>, ItemsRepository>();
            services.AddScoped<ICustomerRepository, CustomersRepository>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddScoped<IAccountsService, AccountsService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddAutoMapper(typeof(MappingProfile));

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.MapControllers();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.SameAsRequest,
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
