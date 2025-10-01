using LaBarracaBar.Models;
using LaBarracaBar.ViewModels;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaBarracaBar.Repositories
{
    public class SalesRepository : BaseRepository
    {
        public int CreateSale(string tableNumber, List<ProductModel> products)
        {
            using var conn = GetConnection();
            conn.Open();
            using var trans = conn.BeginTransaction();

            try
            {
                var total = products.Sum(p => (decimal)p.Price * p.Quantity);

                var insertSale = new MySqlCommand("INSERT INTO sales (total) VALUES (@total); SELECT LAST_INSERT_ID();", conn, trans);
                insertSale.Parameters.AddWithValue("@total", total);
                int saleId = Convert.ToInt32(insertSale.ExecuteScalar());

                foreach (var product in products)
                {
                    var insertItem = new MySqlCommand(@"INSERT INTO sale_items (sale_id, product_id, quantity, subtotal) 
                                                    VALUES (@saleId, @prodId, @qty, @subtotal)", conn, trans);
                    insertItem.Parameters.AddWithValue("@saleId", saleId);
                    insertItem.Parameters.AddWithValue("@prodId", product.Id);
                    insertItem.Parameters.AddWithValue("@qty", product.Quantity);
                    insertItem.Parameters.AddWithValue("@subtotal", product.Quantity * (decimal)product.Price);
                    insertItem.ExecuteNonQuery();
                }

                trans.Commit();
                return saleId;
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }
        public List<TopProductModel> GetSalesAnalysis(DateTime from, DateTime to)
        {
            var list = new List<TopProductModel>();

            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"
                    SELECT p.name, SUM(si.quantity) AS quantity, SUM(si.subtotal) AS revenue
                    FROM sale_items si
                    JOIN sales s ON s.id = si.sale_id
                    JOIN products p ON p.id = si.product_id
                    WHERE DATE(s.date) BETWEEN @from AND @to
                    GROUP BY p.name
                    ORDER BY revenue DESC";

                command.Parameters.AddWithValue("@from", from);
                command.Parameters.AddWithValue("@to", to);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TopProductModel
                        {
                            Name = reader.GetString("name"),
                            Quantity = reader.GetInt32("quantity"),
                            Revenue = reader.GetDecimal("revenue")
                        });
                    }
                }
            }

            return list;
        }
        public decimal GetTotalRevenueForDay(DateTime day)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT SUM(total) FROM sales WHERE DATE(date) = @day";
                command.Parameters.AddWithValue("@day", day.Date);

                var result = command.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0m;
            }
        }
    }
}
