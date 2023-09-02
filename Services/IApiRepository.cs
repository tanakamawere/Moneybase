using MoneybaseLibrary.Models;

namespace Moneybase.Services;

public interface IApiRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<IEnumerable<Account>> GetAccountsAsync(string id);
    Task<IEnumerable<Transaction>> GetTransactionsAsync(string userPhoneNumber, int number);

    Task<User> GetUser(string authId);
    Task<Account> GetAccount(int userPhoneNumber);
    Task<FirstTimeUser> UserIsNew(string authId);
    Task<bool> CheckPIN(string authId, string pinString);

    Task<bool> PostUser(User user);
    Task PostAccount(Account account);
    Task PostTransaction(Transaction transaction);

    Task<IEnumerable<VirtualCard>> GetVirtualCards(string userPhoneNumber);
    Task<bool> ChangeCardBlockStatus(int cardId);
    Task<bool> CreateCard(string userPhoneNumber);
    Task<bool> DeleteCard(int cardId);

    Task<bool> CreateSavingsAccount(Account account, string userPhoneNumber);
    Task<bool> DeleteSavingsAccount(int accountId);
    Task<IEnumerable<Account>> GetSavingsAccounts(string userPhoneNumber);
    Task<Account> GetSavingsAccount (int accountId);
    Task<bool> TransactSavingsAccount(Transaction transaction);

    //REMOTE PAY
    Task<bool> CheckForActiveRemotePaySession(string userPhoneNumber);
    Task CreateRemoteSession(RemotePayClientSideDto remotePay);
    Task InitiateRemotePayment(RemotePayTransactionDto RemotePayTransactionDto);
    Task<IEnumerable<RemotePay>> GetRemotePays(string userPhoneNumber);
    Task DeleteRemotePayment(RemotePay payment);
    Task InactivateRemotePayment(RemotePay payment);

    //GROUP PAY
    Task CreateGroupPayment(GroupPay payment);
    Task DeleteGroupPayment(GroupPay payment);
    Task MakeParticipantConfirmation(GroupPayParticipant participant);
    Task InitiateGroupPayment(GroupPay payment);
}
