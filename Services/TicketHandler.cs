namespace Services;


using Models;

public class TicketHandler
{
    private List<Ticket> tickets;

    public TicketHandler()
    {
        tickets = new List<Ticket>();
    }

    public void addTicket(double amt, string desc)
    {
        tickets.Add(new Ticket(amt, desc));
    }
}