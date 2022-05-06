using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLib.Entities;

namespace DataAccessLib.DAO
{
    public class ProductDAO
    {
        private dataContext data;
        public ProductDAO()
        {
            data = new dataContext();
        }
        public List<Product> getAllProduct()
        {
            return data.Products.ToList();
        }
        public List<Category> getAllCategory()
        {
            return data.Categories.ToList();
        }
        
        public List<Product> getProductByCat(string cartID)
        {
            return data.Products.Where(x=>x.categoryID == cartID).ToList(); 
        }

        public Product getProductDetail(string productID)
        {
            return data.Products.Where(x=>x.productID == productID).FirstOrDefault();
        }
    }
}
