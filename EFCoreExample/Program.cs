using System;
using System.Linq;
using System.Linq.Expressions;

namespace EFCoreExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new OrganizationContext();

            if (!context.Departments.Any())
                AddData(context);

            var x = 1;

            PredicateTest(context);

            ExpressionTest(context);

            GroupByTest(context);
        }

        private static void GroupByTest(OrganizationContext context)
        {
            var z = 1;

            var departments = context.Departments.Where(dept => context.Departments.GroupBy(dept => dept.ReferenceId).Select(dept => new { UpdateDate = dept.Max(d => d.UpdateDate) }).Any(dept_in => dept_in.UpdateDate == dept.UpdateDate));

            var x = departments.ToList();

            //foreach (var dept in departments.ToList())
            //{
            //    Console.WriteLine($"{dept.DepartmentId} | {dept.Name} | {dept.Description} | {dept.ReferenceId}");
            //}

            Console.ReadLine();
        }

        private static void ExpressionTest(OrganizationContext context)
        {
            Expression<Func<Department, bool>> expression = (department) => department.Name == "Name2002";

            var department = context.Departments.Where(expression).FirstOrDefault();

            Console.WriteLine(department.Description);

            Console.ReadLine();
        }

        private static void PredicateTest(OrganizationContext context)
        {
            Func<Department, bool> predicate = (department) => department.Name == "Name2001";

            var deparment = context.Departments.Where(predicate).FirstOrDefault();

            Console.WriteLine(deparment.Description);

            Console.ReadLine();
        }

        private static void AddData(OrganizationContext context)
        {
            var referenceId = 1001;

            for (int i = 1; i <= 1000000; i++)
            {
                var currentDate = DateTime.UtcNow.AddSeconds(1);

                // Create a new department record...
                var department = new Department
                {
                    Name = $"Name{referenceId}",
                    Description = $"Description{referenceId}",
                    ReferenceId = referenceId.ToString(),
                    CreateDate = currentDate,
                    UpdateDate = currentDate.AddSeconds(5)
                };

                // Every 5 records increase the reference Id...
                if (i % 5 == 0)
                    referenceId++;

                context.Departments.Add(department);

                // Save every 1000 records...
                if (i % 1000 == 0)
                    context.SaveChanges();
            }

            // Save any records left over...
            context.SaveChanges();
        }
    }
}
