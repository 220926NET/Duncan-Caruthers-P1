namespace Models;

public class Ticket
{

    public User Creator { get; set; }
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

    private int _status;
    public string Status
    {
        get
        {
            switch (_status)
            {
                case 0:
                    return "pending";
                case 1:
                    return "approved";
                case 2:
                    return "denied";
                default:
                    return "";
            }
        }
        set
        {
            if (value.Equals("pending"))
            {
                _status = 0;
            }
            else if (value.Equals("approved"))
            {
                _status = 1;
            }
            else if (value.Equals("denied"))
            {
                _status = 2;
            }
            else
            {
                throw new ArgumentException("Status must be: pending, approved, or denied");
            }
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

    public Ticket(User creator, double amt, string desc)
    {
        Amount = amt;
        _description = "";
        Description = desc;
        Creator = creator;
        Status = "pending";
    }

    public override string ToString()
    {
        return "" + Amount + '\t' + Description + '\t' + Status;
    }
}