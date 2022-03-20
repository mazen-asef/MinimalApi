using xeban.Infrastructure;
using xeban.Models;
using xeban.Responses;

namespace xeban.Services;

public class AccountService
{
    private readonly AccountRepository _accountRepository;

    public AccountService(AccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public Account? CreateAccount(int accountId, int initialBalance)
    {
        return _accountRepository.CreateAccount(accountId, initialBalance);
    }

    public Account? RetrieveAccount(int accountId)
    {
        return _accountRepository.RetrieveAccount(accountId);
    }

    public int? RetrieveAccountBalance(int accountId)
    {
        return _accountRepository.RetrieveAccount(accountId)?.Balance;
    }

    public DepositEventResponse? HandleDeposit(int accountId, int initialBalance)
    {
        var acc = RetrieveAccount(accountId); 
        
        if (acc == null)
            acc = CreateAccount(accountId, initialBalance);
        else
            _accountRepository.ModifyAccountBalance(accountId, initialBalance);

        var eventResponse = new DepositEventResponse
        {
            Destination = acc
        };
        
        return eventResponse;
    }
}