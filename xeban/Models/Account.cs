namespace xeban.Models;

public class Account
{
    public int Id { get; set; }
    public int Balance { get; set; }

    public Account(int id, int initialBalance)
    {
        Id = id;
        Balance = initialBalance;
    }
}