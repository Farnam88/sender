namespace AzurePractice.RequestSender.Services;

public interface IExternalServices
{
    Task SendRequest(int quantity);
}

public class ExternalServices : IExternalServices
{
    private readonly HttpClient _httpClient;

    public ExternalServices(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("receiver");
    }

    public async Task SendRequest(int quantity)
    {
        try
        {
            for (int i = 1; i <= quantity; i++)
            {
                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{_httpClient.BaseAddress}api/receiver")
                };
                
                var result = await _httpClient.SendAsync(httpRequestMessage);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        string a = "";
    }
}