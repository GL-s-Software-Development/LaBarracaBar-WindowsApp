using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaBarracaBar.Repositories
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString;

        public BaseRepository()
        {
            _connectionString = "Server=localhost; Port=3306; Database=bar_db; User=niicox; Pwd=niicox21;";
        }
        protected MySqlConnection GetConnection()
        {
            try
            {
                return new MySqlConnection(_connectionString);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Failed to connect to the database. Please check your connection settings.");
                throw new InvalidOperationException("Failed to connect to the database.", ex);
            }
        }

        private void ShowErrorMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
