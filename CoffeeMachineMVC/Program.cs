using CoffeeMachineMVC.Clients;

namespace CoffeeMachineMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var coffeeApiBaseUrl = builder.Configuration["CoffeeMachineApi:BaseUrl"];

            builder.Services.AddHttpClient<ICoffeeMachineClient, CoffeeMachineClient>(client =>
            {
                client.BaseAddress = new Uri(coffeeApiBaseUrl);
            });

            builder.Services.AddControllersWithViews();

            builder.Services.Configure<ApiSettings>(builder.Configuration);



            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=CoffeeMachine}/{action=Index}/{id?}"
            );

            app.Run();
        }
    }
}
