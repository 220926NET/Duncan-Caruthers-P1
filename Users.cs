namespace Users
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsManager { get; set; }

        public User(string usr, string passwd, bool mgr)
        {
            UserName = usr;
            Password = passwd;
            IsManager = mgr;
        }

        public bool checkCredentials(string usr, string passwd)
        {
            if (UserName.Equals(usr) && Password.Equals(passwd))
            {
                return true;
            }
            return false;
        }
    }
}