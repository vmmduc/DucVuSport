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
        
        public Product getProductByID(int id)
        {
            return data.Products.Where(x=>x.productID == id).FirstOrDefault();
        }

        public List<Product> getProductByCat(int cartID)
        {
            return data.Products.Where(x=>x.categoryID == cartID).ToList(); 
        }

        public Product getProductDetail(int productID)
        {
            return data.Products.Where(x=>x.productID == productID).FirstOrDefault();
        }
        public List<Image> getImage(int productID)
        {
            return data.Images.Where(x=>x.productID == productID).ToList();
        }
    }
}
