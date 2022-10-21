using System.Collections.Specialized;
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
    private readonly LoginHandler _users;
    private readonly TicketHandler _tickets;
    public ERSController(ILogger<ERSController> logger)
    {
        _logger = logger;
        _users = new(new DatabaseStorage());
        _tickets = new(new DatabaseStorage());
    }

    [HttpPost(Name = "RegisterUser")]
    [Route("register")]
    public ActionResult RegisterUser()
    {
        if (ModelState.IsValid)
        {
            FormCollection form = (FormCollection)Request.Form;
            Models.User temp = new Models.User(form["username"], "", false);
            temp.Password = Models.User.Hash(form["password"], temp.Salt);
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
                return Ok(new { Id = temp.Id });
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

                        _tickets.AddTicket(new Ticket(-1, "pending", temp.Username, amt, form["description"]));
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
    [Route("ticket/veiw")]
    public ActionResult ViewAll()
    {
        throw new NotImplementedException();
    }

    [HttpGet(Name = "ViewByStatus")]
    [Route("ticket/veiw/{status}")]
    public ActionResult ViewByStatus(string status)
    {
        throw new NotImplementedException();
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
            if (!temp.IsManager)
            {
                return Unauthorized($"User {temp.Username} is not a manager");
            }
            _tickets.UpdateTicket(id, "denied");
            return Ok("Status updated");
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
            if (!temp.IsManager)
            {
                return Unauthorized($"User {temp.Username} is not a manager");
            }
            _tickets.UpdateTicket(id, "approved");
            return Ok("Status updated");
        }
        return BadRequest("Invalid Model State");
    }
}