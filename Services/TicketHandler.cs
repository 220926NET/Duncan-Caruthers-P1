using Models;
using DataAccess;

namespace Services;

public class TicketHandler
{
    private IStorage storage;

    public TicketHandler(IStorage storage)
    {
        this.storage = storage;
    }

    public TicketHandler()
    {
    }

    public void AddTicket(Ticket ticket)
    {
        storage.AddTicket(ticket);
    }

    public Ticket? GetTicket(int id)
    {
        return storage.GetTicket(id);
    }

    public List<Ticket> GetTickets(User usr)
    {
        List<Ticket> temp = new List<Ticket>();
        foreach (Ticket t in storage.GetTickets())
        {
            if (t.creator == usr.username)
            {
                temp.Add(t);
            }
        }
        return temp;
    }

    public List<Ticket> GetTickets()
    {
        return storage.GetTickets();
    }

    public override string ToString()
    {
        string output = "";
        List<Ticket> tickets = storage.GetTickets();
        for (int i = 0; i < tickets.Count; i++)
        {
            output += "" + i + '\t' + tickets.ElementAt(i).ToString() + '\n';

        }
        return output;
    }

    public List<Ticket> GetPending()
    {
        List<Ticket> temp = new List<Ticket>();
        foreach (Ticket t in storage.GetTickets())
        {
            if (t.status == "pending")
            {
                temp.Add(t);
            }
        }
        return temp;
    }

    public bool UpdateTicket(int id, string v)
    {
        return storage.UpdateTicket(id, v);
    }
}