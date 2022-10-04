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

using Users;

static int GetSelection()
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

static double GetDouble()
{
    string? selection = Console.ReadLine();
    if (selection != null)
    {
        double value;
        bool check = double.TryParse(selection, out value);
        if (check)
        {
            return value;
        }
    }
    return -1;
}

static void ManagerInteraction()
{
    Console.WriteLine("Welcome Manager!");
    Console.WriteLine("----------------------------------------");
    Console.WriteLine(" [1] View list");
    Console.WriteLine(" [2] Exit");
    Console.WriteLine("----------------------------------------");
    int selection = GetSelection();
    if (selection == 1)
    {
        Console.WriteLine("Yee haw! this section of the system does not yet exist");
    }
    else if (selection == 2)
    {
        Environment.Exit(0);
    }
    else
    {
        Console.WriteLine("Invalid selection");
    }
}

static void EmployeeInteraction()
{
    Console.WriteLine("Welecome Employee!");
    Console.WriteLine("----------------------------------------");
    Console.WriteLine(" [1] View Past Tickets");
    Console.WriteLine(" [2] Create Reimbursement Ticket");
    Console.WriteLine(" [3] Exit");
    Console.WriteLine("----------------------------------------");
    int selection = GetSelection();
    if (selection == 1)
    {
        Console.WriteLine("Yee haw! this part of the system does not exist yet");
    }
    else if (selection == 2)
    {
        Console.Write("Reimbursement Amount: ");
        double amt = GetDouble();
        Console.Write("Description of Ticket: ");
        string? desc = Console.ReadLine();
        if (desc == null)
        {
            Console.WriteLine("Null description");
        }
        Console.WriteLine("Cant Do anything with this yet, but heres what you entered");
        Console.WriteLine(amt);
        Console.WriteLine(desc);
    }
    else if (selection == 3)
    {
        Environment.Exit(0);
    }
    else
    {
        Console.WriteLine("Invalid selection");
    }

}

User temp = new User("test", "test", false);

while (true)
{
    Console.WriteLine("Welcome To the Reimbursemt System");
    Console.WriteLine("----------------------------------------");
    Console.WriteLine(" [1] Login");
    Console.WriteLine(" [2] Register");
    Console.WriteLine(" [3] Exit");
    Console.WriteLine("----------------------------------------");
    int selection = GetSelection();
    if (selection == 1)
    {
        Console.Write("Username: ");
        string? username = Console.ReadLine();
        Console.Write("Password: ");
        string? password = Console.ReadLine();
        if (username == null || password == null)
        {
            continue;
        }
        if (temp.checkCredentials(username, password))
        {
            if (temp.IsManager)
            {
                ManagerInteraction();
            }
            else
            {
                EmployeeInteraction();
            }
        }
    }
    else if (selection == 2)
    {
        // Employee
        EmployeeInteraction();
    }
    else if (selection == 3)
    {
        // Quit
        Environment.Exit(0);
    }
    else
    {
        Console.WriteLine("Invalid selection");
    }
}