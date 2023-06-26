#nullable disable

using Microsoft.AspNetCore.Builder; // WebApplication
using Microsoft.EntityFrameworkCore; // UseNpgsql
using Microsoft.Extensions.DependencyInjection; // AddControllers, AddEndpointsApiExplorer, AddSwaggerGen
using Microsoft.Extensions.Hosting; // IsDevelopment
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// Create a new instance of WebApplication. Builder object that allows you to configure your application. 
var builder = WebApplication.CreateBuilder(args);

// Add services required for using controllers.
builder.Services.AddControllers();
// Add services required for enabling CORS policies.
builder.Services.AddCors();
// Add services that allow the application to discover and describe its own API endpoints. Such as generating OpenAPI/Swagger documents.
builder.Services.AddEndpointsApiExplorer();
// Add services for authentication to create a JWT (JSON Web Token) when a successful login request is received.
builder.Services.AddJwtAuthentication(builder.Configuration);
// Add a Swagger generator services to the Dependency Injection container. 
builder.Services.AddSwaggerGen();
// Configure the connection string. Check the system environment variables for BreadDatabase, else check the ConnectionString object for BreadDatabase in the appsettings.json file.
string connectionString = Environment.GetEnvironmentVariable("BreadDatabase", EnvironmentVariableTarget.Machine) ?? builder.Configuration.GetConnectionString("BreadDatabase");
// Add services required for using the ASP.NET Core Health Check middleware. Requires connectionString, has to be below the variable decleration.
builder.Services.AddHealthChecks().AddNpgSql(connectionString);
// Configure DbContext to use PostgreSQL with the appropriate connection string.
builder.Services.AddDbContext<BreadDbContext>(options => options.UseNpgsql(connectionString));

// Builds WebApplication which includes all the services and configurations defined before this line. 'app' object represents your application and is used to configure the apps HTTP request pipeline.
var app = builder.Build();

// Configure the HTTP request pipeline.
// Check to see if the application is currently running in a "Development" environment.
// Used to enable or disable certain features in certain environments.
if (app.Environment.IsDevelopment())
{
  // Middleware that catches exceptions thrown by later middleware and generates HTML error responses.
  app.UseDeveloperExceptionPage();
  // OpenAPI documentation and testing. Describes endpoints, expected input/output for each endpoint, data models used, etc.
  app.UseSwagger();
  // Set up the interactive Swagger UI that can be accessed through the web browser. Setting RoutePrefix to an empty string allows Swagger UI to be accessed at the root. 'https://localhost:PORT/'
  app.UseSwaggerUI(c => c.RoutePrefix = string.Empty);
  // Use HTTP Strict Transport Security Protocol. https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-7.0&tabs=visual-studio%2Clinux-ubuntu
  app.UseHsts();
}

// Configure CORS middleware.
// app.UseCors(options => options.WithOrigins("http://trustedorigins.com")) An example for what to change to later once the origins are determined.
// Can also adjust what methods are allowed, for example GET, POST, PUT only, or any combination.
// Can also specify which headers are manually set, for example, Accept, Content-Language, Content-Type, etc..
// Configures the app to redirect HTTP requests to HTTPS (Secure).
// See https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS for more CORS policy settings.
//app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); // Will be adjusted to be more strict for production. This is temporary to avoid extra headaches during early development.
app.UseCors(options => options.WithOrigins("http://localhost:3000", "http://bread:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials());
// Create a lightweight health check to simply know if the app is running and responding. Navigate using /health.
// If a more detailed health check is needed, navigate to /healthcheck which is the custom HealthCheckController that returns a detailed JSON object for both the app status and database status.
app.MapHealthChecks("/health");
// Redirects all HTTP requests to HTTPS.
app.UseHttpsRedirection();
// Enables authentication. Determines user access.
app.UseAuthentication();
// Enables authorization. Determines what a user is allowed to do.
app.UseAuthorization();
// Maps attribute-routed controllers. Tells the app to use the routes defined in your controllers to handle incoming requests.
app.MapControllers();
// Last piece of middleware. Basically starts the application. After this method is called, the application will start listening for incoming HTTP requests.
app.Run();
