using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ProductInventoryAPI.Models;
using ProductInventoryAPI.Repositories;

namespace ProductInventoryAPI.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;

        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }

        public async Task AddProductAsync(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name is required.");

            if (product.Price <= 0)
                throw new ArgumentException("Product price must be greater than zero.");

            await _repository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            if (product.Id <= 0)
                throw new ArgumentException("Product ID is required.");

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name is required.");

            if (product.Price <= 0)
                throw new ArgumentException("Product price must be greater than zero.");

            await _repository.UpdateProductAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Product ID is required.");

            await _repository.DeleteProductAsync(id);
        }

        

        public async Task<List<Product>> GetProductsAsync()
        {
            try
            {
                return await _repository.GetProductsAsync();
            }
            catch (SqlException ex)
            {
                
                throw new Exception("An error occurred while retrieving products from the database.", ex);
            }
            catch (Exception ex)
            {
                
                throw new Exception("An unexpected error occurred while retrieving the products.", ex);
            }
        }

    }
}
