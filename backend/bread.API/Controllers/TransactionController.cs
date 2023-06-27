using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
  private readonly BreadDbContext _context;

  public TransactionController(BreadDbContext context)
  {
    _context = context;
  }
  // Route: /Transactions
  // Method: GET
  // Get all transactions.
  [HttpGet]
  [Route("all")]
  public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
  {
    return await _context.Transactions.ToListAsync();
  }

  // Route: /Transaction/5
  // Method: GET
  // Get transaction by id.
  [HttpGet("{id}")]

  public async Task<ActionResult<Transaction>> GetTransaction(int id)
  {
    var transaction = await _context.Transactions.FindAsync(id);

    if (transaction == null)
    {
      return NotFound();
    }

    return transaction;
  }

  // Route: Transaction/Add
  // Method: POST
  // Add new transaction.
  [HttpPost]
  [Route("/add")]
  public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
  {
    // Add the transaction to the DbContext.
    _context.Transactions.Add(transaction);
    // Save changes.
    await _context.SaveChangesAsync();
    // Return transaction information as well as a 201 Ok status.
    return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);
  }

  // Route: /Transaction/5
  // Method: PUT
  // Update an existing transaction.
  [HttpPut("{id}")]
  [Route("/update")]
  public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
  {
    if (id != transaction.Id)
    {
      return BadRequest();
    }
    // Update the state of the transaction to indicate it has been modified.
    _context.Entry(transaction).State = EntityState.Modified;

    try
    {
      // Save the updates to the database.
      await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
      if (!TransactionExists(id))
      {
        return NotFound();
      }
      else
      {
        throw;
      }
    }

    // Return NoContent to show the update was successful.
    return NoContent();
  }

  // Check the DbContext to see if the transaction exists.
  private bool TransactionExists(int id)
  {
    return _context.Transactions.Any(e => e.Id == id);
  }

  // Route: /Transaction/5/Delete
  // Method: DELETE
  // Delete an existing transaction.
  public async Task<IActionResult> DeleteTransaction(int id)
  {
    // Verifying the transaction exists.
    var transaction = await _context.Transactions.FindAsync(id);
    if (transaction == null)
    {
      return NotFound();
    }

    // Remove the transaction.
    _context.Transactions.Remove(transaction);

    // Save changes to the database.
    await _context.SaveChangesAsync();

    // Return the deleted transaction.
    return Ok(transaction);
  }
}
