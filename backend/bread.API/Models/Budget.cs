#nullable disable

public class Budget
{
    // Build the constructor to initialize the object of the class. Avoids null reference warnings.
    public Budget(decimal amount, int categoryId, int userId, DateTime month)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("The budget amount must be greater than 0.", nameof(amount)); // TODO: Budget Validation: Might have to adjust this. This is indicating that a budget must be higher than 0, what if there is no budget for a certain category? Will review later.
        }
        if (categoryId <= 0)
        {
            throw new ArgumentException("Invalid Category. Please select a valid Category for your budget.", nameof(categoryId));
        }
        if (userId <= 0)
        {
            throw new ArgumentException("Invalid user assigned. Please select the person in charge of the Budget.", nameof(userId));
        }
        Amount = amount;
        CategoryId = categoryId;
        UserId = userId;
        Month = month;
    }

    // Each property in this class will represent a column in the respective table.
    public int Id { get; set; }
    public decimal Amount { get; set; } // Budget amount for the category.
    public int CategoryId { get; set; }
    public Category Category { get; set; } // Navigation property.
    public int UserId { get; set; }
    public User User { get; set; } // Navigation property.
    public DateTime Month { get; set; } // The month for which the budget applies.
}