using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentManagerWebApp.Models
{
    //Model to add doctor info
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }
        [Required(ErrorMessage ="Doctor Name is required")]
        public string? DoctorName { get; set;}

        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }

        public int AppointmentSlotTime { get; set; } = 30;

        public TimeSpan DayStartTime { get; set; } = TimeSpan.Parse("09:00");

        public TimeSpan DayEndTime { get; set; } = TimeSpan.Parse("19:00");


        public virtual User? User { get; set; }
    }
}
