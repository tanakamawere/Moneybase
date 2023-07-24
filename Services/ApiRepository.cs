using MoneybaseLibrary.Models;
using System.Net.Http.Json;

namespace Moneybase.Services;

public class ApiRepository : IApiRepository
{
    private static readonly HttpClient _httpClient = new HttpClient()
    {
        BaseAddress = new Uri("https://b30c-77-246-52-36.ngrok-free.app")
    };

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<User>>("/users");
    }

    public async Task<IEnumerable<Account>> GetAccountsAsync(int id)
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
    public async Task<User> GetUser(int id)
    {
        return await _httpClient.GetFromJsonAsync<User>($"/users/get/{id}");
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
}
