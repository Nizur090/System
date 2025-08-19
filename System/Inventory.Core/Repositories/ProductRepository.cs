using Inventory.Core.Data;
using Inventory.Core.Models;
using Microsoft.Data.Sqlite;

namespace Inventory.Core.Repositories
{
    public class ProductRepository
    {
        public IList<Product> GetAll()
        {
            var products = new List<Product>();
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT Id, Name, Sku, Price, Quantity, CreatedAt FROM Products ORDER BY Name";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Sku = reader.GetString(2),
                    Price = Convert.ToDecimal(reader.GetValue(3)),
                    Quantity = reader.GetInt32(4),
                    CreatedAt = DateTime.Parse(reader.GetString(5))
                });
            }
            return products;
        }

        public IList<Product> Search(string query)
        {
            var products = new List<Product>();
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT Id, Name, Sku, Price, Quantity, CreatedAt FROM Products WHERE Name LIKE $q OR Sku LIKE $q ORDER BY Name";
            cmd.Parameters.AddWithValue("$q", "%" + query + "%");
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Sku = reader.GetString(2),
                    Price = Convert.ToDecimal(reader.GetValue(3)),
                    Quantity = reader.GetInt32(4),
                    CreatedAt = DateTime.Parse(reader.GetString(5))
                });
            }
            return products;
        }

        public int Add(Product product)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO Products (Name, Sku, Price, Quantity, CreatedAt)
                                VALUES ($name, $sku, $price, $quantity, $created);
                                SELECT last_insert_rowid();";
            cmd.Parameters.AddWithValue("$name", product.Name);
            cmd.Parameters.AddWithValue("$sku", product.Sku);
            cmd.Parameters.AddWithValue("$price", product.Price);
            cmd.Parameters.AddWithValue("$quantity", product.Quantity);
            cmd.Parameters.AddWithValue("$created", DateTime.UtcNow.ToString("o"));
            var id = (long)cmd.ExecuteScalar()!;
            return (int)id;
        }

        public void Update(Product product)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"UPDATE Products SET Name=$name, Sku=$sku, Price=$price, Quantity=$quantity WHERE Id=$id";
            cmd.Parameters.AddWithValue("$name", product.Name);
            cmd.Parameters.AddWithValue("$sku", product.Sku);
            cmd.Parameters.AddWithValue("$price", product.Price);
            cmd.Parameters.AddWithValue("$quantity", product.Quantity);
            cmd.Parameters.AddWithValue("$id", product.Id);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM Products WHERE Id=$id";
            cmd.Parameters.AddWithValue("$id", id);
            cmd.ExecuteNonQuery();
        }

        public void AdjustStock(int productId, int change, string reason)
        {
            using var connection = new SqliteConnection(Database.ConnectionString);
            connection.Open();
            using var tx = connection.BeginTransaction();

            var movementCmd = connection.CreateCommand();
            movementCmd.Transaction = tx;
            movementCmd.CommandText = @"INSERT INTO StockMovements (ProductId, Change, Reason, CreatedAt)
                                        VALUES ($pid, $chg, $reason, $created)";
            movementCmd.Parameters.AddWithValue("$pid", productId);
            movementCmd.Parameters.AddWithValue("$chg", change);
            movementCmd.Parameters.AddWithValue("$reason", reason);
            movementCmd.Parameters.AddWithValue("$created", DateTime.UtcNow.ToString("o"));
            movementCmd.ExecuteNonQuery();

            var qtyCmd = connection.CreateCommand();
            qtyCmd.Transaction = tx;
            qtyCmd.CommandText = @"UPDATE Products
                                    SET Quantity = CASE WHEN Quantity + $chg < 0 THEN 0 ELSE Quantity + $chg END
                                    WHERE Id = $pid";
            qtyCmd.Parameters.AddWithValue("$chg", change);
            qtyCmd.Parameters.AddWithValue("$pid", productId);
            qtyCmd.ExecuteNonQuery();

            tx.Commit();
        }
    }
}

