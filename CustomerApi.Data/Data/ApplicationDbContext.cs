using CustomerApi.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CustomerApi.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
        
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedCustomerData(modelBuilder);
            SeedRolesAndAdminUser(modelBuilder);

        }
        private void SeedCustomerData(ModelBuilder modelBuilder)
        {
            string jsonString = File.ReadAllText("../CustomerApi.Data/Data/UserData.json");
            var customers = JsonSerializer.Deserialize<List<Customer>>(jsonString);
            if (customers != null)
            {
                int customerId = 1;
                foreach (var customer in customers)
                {
                    modelBuilder.Entity<Customer>().HasData(
                        new
                        {
                            CustomerId = customerId,
                            _Id = customer._Id,
                            Index = customer.Index,
                            Age = customer.Age,
                            EyeColor = customer.EyeColor,
                            Name = customer.Name,
                            Gender = customer.Gender,
                            Company = customer.Company,
                            Email = customer.Email,
                            Phone = customer.Phone,
                            About = customer.About,
                            Registered = customer.Registered,
                            Latitude = customer.Latitude,
                            Longitude = customer.Longitude,
                            Tags = customer.Tags
                        }
                        );

                    modelBuilder.Entity<Address>().HasData(new
                    {
                        AddressId = customerId,
                        Number = customer.Address.Number,
                        Street = customer.Address.Street,
                        City = customer.Address.City,
                        State = customer.Address.State,
                        Zipcode = customer.Address.Zipcode,
                    });

                    customerId++;
                }
            }
        }

        private void SeedRolesAndAdminUser(ModelBuilder modelBuilder)
        {
            // Seed roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
            );

            // Seed admin user
            var adminUser = new IdentityUser
            {
                Id = "1",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123");

            modelBuilder.Entity<IdentityUser>().HasData(adminUser);

            // Assign admin role to admin user~
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminUser.Id, RoleId = "1" }
            );
            // Seed regular user
            var regularUser = new IdentityUser
            {
                Id = "2",
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@gmail.com",
                NormalizedEmail = "USER@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var regularUserPasswordHasher = new PasswordHasher<IdentityUser>();
            regularUser.PasswordHash = regularUserPasswordHasher.HashPassword(regularUser, "User@123");

            modelBuilder.Entity<IdentityUser>().HasData(regularUser);

            // Assign user role to  regular user
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = regularUser.Id, RoleId = "2" }
            );
        }
        
    }
}
