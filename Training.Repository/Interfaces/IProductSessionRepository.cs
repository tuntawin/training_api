using System;
using System.Collections.Generic;
using System.Text;
using Training.Database.Models;

namespace Training.Repository.Interfaces
{
    public interface IProductSessionRepository
    {
        Product GetProductById(int id);
        List<Product> GetProductList();
        int AddProduct(Product session);
        int UpdateProduct(Product session);
        int DeleteProduct(int id);
    }
}
