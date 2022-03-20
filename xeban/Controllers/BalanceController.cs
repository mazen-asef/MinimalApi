using Microsoft.AspNetCore.Mvc;
using xeban.Models;
using xeban.Services;

namespace xeban.Controllers;

[ApiController]
[Route("[controller]")]
public class BalanceController : ControllerBase
{
    private readonly ILogger<BalanceController> _logger;
    private readonly AccountService _accountService;

    public BalanceController(ILogger<BalanceController> logger, AccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    [HttpGet(Name = "GetBalance")]
    public ActionResult<int> Get(int accountId)
    {
        var balance = _accountService.RetrieveAccountBalance(accountId);
        
        return balance ?? 0;
    }
}
