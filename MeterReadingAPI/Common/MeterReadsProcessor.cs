using MeterReadingAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingAPI.Common
{
    public class MeterReadsProcessor
    {
        public static
             (List<MeterRead> meterReads,
             int successCount,
             int failedCount)
             ProcessFile(Stream fileStream)
        {
            using var streamReader = new StreamReader(fileStream, Encoding.UTF8);

            //ignore header
            _ = streamReader.ReadLine();

            var meterReads = new List<MeterRead>();

            string line;
            int failed = 0, success = 0;
            while ((line = streamReader.ReadLine()) != null)
            {
                var lineRead = ValidateMeterRead(line);

                //If Null Failed, else passed 
                if (lineRead == null)
                {
                    failed += 1;
                    continue;
                }

                if (meterReads.Count > 0)
                {
                    //Check for Duplicates
                    if (meterReads.Any(e => e.AccountId == lineRead.AccountId &&
                                            e.MeterReadDateTime == lineRead.MeterReadDateTime &&
                                            e.MeterReadValue == lineRead.MeterReadValue))
                    {
                        failed += 1;
                        continue;
                    }
                }

                meterReads.Add(lineRead);
                success += 1;
            }

            return (meterReads, success, failed);
        }

    

   
    private static MeterRead ValidateMeterRead(string line)
        {
            try
            { 
                string[] stItems = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
                DateTime meterReadDateTime;
                int meterReadValue;

                var accountId = int.Parse(stItems[0]);

                //TODO : Check if this Account Id Exists in the DB 

                //Failed validation 
                if (!DateTime.TryParse(stItems[1], out meterReadDateTime))
                    return null;
                
                if (!int.TryParse(stItems[2], out meterReadValue))
                    return null;

                if (meterReadValue <= 0 || meterReadValue.ToString().Length != 5)
                         return null;

                    return new MeterRead {  AccountId = accountId ,
                                        MeterReadDateTime = meterReadDateTime ,
                                        MeterReadValue = meterReadValue  };
            }
            catch
            {
                //// TODO: log in logging system 
                return null;
            }

        }
    }
}
