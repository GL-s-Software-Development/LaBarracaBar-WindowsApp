using LaBarracaBar.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaBarracaBar.Repositories
{
    public class CategoryRepository : BaseRepository
    {
        public List<CategoryModel> GetAll()
        {
            var categories = new List<CategoryModel>();

            using (var connection = GetConnection())
            using (var command = new MySqlCommand("SELECT id, name FROM categories", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new CategoryModel
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Name = reader["name"].ToString()
                        });
                    }
                }
            }

            return categories;
        }
    }
}
