using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TUPApp.Models;

namespace TUPApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TupContext _context;

        private readonly ILogger<HomeController> _logger;

        public static string Title = "";
        public static string Description = "";
        public static string Gender = "";

        public HomeController(ILogger<HomeController> logger, TupContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index()
        {
            var student = _context.Students
                                    //.Where(x => x.Id == 1) -Use if you want specific Id   
                                    .FirstOrDefault();

            var vm = new HomeViewModel()
            { 
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                LastName = student.LastName,
                Address = student.Address,
                Contact = student.Contact,
                Email = student.Email
            };

            //--------------------SKILLS--------------------
            var skills = _context.Skills
                                .Where (x => x.StudentId == student.Id)
                                .ToList();

            vm.Skills = new List<Skill1>();

            foreach (var l in skills) 
            {
                var sk = new Skill1
                {
                    Id = l.Id,
                    SkillName = l.Skill1
                };
                vm.Skills.Add(sk);
            }

            //--------------------EDUCATION--------------------
            var education = _context.EducationalBackgrounds
                                .Where(x => x.StudentId == student.Id)
                                .ToList();

            vm.Education = new List<Education1>();

            foreach (var e in education)
            {
                var ed = new Education1
                {
                    Id = e.Id,
                    School = e.School,
                    Year = (int)e.Year

                };
                vm.Education.Add(ed);
            }

            //--------------------EXPERIENCES--------------------
            var experiences = _context.Experiences
                                .Where(x => x.StudentId == student.Id)
                                .ToList();

            vm.Experiences = new List<Experience1>();

            foreach (var ex in experiences)
            {
                var exp = new Experience1
                {
                    Id = ex.Id,
                    JobPosition = ex.JobPosition,
                    Year= (int)ex.Year

                };
                vm.Experiences.Add(exp);
            }
            //--------------------TRAININGS--------------------
            var trainings = _context.TrainingAttendeds
                                .Where(x => x.StudentId == student.Id)
                                .ToList();

            vm.Trainings = new List<Training1>();

            foreach (var t in trainings)
            {
                var tr = new Training1
                {
                    Id = t.Id,
                    Year =  (int)t.Year,
                    TrainingName = t.TrainingName,
                    Address = t.Address

                };
                vm.Trainings.Add(tr);
            }
            //--------------------Emergency--------------------
            var emergency = _context.EmergencyContacts
                                .Where(x => x.StudentId == student.Id)
                                .ToList();

            vm.Emergency = new List<Emergency1>();

            foreach (var em in emergency)
            {
                var eme = new Emergency1
                {
                    Id = em.Id,
                    FirstName = em.FirstName,
                    LastName = em.LastName,
                    Contact = em.Contact

                };
                vm.Emergency.Add(eme);
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult Insert(HomeModel model)
        {
            Title = model.Title;
            Description = model.Description;
            Gender = model.Gender;

            return RedirectToAction("Index", "Sample");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}