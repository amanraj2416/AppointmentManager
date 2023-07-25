﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentManagerWebApp.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? PatientName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string? PatientEmail { get; set;}
        [Required(ErrorMessage = "Phone number is required")]
        public string? PatientPhone { get; set;}
        public string Status { get; set; } = "open";
        [ForeignKey(nameof(Doctor))]
        public int? DoctorIdFk { get; set; }
        public DateTime? AppointmentDate { get; set; }

        public string? AppointmentTime { get; set; }
        public virtual Doctor? Doctor{ get; set; }
    }
}
