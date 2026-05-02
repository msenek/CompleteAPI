using TestAPI.DB.DTOs;
using TestAPI.Repository;
using TestAPI.DB.Entities;
namespace TestAPI.Repository.Interface
{
    public interface IProductRepository
    {
       Task<List<Product>> GetAllAsync(int page, int pageSize, FilteringDto filtering);
        Task<Product?> GetById(int Id);
        Task<Product> CreateAsync(Product product);
        Task<Product> UpdateAsync(int Id, Product product);
        Task<bool> DeleteAsync(int Id);
    }
}
