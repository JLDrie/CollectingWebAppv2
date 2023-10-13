using CollectingWebApp.Data;
using CollectingWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CollectingWebAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CollectionsController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/<CollectionsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collection>>> GetCollection()
        {
            if (_context.Collection == null)
            {
                return NotFound();
            }
            return await _context.Collection/*.Include(o => o.Category)*/.ToListAsync();
        }

        // GET api/<CollectionsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Collection>> GetCollection(int id)
        {
            if (_context.Collection == null)
            {
                return NotFound();
            }
            var collection = await _context.Collection.FindAsync(id);

            if (collection == null)
            {
                return NotFound();
            }

            return collection;
        }

        // POST api/<CollectionsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CollectionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CollectionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
