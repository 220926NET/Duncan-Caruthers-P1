/*
    Planning for User Tickets:
        Class Ticket -> stores ticket information (amount,description,status,who made it)
        Class TIcketHandler -> Manages ticket database
*/

using Users;

namespace Tickets
{
    public class Ticket
    {
        public enum States
        {
            PENDING, APROVED, DENIED
        }
        public double Amount { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public User Creator { get; set; }

        public Ticket(double amt, string desc, User creator)
        {
            Amount = amt;
            Description = desc;
            Status = (int)States.PENDING;
            Creator = creator;
        }

        public void approve()
        {
            Status = (int)States.APROVED;
        }

        public void deny()
        {
            Status = (int)States.DENIED;
        }

        public string GetStatusString()
        {
            switch (Status)
            {
                case (int)States.PENDING:
                    return "pending";
                case (int)States.DENIED:
                    return "denied";
                case (int)States.APROVED:
                    return "approved";
            }
            return "n/a";
        }


        override public string ToString()
        {
            return Creator.UserName + "\t" + Amount + "\t\t" + Description + "\t" + GetStatusString();
        }
    }

    public class TicketHandler
    {
        private List<Ticket> tickets;

        public TicketHandler()
        {
            tickets = new List<Ticket>();
        }

        public void addTicket(User usr, double amt, string desc)
        {
            tickets.Add(new Ticket(amt, desc, usr));
        }

        public void apporveTicket(int index)
        {
            tickets.ElementAt(index).approve();
        }

        public void denyTicket(int index)
        {
            tickets.ElementAt(index).deny();
        }

        public void printTickets(User usr)
        {
            if (usr.IsManager)
            {
                for (int i = 0; i < tickets.Count; i++)
                {
                    Console.WriteLine($" [{i}] " + tickets.ElementAt(i).ToString());
                }
            }
            else
            {
                for (int i = 0; i < tickets.Count; i++)
                {
                    if (tickets.ElementAt(i).Creator.Equals(usr))
                    {
                        Console.WriteLine($" [{i}] " + tickets.ElementAt(i).ToString());
                    }
                }
            }
        }
    }
}