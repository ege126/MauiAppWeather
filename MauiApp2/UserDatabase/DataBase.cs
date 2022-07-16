namespace MauiApp2.UserDatabase;

using SQLite;

public class DataBase
{
    private readonly SQLiteAsyncConnection _database;
    public DataBase(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Person>();
    }
    
    public Task<int> addPersonAsync(Person persn)
    {
        return _database.InsertAsync(persn);
    }

    public Task<List<Person>> getAllPersonsAync()
    {
        return _database.Table<Person>().ToListAsync();
    }

    public Task<int> updatePersonAsync(Person prs)
    {
        return _database.UpdateAsync(prs);
    }

    public Task<Person> checkEmailExists(string e_MAaIl)
    {
        return _database.Table<Person>().Where(p =>
        p.Email.Equals(e_MAaIl) == true).FirstOrDefaultAsync();
    }

    public Task<Person> check_EmailPasswordPair_Exists(string em, string pd)
    {
        return _database.Table<Person>().Where(p =>
        p.Email.Equals(em) == true && p.Password.Equals(pd) == true).FirstOrDefaultAsync();
    }
}
