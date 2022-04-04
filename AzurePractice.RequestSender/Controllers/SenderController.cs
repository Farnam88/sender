using AzurePractice.RequestSender.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzurePractice.RequestSender.Controllers;

[ApiController]
[Route("/api/sender")]
public class SenderController : ControllerBase
{
    private readonly ILogger<SenderController> _logger;
    private readonly IExternalServices _externalServices;

    public SenderController(ILogger<SenderController> logger, IExternalServices externalServices)
    {
        _logger = logger;
        _externalServices = externalServices;
    }

    [HttpGet("/{quantity}")]
    public IActionResult Get(int quantity)
    {
        try
        {
            Task.Run(() =>
            {
                _externalServices.SendRequest(quantity);
            });
            return Ok($"{quantity} request has been sent to destination");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}