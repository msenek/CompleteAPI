using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Security.Cryptography.X509Certificates;
using TestAPI.DB;
using TestAPI.DB.DTOs;
using TestAPI.DB.Entities;
using TestAPI.Repository;

namespace TestAPI.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;
        public ProductService(ProductRepository productrepository)
        {
            _repository = productrepository;
        }

        // ⅁ET
        public async Task<List<ResponseDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();

            var productResponse = new List<ResponseDto>();

            foreach (var product in products)
            {
                var Response = new ResponseDto();

                Response.Id = product.Id;
                Response.Name = product.Name;
                Response.Description = product.Description;
                Response.Price = product.Price;

                productResponse.Add(Response);


            }
            return (productResponse);
        }

        // ⅁ET by 🅸d

        public async Task<ResponseDto?> GetById(int Id)
        {
            var product = await _repository.GetById(Id);

            if (product == null)
            {
                return null; // manejar con middleware mas adelante
            }
            else
            {
                var response = new ResponseDto();

                response.Id = product.Id;
                response.Name = product.Name;
                response.Description = product.Description;
                response.Price = product.Price;

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
            var product = await _repository.GetById(Id);
            if (product == null)
            {
                return null; // manejar con middleware mas adelante
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
            var delete = await _repository.DeleteAsync(Id);
            return (delete);
        }

    }
}