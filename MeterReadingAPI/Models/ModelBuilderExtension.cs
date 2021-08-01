using MeterReadingAPI.Common;
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

            using var stream = assembly.GetManifestResourceStream(resourceName);

            if (stream == null)
                return;

            var accounts = CsvHelper.ReadAccounts(stream);

            modelBuilder.Entity<Account>().HasData(accounts);

            }
            catch (Exception)
            {
                //TOD0 : Need to do proper logging in DB
            }
        }
    }
}
