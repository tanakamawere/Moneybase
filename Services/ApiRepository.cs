﻿using MoneybaseLibrary.Models;
using System.Net.Http.Json;

namespace Moneybase.Services;

public class ApiRepository : IApiRepository
{
    private static readonly HttpClient _httpClient = new HttpClient()
    {
        //BaseAddress = new Uri("http://10.0.2.2:5052/")
        BaseAddress = new Uri("https://f8d6-77-246-52-37.ngrok-free.app/")
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

    public async Task PostAccount(Account account)
    {
        await _httpClient.PostAsJsonAsync($"/accounts/add/{account}", account);
    }

    public async Task PostTransaction(Transaction transaction)
    {
        await _httpClient.PostAsJsonAsync($"/transaction/post/{transaction}", transaction);
    }

    public async Task PostUser(User user)
    {
        await _httpClient.PostAsJsonAsync($"/users/add/{user}", user);
    }

    public async Task<FirstTimeUser> UserIsNew(string authId)
    {
        return await _httpClient.GetFromJsonAsync<FirstTimeUser>($"/users/user_is_new/{authId}");
    }

    public async Task CreateCard(string userId)
    {
        await _httpClient.PostAsJsonAsync($"/virtualcards/createcard/{userId}", userId);
    }

    public async Task ChangeCardBlockStatus(int cardId)
    {
        await _httpClient.PostAsJsonAsync($"/virtualcards/changeblock/{cardId}", cardId);
    }
    public async Task DeleteCard(int cardId)
    {
        await _httpClient.DeleteAsync($"/virtualcards/deletecard/{cardId}");
    }

    public async Task<IEnumerable<VirtualCard>> GetVirtualCards(string userId)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<VirtualCard>>($"/virtualcards/getcards/{userId}");
    }

    public async Task CreateSavingsAccount(Account account, string userId)
    {
        await _httpClient.PostAsJsonAsync($"/savings/create/{account}/{userId}", account);
    }

    public async Task DeleteSavingsAccount(int accountId)
    {
        await _httpClient.DeleteAsync($"/savings/delete/{accountId}");
    }

    public async Task<IEnumerable<Account>> GetSavingsAccounts(string userId)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Account>>($"/savings/get_accounts/{userId}");
    }

    public async Task<Account> GetSavingsAccount(int accountId)
    {
        return await _httpClient.GetFromJsonAsync<Account>($"/savings/get_single/{accountId}");
    }

    public async Task TransactSavingsAccount(Transaction transaction)
    {
        await _httpClient.PostAsJsonAsync($"/savings/transact/{transaction}", transaction);
    }

}
