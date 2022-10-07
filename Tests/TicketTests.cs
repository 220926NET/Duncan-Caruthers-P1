namespace Tests;

using Models;

public class TicketTests
{
    public static User u = new User("", "", false);

    [Fact]
    public void TicketCreated()
    {
        Ticket t = new Ticket(u, 1, "test");
        Assert.NotNull(t);
        Assert.Equal("pending", t.Status);
        Assert.Equal("test", t.Description);
        Assert.Equal(1.0, t.Amount);
    }

    [Fact]
    public void TicketExceptions()
    {
        Ticket t = new(u, 1, "test");
        Assert.Throws<ArgumentException>(() => t.Amount = -1);
        Assert.Throws<ArgumentException>(() => t.Description = "");
        Assert.Throws<ArgumentException>(() => t.Status = "NOT");
    }
}