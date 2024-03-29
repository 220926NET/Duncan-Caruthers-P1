using Microsoft.Data.SqlClient;
using Models;

namespace DataAccess;

public class DatabaseStorage : IStorage
{
    private SqlConnection connection;

    public DatabaseStorage()
    {
        this.connection = new SqlConnection(File.ReadAllText("./connection_string.txt"));

    }

    public static bool IsTheDatabaseLive()
    {
        try
        {
            SqlConnection connection = new SqlConnection(File.ReadAllText("./connection_string.txt"));
            connection.Open();
            SqlCommand cmd = new SqlCommand(";", connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            return true;
        }
        catch (SqlException)
        {
            return false;
        }
    }

    public void AddTicket(Ticket ticket)
    {
        connection.Open();
        SqlCommand cmd = new SqlCommand("sp_insert_ticket @stat=@s,@creator=@c,@cost=@cr,@description=@d", connection);
        cmd.Parameters.AddWithValue("@s", ticket.status);
        cmd.Parameters.AddWithValue("@c", ticket.creator);
        cmd.Parameters.AddWithValue("@cr", ticket.amount);
        cmd.Parameters.AddWithValue("@d", ticket.description);
        cmd.ExecuteNonQuery();

        //int id = (int)cmd.ExecuteScalar();
        //ticket.Id = id;
        connection.Close();
    }

    public bool AddUser(User usr)
    {
        try
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("insert into Users values (@id,@u,@p,@i,@s);", connection);
            cmd.Parameters.AddWithValue("@id", usr.id);
            cmd.Parameters.AddWithValue("@u", usr.username);
            cmd.Parameters.AddWithValue("@p", usr.password);
            cmd.Parameters.AddWithValue("@i", (usr.ismanager) ? 1 : 0);
            cmd.Parameters.AddWithValue("@s", Convert.ToHexString(usr.Salt));
            cmd.ExecuteNonQuery();

            connection.Close();
            return true;
        }
        catch (SqlException)
        {
            connection.Close();
            return false;
        }
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
                User temp = new User((Guid)reader["id"], (string)reader["usr"], (string)reader["passwd"], ((int)reader["isManager"] == 0) ? false : true);

                temp.Salt = Convert.FromHexString((string)reader["salt"]);

                users.Add(temp);
            }
        }
        connection.Close();
        return users;
    }

    public bool UpdateTicket(int id, string newStatus)
    {
        connection.Open();

        SqlCommand c = new SqlCommand("select * from Tickets where id = @i;", connection);
        c.Parameters.AddWithValue("@i", id);
        SqlDataReader r = c.ExecuteReader();
        if (!r.HasRows)
        {
            r.Close();
            connection.Close();
            return false;
        }
        r.Close();

        SqlCommand cmd = new SqlCommand("update Tickets set stat = @s where id = @i", connection);
        cmd.Parameters.AddWithValue("@s", newStatus);
        cmd.Parameters.AddWithValue("@i", id);
        cmd.ExecuteNonQuery();

        connection.Close();
        return true;
    }
}