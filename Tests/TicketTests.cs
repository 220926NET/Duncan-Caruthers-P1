namespace Tests;

using Models;

public class TicketTests
{
    [Fact]
    public void TicketCreated()
    {
        Ticket t = new Ticket(1, "test");
        Assert.NotNull(t);
        Assert.Equal("pending", t.Status);
        Assert.Equal("test", t.Description);
        Assert.Equal(1.0, t.Amount);
    }

    [Fact]
    public void TicketExceptions()
    {
        Ticket t = new(1, "test");
        Assert.Throws<ArgumentException>(() => t.Amount = -1);
        Assert.Throws<ArgumentException>(() => t.Description = "");
        Assert.Throws<ArgumentException>(() => t.Status = "NOT");
    }
}