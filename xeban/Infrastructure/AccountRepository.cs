using xeban.Models;

namespace xeban.Infrastructure;

public class AccountRepository
{
    private readonly List<Account> _accounts;

    public AccountRepository()
    {
        _accounts = new List<Account>();
    }

    public Account? CreateAccount(int accountId, int initialBalance)    
    {
        if (RetrieveAccount(accountId) != null) 
            return null;
        
        var acc = new Account(accountId, initialBalance);
        _accounts.Add(acc);

        return acc;
    }

    public Account? RetrieveAccount(int accountId)
    {
        var acc = _accounts.Find(f => f.Id == accountId);

        return acc;
    }

    public bool ModifyAccountBalance(int accountId, int balanceModifier)
    {
        var acc = RetrieveAccount(accountId);

        if (acc == null) 
            return false;
        
        acc.Balance += balanceModifier;
        return true;
    }
}