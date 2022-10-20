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
    public ActionResult<HttpResponse> SubmitTicket()
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
                    Console.WriteLine(form["amount"]);
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
    [Route("ticket/{userName}/veiw")]
    public ActionResult<HttpResponse> ViewAll(string userName)
    {
        throw new NotImplementedException();
    }

    [HttpGet(Name = "ViewByStatus")]
    [Route("ticket/{userName}/veiw/{status}")]
    public ActionResult<HttpResponse> ViewByStatus(string userName, string status)
    {
        throw new NotImplementedException();
    }

    [HttpPut(Name = "ProcessTicket")]
    [Route("ticket/process/{id}/{status}")]
    public ActionResult<HttpResponse> ProcessTicket(int id, string status)
    {
        throw new NotImplementedException();
    }
}