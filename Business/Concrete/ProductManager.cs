using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {

        private IProductDal _productDal;

        public ProductManager(IProductDal productDal) { 
            
            _productDal = productDal;
        
        }


        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }

        public List<Product> GetAll()
        {
            List<Product> products = _productDal.GetAll();
            return products;
        }

        public Product GetById(int id)
        {
            Product product = _productDal.Get(p => p.Id == id);
            return product;

        }

        public void Update(Product product)
        {
            _productDal.Update(product);
        }
    }
}
