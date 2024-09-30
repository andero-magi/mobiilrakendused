using SQLite;

namespace crud.Data
{
    public class DatabaseContext: IAsyncDisposable
    {
        private const string DbName = "CRUD_db";
        private static string DbPath => Path.Combine(".", DbName);

        private SQLiteAsyncConnection _connection;

        private SQLiteAsyncConnection Database =>
            (_connection ??= new SQLiteAsyncConnection(DbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite));

        public DatabaseContext() 
        { 

        }

        public async Task<IEnumerable<TTable>> GetAllAsync<TTable>() where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.ToListAsync();
        }

        private async Task<AsyncTableQuery<T>> GetTableAsync<T>() where T : class, new()
        {
            await CreateTableResultIfNotExists<T>();
            return Database.Table<T>();
        }

        private async Task CreateTableResultIfNotExists<T>() where T: class, new()
        {
            await Database.CreateTableAsync<T>();
        }

        private async Task<R> Execute<T, R>(Func<Task<R>> action) where T : class, new()
        {
            await CreateTableResultIfNotExists<T>();
            return await action();
        }

        public async Task<T> GetItemByKeyAsync<T>(object primaryKey) where T : class, new()
        {
            return await Execute<T, T>(async () => await Database.GetAsync<T>(primaryKey));
        }

        public async Task<bool> AddItemAsync<T>(T item) where T : class, new()
        {
            return await Execute<T, bool>(async () => await Database.InsertAsync(item) > 0);
        }

        public async Task<bool> UpdateItemAsync<T>(T item) where T : class, new()
        {
            return await Execute<T, bool>(async () => await Database.UpdateAsync(item) > 0);
        }

        public async Task<bool> DaleteItemAsync<T>(T item) where T : class, new()
        {
            return await Execute<T, bool>(async () => await Database.DeleteAsync(item) > 0);
        }

        public async Task<bool> DeleteItemByKeyAsync<T>(object primaryKey) where T : class, new()
        {
            return await Execute<T, bool>(async () => await Database.DeleteAsync<T>(primaryKey) > 0);
        }

        public async ValueTask DisposeAsync()
        {
            await _connection.CloseAsync();
        }
    }
}
