using HomeAway.Service;

namespace home_away
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // HotelService
            //builder.Services.AddHttpClient<HotelService>(client =>
            //{
            //    client.BaseAddress = new Uri("https://localhost:7184");
            //});
            builder.Services.AddHttpClient("HomeAwayAPI", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7184/api/");
            });

            // BookingService  ✔ تمت إضافته
            //builder.Services.AddHttpClient<BookingService>(client =>
            //{
            //    client.BaseAddress = new Uri("https://localhost:7184");
            //});

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}