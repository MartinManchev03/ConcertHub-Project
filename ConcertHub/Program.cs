using ConcertHub.Data;
using ConcertHub.Data.Models;
using ConcertHub.Data.Repository;
using ConcertHub.Services.Data.Interfaces;
using ConcertHub.Services.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ConcertHub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.WithOrigins("https://example.com", "https://another.com")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddScoped<IRepository<Performer, Guid>, Repository<Performer, Guid>>();
            builder.Services.AddScoped<IRepository<Concert, Guid>, Repository<Concert, Guid>>();
            builder.Services.AddScoped<IRepository<FeedBack, Guid>, Repository<FeedBack, Guid>>();
            builder.Services.AddScoped<IRepository<Ticket, Guid>, Repository<Ticket, Guid>>();
            builder.Services.AddScoped<IMappingRepository<ConcertPerformer, string, Guid>, MappingRepository<ConcertPerformer, string, Guid>>();
            builder.Services.AddScoped<IRepository<TicketType, Guid>, Repository<TicketType, Guid>>();
            builder.Services.AddScoped<IMappingRepository<UserTicket, string, Guid>, MappingRepository<UserTicket, string, Guid>>();
            builder.Services.AddScoped<IRepository<Category, Guid>, Repository<Category, Guid>>();

            builder.Services.AddScoped<IPerformerService, PerformerService>();
            builder.Services.AddScoped<IFeedbackService, FeedbackService>();
            builder.Services.AddScoped<ITicketService, TicketService>();
            builder.Services.AddScoped<IUserTicketService, UserTicketService>();
            builder.Services.AddScoped<IConcertPerformerService, ConcertPerformerService>();
            builder.Services.AddScoped<IConcertService, ConcertService>();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("AllowSpecificOrigins");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "NotFound",
                pattern: "{*url}",
                defaults: new { controller = "Error", action = "Error404" });
            app.MapRazorPages();

            app.Run();
        }
    }
}
