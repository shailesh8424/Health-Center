using System.ComponentModel.DataAnnotations;

namespace healt_Center.Models
{
    public class Branch
    {
        //-----------------Barnch---------------------
        [Key]
        public int? Branch_Id { get; set; }
        public string? Branch_Description { get; set; }
        public long? Branch_number { get; set; }
        public string? Branch_email { get; set; }
        public string? Branch_mon_fri { get; set; }
        public string? Branch_Saturday { get; set; }
        public string? Branch_sunday { get; set; }
        public string? Branch_facebook { get; set; }
        public string? Branch_twitter { get; set; }
        public string? Branch_instagram { get; set; }
        public string? Branch_Logo { get; set; }
    }
}
