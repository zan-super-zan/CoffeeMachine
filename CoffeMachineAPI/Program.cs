
using CoffeeMachineModel;
using CoffeMachineAPI.Data;
using CoffeMachineAPI.Mappings;
using CoffeMachineAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace CoffeMachineAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var coffeeRecipes = new List<CoffeeRecipe>();
            builder.Configuration.GetSection("CoffeeRecipes").Bind(coffeeRecipes);

            builder.Services.AddSingleton(coffeeRecipes);

            builder.Services.AddScoped<ICoffeeMachineService, CoffeeMachineService>();

            builder.Services.AddControllers();

            builder.Services.AddDbContext<CoffeeMachineContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddAuthorization();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CoffeeMachineContext>();
                dbContext.Database.Migrate();
            }

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
