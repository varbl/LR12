using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LR12
{

    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UserDB;Trusted_Connection=True;");
    }

    class Program
    {
        static void Main(string[] args)
        {

            using (var context = new UserContext())
            {
                context.Database.EnsureCreated();

                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { FirstName = "Іван", LastName = "Іванов", Age = 30 },
                        new User { FirstName = "Петро", LastName = "Петров", Age = 25 },
                        new User { FirstName = "Марія", LastName = "Сидорова", Age = 35 }
                    );
                    context.SaveChanges();
                }
            }

            // Витягуємо користувачів з бази даних та виводимо на консоль
            using (var context = new UserContext())
            {
                var users = context.Users.ToList();
                Console.WriteLine("Список користувачів:");
                foreach (var user in users)
                {
                    Console.WriteLine($"Ім'я: {user.FirstName}, Прізвище: {user.LastName}, Вік: {user.Age}");
                }
            }
        }
    }
}
