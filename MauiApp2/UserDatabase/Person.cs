
using SQLite;
namespace MauiApp2.UserDatabase
{

    public class Person 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public void clearFields()
        {
            Name = string.Empty;
            Surname = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string surname;
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public Person(string name, string surname, string email, string password)
        {
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.password = password;
        }
        
        public Person()
        {
            name = string.Empty;
            surname = string.Empty;
            email = string.Empty;
            password = string.Empty;
        }
    }
}
