using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ProductInventoryAPI.Models;

namespace ProductInventoryAPI.Repositories
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        
        public async Task AddProductAsync(Product product)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("uspAddProduct", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Price", product.Price);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                
                throw new Exception("Something went wrong while adding the product.", ex);
            }
            catch (Exception ex)
            {
                
                throw new Exception("An unexpected issue occurred while adding the product.", ex);
            }
        }


        

        public async Task UpdateProductAsync(Product product)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("uspUpdateProduct", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Id", product.Id);
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Description", product.Description);
                    cmd.Parameters.AddWithValue("@Price", product.Price);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                
                throw new Exception("Something went wrong while updating the product.", ex);
            }
            catch (Exception ex)
            {
                
                throw new Exception("An unexpected issue occurred while updating the product.", ex);
            }
        }


        public async Task DeleteProductAsync(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("uspDeleteProduct", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@Id", id);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                
                throw new Exception("An error while deleting the product from the database.", ex);
            }
            catch (Exception ex)
            {
                
                throw new Exception("An unexpected error occurred while deleting the product.", ex);
            }
        }


        

        public async Task<List<Product>> GetProductsAsync()
        {
            List<Product> products = new List<Product>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspGetProducts", con))
                    {
                        await con.OpenAsync();
                        SqlDataReader reader = await cmd.ExecuteReaderAsync();

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                Product product = new Product
                                {
                                    Id = (int)reader["Id"],
                                    Name = reader["Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = (decimal)reader["Price"]
                                };

                                products.Add(product);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                
                throw new Exception("while retrieving products from the database.", ex);
            }
            catch (Exception ex)
            {
                
                throw new Exception("An unexpected error occurred while retrieving the products.", ex);
            }

            return products;
        }




    }
}
