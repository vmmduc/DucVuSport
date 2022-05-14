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
        /*
         * Table product
         */
        public List<Product> getAllProduct()
        {
            return data.Products.ToList();
        }

        public Product getProductByID(int id)
        {
            return data.Products.Where(x => x.ProductID == id).FirstOrDefault();
        }
        public List<Product> getProductByCat(int cartID)
        {
            return data.Products.Where(x => x.CategoryID == cartID).ToList();
        }

        public List<Product> getTop10()
        {
            return data.Products.OrderByDescending(x => x.Sold).Take(10).ToList();
        }
        /*
         * Table Category
         */
        public List<Category> getAllCategory()
        {
            return data.Categories.ToList();
        }

        /*
         * Table Image
         */
        public List<Image> getImage(int productID)
        {
            return data.Images.Where(x => x.ProductID == productID).ToList();
        }
    }
}
