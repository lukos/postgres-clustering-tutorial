using Microsoft.EntityFrameworkCore;
using PostgresTester.Database;

namespace PostgresTester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<GeneralContext>(options =>
            {
                //options.UseNpgsql("host=192.168.56.211;port=5000;database=books;user id=books;password=bookspassword;timeout=3;");    // Proxy
                //options.UseNpgsql("host=192.168.56.201,192.168.56.202,192.168.56.203;port=5432;database=books;user id=books;password=bookspassword;timeout=3;Target Session Attributes=primary");      // Using multiple hosts
                options.UseNpgsql("host=192.168.56.201;port=5432;database=books;user id=books;password=bookspassword;timeout=3;");      // Direct to postgres1
            });
            builder.Services.AddDbContext<ReadonlyContext>(options =>
            {
                //options.UseNpgsql("host=192.168.56.211;port=5001;database=books;user id=books;password=bookspassword;timeout=3;");    // Proxy
                //options.UseNpgsql("host=192.168.56.202,192.168.56.203,192.168.56.201;port=5432;database=books;user id=books;password=bookspassword;timeout=3;Target Session Attributes=prefer-standby");      // Using multiple hosts
                options.UseNpgsql("host=192.168.56.201;port=5432;database=books;user id=books;password=bookspassword;timeout=3;");      // Direct to postgres1
            });
            builder.Services.AddDbContext<Postgres1Context>(options =>
            {
                options.UseNpgsql("host=192.168.56.201;port=5432;database=books;user id=books;password=bookspassword;timeout=3");
            });
            builder.Services.AddDbContext<Postgres2Context>(options =>
            {
                options.UseNpgsql("host=192.168.56.202;port=5432;database=books;user id=books;password=bookspassword;timeout=3");
            });
            builder.Services.AddDbContext<Postgres3Context>(options =>
            {
                options.UseNpgsql("host=192.168.56.203;port=5432;database=books;user id=books;password=bookspassword;timeout=3");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}