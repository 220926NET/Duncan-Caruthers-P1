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

                    }
                    else if (selection == 2)
                    {

                    }
                    else if (selection == 3)
                    {

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
    }
}