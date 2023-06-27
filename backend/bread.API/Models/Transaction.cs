#nullable disable

public class Transaction
{
  public Transaction(decimal income, decimal expense, DateTime date, string description, int categoryId, int userId)
  {
    if (income < 0 || expense < 0)
    {
      throw new ArgumentException("Amount must be greater than 0 or equal to 0.");
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

    Income = income;
    Expense = expense;
    Date = date;
    Description = description;
    CategoryId = categoryId;
    UserId = userId;
    CreatedAt = DateTime.UtcNow;
    UpdatedAt = DateTime.UtcNow;
  }

  public enum RecurrenceType
  {
    None,
    Daily,
    Weekly,
    Monthly,
    Yearly
  }

  // Each property in this class will represent a column in the respective table.
  public int Id { get; set; }
  public decimal Income { get; set; }
  public decimal Expense { get; set; }
  public DateTime Date { get; set; }
  public string Description { get; set; }
  public int CategoryId { get; set; }
  public Category Category { get; set; } // Navigation property. Used to categorize transactions.
  public int UserId { get; set; }
  public User User { get; set; } // Navigation property.
  public DateTime CreatedAt { get; set; } // Timestamp when the transaction was created.
  public DateTime UpdatedAt { get; set; } // Timestamp to capture last time a transaction was updated.

  public RecurrenceType Recurrence { get; set; } = RecurrenceType.None; // Adding Recurrance for future implementations.
  public DateTime? NextOccurranceDate { get; set; } // Adding NextOccurranceDate for future implementation.
}