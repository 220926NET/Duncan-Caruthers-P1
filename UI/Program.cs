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

bool done = false;
while (!done)
{
    Console.WriteLine("Welcome To the Reimbursemt System");
    Console.WriteLine("----------------------------------------");
    Console.WriteLine(" [1] Manager");
    Console.WriteLine(" [2] Employee ");
    Console.WriteLine(" [3] Exit");
    Console.WriteLine("----------------------------------------");
    string selectionRaw = Console.ReadLine();
    int selection = int.Parse(selectionRaw);
    if (selection == 1)
    {
        // Manager
        Console.WriteLine("Welcome Manager!");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine(" [1] View list");
        Console.WriteLine(" [2] Exit");
        Console.WriteLine("----------------------------------------");
        selectionRaw = Console.ReadLine();
        selection = int.Parse(selectionRaw);
        if (selection == 1)
        {
            Console.WriteLine("Yee haw! this section of the system does not yet exist");
        }
        else if (selection == 2)
        {
            Environment.Exit(0);
        }
    }
    else if (selection == 2)
    {
        // Employee
        Console.Write("Welecome Employee!");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine(" [1] View Past Tickets");
        Console.WriteLine(" [2] Create Reimbursement Ticket");
        Console.WriteLine(" [3] Exit");
        Console.WriteLine("----------------------------------------");
        selectionRaw = Console.ReadLine();
        selection = int.Parse(selectionRaw);
        if (selection == 1)
        {
            Console.WriteLine("Yee haw! this part of the system does not exist yet");
        }
        else if (selection == 2)
        {
            Console.Write("Reimbursement Amount: ");
            selectionRaw = Console.ReadLine();
            double amt = double.Parse(selectionRaw);
            Console.Write("Description of Ticket: ");
            selectionRaw = Console.ReadLine();
            Console.WriteLine("Cant Do anything with this yet, but heres what you entered");
            Console.WriteLine(amt);
            Console.WriteLine(selectionRaw);
        }
        else if (selection == 3)
        {
            Environment.Exit(0);
        }

    }
    else if (selection == 3)
    {
        // Quit
        Environment.Exit(0);
    }
}