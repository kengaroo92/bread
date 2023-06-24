#nullable disable

public class Transaction
{
    public Transaction(decimal amount, DateTime date, string description, int categoryId, int userId, bool isIncome)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than 0.", nameof(amount));
        }
        if (string.IsNullOrEmpty(description))
        {
            throw new ArgumentException("Description cannot be null or empty. Please provide a value.", nameof(description));
        }
        if (categoryId <= 0)
        {
            throw new ArgumentException("Invalid Category ID.", nameof(categoryId));
        }
        if (userId <= 0)
        {
            throw new ArgumentException("Invalid User ID.", nameof(userId));
        }

        Amount = amount;
        Date = date;
        Description = description;
        CategoryId = categoryId;
        UserId = userId;
        IsIncome = isIncome;
        CreatedAt = DateTime.UtcNow;
    }

    // Each property in this class will represent a column in the respective table.
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } // Navigation property. Used to categorize transactions.
    public int UserId { get; set; }
    public User User { get; set; } // Navigation property.
    public bool IsIncome { get; set; } // Indicating if the transaction is an income or expense.
    public DateTime CreatedAt { get; set; } // Timestamp when the transaction was created.
}