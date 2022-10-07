namespace DataAccess;

using Models;
using Services;

public interface IStorage
{
    void writeUsers(LoginHandler input);
    LoginHandler readUsers();

    void writeTickets(TicketHandler tickets);
    TicketHandler readTickets();
}