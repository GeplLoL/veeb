using Microsoft.AspNetCore.Mvc;
using veeb2._0.Models;

namespace veeb.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ToodeController : ControllerBase
    {
        private static Toode _toode = new Toode(1, "Koola", 1.5, true);

        // GET: toode
        [HttpGet]
        public Toode GetToode()
        {
            return _toode;
        }

        // GET: toode/suurenda-hinda
        [HttpGet("suurenda-hinda")]
        public Toode SuurendaHinda()
        {
            _toode.Price += 1;
            return _toode;
        }

        // GET: toode/muuda-aktiivsus
        [HttpGet("muuda-aktiivsus")]
        public Toode MuudaAktiivsus()
        {
            _toode.IsActive = !_toode.IsActive;
            return _toode;
        }

        // GET: toode/muuda-nimi/{nimi}
        [HttpGet("muuda-nimi/{nimi}")]
        public Toode MuudaNimi(string nimi)
        {
            _toode.Name = nimi;
            return _toode;
        }

        // GET: toode/korruta-hind/{kordaja}
        [HttpGet("korruta-hind/{kordaja}")]
        public Toode KorrutaHind(double kordaja)
        {
            _toode.Price *= kordaja;
            return _toode;
        }
    }
}
