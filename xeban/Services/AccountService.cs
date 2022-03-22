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

    public DepositEventResponse HandleDeposit(int accountId, int amount)
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

    public TransferEventResponse? HandleTransfer(int origin, int destination, int amount)
    {
        // Special case that warrants a comment. We care about who we're taking money, but not
        // caring to whom we're giving to. So just check the origin money to see if the deal is ON
        var originAccount = RetrieveAccount(origin);

        if (originAccount == null)
            return null;
        
        HandleWithdrawal(originAccount.Id, amount);
        var destinationAccount = HandleDeposit(destination, amount);

        var eventResponse = new TransferEventResponse
        {
            Origin = originAccount,
            Destination = destinationAccount.Destination
        };

        return eventResponse;
    }

    public void HandleReset()
    {
        _accountRepository.ResetData();
    }
}