using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class ERSController : ControllerBase
{
    private readonly ILogger<ERSController> _logger;

    public ERSController(ILogger<ERSController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "RegisterUser")]
    [Route("register/{userName}")]
    public ActionResult<HttpResponse> RegisterUser(string userName)
    {
        throw new NotImplementedException();
    }

    [HttpPost(Name = "LoginUser")]
    [Route("login/{userName}")]
    public ActionResult<HttpResponse> LoginUser(string userName)
    {
        throw new NotImplementedException();
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