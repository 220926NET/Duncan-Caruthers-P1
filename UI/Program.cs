﻿/*
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

while (true)
{
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
        Environment.Exit(0);
    }
    else
    {
        Console.WriteLine("Invalid selection");
    }
}

public class UIHandler
{
    private static LoginHandler users = new LoginHandler();
    private static TicketHandler tickets = new TicketHandler();

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

    public static double GetDouble()
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

    public static void ManagerInteraction()
    {
        Console.WriteLine("Welcome Manager!");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine(" [1] View list");
        Console.WriteLine(" [2] Exit");
        Console.WriteLine("----------------------------------------");
        int selection = GetSelection();
        if (selection == 1)
        {
            Console.WriteLine(tickets.ToString());
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

    public static void EmployeeInteraction()
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
            if (desc != null)
            {
                tickets.addTicket(amt, desc);
                Console.WriteLine("Ticket submitted");
            }
            else
            {
                Console.WriteLine("Cannot add null description ticket");
            }
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

    public static void LoginInteraction()
    {
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
            Console.WriteLine("Invalid Username or password");
            return;
        }

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
        Console.Write("Will this user be a manager [y/N]?");
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
            Console.WriteLine("Invalid Input");
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

        if (users.addUser(username, password, m))
        {
            return;
        }
        else
        {
            Console.WriteLine("Username taken");
        }
    }
}