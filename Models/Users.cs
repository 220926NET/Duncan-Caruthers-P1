namespace Models;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsManager { get; set; }

    public User(string username, string password, bool mgr)
    {
        Username = username;
        Password = password;
        IsManager = mgr;
    }

    public bool checkCredentials(string username, string password)
    {
        if (Username.Equals(username) && Password.Equals(Password))
        {
            return true;
        }
        return false;
    }


}