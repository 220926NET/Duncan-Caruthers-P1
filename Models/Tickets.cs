namespace Models;

public class Ticket
{
    public int id { get; set; } = -1;
    public string status { get; set; } = "pending";
    public string creator { get; set; } = "";
    public decimal amount { get; set; }
    public string description { get; set; } = "";

    public Ticket() { }

    public Ticket(int id, string status, string creator, decimal amount, string description)
    {
        this.id = id;
        this.status = status;
        this.creator = creator;
        this.amount = amount;
        this.description = description;
    }

    public Ticket(string creator, decimal amount, string description)
    {
        this.creator = creator;
        this.amount = amount;
        this.description = description;
    }

    public override string ToString()
    {
        return $"|{" " + status,-20}|{" " + creator,-20}|{" " + amount,-20}|{" " + description}";
    }
}