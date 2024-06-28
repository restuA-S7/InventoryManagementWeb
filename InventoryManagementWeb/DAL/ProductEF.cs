using InventoryManagementWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementWeb.DAL
{
    public class ProductEF : IProduct
    {
        private readonly InventoryDbContext _appDbContext;
        public ProductEF(InventoryDbContext appDbContext)
        {
               _appDbContext = appDbContext;
        }
        public Product Add(Product entity)
        {
            _appDbContext.Products.Add(entity);
            _appDbContext.SaveChanges();
            return entity;
        }

        public void Delete(int id)
        {
            var deleteProduct = GetById(id);
            _appDbContext.Products.Remove(deleteProduct);
            _appDbContext.SaveChanges();

        }

        public IEnumerable<Product> GetAll()
        {
            var results = _appDbContext.Products.ToList();
            return results;
        }

        public Product GetById(int id)
        {
            var result = _appDbContext.Products.Find(id);
            if(result == null)
            {
                throw new Exception("Product Tidak Ditemukan");
            }
            return result;
        }

        public Product Update(Product entity)
        {
            var updateProduct = GetById(entity.ProductId);
            if(updateProduct != null)
            {
                updateProduct.Name = entity.Name;
                updateProduct.StockLevel = entity.StockLevel;
                _appDbContext.SaveChanges();
            }
            return updateProduct;
        }
    }
}
