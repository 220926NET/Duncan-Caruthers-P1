using Services;

namespace DataAccess;

public class FileStorage : IStorage
{
    public string? UserPath { get; set; }
    public string? TicketPath { get; set; }

    public TicketHandler readTickets()
    {
        throw new NotImplementedException();
    }

    public LoginHandler readUsers()
    {
        throw new NotImplementedException();
    }

    public void writeTickets(TicketHandler tickets)
    {
        throw new NotImplementedException();
    }

    public void writeUsers(LoginHandler input)
    {
        throw new NotImplementedException();
    }
}