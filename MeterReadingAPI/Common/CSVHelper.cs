using MeterReadingAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MeterReadingAPI.Common
{
    public static class CsvHelper
    {
        public static List<Account> ReadAccounts(Stream csvFileContent)
        {
            var accounts = new List<Account>();
            using var streamReader = new StreamReader(csvFileContent, Encoding.UTF8);

            _ = streamReader.ReadLine();

            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] stItems = line.Split(',');
                accounts.Add(new Account
                {
                    AccountId = int.Parse(stItems[0]),
                    FirstName = stItems[1],
                    LastName = stItems[2]
                });
            }

            return accounts;
        }
    }
}
