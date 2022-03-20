using Microsoft.AspNetCore.Mvc;
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
        
        if (balance is null)
            return NotFound(0);

        return balance;
    }
}
