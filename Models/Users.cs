using System.Security.Cryptography;

namespace Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsManager { get; set; }

    public byte[] Salt { get; set; } = RandomNumberGenerator.GetBytes(512);

    public User() { }

    public User(string username, string password, bool mgr)
    {
        Id = Guid.NewGuid();
        Username = username;
        Password = password;
        IsManager = mgr;
    }

    public User(Guid id, string username, string password, bool mgr)
    {
        Id = id;
        Username = username;
        Password = password;
        IsManager = mgr;
    }

    public bool checkCredentials(string username, string password)
    {
        if (Username.Equals(username) && Password.Equals(User.Hash(password, Salt)))
        {
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return Username;
    }

    public static string Hash(string input, byte[] salt)
    {
        return Convert.ToHexString(Rfc2898DeriveBytes.Pbkdf2(input, salt, 1000, HashAlgorithmName.SHA512, 64));
    }
}