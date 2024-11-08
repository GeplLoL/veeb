[ApiController]
[Route("api/[controller]")]
public class KasutajadTootedController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public KasutajadTootedController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{kasutajaId}/tooted")]
    public ActionResult<List<Toode>> GetProducts(int kasutajaId)
    {
        var kasutaja = _context.Kasutajad
            .Include(k => k.Tooted)
            .FirstOrDefault(k => k.Id == kasutajaId);
        if (kasutaja == null)
        {
            return NotFound("Kasutajat ei leitud.");
        }
        return Ok(kasutaja.Tooted);
    }

    [HttpPost("{kasutajaId}/lisa")]
    public ActionResult<List<Toode>> AddProduct(int kasutajaId, [FromBody] Toode newToode)
    {
        var kasutaja = _context.Kasutajad
            .Include(k => k.Tooted)
            .FirstOrDefault(k => k.Id == kasutajaId);
        if (kasutaja == null)
        {
            return NotFound("Kasutajat ei leitud.");
        }

        newToode.KasutajaId = kasutajaId;
        _context.Tooted.Add(newToode);
        _context.SaveChanges();
        return Ok(kasutaja.Tooted);
    }

    [HttpDelete("{kasutajaId}/kustuta/{toodeId}")]
    public ActionResult<List<Toode>> DeleteProduct(int kasutajaId, int toodeId)
    {
        var toode = _context.Tooted.FirstOrDefault(t => t.Id == toodeId && t.KasutajaId == kasutajaId);
        if (toode == null)
        {
            return NotFound("Toodet ei leitud.");
        }

        _context.Tooted.Remove(toode);
        _context.SaveChanges();
        return Ok(_context.Tooted.Where(t => t.KasutajaId == kasutajaId).ToList());
    }

    [HttpPut("{kasutajaId}/uuenda")]
    public IActionResult UpdateProduct(int kasutajaId, [FromBody] Toode updatedToode)
    {
        var existingToode = _context.Tooted.FirstOrDefault(t => t.Id == updatedToode.Id && t.KasutajaId == kasutajaId);
        if (existingToode == null)
        {
            return NotFound("Toodet ei leitud.");
        }

        existingToode.Name = updatedToode.Name;
        existingToode.Price = updatedToode.Price;
        existingToode.IsActive = updatedToode.IsActive;

        _context.SaveChanges();
        return Ok(existingToode);
    }
}
