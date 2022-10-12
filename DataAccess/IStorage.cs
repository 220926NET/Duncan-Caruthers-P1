using Models;

namespace DataAccess;

public interface IStorage
{
    void AddTicket(Ticket ticket);
    Ticket? GetTicket(int id);
    List<Ticket> GetTickets();
    void AddUser(User usr);
    List<User> GetUsers();
}