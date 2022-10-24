using System.Security.Cryptography;

namespace Models;

public class User
{
    public Guid id { get; set; }
    public string username { get; set; } = "";
    public string password { get; set; } = "";
    public bool ismanager { get; set; }

    public byte[] Salt { get; set; } = RandomNumberGenerator.GetBytes(512);

    public User() { }

    public User(string username, string password, bool mgr)
    {
        id = Guid.NewGuid();
        this.username = username;
        this.password = password;
        ismanager = mgr;
    }

    public User(Guid id, string username, string password, bool mgr)
    {
        this.id = id;
        this.username = username;
        this.password = password;
        ismanager = mgr;
    }

    public bool checkCredentials(string username, string password)
    {
        if (username.Equals(username) && this.password.Equals(User.Hash(password, Salt)))
        {
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return username;
    }

    public static string Hash(string input, byte[] salt)
    {
        return Convert.ToHexString(Rfc2898DeriveBytes.Pbkdf2(input, salt, 1000, HashAlgorithmName.SHA512, 64));
    }
}