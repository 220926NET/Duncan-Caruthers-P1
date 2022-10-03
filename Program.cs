using Login;
using Users;

LoginHandler handler = new LoginHandler();
handler.addUser("test", "test", true);

User? test = handler.login("test", "test");
if (test != null)
{
    if (test.IsManager)
    {
        Console.WriteLine("It works!");
    }
}