using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.DAL.Entities.Identity;

namespace Talabat.DAL.Identity
{
    public class AppIdentityDbContextSeeding
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName ="Ahmed Mohamed",
                    UserName ="AhmedMohamed24",
                    Email ="aahmedfarhattt@gmail.com",
                    PhoneNumber ="01018451083",
                    Address = new Address()
                    {
                        FirstName ="Ahmed",
                        SecondName ="Mohamed",
                        Country ="Egypt",
                        City =  "El-Sinbellawein",
                        Street ="El mo3hda"
                    }
                };
                await userManager.CreateAsync(user, "P@ssw0rd");
            }
            
        }
    }
}
