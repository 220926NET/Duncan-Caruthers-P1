using Microsoft.Data.SqlClient;
using Models;

namespace DataAccess;

public class DatabaseStorage : IStorage
{
    private SqlConnection connection;

    public DatabaseStorage()
    {
        this.connection = new SqlConnection($"Server=tcp:duncan075.database.windows.net,1433;Initial Catalog=duncan075_DB;Persist Security Info=False;User ID=duncan;Password=$P1a1s1s1w1o1r1d1$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    }

    public void AddTicket(Ticket ticket)
    {
        throw new NotImplementedException();
    }

    public void AddUser(User usr)
    {
        throw new NotImplementedException();
    }

    public Ticket GetTicket(int id)
    {
        throw new NotImplementedException();
    }

    public List<Ticket> GetTickets()
    {
        throw new NotImplementedException();
    }

    public List<User> GetUsers()
    {
        throw new NotImplementedException();
    }
}