using healt_Center.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace healt_Center.Controllers
{
    public class DaseboardController : Controller
    {
        private readonly Patient_DbContext _context;
        public DaseboardController(Patient_DbContext context)
        {

            _context = context;

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var allbranch= _context.tbl_Branch.Where(a=> a.Branch_Id==1).FirstOrDefault();
            if(allbranch !=null)
            {
                ViewData["Branch_Description"] =allbranch.Branch_Description;
                ViewData["Branch_number"] =allbranch.Branch_number;
                ViewData["Branch_email"] =allbranch.Branch_email;
                ViewData["Branch_mon_fri"] =allbranch.Branch_mon_fri;
                ViewData["Branch_Saturday"] =allbranch.Branch_Saturday;
                ViewData["Branch_sunday"] =allbranch.Branch_sunday;
                ViewData["Branch_facebook"] =allbranch.Branch_facebook;
                ViewData["Branch_twitter"] =allbranch.Branch_twitter;
                ViewData["Branch_instagram"] =allbranch.Branch_instagram;
                ViewData["Branch_Logo"] = Url.Content(allbranch.Branch_Logo);
            }
            base.OnActionExecuting(context);
        }
        public IActionResult Index()
        {
            return View(_context.tbl__Doctor.ToList());
        }
        [HttpPost]
        public IActionResult Index(patient adddata)
        {
            _context.patients.Add(adddata);
            _context.SaveChanges();
            return View("index");
        }

    }
}
