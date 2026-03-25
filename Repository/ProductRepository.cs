using TestAPI.DB;
using TestAPI.DB.Entities;
using TestAPI.DB.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
namespace TestAPI.Repository
{
    public class ProductRepository
    {
        public TestAPIContext _context;
        public ProductRepository(TestAPIContext productcontext)
        {
            _context = productcontext;
        }

        // ⅁ET
        public async Task<List<Product>> GetAllAsync()
        {
            var product = await _context.Products.ToListAsync();
            return(product);
        }


        // ⅁ET by 🅸d

        public async Task<Product?> GetById(int Id)
        {
            var product = await _context.Products.FindAsync(Id);
            return (product);
        }

        // pOST 
        public async Task<Product> CreateAsync(Product product)
        {
            var products = _context.Products.Add(product);
            var saved = await _context.SaveChangesAsync();
            return (product);
        }

        // put

        public async Task<Product> UpdateAsync(int Id, Product product)
        {
            var products = await _context.Products.FindAsync(Id);
            {
                products.Name = product.Name;
                products.Description = product.Description;
                products.Price = product.Price;
            }
            var saved = await _context.SaveChangesAsync();

            return (products);
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var product = await _context.Products.FindAsync(Id);
            if (product == null)
            {
                return false; // manejar con midleware 
            }
            else {
            var delete = _context.Products.Remove(product);
            var saved = await _context.SaveChangesAsync();
            return true;
            }
              


        }
    }
}
