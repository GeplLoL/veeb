using Microsoft.AspNetCore.Mvc;
using veeb.models;

namespace veeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TootedController : ControllerBase
    {
        private static List<Toode> _tooted = new()
        {
            new Toode(1, "Koola", 1.5, true),
            new Toode(2, "Fanta", 1.0, false),
            new Toode(3, "Sprite", 1.7, true),
            new Toode(4, "Vichy", 2.0, true),
            new Toode(5, "Vitamin well", 2.5, true)
        };

        [HttpGet]
        public List<Toode> Get()
        {
            return _tooted;
        }

        [HttpDelete("kustuta/{index}")]
        public List<Toode> Delete(int index)
        {
            _tooted.RemoveAt(index);
            return _tooted;
        }

        [HttpPost("lisa")]
        public List<Toode> Add([FromBody] Toode toode)
        {
            _tooted.Add(toode);
            return _tooted;
        }

        [HttpPatch("hind-dollaritesse/{kurss}")]
        public List<Toode> UpdatePrices(double kurss)
        {
            foreach (var toode in _tooted)
            {
                toode.Price *= kurss;
            }
            return _tooted;
        }

        // PUT: https://localhost:4444/tooted/uuenda
        [HttpPut("uuenda")]
        public IActionResult UpdateProduct([FromBody] Toode updatedToode)
        {
            var existingToode = _tooted.FirstOrDefault(t => t.Id == updatedToode.Id);
            if (existingToode == null)
            {
                return NotFound("Toodet ei leitud.");
            }

            existingToode.Name = updatedToode.Name;
            existingToode.Price = updatedToode.Price;
            existingToode.IsActive = updatedToode.IsActive;

            return Ok(existingToode);
        }
    }
}
