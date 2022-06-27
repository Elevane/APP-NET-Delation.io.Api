using Microsoft.AspNetCore.Mvc;

namespace Delation.io.Api
{
    [ApiController]
    [Route("api/delations")]
    public class DelationsController : Controller
    {
        private readonly DelationContext _context;

        public DelationsController(DelationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Delations.ToList());
        }

        [HttpPost]
        public IActionResult Post(Delation delation)
        {
            _context.Delations.Add(delation);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int Id) { 
            Delation toDelete = _context.Delations.Find(Id);
            _context.Delations.Remove(toDelete);
            _context.SaveChanges();
            return Ok();
        }
    }
}
