using Login;
using Users;

namespace ControlLoop
{
    public class Controller
    {
        public static LoginHandler handler = new LoginHandler();

        public static void RunLoop()
        {
            Console.WriteLine("Welcome to the expense system");
            Console.WriteLine("###################################");
            Console.WriteLine(" [1] Login");
            Console.WriteLine(" [2] Register");
            Console.WriteLine(" [3] Quit");
            Console.WriteLine("###################################");
            string? input = Console.ReadLine();
            if (input != null)
            {
                int selection;
                bool check = int.TryParse(input, out selection);
                if (check)
                {
                    if (selection == 1)
                    {
                        Controller.RunLogin();
                    }
                    else if (selection == 2)
                    {
                        Controller.RunRegister();
                    }
                    else if (selection == 3)
                    {
                        Console.WriteLine("Goodbye!");
                        Environment.Exit(0);
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Selection, Press enter to continue");
                        Console.ReadLine();
                        Controller.RunLoop();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter 1, 2, or 3, press enter to continue");
                    Console.ReadLine();
                    Controller.RunLoop();
                }
            }
            else
            {
                Console.WriteLine("Read error, press enter to continue");
                Console.ReadLine();
                Controller.RunLoop();
            }
        }

        public static void RunLogin()
        {
            Console.Write("Username: ");
            string? usr = Console.ReadLine();
            Console.Write("Password: ");
            string? passwd = Console.ReadLine();

            if (usr != null && passwd != null)
            {
                User? temp = handler.login(usr, passwd);
                if (temp != null)
                {
                    Console.WriteLine("Logged in as: " + temp.UserName);
                }
                else
                {
                    Console.WriteLine("Invalid login information, press enter to continue");
                    Console.ReadLine();
                    Controller.RunLoop();
                }
            }
            else
            {
                Console.WriteLine("Read error, press enter to continue");
                Console.ReadLine();
                Controller.RunLogin();
            }
        }

        public static void RunRegister()
        {
            Console.Write("Will this new user be a manager? (y/N) -> default no: ");
            string? input = Console.ReadLine();
            if (input != null)
            {
                bool mgr = false;
                if (input.ToLower().StartsWith('y'))
                {
                    mgr = true;
                }
                Console.Write("New Username: ");
                string? usr = Console.ReadLine();
                Console.Write("New Password: ");
                string? passwd = Console.ReadLine();
                if (usr != null && passwd != null)
                {
                    if (handler.addUser(usr, passwd, mgr))
                    {
                        Console.WriteLine("Suceess!");
                        Controller.RunLoop();
                    }
                    else
                    {
                        Console.WriteLine("User name already in use, press enter to continue");
                        Console.ReadLine();
                        Controller.RunRegister();
                    }
                }
                else
                {
                    Console.WriteLine("Read error");
                    Console.ReadLine();
                    Controller.RunRegister();
                }
            }
        }

    }
}