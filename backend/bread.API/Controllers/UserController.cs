using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;

// Declare that this is a controller for an API.
[ApiController]
// Set the route for this controller. This is handled by ASP.NET Core's routing system. This becomes "User", it looks for the name of the Controller class and removes the Controller suffix. Naming consistency is important for a reason.
[Route("[controller]")]
// TODO: Custom Routes: Set custom routes for each controller later on.
// [Route("CustomRoute")]
public class UserController : ControllerBase
{
    // Variable to hold the database context.
    private readonly BreadDbContext _context;
    private readonly JwtSettings _jwtSettings; // Declare JwtSettings.

    // Inject the database context into the controller with the constructor below.
    public UserController(BreadDbContext context, IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _jwtSettings = jwtSettings.Value;
    }

    // Route: /User
    // Method: GET
    // Get a list of all users.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        // Returns all users in the Users DbSet as a list.
        return await _context.Users.ToListAsync();
    }

    // Route: /User/{id}
    // Method: GET
    // Get a specific user by their ID.
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        // Find the user by their ID.
        var user = await _context.Users.FindAsync(id);

        // If the user wasn't found by the specified ID, return a 404 Not Found HTTP status code.
        if (user == null)
        {
            return NotFound();
        }

        // Return the user if the ID exists.
        return user;
    }

    // Route: /User
    // Method: POST
    // Create a new user.
    [HttpPost]
    [Route("register")]
    // [FromBody] tells ASP.NET to look for the data in the body of the HTTP request. Then it deserializes the JSON object found there into the RegisterRequest object.
    public async Task<ActionResult<User>> PostUser([FromBody]RegistrationRequest registrationRequest)
    {
      var user = new User(registrationRequest.UserName, registrationRequest.FirstName, registrationRequest.LastName, registrationRequest.Email);
      user.SetPassword(registrationRequest.Password);
      // Add the user to the Users DbSet.
      _context.Users.Add(user);
      // Save changes to the database.
      await _context.SaveChangesAsync();

      // Returns a 201 Created response with the location of the user.
      return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    // Route: /User/Login
    // Method: POST
    // Allow a user to login.
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<User>> LoginUser(LoginRequest loginRequest)
    {
      // Take the UserName from the LoginRequest that is sent from the frontend object. Query the _context.Users DbSet, which represents Users table in the database.
      var user = await _context.Users.Where(u => u.UserName == loginRequest.UserName).FirstOrDefaultAsync();

      if (user == null)
      {
        return NotFound();
      }
      // Check if the password from the LoginRequest matches the password in the Users DbSet for the matched UserName.
      if (!user.VerifyPassword(loginRequest.Password))
      {
        return Unauthorized();
      }

      // If a UserName was found and the passwords match, generate a JWT (JSON Web Token).
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, user.UserName.ToString()),
        }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      // If a UserName was found and the passwords match, return the user object, allowing the user to successfully login.
      return Ok(new { User = user, Token = tokenString });
    }

    // Route: /User/{id}
    // Method: PUT
    // Update a specific user.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
      // If the ID in the route doesn't match a valid user ID, return a 400 Bad Request HTTP status code.
      if (id != user.Id)
      {
        return BadRequest();
      }

      // Tell EF Core to track the entity for any changes.
      _context.Entry(user).State = EntityState.Modified;

      try
      {
        // Try saving any changes to the database.
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        // If there was a concurrency conflict, check if the user still exists.
        if (!UserExists(id))
        {
          // If the user doesn't exist, return a 404 Not Found HTTP status code.
          return NotFound();
        }
        else
        {
          // If the user does exist, rethrow the exception.
          throw;
        }
      }

      // If the save was successful, return a 204 No Content HTTP status code.
      return NoContent();
    }

    // Route: /User/{id}
    // Method: DELETE
    // Delete a specific user.
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
      // Find the user by their ID.
      var user = await _context.Users.FindAsync(id);
      // If the user wasn't found, return a 404 Not Found HTTP status code.
      if (user == null)
      {
        return NotFound();
      }

      // Remove the user from the Users DbSet.
      _context.Users.Remove(user);
      // Save changes to the database.
      await _context.SaveChangesAsync();

      // Return a 204 No Content HTTP status code.
      return NoContent();
    }

    // Check if a user exists in the database.
    private bool UserExists(int id)
    {
      // Return true if any user in the Users DbSet has the passed in ID.
      // Lambda expression, a concise way to define an anonymous function. Reads as "e goes to e.Id equals id" or "e maps to e.Id equals id" or "for each e, e.Id is compared to id".
      // Defines a condition to search for a user with a specific Id in the database.
      return _context.Users.Any(e => e.Id == id);
    }
}
