namespace Models;

public class Ticket
{
    public int Id { get; set; } = -1;
    public string Status { get; set; } = "pending";
    public string Creator { get; set; }
    public double Amount { get; set; }
    public string Description { get; set; }


    public Ticket(int id, string status, string creator, double amount, string description)
    {
        Id = id;
        Status = status;
        Creator = creator;
        Amount = amount;
        Description = description;
    }

    public Ticket(string creator, double amount, string description)
    {
        Creator = creator;
        Amount = amount;
        Description = description;
    }
}