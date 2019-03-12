using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Task3.Models;

namespace Task3.DAL
{
    public class UserInitializer : DropCreateDatabaseAlways<UserContext>
    {
        protected override void Seed(UserContext context)
        {
            var users = new List<User>
            {
                new User{FirstName = "Alex", LastName = "TestLastName", Age = 22},
                new User{FirstName = "Carson", LastName = "Alonso", Age = 15},
                new User{FirstName = "Meredith", LastName = "Baradzin", Age = 43},
                new User{FirstName = "Arturo", LastName = "Barzdukas", Age = 19},
                new User{FirstName = "Gytis", LastName = "Li", Age = 28},
                new User{FirstName = "Peggy", LastName = "Justice", Age = 23},
                new User{FirstName = "Laura", LastName = "Olivetto", Age = 29},
            };

            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
        }
    }
}