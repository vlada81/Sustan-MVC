using Sustan.Models;
using Sustan.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace Sustan.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserFirstName(this IIdentity identity)
        {
            var db = ApplicationDbContext.Create();
            var user = db.Users.FirstOrDefault(u => u.UserName.Equals(identity.Name));

            if (user == null)
            {
                return String.Empty;
            }

            return user.FirstName;
        }

        public static async Task GetUsers(this List<UserViewModel> users)
        {
            var db = ApplicationDbContext.Create();

            users.AddRange(await (from u in db.Users
                                  select new UserViewModel
                                  {
                                      Id = u.Id,
                                      Email = u.Email,
                                      FirstName = u.FirstName,
                                      LastName = u.LastName,
                                      RegistrationDate = u.RegistrationDate
                                  }).OrderBy(o => o.Email).ToListAsync());
        }
    }
}