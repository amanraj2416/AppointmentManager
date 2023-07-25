using AppointmentManagerWebApp.Data;
using AppointmentManagerWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using BCrypt.Net;
namespace AppointmentManagerWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
            
        }
        //Function to get doctors

        [HttpGet]
        public async Task<ActionResult> GetDoctors()
        {
            var doctors= await _context.Doctors.ToListAsync();
            return Ok(doctors);
        }

        //Function to login 
        [HttpPost("login")]
        public async Task<ActionResult> Login(Login loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var error = new Dictionary<string, string>();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
            {
                error.Add("Email", "User not found"); // User not found
            }
            else
            {
                // Compare the entered password with the hashed password from the database
                if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                {
                    error.Add("Password", "Invalid password");
                }
            }
            if (error.Count > 0)
            {

                return BadRequest(new
                {
                    errors = error

                });
                    
                    
            }
            return Ok(user);
        }
    }
}
