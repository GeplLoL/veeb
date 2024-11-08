using Microsoft.AspNetCore.Mvc;
using veeb2._0.Models;
using System.Collections.Generic;
using System.Linq;

namespace veeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KasutajadTootedController : ControllerBase
    {
        private static List<Kasutaja> _kasutajad = new()
        {
            new Kasutaja(1, "kasutaja1", "parool1", "Jaan", "Tamm")
            {
                Tooted = new List<Toode>
                {
                    new Toode(1, "Toode1", 10.0, true),
                    new Toode(2, "Toode2", 15.0, true)
                }
            },
            new Kasutaja(2, "kasutaja2", "parool2", "Mari", "Kask")
            {
                Tooted = new List<Toode>
                {
                    new Toode(3, "Toode3", 20.0, true),
                    new Toode(4, "Toode4", 25.0, false)
                }
            }
        };

        // GET: api/kasutajadtooted/{kasutajaId}/tooted
        [HttpGet("{kasutajaId}/tooted")]
        public ActionResult<List<Toode>> GetProducts(int kasutajaId)
        {
            var kasutaja = _kasutajad.FirstOrDefault(k => k.Id == kasutajaId);
            if (kasutaja == null)
            {
                return NotFound("Kasutajat ei leitud.");
            }
            return Ok(kasutaja.Tooted);
        }

        // POST: api/kasutajadtooted/{kasutajaId}/lisa
        [HttpPost("{kasutajaId}/lisa")]
        public ActionResult<List<Toode>> AddProduct(int kasutajaId, [FromBody] Toode newToode)
        {
            var kasutaja = _kasutajad.FirstOrDefault(k => k.Id == kasutajaId);
            if (kasutaja == null)
            {
                return NotFound("Kasutajat ei leitud.");
            }

            kasutaja.Tooted.Add(newToode);
            return Ok(kasutaja.Tooted);
        }

        // DELETE: api/kasutajadtooted/{kasutajaId}/kustuta/{toodeId}
        [HttpDelete("{kasutajaId}/kustuta/{toodeId}")]
        public ActionResult<List<Toode>> DeleteProduct(int kasutajaId, int toodeId)
        {
            var kasutaja = _kasutajad.FirstOrDefault(k => k.Id == kasutajaId);
            if (kasutaja == null)
            {
                return NotFound("Kasutajat ei leitud.");
            }

            var toode = kasutaja.Tooted.FirstOrDefault(t => t.Id == toodeId);
            if (toode == null)
            {
                return NotFound("Toodet ei leitud.");
            }

            kasutaja.Tooted.Remove(toode);
            return Ok(kasutaja.Tooted);
        }

        // PUT: api/kasutajadtooted/{kasutajaId}/uuenda
        [HttpPut("{kasutajaId}/uuenda")]
        public IActionResult UpdateProduct(int kasutajaId, [FromBody] Toode updatedToode)
        {
            var kasutaja = _kasutajad.FirstOrDefault(k => k.Id == kasutajaId);
            if (kasutaja == null)
            {
                return NotFound("Kasutajat ei leitud.");
            }

            var existingToode = kasutaja.Tooted.FirstOrDefault(t => t.Id == updatedToode.Id);
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
