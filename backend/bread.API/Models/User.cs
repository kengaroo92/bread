#nullable disable

public class User
{
    // Build the constructor to initialize the object of the class. Avoids null reference warnings.
    public User(string firstName, string lastName, string email, string password)
    {
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
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Password cannot be null or empty. Please provide a value.", nameof(password));
        }
        FirstName = firstName;
        LastName = lastName;
        Email = email; // TODO: Email Format Validation: Need to manage validation for email format either in the backend or frontend. Backend would cause extra requests to handle this, will evaluate when the form in the frontend is built.
        Password = password; // Will be replaced with a hashed password later for security purposes.
        FullName = $"{firstName} {lastName}";
        CreatedAt = DateTime.UtcNow;
    }

    // Each property in this class will represent a column in the respective table.
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; } // Will replace this later with a hashed password later for security purposes.
    public DateTime CreatedAt { get; set; } // Timestamp when the user was created.
    public DateTime LastLogin { get; set; } // Timestamp when the user last logged in.

}