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
    public class LiveProductRepository : IProductRepository
    {
        private string _filepath;

        public LiveProductRepository(string filePath)
        {
            _filepath = filePath;
        }

        public List<Product> LoadProducts()
        {
            List<Product> productList = new List<Product>();
            using (StreamReader sr = new StreamReader(_filepath))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "ProductType,CostPerSquareFoot,LaborCostPerSquareFoot")
                    {
                        continue;
                    }
                    Product product = new Product();

                    string[] colums = line.Split(',');

                    product.ProductType = colums[0];
                    product.CostPerSquareFoot = decimal.Parse(colums[1]);
                    product.LaborCostPerSquareFoot = decimal.Parse(colums[2]);

                    productList.Add(product);
                }
            }
            return productList;
        }
    }
}
