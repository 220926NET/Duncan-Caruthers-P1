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
        connection.Open();
        SqlCommand cmd = new SqlCommand("insert into Tickets (stat,creator,cost,descr) VALUES (@s,@cr,@cost,@descr);", connection);
        cmd.Parameters.AddWithValue("@s", ticket.Status);
        cmd.Parameters.AddWithValue("@cr", ticket.Creator);
        cmd.Parameters.AddWithValue("@cost", ticket.Amount);
        cmd.Parameters.AddWithValue("@descr", ticket.Description);
        cmd.ExecuteNonQuery();

        //int id = (int)cmd.ExecuteScalar();
        //ticket.Id = id;
        connection.Close();
    }

    public void AddUser(User usr)
    {
        connection.Open();
        SqlCommand cmd = new SqlCommand("insert into Users (usr,passwd,isManager) VALUES (@u,@p,@m);", connection);
        cmd.Parameters.AddWithValue("@u", usr.Username);
        cmd.Parameters.AddWithValue("@p", usr.Password);
        cmd.Parameters.AddWithValue("@m", (usr.IsManager) ? 1 : 0);
        cmd.ExecuteNonQuery();
        connection.Close();
    }

    public Ticket? GetTicket(int id)
    {
        connection.Open();
        SqlCommand cmd = new SqlCommand("select * from Tickets where id = @i;", connection);
        cmd.Parameters.AddWithValue("@i", id);

        SqlDataReader reader = cmd.ExecuteReader();
        if (!reader.HasRows) return null;

        reader.Read();
        Ticket ticket = new Ticket((int)reader["id"], (string)reader["stat"], (string)reader["creator"], (decimal)reader["cost"], (string)reader["descr"]);
        connection.Close();
        return ticket;
    }

    public List<Ticket> GetTickets()
    {
        List<Ticket> tickets = new List<Ticket>();
        connection.Open();
        SqlCommand cmd = new SqlCommand("select * from Tickets;", connection);

        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                tickets.Add(new Ticket((int)reader["id"], (string)reader["stat"], (string)reader["creator"], (decimal)reader["cost"], (string)reader["descr"]));
            }
        }
        connection.Close();
        return tickets;
    }

    public List<User> GetUsers()
    {
        List<User> users = new List<User>();
        connection.Open();
        SqlCommand cmd = new SqlCommand("select * from Users;", connection);

        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                users.Add(new User((string)reader["usr"], (string)reader["passwd"], ((int)reader["isManager"] == 0) ? false : true));
            }
        }
        connection.Close();
        return users;
    }

    public void UpdateTicket(int id, string newStatus)
    {
        connection.Open();
        SqlCommand cmd = new SqlCommand("update Tickets set stat = @s where id = @i", connection);
        cmd.Parameters.AddWithValue("@s", newStatus);
        cmd.Parameters.AddWithValue("@i", id);
        cmd.ExecuteNonQuery();
        connection.Close();
    }
}