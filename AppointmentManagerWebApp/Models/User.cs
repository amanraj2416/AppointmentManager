using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentManagerWebApp.Models
{
    //user model to add new user
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        public string ? Name { get; set; }
        [Required]
        public string ? Email { get; set; }
        [Required]
        public string ? Password { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
           
    }
}
