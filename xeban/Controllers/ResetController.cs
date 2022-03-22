using System.Net;
using Microsoft.AspNetCore.Mvc;
using xeban.Common;
using xeban.Services;

namespace xeban.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ResetController : ControllerBase
{
    private readonly ILogger<BalanceController> _logger;
    private readonly AccountService _accountService;

    public ResetController(ILogger<BalanceController> logger, AccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    [HttpPost(Name = "PostReset")]
    public ActionResult Post()
    {
        _accountService.HandleReset();
        return StatusCode((int) HttpStatusCode.OK, 0);
    }
}