using AppointmentManagerWebApp.Data;
using AppointmentManagerWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace AppointmentManagerWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }

        


        //Function to register a new user
        [HttpPost]
        public async Task<ActionResult> Create(ViewUser ob)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var error = new Dictionary<string, string>();

            // Check if the email already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == ob.Email);
            if (existingUser != null)
            {
                error.Add("Email", "Email is already taken.");
             
            }

            if (error.Count > 0)
            {
                return BadRequest(new
                {
                    errors = error

                });
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(ob.Password);

            var newUser = new User
            {
                Name = ob.Name,
                Email = ob.Email,
                Password = hashedPassword
            };

            var newDoctor = new Doctor
            {
                DoctorName = ob.Name,
                User = newUser
            };
             
           
            
            _context.Doctors.Add(newDoctor);
            _context.SaveChanges();
            return Ok("User created ");
        }
    }
}
