using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreExample
{
    public class OrganizationContext : DbContext
    {
        //static LoggerFactory object
        public static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder
                .UseSqlServer(@"Server=(local);Database=Organization;Trusted_Connection=True")
                .UseLoggerFactory(_loggerFactory);

        }
    }
}
