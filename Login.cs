using Users;

namespace Login
{
    class LoginHandler
    {
        private List<User> users;

        public LoginHandler()
        {
            users = new List<User>();
        }

        public bool addUser(string usr, string passwd, bool mgr)
        {
            foreach (User u in users)
            {
                if (u.UserName.Equals(usr))
                {
                    return false;
                }
            }
            users.Add(new User(usr, passwd, mgr));
            return true;
        }

        public User? login(string usr, string passwd)
        {
            foreach (User u in users)
            {
                if (u.checkCredentials(usr, passwd))
                {
                    return u;
                }
            }
            return null;
        }
    }
}