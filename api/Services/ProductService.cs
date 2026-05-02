using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Security.Cryptography.X509Certificates;
using TestAPI.DB;
using TestAPI.DB.DTOs;
using TestAPI.DB.Entities;
using TestAPI.Repository.Interface;
using TestAPI.Middleware;
using TestAPI.Middleware.Exceptions;
using TestAPI.Services.Interface;

namespace TestAPI.Services
{
    public class ProductService 
    {
        private readonly IProductRepository _repository;
        private readonly ICacheService _cache;
        public ProductService(IProductRepository productrepository, ICacheService cache)
        {
            _repository = productrepository;
            _cache = cache;
        }

        // ⅁ET
        public async Task<List<ResponseDto>> GetAllAsync(int page, int pageSize, FilteringDto filtering)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 10;
            }
            if (pageSize > 50)
            {
                throw new BadRequestException("PageSize cannot be greater than 50");
            }

            var key = $"Product:Page={page}:PageSize={pageSize}:Name={filtering.Name}:MinPrice={filtering.MinPrice}:MaxPrice={filtering.MaxPrice}";

            var cached = await _cache.GetAsync<List<ResponseDto>>(key);

            if (cached != null) return cached;

            var products = await _repository.GetAllAsync(page, pageSize, filtering);

            var productResponse = new List<ResponseDto>();
            foreach (var product in products)
            {
                var response = new ResponseDto();
                response.Id = product.Id;
                response.Name = product.Name;
                response.Description = product.Description;
                response.Price = product.Price;
                productResponse.Add(response);
            }

            await _cache.SetAsync(productResponse, key, TimeSpan.FromMinutes(10));

            return productResponse;
        }

        // ⅁ET by 🅸d

        public async Task<ResponseDto?> GetById(int Id)
        {
            var key = $"Product:{Id}";
            var cached = await _cache.GetAsync<ResponseDto>(key);
            if (cached != null)
            {
                return cached;
            }
            
            var product = await _repository.GetById(Id);
            if (product == null)
            {
                throw new NotFoundException("Not Found");
            }
            else
            {
                var response = new ResponseDto();

                response.Id = product.Id;
                response.Name = product.Name;
                response.Description = product.Description;
                response.Price = product.Price;
            

            await _cache.SetAsync(product, key, TimeSpan.FromMinutes(10));


            

                return response;
            } 
        }

        // post
        public async Task<ResponseDto> CreateAsync(RequestDto request)

        {
            var newproduct = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            var product = await _repository.CreateAsync(newproduct);

            var response = new ResponseDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };

            return (response);
            
        }

        // put 

        public async Task<ResponseDto> UpdateAsync(int Id, RequestDto request)
        {
            var key = $"Product:{Id}";
            await _cache.RemoveAsync(key);

            var product = await _repository.GetById(Id);
            if (product == null)
            {
                throw new NotFoundException("Not Found");
            }
            else
            {
                {
                    product.Name = request.Name;
                    product.Description = request.Description;
                    product.Price = request.Price;
                }
                ;

                var saved = await _repository.UpdateAsync(Id, product);

                var Response = new ResponseDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price
                };

                return (Response);

            }
            
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var key = $"Product:{Id}";
            await _cache.RemoveAsync(key);

            var delete = await _repository.DeleteAsync(Id);
            return (delete);
        }

    }
}