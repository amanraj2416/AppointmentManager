using System.ComponentModel.DataAnnotations;

namespace AppointmentManagerWebApp.Models
{
    public class ViewAppointment
    {
        [Required(ErrorMessage = "Name is required")]
        public string? PatientName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string? PatientEmail { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        public string? PatientPhone { get; set; }
        public int? DoctorIdFk { get; set; }
        public DateTime? AppointmentDate { get; set; }

        public string? AppointmentTime { get; set; }
    }
}
