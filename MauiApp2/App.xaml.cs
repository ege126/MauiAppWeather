using MauiApp2.UserDatabase;

namespace MauiApp2;

public partial class App : Application
{
    private static DataBase database;
    public static DataBase Database
    {
        get
        {
            if (database == null)
            {
                database = new DataBase(Path.Combine(Environment.GetFolderPath
                    (Environment.SpecialFolder.LocalApplicationData), "customerDatabase"));
            }
            return database;
        }
    }
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell(); 
    }
}
