using MeterReadingAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MeterReadingAPI.Common
{
    public class MeterReadsProcessor
    {
        public static  List<MeterRead> ProcessFile(string filePath, out ResultSet results )
        {
            using (StreamReader streamReader = new StreamReader(new FileStream(filePath, FileMode.Open)))
            {
                //Skip Header
                string headerLine = streamReader.ReadLine();
                List<MeterRead> meterReads = new List<MeterRead>();
                string line;
                int failed =0, success=0;
                while ((line = streamReader.ReadLine()) != null)
                {
                  
                        var mtread = ValidateMeterRead(line);
                        //If Null Failed, else passed 
                        if (mtread != null)
                        {
                            if (meterReads.Count > 0)
                            {
                                //Check for Duplicate 
                                var duplicates = meterReads.Where(e => e.AccountId == mtread.AccountId
                                            && e.MeterReadDateTime == mtread.MeterReadDateTime && e.MeterReadValue == mtread.MeterReadValue);

                                if (duplicates.Any())
                                    failed += 1;
                            }

                            meterReads.Add(mtread);

                            success += 1;
                        }
                        else
                            failed += 1;

                                          
                }
                results = new ResultSet { SuccessCount = success, FailedCount = failed };
                return meterReads;
            }

            
        }

        private static MeterRead ValidateMeterRead(string line)
        {
            try
            { 
                string[] stItems = line.Split(',');
                DateTime meterReadDateTime;
                int meterReadValue;

                var accountId = int.Parse(stItems[0]);

                //TODO : Check if this Account Id Exists in the DB 

                //Failed validation 
                if (!DateTime.TryParse(stItems[1], out meterReadDateTime))
                    return null;
                
                if (!int.TryParse(stItems[2], out meterReadValue))
                    return null;

                if (meterReadValue > 0 && meterReadValue.ToString().Length == 5)
                { //do Nothing 
                }
                else
                    return null;

                return new MeterRead {  AccountId = accountId ,
                                        MeterReadDateTime = meterReadDateTime ,
                                        MeterReadValue = meterReadValue  };
            }
            catch
            {
                //Failed 
                return null;
            }

        }
    }
}
