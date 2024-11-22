using Microsoft.VisualBasic;

namespace healt_Center.Models
{
    public class FixShedule
    {
        public int app_id { get; set; }
        public string app_name { get; set; }
        public string app_email { get; set; }
        public DateTime app_date_time { get; set; }
        public string app_department { get; set; }
        public long app_phone { get; set; }
        public string admin_message { get; set; }
        public DateAndTime admin_time { get; set; }
        public string appoint_sub_id { get; set; }
    }
}
