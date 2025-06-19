using LaBarracaBar.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaBarracaBar.Repositories
{
    public class TemporaryTableRepository : BaseRepository
    {
        public int AddTable(string tableNumber, List<ProductItem> products)
        {
            int tableId = 0;
            using (var conn = GetConnection())
            {
                conn.Open();
                using var trans = conn.BeginTransaction();
                try
                {
                    using var insertTable = new MySqlCommand("INSERT INTO temp_tables (table_number) VALUES (@number); SELECT LAST_INSERT_ID();", conn, trans);
                    insertTable.Parameters.AddWithValue("@number", tableNumber);
                    tableId = Convert.ToInt32(insertTable.ExecuteScalar());

                    foreach (var p in products)
                    {
                        using var insertProd = new MySqlCommand("INSERT INTO temp_table_products (table_id, product_name, quantity) VALUES (@tableId, @name, @qty)", conn, trans);
                        insertProd.Parameters.AddWithValue("@tableId", tableId);
                        insertProd.Parameters.AddWithValue("@name", p.Name);
                        insertProd.Parameters.AddWithValue("@qty", p.Quantity);
                        insertProd.ExecuteNonQuery();
                    }

                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
            return tableId;
        }

        public void UpdateTable(int tableId, List<ProductItem> products)
        {
            using var conn = GetConnection();
            conn.Open();
            using var trans = conn.BeginTransaction();

            try
            {
                // Eliminar los productos anteriores
                var delete = new MySqlCommand("DELETE FROM temp_table_products WHERE table_id = @id", conn, trans);
                delete.Parameters.AddWithValue("@id", tableId);
                delete.ExecuteNonQuery();

                // Insertar los productos actualizados
                foreach (var p in products)
                {
                    var insert = new MySqlCommand("INSERT INTO temp_table_products (table_id, product_name, quantity) VALUES (@tableId, @name, @qty)", conn, trans);
                    insert.Parameters.AddWithValue("@tableId", tableId);
                    insert.Parameters.AddWithValue("@name", p.Name);
                    insert.Parameters.AddWithValue("@qty", p.Quantity);
                    insert.ExecuteNonQuery();
                }

                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }

        public List<(int tableId, string tableNumber, List<ProductItem> products)> GetAllTables()
        {
            var tables = new List<(int, string, List<ProductItem>)>();

            using var conn = GetConnection();
            conn.Open();

            // Paso 1: cargar todas las mesas primero
            var tableInfos = new List<(int id, string number)>();
            using (var cmdTables = new MySqlCommand("SELECT id, table_number FROM temp_tables", conn))
            using (var readerTables = cmdTables.ExecuteReader())
            {
                while (readerTables.Read())
                {
                    tableInfos.Add((
                        Convert.ToInt32(readerTables["id"]),
                        readerTables["table_number"].ToString()
                    ));
                }
            } // ← 🔁 lector cerrado aquí

            // Paso 2: por cada mesa, cargar sus productos (en segundo paso, sin conflicto)
            foreach (var (id, number) in tableInfos)
            {
                var products = new List<ProductItem>();
                using (var cmdProducts = new MySqlCommand("SELECT product_name, quantity FROM temp_table_products WHERE table_id = @id", conn))
                {
                    cmdProducts.Parameters.AddWithValue("@id", id);
                    using var readerProducts = cmdProducts.ExecuteReader();
                    while (readerProducts.Read())
                    {
                        products.Add(new ProductItem
                        {
                            Name = readerProducts["product_name"].ToString(),
                            Quantity = Convert.ToInt32(readerProducts["quantity"])
                        });
                    }
                }

                tables.Add((id, number, products));
            }

            return tables;
        }
        public void DeleteTable(string tableNumber)
        {
            using var conn = GetConnection();
            conn.Open();
            var cmd = new MySqlCommand("DELETE FROM temp_tables WHERE table_number = @number", conn);
            cmd.Parameters.AddWithValue("@number", tableNumber);
            cmd.ExecuteNonQuery();
        }
    }
}
