using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logins.Data;
using Logins.Models;
using Logins.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Logins.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly UserContext _context;

        public LoginsController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<User>> GetAll()
        {
            //remove password before returing user
            return _context.Users.Where(o => o.Name == o.Name).ToList().Select(o =>
                                                                                  {
                                                                                         o.password = ""; 
                                                                                         return o;
                                                                                  }

                                                                              ).ToList();
        }

        // GET by ID action
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.password = "";
            return user;
        }

        // POST action
        [Route("validate")]
        [HttpPost]
        public async Task<ActionResult<User>> Validate( User user)
        {
            //Ideally this should be in different controller

            if (!ModelState.IsValid) return BadRequest();
            var founduser = await _context.Users.Where(o => o.Name == user.Name).FirstOrDefaultAsync();

            if (founduser!=null && (Cryptic.GetHash(user.password) == founduser.password))
            {
                user.password = "";
                return user;
            }

            return NotFound();

        }

        // POST action
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid) return BadRequest();

            user.password = Cryptic.GetHash(user.password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            user.password = "";
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);

        }

        // DELETE action
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
            return NoContent();


        }

    }
}