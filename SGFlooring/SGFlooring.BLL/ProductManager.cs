using SGFlooring.Models;
using SGFlooring.Models.Interfaces;
using SGFlooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SGFlooring.BLL
{
    public class ProductManager
    {
        private IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product GetProductType(string productType)
        {
            return _productRepository.LoadProducts().FirstOrDefault(p => p.ProductType == productType);
        }

        public List<Product> ListAllProduct()
        {
            return _productRepository.LoadProducts();
        }
    }
}
