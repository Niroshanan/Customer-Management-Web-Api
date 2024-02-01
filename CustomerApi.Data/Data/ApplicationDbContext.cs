using CustomerApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CustomerApi.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
    }
}
