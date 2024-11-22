using Microsoft.EntityFrameworkCore;

namespace healt_Center.Models
{
    public class Patient_DbContext : DbContext
    {
        public Patient_DbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<patient> patients { get; set; }
        public DbSet<admin> tbl_admin { get; set; }
        public DbSet<Add_Department> tbl_Departments { get; set; }
        public DbSet<Doctor> tbl__Doctor { get; set; }
        public DbSet<Branch> tbl_Branch { get; set; }
       







    }
}
