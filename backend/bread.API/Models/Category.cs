#nullable disable

public class Category
{
    public Category(string name, bool isIncome)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Category name cannot be null or empty. Please provide a value.", nameof(name));
        }

        Name = name;
        IsIncome = isIncome;
        Transactions = new List<Transaction>(); // Initialize Transactions list
    }

    // Each property in this class will represent a column in the respective table.
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Transaction> Transactions { get; set; } // Navigation property.
    public bool IsIncome { get; set; } // Indicating if the category is for income or expenses.
}