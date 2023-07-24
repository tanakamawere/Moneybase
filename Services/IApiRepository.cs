using MoneybaseLibrary.Models;

namespace Moneybase.Services;

public interface IApiRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<IEnumerable<Account>> GetAccountsAsync(int id);
    Task<IEnumerable<Transaction>> GetTransactionsAsync();

    Task<User> GetUser(int id);
    Task<Account> GetAccount(int userId);

    void PostUser(User user);
    void PostAccount(Account account);
    void PostTransaction(Transaction transaction);
}
