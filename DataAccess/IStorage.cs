using Models;

namespace DataAccess;

public interface IStorage
{
    void AddTicket(Ticket ticket);
    bool UpdateTicket(int id, string newStatus);
    Ticket? GetTicket(int id);
    List<Ticket> GetTickets();
    bool AddUser(User usr);
    List<User> GetUsers();
}