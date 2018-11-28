using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{

    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productcategories;

        public ProductCategoryRepository()
        {
            productcategories = cache["productcategories"] as List<ProductCategory>;
            if (productcategories == null)
            {
                productcategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productcategories"] = productcategories;
        }

        public void Insert(ProductCategory p)
        {
            productcategories.Add(p);
        }

        public void Update(ProductCategory productcategory)
        {
            ProductCategory productCatToUpdate = productcategories.Find(p => p.Id == productcategory.Id);
            if (productCatToUpdate != null)
            {
                productCatToUpdate = productcategory;
            }
            else
            {
                throw new Exception("Product Category not found!!");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory productCatToFind = productcategories.Find(p => p.Id == Id);
            if (productCatToFind != null)
            {
                return productCatToFind;
            }
            else
            {
                throw new Exception("Product Category not found!!");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productcategories.AsQueryable();
        }

        public void delete(ProductCategory productcategory)
        {
            ProductCategory productCatToDelete = productcategories.Find(p => p.Id == productcategory.Id);
            if (productCatToDelete != null)
            {
                productcategories.Remove(productcategory);
            }
            else
            {
                throw new Exception("Product Category not found!!");
            }
        }
    }
}
