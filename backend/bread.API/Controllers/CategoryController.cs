using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly BreadDbContext _context;

    public CategoryController(BreadDbContext context)
    {
        _context = context;
    }

    // GET: api/Category
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        return await _context.Categories.ToListAsync();
    }

    // GET: api/Category/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound();
        }

        return category;
    }

    // TODO: Add POST, PUT, DELETE methods
}
