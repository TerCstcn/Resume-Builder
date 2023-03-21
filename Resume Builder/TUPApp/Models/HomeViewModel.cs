namespace TUPApp.Models
{
    public class HomeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email {get; set;}
        public List<Skill1> Skills { get; set; }
        public List<Education1> Education { get; set; }

        public List<Experience1> Experiences { get; set; }

        public List<Training1> Trainings { get; set; }

        public List<Emergency1> Emergency {get; set; }  

    }
    public class Skill1
    {
        public int Id { get; set; }
        public string SkillName { get; set; }
    }
    public class Education1
    {
        public int Id { get; set; }
        public string School { get; set; }
        public int Year { get; set; }
    }
    public class Experience1
    {
        public int Id { get; set; }

        public string JobPosition { get; set; }

        public int Year { get; set; }
    }

    public class Training1
    {
        public int Id { get; set; }
        public string TrainingName { get; set; }

        public int Year { get; set; }

        public string Address { get; set; }
    }

    public class Emergency1
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Contact { get; set; }
    }

    

}
