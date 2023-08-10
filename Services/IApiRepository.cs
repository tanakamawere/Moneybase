using MoneybaseLibrary.Models;

namespace Moneybase.Services;

public interface IApiRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<IEnumerable<Account>> GetAccountsAsync(string id);
    Task<IEnumerable<Transaction>> GetTransactionsAsync(string userId, int number);

    Task<User> GetUser(string authId);
    Task<Account> GetAccount(int userId);
    Task<FirstTimeUser> UserIsNew(string authId);

    Task PostUser(User user);
    Task PostAccount(Account account);
    Task PostTransaction(Transaction transaction);

    Task<IEnumerable<VirtualCard>> GetVirtualCards(string userId);
    Task<bool> ChangeCardBlockStatus(int cardId);
    Task<bool> CreateCard(string userId);
    Task<bool> DeleteCard(int cardId);

    Task<bool> CreateSavingsAccount(Account account, string userId);
    Task<bool> DeleteSavingsAccount(int accountId);
    Task<IEnumerable<Account>> GetSavingsAccounts(string userId);
    Task<Account> GetSavingsAccount (int accountId);
    Task<bool> TransactSavingsAccount(Transaction transaction);
}
