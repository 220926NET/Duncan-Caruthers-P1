namespace Tickets;

public class Ticket
{
    private string _description;
    public string Description
    {
        get
        {
            return _description;
        }

        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Description cannot be null or empty");
            }
            _description = value;
        }
    }

    private double _amount;
    public double Amount
    {
        get
        {
            return _amount;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Amount must be greater than zero");
            }
            _amount = value;
        }
    }

    public Ticket(double amt, string desc)
    {
        Amount = amt;
        _description = "";
        Description = desc;
    }
}