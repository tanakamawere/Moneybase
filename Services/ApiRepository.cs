using MoneybaseLibrary.Models;
using System.Net.Http.Json;

namespace Moneybase.Services;

public class ApiRepository : IApiRepository
{
    private static readonly HttpClient _httpClient = new HttpClient()
    {
        BaseAddress = new Uri("http://10.0.2.2:5052/")
    };

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<User>>("/users");
    }

    public async Task<IEnumerable<Account>> GetAccountsAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Account>>($"/accounts/get/{id}");
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Transaction>>("/transactions");
    }

    public async Task<Account> GetAccount(int userId)
    {
        //DOES NOT WORK
        return await _httpClient.GetFromJsonAsync<Account>($"/accounts/get/{userId}");
    }
    public async Task<User> GetUser(string authId)
    {
        return await _httpClient.GetFromJsonAsync<User>($"/users/get/{authId}");
    }

    public async void PostAccount(Account account)
    {
        await _httpClient.PostAsJsonAsync($"/accounts/add/{account}", account);
    }

    public async void PostTransaction(Transaction transaction)
    {
        await _httpClient.PostAsJsonAsync($"/transaction/post/{transaction}", transaction);
    }

    public async void PostUser(User user)
    {
        await _httpClient.PostAsJsonAsync($"/users/add/{user}", user);
    }

    public async Task<FirstTimeUser> UserIsNew(string authId)
    {
        return await _httpClient.GetFromJsonAsync<FirstTimeUser>($"/users/user_is_new/{authId}");
    }
}
