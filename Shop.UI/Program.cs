using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shop.Database;

namespace Shop.UI
{
    public class Program
    {
        /// <summary>
        /// Application entry point
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //start setting up the host
            var host = CreateWebHostBuilder(args).Build();
            
            try
            {
                //using host and then disposing memory thereof
                //Using dependancy injection to CreateScope
                using (var scope = host.Services.CreateScope())
                {
                    //getting the ApplicationDbContext service
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    //using UserManager to enable generic methods for Auth
                    var userManger = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                    //Ensure the database has been created, returns bool
                    context.Database.EnsureCreated();

                    //check if any users have been created
                    if (!context.Users.Any())
                    {
                        //creating an an admin user
                        var adminUser = new IdentityUser()
                        {
                            UserName = "Admin"
                        };

                        //creating a manager user
                        var managerUser = new IdentityUser()
                        {
                            UserName = "Manager"
                        };

                        var customerUser = new IdentityUser()
                        {
                            UserName = "Customer"
                        };

                        //creating the users in the backend with given passwords
                        //using GetAwaiter as CreateAsync requires a wait (async) before getting the result
                        userManger.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
                        userManger.CreateAsync(managerUser, "password").GetAwaiter().GetResult();
                        userManger.CreateAsync(customerUser, "password").GetAwaiter().GetResult();

                        //using Claims to describe what the user is able to do
                        var adminClaim = new Claim("Role", "Admin");
                        var managerClaim = new Claim("Role", "Manager");
                        var customerClaim = new Claim("Role", "Customer");

                        //adding the specific claims to each user
                        userManger.AddClaimAsync(adminUser, adminClaim).GetAwaiter().GetResult();
                        userManger.AddClaimAsync(managerUser, managerClaim).GetAwaiter().GetResult();
                        userManger.AddClaimAsync(customerUser, customerClaim).GetAwaiter().GetResult();
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
