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

using System.Text.Json;
using Models;

// if (!DatabaseStorage.IsTheDatabaseLive())
// {
//     UIHandler.DoInputError("It would appear the database is down");
//     UIHandler.DoExit(1);
// }

UIHandler.client.BaseAddress = new Uri("http://localhost:5093/");
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
        await UIHandler.LoginInteraction();
    }
    else if (selection == 2)
    {
        // Employee
        await UIHandler.RegisterInteraction();
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
    public static readonly HttpClient client = new HttpClient();
    private static Guid id = Guid.Empty;

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

    public async static Task ManagerInteraction()
    {
        Console.Clear();
        Console.WriteLine($"Welcome Manager!");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine(" [1] View Past Tickets");
        Console.WriteLine(" [2] Process Pending Tickets"); ;
        Console.WriteLine(" [3] Logout");
        Console.WriteLine("----------------------------------------");
        int selection = GetSelection();
        if (selection == 1)
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine($"|{" Status ",-20}|{" Creator ",-20}|{" Amount ",-20}|{" Description "}");
            Console.WriteLine("------------------------------------------------------------------------------------------------");

            HttpResponseMessage msg = await client.GetAsync($"ERS/ticket/veiw/{id}");
            List<Ticket>? tickets = JsonSerializer.Deserialize<List<Ticket>>(await msg.Content.ReadAsStringAsync());

            if (tickets == null)
            {
                UIHandler.DoInputError("Message recieve failure...");
            }
            else
            {
                foreach (Ticket t in tickets)
                {
                    Console.WriteLine(t.ToString());
                    Console.WriteLine("------------------------------------------------------------------------------------------------");
                }
                DoInputError("");
            }

            await ManagerInteraction();
        }
        else if (selection == 2)
        {
            HttpResponseMessage msg = await client.GetAsync($"ERS/ticket/veiw/{id}/pending");
            List<Ticket>? tickets = JsonSerializer.Deserialize<List<Ticket>>(await msg.Content.ReadAsStringAsync());

            if (tickets == null)
            {
                DoInputError("Unable to deserialize object");
            }
            else
            {
                foreach (Ticket t in tickets)
                {
                    Console.WriteLine("Ticket: " + t.ToString());
                    Console.WriteLine("[1] Approve [2] Deny [3] Skip");
                    selection = GetSelection();
                    if (selection == 1)
                    {
                        HttpResponseMessage m = await client.PutAsync($"ERS/ticket/approve/{t.Id}", new FormUrlEncodedContent(new[] {
                            new KeyValuePair<string,string> ("id",id.ToString())
                        }));

                        if (m.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            DoInputError("Was unable to update the ticket");
                        }
                    }
                    else if (selection == 2)
                    {
                        HttpResponseMessage m = await client.PutAsync($"ERS/ticket/denied/{t.Id}", new FormUrlEncodedContent(new[] {
                            new KeyValuePair<string,string> ("id",id.ToString())
                        }));

                        if (m.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            DoInputError("Was unable to update the ticket");
                        }
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
            }
            await ManagerInteraction();
        }
        else if (selection == 3)
        {
            return;
        }
        else
        {
            DoInputError("Invalid Selection");
            await ManagerInteraction();
        }

    }

    public async static Task EmployeeInteraction()
    {
        Console.Clear();
        Console.WriteLine($"Welecome Employee!");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine(" [1] View Past Tickets");
        Console.WriteLine(" [2] Create Reimbursement Ticket");
        Console.WriteLine(" [3] Logout");
        Console.WriteLine("----------------------------------------");
        int selection = GetSelection();
        if (selection == 1)
        {

            Console.WriteLine("------------------------------------------------------------------------------------------------");
            Console.WriteLine($"|{" Status ",-20}|{" Creator ",-20}|{" Amount ",-20}|{" Description "}");
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            HttpResponseMessage msg = await client.GetAsync($"ERS/ticket/veiw/{id}");
            List<Ticket>? tickets = JsonSerializer.Deserialize<List<Ticket>>(await msg.Content.ReadAsStringAsync());

            if (tickets == null)
            {
                UIHandler.DoInputError("Message recieve failure...");
            }
            else
            {
                foreach (Ticket t in tickets)
                {
                    Console.WriteLine(t.ToString());
                    Console.WriteLine("------------------------------------------------------------------------------------------------");
                }
                DoInputError("");
            }
            await EmployeeInteraction();
        }
        else if (selection == 2)
        {
            Console.Write("Reimbursement Amount: ");
            decimal amt = GetDecimal();
            if (amt < 0)
            {
                DoInputError("Invalid Amount given");
                await EmployeeInteraction();
                return;
            }
            Console.Write("Description of Ticket: ");
            string? desc = Console.ReadLine();
            if (desc != null)
            {
                HttpResponseMessage m = await client.PutAsync($"ERS/ticket/submit", new FormUrlEncodedContent(new[] {
                            new KeyValuePair<string,string> ("id",id.ToString()),
                            new KeyValuePair<string, string> ("amount",Convert.ToString(amt)),
                            new KeyValuePair<string, string>("description",desc)
                }));

                if (m.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    DoInputError("Was unable to update the ticket");
                }
                Console.WriteLine("Ticket submitted");
            }
            else
            {
                Console.WriteLine("Cannot add null description ticket");
            }
            await EmployeeInteraction();
        }
        else if (selection == 3)
        {
            return;
        }
        else
        {
            DoInputError("Invalid Selection");
            await EmployeeInteraction();
        }


    }

    public async static Task LoginInteraction()
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

        HttpResponseMessage msg = await client.PostAsync("ERS/login", new FormUrlEncodedContent(new[] {
            new KeyValuePair<string,string>("username",username),
            new KeyValuePair<string, string>("password",password)
        }));
        User? temp = JsonSerializer.Deserialize<User>(await msg.Content.ReadAsStringAsync());

        if (temp == null)
        {
            DoInputError("Invalid Username or Password");
            return;
        }

        id = temp.Id;
        if (temp.IsManager)
        {
            await ManagerInteraction();
        }
        else
        {
            await EmployeeInteraction();
        }
    }

    public async static Task RegisterInteraction()
    {
        Console.Clear();

        Console.Write("New Username: ");
        string? username = Console.ReadLine();
        Console.Write("New Password: ");
        string? password = Console.ReadLine();
        if (username == null || password == null)
        {
            return;
        }

        HttpResponseMessage msg = await client.PostAsync("ERS/register", new FormUrlEncodedContent(new[] {
            new KeyValuePair<string,string>("username",username),
            new KeyValuePair<string,string> ("password",password)
        }));

        if (msg.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return;
        }
        else
        {
            DoInputError("Username already in use");
        }
    }
}