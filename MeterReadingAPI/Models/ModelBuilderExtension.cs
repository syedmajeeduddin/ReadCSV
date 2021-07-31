using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingAPI.Models
{
    public static class ModelBuilderExtension
    {
        public static void  Seed(this ModelBuilder modelBuilder)
        {

            try
            {

                var resourceName = "MeterReadingAPI.SeedData.Test_Accounts.csv";
                var assembly = Assembly.GetExecutingAssembly();
                
                var accounts = new List<Account>();
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))

                using (StreamReader streamReader = new StreamReader(stream, Encoding.UTF8))
                {
                    string headerLine = streamReader.ReadLine();

                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] stItems = line.Split(',');
                        accounts.Add(new Account {  AccountId = int.Parse(stItems[0]), 
                                                    FirstName = stItems[1],
                                                    LastName = stItems[2] });
                    }
                }

                modelBuilder.Entity<Account>().HasData(accounts.ToArray<Account>());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
