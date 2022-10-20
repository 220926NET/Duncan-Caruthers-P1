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
    [Route("ticket/{userName}/submit")]
    public ActionResult<HttpResponse> SubmitTicket(string userName)
    {
        throw new NotImplementedException();
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