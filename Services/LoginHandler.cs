using Models;
using DataAccess;

namespace Services;

public class LoginHandler
{
    private IStorage storage;

    public LoginHandler(IStorage storage)
    {
        this.storage = storage;
    }

    public bool AddUser(User usr)
    {
        return storage.AddUser(usr);
    }

    public User? login(string usr, string passwd)
    {
        foreach (User u in storage.GetUsers())
        {
            if (u.checkCredentials(usr, passwd))
            {
                return u;
            }
        }
        return null;
    }

    public User? GetUser(Guid id)
    {
        List<User> users = storage.GetUsers();
        foreach (User u in users)
        {
            if (id == u.Id)
            {
                return u;
            }
        }
        return null;
    }
}