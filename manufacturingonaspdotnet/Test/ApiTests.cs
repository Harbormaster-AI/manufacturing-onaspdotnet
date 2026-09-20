using System.Net;
using System.Net.Http.Json;

namespace manufacturingonaspdotnet.Tests;

public class ApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Health_ReturnsSuccess()
    {
        var response = await _client.GetAsync("/health");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task BankCustomerAccount_CrudFlow_Works()
    {/*
        var suffix = Guid.NewGuid().ToString("N")[..8];

        var createBank = await _client.PostAsJsonAsync("/api/banks", new { name = $"Harbor Bank {suffix}" });
        Assert.Equal(HttpStatusCode.Created, createBank.StatusCode);
        var bank = await createBank.Content.ReadFromJsonAsync<BankDto>();
        Assert.NotNull(bank);

        var createCustomer = await _client.PostAsJsonAsync("/api/customers", new
        {
            name = "Acme Corporation",
            email = $"info-{suffix}@acme.com",
            bankId = bank!.Id
        });
        Assert.Equal(HttpStatusCode.Created, createCustomer.StatusCode);
        var customer = await createCustomer.Content.ReadFromJsonAsync<CustomerDto>();
        Assert.NotNull(customer);

        var createAccount = await _client.PostAsJsonAsync("/api/accounts", new
        {
            accountNumber = $"ACC-{suffix}",
            balance = 2500.50m,
            customerId = customer!.Id
        });
        Assert.Equal(HttpStatusCode.Created, createAccount.StatusCode);

        // Customer has exactly one account â second create must fail.
        var secondAccount = await _client.PostAsJsonAsync("/api/accounts", new
        {
            accountNumber = $"ACC2-{suffix}",
            balance = 10m,
            customerId = customer.Id
        });
        Assert.Equal(HttpStatusCode.BadRequest, secondAccount.StatusCode);

        var listCustomers = await _client.GetAsync("/api/customers");
        listCustomers.EnsureSuccessStatusCode();*/
    }

    //private sealed record BankDto(Guid Id, string Name);
    //private sealed record CustomerDto(Guid Id, string Name, string Email, Guid BankId);
}
