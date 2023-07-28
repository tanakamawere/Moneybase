using MoneybaseLibrary.Models;

namespace Moneybase.Services;

public interface IApiRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<IEnumerable<Account>> GetAccountsAsync(string id);
    Task<IEnumerable<Transaction>> GetTransactionsAsync();

    Task<User> GetUser(string authId);
    Task<Account> GetAccount(int userId);
    Task<FirstTimeUser> UserIsNew(string authId);

    void PostUser(User user);
    void PostAccount(Account account);
    void PostTransaction(Transaction transaction);
}
