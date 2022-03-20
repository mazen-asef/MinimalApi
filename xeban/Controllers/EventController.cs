using System.Net;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using xeban.Common;
using xeban.Models;
using xeban.Responses;
using xeban.Services;

namespace xeban.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class EventController : ControllerBase
{
    private readonly ILogger<BalanceController> _logger;
    private readonly IHttpContextAccessor _acessor;
    private readonly AccountService _accountService;

    public EventController(ILogger<BalanceController> logger, AccountService accountService, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _acessor = httpContextAccessor;
        _accountService = accountService;
    }

    [HttpPost(Name = "PostEvent")]
    public ActionResult<DepositEventResponse> Post(string type, int destination, int amount)
    {
        if (type != EventType.DEPOSIT_EVENT) 
            return NotFound(0);
        
        var account = _accountService.HandleDeposit(destination, amount);
        
        return account is null ? NotFound(0) : Created(_acessor.HttpContext?.Request.GetDisplayUrl() ?? string.Empty, account);
    }
}
