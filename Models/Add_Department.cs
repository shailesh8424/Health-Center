using System.ComponentModel.DataAnnotations;

namespace healt_Center.Models
{
    public class Add_Department
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Department { get; set; }
        public List<patient> patients { get; set; }
    }
}
