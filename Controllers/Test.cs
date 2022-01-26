using Microsoft.AspNetCore.Mvc;

namespace ABU.Portfolio.Controllers;

[ApiController]
[Route("api/[controller]")]
public class Test : Controller
{
    private readonly IConfiguration _config;
    
    public Test(IConfiguration config) 
        => _config = config;

    [HttpGet]
    public IActionResult GetAppSettingsValue()
    {
        var gitHubUser = _config.GetValue<string>("GitHub:User");
        return Ok(gitHubUser);
    }
}