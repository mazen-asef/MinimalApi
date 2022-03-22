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
    
    public Account? CreateAccount(int accountId, int amount)
    {
        return _accountRepository.CreateAccount(accountId, amount);
    }

    public Account? RetrieveAccount(int accountId)
    {
        return _accountRepository.RetrieveAccount(accountId);
    }

    public int? RetrieveAccountBalance(int accountId)
    {
        return _accountRepository.RetrieveAccount(accountId)?.Balance;
    }

    public DepositEventResponse? HandleDeposit(int accountId, int amount)
    {
        var acc = RetrieveAccount(accountId); 
        
        if (acc == null)
            acc = CreateAccount(accountId, amount);
        else
            _accountRepository.ModifyAccountBalance(accountId, amount);

        var eventResponse = new DepositEventResponse
        {
            Destination = acc
        };
        
        return eventResponse;
    }

    public WithdrawEventResponse? HandleWithdrawal(int accountId, int amount)
    {
        var acc = RetrieveAccount(accountId);

        if (acc == null)
            return null;
        
        _accountRepository.ModifyAccountBalance(acc.Id, - amount);
            
        var eventResponse = new WithdrawEventResponse
        {
            Origin = acc
        };
        
        return eventResponse;
    }
}