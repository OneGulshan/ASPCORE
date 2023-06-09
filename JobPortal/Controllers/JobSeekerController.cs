using JobPortal.Data;
using JobPortal.Models;
using JobPortal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Net.Mail;

namespace JobPortal.Controllers
{
    [Authorize]
    public class JobSeekerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public JobSeekerController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var result = _context.Employees.ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.JobProfile = new SelectList(_context.JobProfile.ToList(), "JPId", "Name");
            ViewBag.Country = new SelectList(_context.Countries.ToList(), "CId", "Name");

            if (id > 0)
            {
                var Employee = _context.Employees.Where(_ => _.EId == id).FirstOrDefault();
                GetSkills(Employee.JobProfileId);
                GetStates(Employee.CountryId);
                GetCity(Employee.StateId);
                ViewBag.Bt = "Update";
                return View(Employee);
            }
            else
            {
                ViewBag.BT = "Create";
                return View();
            }
        }

        public JsonResult GetSkills(int JPId)
        {
            var SkillsList = _context.Skills.Where(_ => _.JPId == JPId).ToList();
            ViewBag.Skills = new SelectList(SkillsList, "SklId", "Name");
            return Json(ViewBag.Skills);
        }

        public JsonResult GetStates(int CId)
        {
            var StatesList = _context.States.Where(_ => _.CId == CId).ToList();
            ViewBag.State = new SelectList(StatesList, "SId", "Name");
            return Json(ViewBag.State);
        }

        public JsonResult GetCity(int StateId)
        {
            var CitiesList = _context.Cities.Where(_ => _.SId == StateId).ToList();
            ViewBag.City = new SelectList(CitiesList, "CityId", "Name");
            return Json(ViewBag.City);
        }


        [HttpPost]
        public IActionResult Create(Employee employee, int id)
        {
            if (id == 0)
            {
                string fileName = FileUpload(employee);

                employee.ImagePath = fileName;
                _context.Employees.Add(employee);
                _context.SaveChanges();
            }
            else if (id != 0)
            {
                var Employee = _context.Employees.Find(id);
                Employee.EId = employee.EId;
                Employee.Name = employee.Name;
                Employee.Gender = employee.Gender;
                Employee.JobProfileId = employee.JobProfileId;
                Employee.SkillsId = employee.SkillsId;
                Employee.Email = employee.Email;
                Employee.Age = employee.Age;
                Employee.Mobno = employee.Mobno;
                Employee.CountryId = employee.CountryId;
                Employee.StateId = employee.StateId;
                Employee.CityId = employee.CityId;
                Employee.Comment = employee.Comment;

                var oldImagePath = Path.Combine(_environment.WebRootPath, "Images", Employee.ImagePath);

                if (employee.Password == null)
                {
                    Employee.Password = Employee.Password;
                }
                if (employee.Password != null)
                {
                    Employee.Password = employee.Password;
                }

                if (employee.ImageFile != null)
                {
                    string fileName = FileUpload(employee);

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    Employee.ImagePath = fileName;
                }

                _context.Employees.Update(Employee);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        private string FileUpload(Employee employee)
        {
            string uploadDir = Path.Combine(_environment.WebRootPath, "Images");
            string fileName = Guid.NewGuid().ToString() + "-" + employee.ImageFile.FileName;
            string filePath = Path.Combine(uploadDir, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                employee.ImageFile.CopyTo(fileStream);
            }

            return fileName;
        }

        public IActionResult Delete(int id)
        {
            var Employee = _context.Employees.Find(id);
            _context.Employees.Remove(Employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult MailSent()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult MailSent(Message messageDetails)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClientsmtp = new SmtpClient();
            message.From = new MailAddress("gulshankumar.mailid01@gmail.com");
            message.To.Add(messageDetails.Email);
            message.Subject = "Test Mail";


            string MailBody = "<!DOCTYPE html>" +
  "<body style=\"display:flex; justify-content:center;\">" +
  "<div style=\"min-height: 10rem; width: 40rem; background-color: aqua; padding: 1rem; border-radius: 10px;\">" +
       " <table style=\"width:100%; color:white; border: 1px solid black; text-align: center;\">" +
           " <thead style=\"color:black;\">" +
                "<tr>" +
                   " <th style=\"border: 1px solid black;\"> Name </th>" +
                   " <th style=\"border: 1px solid black;\"> Email </th>" +
                   " <th style=\"border: 1px solid black;\"> Message </th>" +
               " </tr>" +
            "</thead>" +
            "<tbody>" +
                "<tr>" +
                   $" <td style=\"border: 1px solid black;\"> {messageDetails.Name} </td>" +
                    $"<td style=\"border: 1px solid black;\"> {messageDetails.Email} </td>" +
                   $" <td style=\"border: 1px solid black;\"> {messageDetails.MessageData} </td>" +
                "</tr>" +
           " </tbody>" +
        "</table>" +
"</body>" +

"</html>";
            message.Body = MailBody;

            message.IsBodyHtml = true;
            smtpClientsmtp.Port = 587;
            smtpClientsmtp.Host = "smtp.gmail.com";
            smtpClientsmtp.EnableSsl = true;
            smtpClientsmtp.UseDefaultCredentials = false;
            smtpClientsmtp.Credentials = new NetworkCredential("gulshankumar.mailid01@gmail.com", "aeljomqrgsaqgtkv");
            smtpClientsmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClientsmtp.Send(message);
            ModelState.Clear();

            return View();
        }

        public IActionResult ViewJobs()
        {
            var result = (from jp in _context.JobPosts
                          join c in _context.Companies
                          on jp.JobProfileId equals c.JobProfileId
                          join jpr in _context.JobProfile
                          on jp.JobProfileId equals jpr.JPId
                          select new
                          {
                              jp.MinExp,
                              jp.MaxExp,
                              jp.MinSal,
                              jp.MaxSal,
                              jp.NoOfVac,
                              jp.NoticePeriod,
                              jp.Comment,
                              jp.InsertedDate,
                              c.Id,
                              CName = c.Name,
                              JPName = jpr.Name
                          }).ToList();

            List<JobsViewJobSeekerViewModel> vm = new();

            foreach (var i in result)
            {
                vm.Add(new JobsViewJobSeekerViewModel { MinExp = i.MinExp, MaxExp = i.MaxExp, MinSal = i.MinSal,
                MaxSal = i.MaxSal, NoOfVac = i.NoOfVac, NoticePeriod = i.NoticePeriod, Comment = i.Comment, InsertedDate = i.InsertedDate,
                CompanyId = i.Id , CompanyName = i.CName, JPName = i.JPName });
            }

            return View(vm);
        }
    }
}
