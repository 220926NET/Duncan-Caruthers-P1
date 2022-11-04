using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ERSController : ControllerBase
{
    private readonly ILogger<ERSController> _logger;
    private readonly LoginHandler? _users;
    private readonly TicketHandler? _tickets;
    public ERSController(ILogger<ERSController> logger)
    {
        _logger = logger;
        _users = null;
        _tickets = null;
    }

    [HttpGet(Name = "test")]
    [Route("howdy")]
    public ActionResult SayHello()
    {
        return Ok("Howdy!");
    }

    [HttpGet(Name = "GetPresentation")]
    [Route("present")]
    public ActionResult GetPresentation()
    {
        if (ModelState.IsValid)
        {
            return Ok(System.IO.File.ReadAllText("../Presentation.html"));
        }
        return BadRequest("Invalid Model State");
    }

    [HttpPost(Name = "RegisterUser")]
    [Route("register")]
    public ActionResult RegisterUser()
    {
        if (ModelState.IsValid)
        {
            FormCollection form = (FormCollection)Request.Form;
            Models.User temp = new Models.User(form["username"], "", false);
            temp.password = Models.User.Hash(form["password"], temp.Salt);
            if (_users.AddUser(temp))
            {
                return Ok(new { Username = form["username"] });
            }
            return BadRequest("Username is already in use");
        }
        return BadRequest("Invalid model state");
    }

    [HttpPost(Name = "LoginUser")]
    [Route("login")]
    public ActionResult LoginUser()
    {
        if (ModelState.IsValid)
        {
            FormCollection form = (FormCollection)Request.Form;
            Models.User? temp = _users.login(form["username"], form["password"]);
            if (temp != null)
            {
                return Ok(temp);
            }
            return Unauthorized("Username or password is invalid");
        }
        return BadRequest("Invalid model state");
    }

    [HttpPost(Name = "SubmitTicket")]
    [Route("ticket/submit")]
    public ActionResult SubmitTicket()
    {
        if (ModelState.IsValid)
        {
            FormCollection form = (FormCollection)Request.Form;

            if (string.IsNullOrEmpty(form["id"]) || string.IsNullOrEmpty(form["description"]) || string.IsNullOrEmpty(form["amount"]))
            {
                return BadRequest("One of the nessasary form values is missing!!");
            }

            Guid id;
            if (Guid.TryParse(form["id"], out id))
            {
                Models.User? temp = _users.GetUser(id);
                if (temp != null)
                {
                    decimal amt;
                    if (decimal.TryParse(form["amount"], out amt))
                    {

                        _tickets.AddTicket(new Ticket(-1, "pending", temp.username, amt, form["description"]));
                        return Ok("Success!");
                    }
                    return BadRequest("Improper Amount form");
                }
                return Unauthorized("This is not a register user id");
            }
            else
            {
                return BadRequest("Improper ID");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpGet(Name = "ViewAll")]
    [Route("ticket/veiw/{id}")]
    public ActionResult ViewAll(Guid id)
    {
        if (ModelState.IsValid)
        {
            Models.User? temp = _users.GetUser(id);
            if (temp != null)
            {
                if (temp.ismanager) return Ok(_tickets.GetTickets());
                List<Ticket> tickets = _tickets.GetTickets();
                List<Ticket> list = new List<Ticket>();
                foreach (Ticket t in tickets)
                {
                    if (t.creator.Equals(temp.username))
                    {
                        list.Add(t);
                    }
                }
                return Ok(list);
            }
            return Unauthorized("This is not a register user id");
        }
        return BadRequest("Invalid model state");
    }

    [HttpGet(Name = "ViewByStatus")]
    [Route("ticket/veiw/{id}/{status}")]
    public ActionResult ViewByStatus(Guid id, string status)
    {
        if (ModelState.IsValid)
        {
            Models.User? temp = _users.GetUser(id);
            if (temp != null)
            {
                if (temp.ismanager)
                {
                    List<Ticket> ticketsAll = _tickets.GetTickets();
                    List<Ticket> l = new List<Ticket>();
                    foreach (Ticket t in ticketsAll)
                    {
                        if (t.status.Equals(status))
                        {
                            l.Add(t);
                        }
                    }
                    return Ok(l);
                }

                List<Ticket> tickets = _tickets.GetTickets();
                List<Ticket> list = new List<Ticket>();
                foreach (Ticket t in tickets)
                {
                    if (t.creator.Equals(temp.username) && t.status.Equals(status))
                    {
                        list.Add(t);
                    }
                }
                return Ok(list);
            }
            return Unauthorized("This is not a register user id");
        }
        return BadRequest("Invalid model state");
    }

    [HttpPut(Name = "DenyTicket")]
    [Route("ticket/deny/{id}")]
    public ActionResult DenyTicket(int id)
    {
        if (ModelState.IsValid)
        {
            FormCollection form = (FormCollection)Request.Form;
            if (string.IsNullOrEmpty(form["id"]))
            {
                return BadRequest("This request requires an ID");
            }
            Guid guid;
            if (!Guid.TryParse(form["id"], out guid))
            {
                return BadRequest("The id is in inproper form");
            }
            User? temp = _users.GetUser(guid);
            if (temp == null)
            {
                return Unauthorized("Not a valid user id");
            }
            if (!temp.ismanager)
            {
                return Unauthorized($"User {temp.username} is not a manager");
            }
            if (_tickets.UpdateTicket(id, "denied"))
            {
                return Ok("Status updated");
            }
            else
            {
                return BadRequest("Cannot update non existent or non pending ticket");
            }
        }
        return BadRequest("Invalid Model State");
    }

    [HttpPut(Name = "ApproveTicket")]
    [Route("ticket/approve/{id}")]
    public ActionResult ApproveTicket(int id)
    {
        if (ModelState.IsValid)
        {
            FormCollection form = (FormCollection)Request.Form;
            if (string.IsNullOrEmpty(form["id"]))
            {
                return BadRequest("This request requires an ID");
            }
            Guid guid;
            if (!Guid.TryParse(form["id"], out guid))
            {
                return BadRequest("The id is in inproper form");
            }
            User? temp = _users.GetUser(guid);
            if (temp == null)
            {
                return Unauthorized("Not a valid user id");
            }
            if (!temp.ismanager)
            {
                return Unauthorized($"User {temp.username} is not a manager");
            }
            if (_tickets.UpdateTicket(id, "approved"))
            {
                return Ok("Status updated");
            }
            else
            {
                return BadRequest("Cannot update non existent or non pending ticket");
            }
        }
        return BadRequest("Invalid Model State");
    }
}