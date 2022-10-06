namespace Services;


using Models;

public class TicketHandler
{
    private List<Ticket> tickets;

    public TicketHandler()
    {
        tickets = new List<Ticket>();
    }

    public void AddTicket(User creator, double amt, string desc)
    {
        tickets.Add(new Ticket(creator, amt, desc));
    }

    public Ticket GetTicket(int id)
    {
        return tickets.ElementAt(id);
    }

    public List<Ticket> GetTickets(User usr)
    {
        List<Ticket> temp = new List<Ticket>();
        foreach (Ticket t in tickets)
        {
            if (t.Creator == usr)
            {
                temp.Add(t);
            }
        }
        return temp;
    }

    public override string ToString()
    {
        string output = "";
        for (int i = 0; i < tickets.Count; i++)
        {
            output += "" + i + '\t' + tickets.ElementAt(i).ToString() + '\n';

        }
        return output;
    }

    public List<Ticket> GetPending()
    {
        List<Ticket> temp = new List<Ticket>();
        foreach (Ticket t in tickets)
        {
            if (t.Status == "pending")
            {
                temp.Add(t);
            }
        }
        return temp;
    }
}