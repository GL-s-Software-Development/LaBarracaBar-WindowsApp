using LaBarracaBar.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LaBarracaBar.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public void Add(ProductModel productModel)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO products (name, category_id, price) VALUES (@name, @category, @price)";
                command.Parameters.Add("@name", MySqlDbType.VarChar).Value = productModel.Name;
                command.Parameters.Add("@category", MySqlDbType.Int32).Value = productModel.Category;
                command.Parameters.Add("@price", MySqlDbType.Decimal).Value = productModel.Price;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(ProductModel productModel)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE products SET name = @name, category_id = @category, price = @price WHERE id = @id";
                command.Parameters.AddWithValue("@id", productModel.Id);
                command.Parameters.AddWithValue("@name", productModel.Name);
                command.Parameters.AddWithValue("@category", productModel.Category);
                command.Parameters.AddWithValue("@price", productModel.Price);
                command.ExecuteNonQuery();
            }
        }

        public void Remove(ProductModel product)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM products WHERE id = @id";
                command.Parameters.AddWithValue("@id", product.Id);
                command.ExecuteNonQuery();
            }
        }

        public List<ProductModel> GetByProduct(string searchProduct)
        {
            List<ProductModel> products = new List<ProductModel>();
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"SELECT p.id, p.name, p.price, p.category_id, c.name AS category_name 
                                        FROM products p 
                                        JOIN categories c ON p.category_id = c.id 
                                        WHERE p.name LIKE @searchString";
                command.Parameters.Add("@searchString", MySqlDbType.VarChar).Value = "%" + searchProduct + "%";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductModel product = new ProductModel()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString(),
                            Price = Convert.ToDouble(reader["price"]),
                            Category = Convert.ToInt32(reader["category_id"]),
                            CategoryName = reader["category_name"].ToString()
                        };
                        products.Add(product);
                    }
                }
            }
            return products;
        }

        public IEnumerable<ProductModel> GetAll() => throw new NotImplementedException();
        public ProductModel GetById(int id) => throw new NotImplementedException();
        public ProductModel GetByName(string product)
        {
            using var connection = GetConnection();
            connection.Open();

            using var command = new MySqlCommand("SELECT p.id, p.name, p.price, p.category_id, c.name AS category_name FROM products p JOIN categories c ON p.category_id = c.id WHERE p.name = @name", connection);
            command.Parameters.AddWithValue("@name", product);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new ProductModel
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = reader["name"].ToString(),
                    Category = Convert.ToInt32(reader["category_id"]),
                    CategoryName = reader["name"].ToString(), // si lo agregás en el SELECT
                    Price = Convert.ToDouble(reader["price"])
                };
            }

            return null;
        }
    }
}