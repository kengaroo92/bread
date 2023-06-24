#nullable disable

using Microsoft.AspNetCore.Builder; // WebApplication
using Microsoft.EntityFrameworkCore; // UseNpgsql
using Microsoft.Extensions.DependencyInjection; // AddControllers, AddEndpointsApiExplorer, AddSwaggerGen
using Microsoft.Extensions.Hosting; // IsDevelopment

// Create a new instance of WebApplication. Builder object that allows you to configure your application. 
var builder = WebApplication.CreateBuilder(args);

// Add services required for using controllers.
builder.Services.AddControllers();
// Add services that allow the application to discover and describe its own API endpoints. Such as generating OpenAPI/Swagger documents.
builder.Services.AddEndpointsApiExplorer();
// Add a Swagger generator services to the Dependency Injection container. 
builder.Services.AddSwaggerGen();
// Configure the connection string. Check the system environment variables for BreadDatabase, else check the ConnectionString object for BreadDatabase in the appsettings.json file.
string connectionString = Environment.GetEnvironmentVariable("BreadDatabase", EnvironmentVariableTarget.Machine) ?? builder.Configuration.GetConnectionString("BreadDatabase");
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
}

// Configures the app to redirect HTTP requests to HTTPS (Secure).
app.UseHttpsRedirection();
// Enables authorization. Determines what a user is allowed to do.
app.UseAuthorization();
// Maps attribute-routed controllers. Tells the app to use the routes defined in your controllers to handle incoming requests.
app.MapControllers();
// Last piece of middleware. Basically starts the application. After this method is called, the application will start listening for incoming HTTP requests.
app.Run();
