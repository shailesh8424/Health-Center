using System.ComponentModel.DataAnnotations;

namespace healt_Center.Models
{
    public class Doctor
    {
        [Key]
        public int doctor_id { get; set; }
        public string? doctor_name { get; set; }
        public string? doctor_email { get; set; }
        public long? doctor_number { get; set; }
        public string? doctor_department { get; set; }
        public string? doctor_gender { get; set; }
        public string? doctor_profile { get; set; }
        public string? doctor_linkedin_profile { get; set; }



       
    }
}
