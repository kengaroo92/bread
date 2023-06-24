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

    // GET: /Transaction
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
    {
        return await _context.Transactions.ToListAsync();
    }

    // GET: /Transaction/5
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

    // TODO: Add POST, PUT, DELETE methods
}
