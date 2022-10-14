/*
    Decision tree:
        .
        |-- user
        |   |-- Veiw Past Tickets
        |   |-- Add Reimbursement Request
        |   `-- Quit
        |-- manager
        |   |-- View All Reimbersement for all employees
        |   |   |-- Approve/Deny Requests
        |   `-- Quit
        `-- Quit
*/

using Models;
using Services;
using DataAccess;

if (!DatabaseStorage.IsTheDatabaseLive())
{
    UIHandler.DoInputError("It would appear the database is down");
    UIHandler.DoExit(1);
}

while (true)
{
    Console.Clear();
    Console.WriteLine("Welcome To the Reimbursemt System");
    Console.WriteLine("----------------------------------------");
    Console.WriteLine(" [1] Login");
    Console.WriteLine(" [2] Register");
    Console.WriteLine(" [3] Exit");
    Console.WriteLine("----------------------------------------");
    int selection = UIHandler.GetSelection();
    if (selection == 1)
    {
        UIHandler.LoginInteraction();
    }
    else if (selection == 2)
    {
        // Employee
        UIHandler.RegisterInteraction();
    }
    else if (selection == 3)
    {
        // Quit
        UIHandler.DoExit(0);
    }
    else
    {
        UIHandler.DoInputError("Invalid Selection");
    }
}

public class UIHandler
{
    private static LoginHandler users = new LoginHandler(new DatabaseStorage());
    private static TicketHandler tickets = new TicketHandler(new DatabaseStorage());
    private static User loggedInUser = new User("incorrect", "not possiable to use", false);

    public static void DoExit(int code)
    {
        Environment.Exit(code);
    }

    public static void DoInputError(string Message)
    {
        Console.WriteLine(Message);
        Console.WriteLine("press enter to continue...");
        Console.ReadLine();
    }

    public static int GetSelection()
    {
        string? selection = Console.ReadLine();
        if (selection != null)
        {
            int value;
            bool check = int.TryParse(selection, out value);
            if (check)
            {
                return value;
            }
        }
        return -1;
    }

    public static decimal GetDecimal()
    {
        string? selection = Console.ReadLine();
        if (selection != null)
        {
            decimal value;
            bool check = decimal.TryParse(selection, out value);
            if (check)
            {
                return value;
            }
        }
        return -1;
    }

    public static void ManagerInteraction()
    {
        Console.Clear();
        Console.WriteLine($"Welcome Manager, {loggedInUser}!");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine(" [1] View Past Tickets");
        Console.WriteLine(" [2] Process Pending Tickets"); ;
        Console.WriteLine(" [3] Logout");
        Console.WriteLine("----------------------------------------");
        int selection = GetSelection();
        if (selection == 1)
        {
            Console.WriteLine(tickets.ToString());
            DoInputError("");
            ManagerInteraction();
        }
        else if (selection == 2)
        {
            foreach (Ticket t in tickets.GetPending())
            {
                Console.WriteLine("Ticket: " + t.ToString());
                Console.WriteLine("[1] Approve [2] Deny [3] Skip");
                selection = GetSelection();
                if (selection == 1)
                {
                    tickets.UpdateTicket(t.Id, "approved");
                }
                else if (selection == 2)
                {
                    tickets.UpdateTicket(t.Id, "denied");
                }
                else if (selection == 3)
                {
                    continue;
                }
                else
                {
                    Console.WriteLine("Invalid Option skipping...");
                }
            }
            ManagerInteraction();
        }
        else if (selection == 3)
        {
            return;
        }
        else
        {
            DoInputError("Invalid Selection");
            ManagerInteraction();
        }

    }

    public static void EmployeeInteraction()
    {
        Console.Clear();
        Console.WriteLine($"Welecome Employee, {loggedInUser}!");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine(" [1] View Past Tickets");
        Console.WriteLine(" [2] Create Reimbursement Ticket");
        Console.WriteLine(" [3] Logout");
        Console.WriteLine("----------------------------------------");
        int selection = GetSelection();
        if (selection == 1)
        {
            foreach (Ticket t in tickets.GetTickets(loggedInUser))
            {
                Console.WriteLine(t.ToString());
            }
            DoInputError("");
            EmployeeInteraction();
        }
        else if (selection == 2)
        {
            Console.Write("Reimbursement Amount: ");
            decimal amt = GetDecimal();
            if (amt < 0)
            {
                DoInputError("Invalid Amount given");
                EmployeeInteraction();
                return;
            }
            Console.Write("Description of Ticket: ");
            string? desc = Console.ReadLine();
            if (desc != null)
            {
                tickets.AddTicket(new Ticket(loggedInUser.Username, amt, desc));
                Console.WriteLine("Ticket submitted");
            }
            else
            {
                Console.WriteLine("Cannot add null description ticket");
            }
            EmployeeInteraction();
        }
        else if (selection == 3)
        {
            return;
        }
        else
        {
            DoInputError("Invalid Selection");
            EmployeeInteraction();
        }


    }

    public static void LoginInteraction()
    {
        Console.Clear();
        Console.Write("Username: ");
        string? username = Console.ReadLine();
        Console.Write("Password: ");
        string? password = Console.ReadLine();
        if (username == null || password == null)
        {
            return;
        }

        User? temp = users.login(username, password);
        if (temp == null)
        {
            DoInputError("Invalid Username or Password");
            return;
        }

        loggedInUser = temp;
        if (temp.IsManager)
        {
            ManagerInteraction();
        }
        else
        {
            EmployeeInteraction();
        }
    }

    public static void RegisterInteraction()
    {
        Console.Clear();
        Console.Write("Will this user be a manager [y/n]? ");
        string? man = Console.ReadLine();
        if (man == null)
        {
            return;
        }
        bool m;
        if (man.ToLower().Equals("y"))
        {
            m = true;
        }
        else if (man.ToLower().Equals("n"))
        {
            m = false;
        }
        else
        {
            DoInputError("Invalid Selection");
            return;
        }

        Console.Write("New Username: ");
        string? username = Console.ReadLine();
        Console.Write("New Password: ");
        string? password = Console.ReadLine();
        if (username == null || password == null)
        {
            return;
        }
        User temp = new User(username, "", m);
        temp.Password = User.Hash(password, temp.Salt);
        if (users.AddUser(temp))
        {
            return;
        }
        else
        {
            DoInputError("Username already in use");
        }
    }
}