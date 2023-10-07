using Moneybase.Helpers;
using MoneybaseLibrary.Models;
using System.Net.Http.Json;

namespace Moneybase.Services;

public class ApiRepository : IApiRepository
{
    private static readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri(Constants.ApiUrl())
    };


    //Gets user with associated accounts for home page
    public async Task<User> GetUserWithCurrentAccountsAsync(string userPhoneNumber)
    {
        return await _httpClient.GetFromJsonAsync<User>($"/users/get_user_with_accounts/{userPhoneNumber}");
    }
    public async Task<User> GetUserAsync(string userPhoneNumber)
    {
        return await _httpClient.GetFromJsonAsync<User>($"/users/get_user/{userPhoneNumber}");
    }

    //Authentication
    public async Task<bool> RegisterUserAsync(RegisterRequest registerRequest)
    {
        var response = await _httpClient.PostAsJsonAsync($"/auth/register", registerRequest);
        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<bool> LoginUserAsync(AuthUser authUser)
    {
        bool isAuthenticated = false;
        var response = await _httpClient.PostAsJsonAsync($"/auth/login", authUser);

        //Check if response is Ok or Bad Request
        if (response.IsSuccessStatusCode)
            isAuthenticated = true;
        else if (response.StatusCode.Equals(System.Net.HttpStatusCode.Unauthorized))  
            isAuthenticated = false;
            return isAuthenticated;
    }
    public async Task<FirstTimeUser> UserIsNew(string userPhoneNumber)
    {
        return await _httpClient.GetFromJsonAsync<FirstTimeUser>($"/auth/is_new/{userPhoneNumber}");
    }

    public async Task<IEnumerable<Account>> GetAccountsAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Account>>($"/accounts/get/{id}");
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync(string userPhoneNumber, int number)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Transaction>>($"/transactions_recent/{userPhoneNumber}/{number}");
    }


    public async Task PostAccount(Account account)
    {
        await _httpClient.PostAsJsonAsync($"/accounts/add/{account}", account);
    }

    public async Task PostTransaction(Transaction transaction)
    {
        await _httpClient.PostAsJsonAsync($"/transaction/post/{transaction}", transaction);
    }

    public async Task<bool> CreateCard(string userPhoneNumber)
    {
        var response = await _httpClient.PostAsJsonAsync($"/virtualcards/createcard/{userPhoneNumber}", userPhoneNumber); 
        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<bool> ChangeCardBlockStatus(int cardId)
    {
        var response = await _httpClient.PostAsJsonAsync($"/virtualcards/changeblock/{cardId}", cardId);
        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }
    public async Task<bool> DeleteCard(int cardId)
    {
        var response = await _httpClient.DeleteAsync($"/virtualcards/deletecard/{cardId}");
        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<IEnumerable<VirtualCard>> GetVirtualCards(string userPhoneNumber)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<VirtualCard>>($"/virtualcards/getcards/{userPhoneNumber}");
    }

    public async Task<bool> CreateSavingsAccount(Account account, string userPhoneNumber)
    {
        var response = await _httpClient.PostAsJsonAsync($"/savings/create/{account}/{userPhoneNumber}", account);
        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<bool> DeleteSavingsAccount(int accountId)
    {
        var response = await _httpClient.DeleteAsync($"/savings/delete/{accountId}");
        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<IEnumerable<Account>> GetSavingsAccounts(string userPhoneNumber)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Account>>($"/savings/get_accounts/{userPhoneNumber}");
    }

    public async Task<Account> GetSavingsAccount(int accountId)
    {
        return await _httpClient.GetFromJsonAsync<Account>($"/savings/get_single/{accountId}");
    }

    public async Task<bool> TransactSavingsAccount(Transaction transaction)
    {
        var response = await _httpClient.PostAsJsonAsync($"/savings/transact/{transaction}", transaction);
        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }


    public async Task<bool> CheckForActiveRemotePaySession(string userPhoneNumber)
    {
        var response = await _httpClient.PostAsJsonAsync($"/remote_pay/check/{userPhoneNumber}", userPhoneNumber);
        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task InitiateRemotePayment(RemotePayTransactionDto RemotePayTransactionDto)
    {
        await _httpClient.PostAsJsonAsync($"/remote_pay/initpayment/{RemotePayTransactionDto}", RemotePayTransactionDto);
    }

    public async Task DeleteRemotePayment(RemotePay payment)
    {
        await _httpClient.DeleteAsync($"/remote_pay/delete_session/{payment}");
    }
    public async Task<object> CreateRemoteSession(RemotePayClientSideDto remotePayClientDto)
    {
        return await _httpClient.PostAsJsonAsync($"/remote_pay/create_session/{remotePayClientDto}", remotePayClientDto);
    }

    public async Task InactivateRemotePayment(RemotePay payment)
    {
        await _httpClient.PostAsJsonAsync($"/remote_pay/toggle_activation/{payment}", payment);
    }

    public async Task<IEnumerable<RemotePay>> GetRemotePays(string userPhoneNumber)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<RemotePay>>($"/remote_pay/get_list/{userPhoneNumber}");
    }

    public async Task<bool> CreateGroupPayment(GroupPay payment)
    {
        bool isSuccessful = false;
        var response = await _httpClient.PostAsJsonAsync("/group_pay/post_session/", payment);
        if (response.IsSuccessStatusCode)
            isSuccessful = true;
        return isSuccessful;
    }

    public async Task DeleteGroupPayment(GroupPay payment)
    {
        await _httpClient.DeleteAsync($"/group_pay/delete_session/{payment}");
    }

    public async Task MakeParticipantConfirmation(GroupPayParticipant participant)
    {
        await _httpClient.PostAsJsonAsync($"/group_pay/participant_confirmation/{participant}", participant);
    }

    public async Task InitiateGroupPayment(GroupPay payment)
    {
        await _httpClient.PostAsJsonAsync($"/group_pay/init_payment/{payment}", payment);
    }

}
