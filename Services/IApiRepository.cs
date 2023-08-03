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

    Task PostUser(User user);
    Task PostAccount(Account account);
    Task PostTransaction(Transaction transaction);

    Task<IEnumerable<VirtualCard>> GetVirtualCards(string userId);
    Task ChangeCardBlockStatus(int cardId);
    Task CreateCard(string userId);
    Task DeleteCard(int cardId);

    Task CreateSavingsAccount(Account account, string userId);
    Task DeleteSavingsAccount(int accountId);
    Task<IEnumerable<Account>> GetSavingsAccounts(string userId);
    Task<Account> GetSavingsAccount (int accountId);
    Task TransactSavingsAccount(Transaction transaction);
}
