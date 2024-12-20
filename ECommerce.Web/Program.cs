using ECommerce.DAL;
using Microsoft.EntityFrameworkCore;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration;

            builder.Services.AddDbContext<ECommerceDbContext>
            (
                options =>
                {
                    options.UseNpgsql(config.GetConnectionString(nameof(ECommerceDbContext)));
                }
            );

            var app = builder.Build();
            app.Run();
        }
    }
}
