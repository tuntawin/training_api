using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Training.Database.Models;
using Training.Repository.Interfaces;

namespace Training.Repository
{
    public class ProductSessionRepository : IProductSessionRepository
    {
        private readonly TrainingapiContext _context;

        public ProductSessionRepository(TrainingapiContext context)
        {
            _context = context;
        }


        public int AddProduct(Product session)
        {
            _context.Product.Add(session);
            return _context.SaveChanges();
        }

        public int DeleteProduct(int id)
        {
            var product = _context.Product.SingleOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                return 0;
            }

            _context.Product.Remove(product);
            return _context.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            return _context.Product.SingleOrDefault(m => m.ProductId == id);
        }

        public List<Product> GetProductList()
        {
            return _context.Product.ToList();
        }

        public int UpdateProduct(Product session)
        {
            _context.Entry(session).State = EntityState.Modified;
            return _context.SaveChanges();
        }
    }
}
