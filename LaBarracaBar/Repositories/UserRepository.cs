using LaBarracaBar.Models;
using System;
using System.Net;
using MySqlConnector;
using System.Data;

namespace LaBarracaBar.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"INSERT INTO USERS (username, password, name, lastname, email) VALUES (@username, @password, @name, @lastname, @email)";

                // Hashear contraseña antes de guardarla
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userModel.Password, workFactor: 12);

                command.Parameters.Add("@username", MySqlDbType.VarChar).Value = userModel.Username;
                command.Parameters.Add("@password", MySqlDbType.VarChar).Value = hashedPassword;
                command.Parameters.Add("@name", MySqlDbType.VarChar).Value = userModel.Name;
                command.Parameters.Add("@lastname", MySqlDbType.VarChar).Value = userModel.LastName;
                command.Parameters.Add("@email", MySqlDbType.VarChar).Value = userModel.Email;

                command.ExecuteNonQuery();
            }
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT password FROM USERS WHERE username=@username LIMIT 1";
                command.Parameters.Add("@username", MySqlDbType.VarChar).Value = credential.UserName;

                var storedPassword = command.ExecuteScalar() as string;
                if (string.IsNullOrEmpty(storedPassword))
                    return false;

                string inputPassword = credential.Password; // ⚠ Credential.Password no es SecureString en tu repo

                // Caso 1: ya está encriptado con BCrypt
                if (storedPassword.StartsWith("$2"))
                {
                    return BCrypt.Net.BCrypt.Verify(inputPassword, storedPassword);
                }

                // Caso 2: está en plano → migrar
                if (storedPassword == inputPassword)
                {
                    string newHash = BCrypt.Net.BCrypt.HashPassword(inputPassword, workFactor: 12);

                    using (var updateCmd = new MySqlCommand("UPDATE USERS SET password=@p WHERE username=@u", connection))
                    {
                        updateCmd.Parameters.Add("@p", MySqlDbType.VarChar).Value = newHash;
                        updateCmd.Parameters.Add("@u", MySqlDbType.VarChar).Value = credential.UserName;
                        updateCmd.ExecuteNonQuery();
                    }

                    return true;
                }

                return false;
            }
        }

        public void Edit(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                // Verificamos si hay nueva contraseña
                if (!string.IsNullOrWhiteSpace(userModel.Password))
                {
                    // Actualizamos todo, incluyendo la contraseña
                    command.CommandText = @"UPDATE USERS SET username=@username, password=@password, name=@name, lastname=@lastname, email=@email WHERE id=@id";

                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userModel.Password, workFactor: 12);
                    command.Parameters.Add("@password", MySqlDbType.VarChar).Value = hashedPassword;
                }
                else
                {
                    // No actualizar contraseña
                    command.CommandText = @"UPDATE USERS SET username=@username, name=@name, lastname=@lastname, email=@email WHERE id=@id";
                }

                // Parámetros comunes
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = userModel.Id;
                command.Parameters.Add("@username", MySqlDbType.VarChar).Value = userModel.Username;
                command.Parameters.Add("@name", MySqlDbType.VarChar).Value = userModel.Name;
                command.Parameters.Add("@lastname", MySqlDbType.VarChar).Value = userModel.LastName;
                command.Parameters.Add("@email", MySqlDbType.VarChar).Value = userModel.Email;

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUsername(string username)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM USERS WHERE username=@username";
                command.Parameters.Add("@username", MySqlDbType.VarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = int.Parse(reader[0].ToString()),
                            Username = reader[1].ToString(),
                            Password = string.Empty, // ⚠ nunca devolver el hash a la vista
                            Name = reader[3].ToString(),
                            LastName = reader[4].ToString(),
                            Email = reader[5].ToString(),
                        };
                    }
                }
            }
            return user;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
        public void ChangePassword(int userId, string newPassword)
        {
            using (var connection = GetConnection())
            using (var command = new MySqlCommand())
            {
                connection.Open();
                command.Connection = connection;

                command.CommandText = @"UPDATE USERS SET password=@password WHERE id=@id";

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword, workFactor: 12);

                command.Parameters.Add("@id", MySqlDbType.Int32).Value = userId;
                command.Parameters.Add("@password", MySqlDbType.VarChar).Value = hashedPassword;

                command.ExecuteNonQuery();
            }
        }
    }
}