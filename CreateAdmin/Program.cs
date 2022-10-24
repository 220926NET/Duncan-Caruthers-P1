﻿
using Models;
using DataAccess;
using Services;

User admin = new User("admin", "", true);
admin.password = User.Hash("admin", admin.Salt);

LoginHandler handeler = new LoginHandler(new DatabaseStorage());
Console.WriteLine(handeler.AddUser(admin));