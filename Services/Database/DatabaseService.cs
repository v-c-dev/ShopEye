using ShopEye.Models.Entities;
using SQLite;

namespace ShopEye.Services.Database
{
    public class DatabaseService : IDatabaseService
    {
        private readonly SQLiteAsyncConnection _database;
        private static SQLiteConnection database;

        public static void Initialize()
        {
            // path to db file
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "mydatabase.db3");
            // Initialize the connection. will be created if it doesn't exist
            database = new SQLiteConnection(dbPath);
            // Create tables if they don't exist
            database.CreateTable<ItemEntity>();
        }
        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
        }

        public async Task InitializeAsync()
        {
            await _database.CreateTableAsync<ItemEntity>();
        }

        public async Task AddItemAsync(ItemEntity item)
        {
            await _database.InsertAsync(item);
        }

        public async Task<List<ItemEntity>> GetItemsAsync()
        {
            return await _database.Table<ItemEntity>().ToListAsync();
        }

        public async Task<ItemEntity> GetItemByIdAsync(int id)
        {
            return await _database.FindAsync<ItemEntity>(id);
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await GetItemByIdAsync(id);
            if (item != null)
            {
                await _database.DeleteAsync(item);
            }
        }
    }
}