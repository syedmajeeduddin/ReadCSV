namespace DomainEntity.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Ensek.Assignment.DomainEntity;

    internal sealed class Configuration : DbMigrationsConfiguration<Ensek.Assignment.DomainEntity.MeterReadDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Ensek.Assignment.DomainEntity.MeterReadDBContext context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            try
            {

                var resourceName = "DomainEntity.SeedData.Test_Accounts.csv";
                var assembly = Assembly.GetExecutingAssembly();
                //var stream = assembly.GetManifestResourceStream(resourceName);

                var accounts = new List<Account>();
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))

                using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                {
                    string headerLine = streamReader.ReadLine();

                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] stItems = line.Split(',');
                        accounts.Add(new Account { AccountId = int.Parse(stItems[0]), FirstName = stItems[1], LastName = stItems[2] });
                    }
                }

                context.Accounts.AddOrUpdate( accounts.ToArray<Account>());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
