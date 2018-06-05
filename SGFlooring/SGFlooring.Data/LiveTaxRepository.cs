using SGFlooring.Models;
using SGFlooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.Data
{
    public class LiveTaxRepository : ITaxRepository
    {
        private string _filepath;

        public LiveTaxRepository(string filePath)
        {
            _filepath = filePath;
        }

        public List<Tax> LoadTax()
        {
            List<Tax> taxList = new List<Tax>();
            using (StreamReader sr = new StreamReader(_filepath))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "StateAbbreviation,StateName,TaxRate")
                    {
                        continue;
                    }
                    Tax tax = new Tax();

                    string[] colums = line.Split(',');

                    tax.StateAbbreviation = colums[0];
                    tax.StateName = colums[1];
                    tax.TaxRate = decimal.Parse(colums[2]);
                    
                    taxList.Add(tax);
                };
            }
            return taxList;
        }
    }
}
