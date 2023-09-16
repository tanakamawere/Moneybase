using MoneybaseLibrary.Models;

namespace Moneybase.Services;

public interface IApiRepository
{
    Task<IEnumerable<Account>> GetAccountsAsync(string id);
    Task<IEnumerable<Transaction>> GetTransactionsAsync(string userPhoneNumber, int number);

    //Authentication
    Task<bool> RegisterUserAsync(RegisterRequest registerRequest);
    Task<User> GetUserWithCurrentAccountsAsync(string userPhoneNumber);
    Task<FirstTimeUser> UserIsNew(string userPhoneNumber);
    Task<bool> LoginUserAsync(AuthUser authUser);
    Task<User> GetUserAsync(string userPhoneNumber);

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
    Task<object> CreateRemoteSession(RemotePayClientSideDto remotePay);
    Task InitiateRemotePayment(RemotePayTransactionDto RemotePayTransactionDto);
    Task<IEnumerable<RemotePay>> GetRemotePays(string userPhoneNumber);
    Task DeleteRemotePayment(RemotePay payment);
    Task InactivateRemotePayment(RemotePay payment);

    //GROUP PAY
    Task<bool> CreateGroupPayment(GroupPay payment);
    Task DeleteGroupPayment(GroupPay payment);
    Task MakeParticipantConfirmation(GroupPayParticipant participant);
    Task InitiateGroupPayment(GroupPay payment);
}
