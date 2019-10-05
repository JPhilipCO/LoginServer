using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Logins.Models;
using Logins.Utils;

namespace Logins.Data
{
    public static class SeedData
    {
        public static void Initialize(UserContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User
                    {
                        Name = "Madison",
                        password = Cryptic.GetHash("Secret")
                    },
                    new User
                    {
                        Name = "Randy",
                        password = Cryptic.GetHash("Secret2")
                    }
                ); ; ; ;

                context.SaveChanges();
            }
        }
    }
}