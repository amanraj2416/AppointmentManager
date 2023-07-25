using AppointmentManagerWebApp.Data;
using AppointmentManagerWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System;

namespace AppointmentManagerWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        //Function to add new appointment details 
        [HttpPost]
        public async Task<ActionResult> Create(Appointment ob)
        {
            //check Model state

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Appointments.Add(ob);
            await  _context.SaveChangesAsync();
            return Ok("User created ");
        }

        //Function to get appointments of a particular month

        [HttpGet("{selectedMonth}/appointment-summary")]
        public async Task<ActionResult> GetAppointmentsByMonth(int selectedMonth)
        {
            var result = await _context.Appointments
                    .Where(appointment => appointment.AppointmentDate.HasValue && appointment.AppointmentDate.Value.Month == selectedMonth)
                    .GroupBy(appointment => appointment.AppointmentDate)
                    .Select(groupedData => new
                    {
                        Date = groupedData.Key,
                        NumAppointments = groupedData.Count(),
                        NumClosedAppointments = groupedData.Count(a => a.Status == "close"),
                        NumCancelledAppointments = groupedData.Count(a => a.Status == "cancel"),
                        Patients = groupedData.Select(a => new { a.PatientName, a.Status }).ToList()
                    })
                    .ToListAsync();

            return Ok(result);
        }

        //Function to get booked slot on a particular date of a doctor

        [HttpGet("{doctorId}/bookedslots/{appointmentDate}")]
        public async Task<ActionResult> GetSlotInterval(int doctorId ,DateTime appointmentDate)
        {
            var bookedSlots = await  _context.Appointments
                        .Where(a => a.DoctorIdFk == doctorId && a.AppointmentDate == appointmentDate)
                        .Select(a => new
                        {
                            a.AppointmentTime
                        }).ToListAsync();
            return Ok(bookedSlots);
        }

        //Function to get appointments of a doctor

        [HttpGet("{doctorId}/appointments")]
        public async Task<ActionResult> GetAppointments(int doctorId)
        {
            var appointments = await _context.Appointments
                .Join(_context.Doctors,
                    appointment => appointment.DoctorIdFk,
                    doctor => doctor.DoctorId,
                    (appointment, doctor) => new { Appointment = appointment, Doctor = doctor })
                .Where(appointmentDoctor => appointmentDoctor.Doctor.UserId == doctorId)
                .Select(appointmentDoctor => appointmentDoctor.Appointment)
                .ToListAsync();
            return Ok(appointments);
        }

        //Function to get appointments of a doctor of a particular date

        [HttpGet("{doctorId}/appointments/{date}")]
        public async Task<ActionResult> GetAppointmentsByDate(int doctorId,DateTime date)
        {
            var appointments = await _context.Appointments
                .Join(_context.Doctors,
                    appointment => appointment.DoctorIdFk,
                    doctor => doctor.DoctorId,
                    (appointment, doctor) => new { Appointment = appointment, Doctor = doctor })
                .Where(appointmentDoctor => appointmentDoctor.Doctor.UserId == doctorId && appointmentDoctor.Appointment.AppointmentDate == date)
                .Select(appointmentDoctor => appointmentDoctor.Appointment)
                .ToListAsync();
            return Ok(appointments);
        }

        //Function to change status of an appointment

        [HttpPost("update-status")]
        public IActionResult UpdateStatus(UpdateView ob)
        {
            var existingAppointment = _context.Appointments.SingleOrDefault(a => a.AppointmentId == ob.AppointmentId);
            if (existingAppointment == null)
            {
                return NotFound();
            }
            existingAppointment.Status = ob.Status;
            _context.SaveChanges();
            return Ok("updated successfully");
        }
    }
}
  