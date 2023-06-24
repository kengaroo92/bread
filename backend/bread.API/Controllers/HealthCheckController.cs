using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

[ApiController]
[Route("[controller]")]
// Health Check Resources in ASP.NET Core 
// https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-7.0
// https://code-maze.com/health-checks-aspnetcore/
// 
public class HealthCheckController : ControllerBase
{
  private readonly HealthCheckService _healthCheckService;

  public HealthCheckController(HealthCheckService healthCheckService)
  {
    _healthCheckService = healthCheckService;
  }

  // GET: /HealthCheck
  [HttpGet]
  public async Task<IActionResult> Get()
  {
    var report = await _healthCheckService.CheckHealthAsync();

    var response = new
    {
      status = report.Status.ToString(),
      checks = report.Entries.Select(x => new
      {
        component = x.Key,
        status = x.Value.Status.ToString(),
        description = x.Value.Description,
        data = x.Value.Data,
        exception = x.Value.Exception != null ? x.Value.Exception.Message : "No HealthCheck Exceptions"
      })
    };

    return report.Status == HealthStatus.Healthy 
      ? Ok(response) 
      : StatusCode(503, response);
  }
}