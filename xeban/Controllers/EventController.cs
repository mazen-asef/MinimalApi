using System.Net;
using Microsoft.AspNetCore.Mvc;
using xeban.Common;
using xeban.Services;

namespace xeban.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class EventController : ControllerBase
{
    private readonly ILogger<BalanceController> _logger;
    private readonly AccountService _accountService;

    public EventController(ILogger<BalanceController> logger, AccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    [HttpPost(Name = "PostEvent")]
    public ActionResult Post(string type, int destination, int amount, int origin = 0)
    {
        switch (type)
        {
            case EventType.DEPOSIT_EVENT:
                var depositEvent = _accountService.HandleDeposit(destination, amount);
                return depositEvent is null ? StatusCode((int) HttpStatusCode.NotFound, 0) : StatusCode((int) HttpStatusCode.Created, depositEvent);;
                break;
            case EventType.WITHDRAW_EVENT:
                var withdrawEvent = _accountService.HandleWithdrawal(destination, amount);
                
                return withdrawEvent is null ? StatusCode((int) HttpStatusCode.NotFound, 0) : StatusCode((int) HttpStatusCode.Created, withdrawEvent);;
                break;
        }

        return StatusCode((int) HttpStatusCode.NotFound, 0);
    }
}
