using Microsoft.Data.Sqlite;

namespace InventoryApp.Data
{
    public static class Database
    {
        private static readonly string _dbPath = Path.Combine(AppContext.BaseDirectory, "inventory.db");
        private static readonly string _connectionString = $"Data Source={nameof(inventory)}.db".Replace(nameof(inventory), "inventory");

        public static string ConnectionString => $"Data Source={_dbPath}";

        public static void Initialize()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_dbPath) ?? AppContext.BaseDirectory);

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var createProducts = @"
                CREATE TABLE IF NOT EXISTS Products (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Sku TEXT NOT NULL UNIQUE,
                    Price REAL NOT NULL,
                    Quantity INTEGER NOT NULL DEFAULT 0,
                    CreatedAt TEXT NOT NULL
                );
            ";

            var createMovements = @"
                CREATE TABLE IF NOT EXISTS StockMovements (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ProductId INTEGER NOT NULL,
                    Change INTEGER NOT NULL,
                    Reason TEXT,
                    CreatedAt TEXT NOT NULL,
                    FOREIGN KEY(ProductId) REFERENCES Products(Id) ON DELETE CASCADE
                );
            ";

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = createProducts;
                cmd.ExecuteNonQuery();
            }

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = createMovements;
                cmd.ExecuteNonQuery();
            }
        }
    }
}

