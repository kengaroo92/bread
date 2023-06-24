#nullable disable

// BCrypt in .NET : https://code-maze.com/dotnet-secure-passwords-bcrypt/
public class User
{
    // Build the constructor to initialize the object of the class. Avoids null reference warnings.
    public User(string userName, string firstName, string lastName, string email)
    {
        if (string.IsNullOrEmpty(userName))
        {
            throw new ArgumentException("UserName cannot be null or empty. Please provide a value.", nameof(userName));
        }
        if (string.IsNullOrEmpty(firstName))
        {
            throw new ArgumentException("First name cannot be null or empty. Please provide a value.", nameof(firstName));
        }
        if (string.IsNullOrEmpty(lastName))
        {
            throw new ArgumentException("Last name cannot be null or empty. Please provide a value.", nameof(lastName));
        }
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("Email cannot be null or empty. Please provide a value.", nameof(email)); 
        }
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Email = email; // TODO: Email Format Validation: Need to manage validation for email format either in the backend or frontend. Backend would cause extra requests to handle this, will evaluate when the form in the frontend is built.
        FullName = $"{firstName} {lastName}";
        CreatedAt = DateTime.UtcNow;
    }

    // Each property in this class will represent a column in the respective table.
    public int Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; private set; }
    public DateTime CreatedAt { get; set; } // Timestamp when the user was created.
    public DateTime LastLogin { get; set; } // Timestamp when the user last logged in.

    // Automatically set or change the password, which is automatically hashed. BCrypt automaticall salts the password for you.
    public void SetPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Password cannot be null or empty. Please provide a value.", nameof(password));
        }

        PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
    }

    // Verify the password.
    public bool VerifyPassword(string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
    }
}