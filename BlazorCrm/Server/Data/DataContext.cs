using BlazorCrm.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrm.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.
                UseSqlServer("Server=.\\SQLExpress;Database=BlazorCrmDb1;Trusted_Connection=true;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "Nikos",
                    LastName = "Bezos",
                    Email = "Nikos.Bezos@gmail.com",
                    Phone = "232045553",
                    DateOfBirth = new DateTime(2001, 2, 3),
                    NickName = "nickbez",
                    Place = "Trikala",
                    Salary = 3000
                },
                 new Employee
                 {
                     Id = 2,
                     FirstName = "Nikos",
                     LastName = "Bezos",
                     Email = "Nikos.Bezos@gmail.com",
                     Phone = "232045553",
                     DateOfBirth = new DateTime(2001, 2, 3),
                     NickName = "nickbez",
                     Place = "Trikala",
                     Salary = 3000
                 }
                 );

            modelBuilder.Entity<Note>().HasData(
               new Note { Id = 1, EmployeeId = 1, Text = "1st employee" },
               new Note { Id = 2, EmployeeId = 2, Text = "2st employee" }
              );
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }


    }
}
