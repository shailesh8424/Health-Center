using System.ComponentModel.DataAnnotations;

namespace healt_Center.Models
{
    public class admin
    {
        [Key]
        public int Id { get; set; }
        public string? admin_profile { get; set; }
        public string? admin_email { get; set; }
        public string? admin_Password { get; set; }
        public string? admin_name { get; set; }
    }
}
