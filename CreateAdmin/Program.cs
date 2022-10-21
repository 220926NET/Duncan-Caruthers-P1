
using Models;
using DataAccess;
using Services;

User admin = new User("admin", "", true);
admin.Password = User.Hash("admin", admin.Salt);

LoginHandler handeler = new LoginHandler(new DatabaseStorage());
handeler.AddUser(admin);