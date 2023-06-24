using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class BudgetController : ControllerBase
{
    private readonly BreadDbContext _context;

    public BudgetController(BreadDbContext context)
    {
        _context = context;
    }

    // GET: api/Budget
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Budget>>> GetBudgets()
    {
        return await _context.Budgets.ToListAsync();
    }

    // GET: api/Budget/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Budget>> GetBudget(int id)
    {
        var budget = await _context.Budgets.FindAsync(id);

        if (budget == null)
        {
            return NotFound();
        }

        return budget;
    }

    // TODO: Add POST, PUT, DELETE methods
}
