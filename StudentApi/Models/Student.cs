using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace StudentApi.Models
{
    public class Student
    {
        [Required]
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public double Grade { get; set; }
        public List<Subject> studentSubjects {get; set; } = new List<Subject>();
    }
}
