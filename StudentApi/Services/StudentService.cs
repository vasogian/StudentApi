using Microsoft.EntityFrameworkCore;
using StudentApi.Models;
using System.Configuration;

namespace StudentApi.Services
{
    public class StudentService
    {
        private readonly StudentContext? _context;
        public StudentService(StudentContext? context)
        {
            _context = context;
        }

        public void AddStudent(Student student)
        {
            _context.Students.Add(student);

            _context.SaveChanges();
        }

        public Student GetStudentFromDb(long id)
        {
            var selectedStudent = _context.Students
                .Include(x => x.studentSubjects)
                .FirstOrDefault(x => x.Id == id);
            return selectedStudent;
        }

        public List<Student> GetAllStudents()
        {
            var allStudents = _context.Students
                .Include(x => x.studentSubjects)
                .ToList();
            return allStudents;
        }
        public int DeleteStudentFromDb(long id)
        {
            var studentToDelete = GetStudentFromDb(id);
            if (studentToDelete != null)
            {
                _context.Students.Remove(studentToDelete);
                _context.SaveChanges();

                return 1;
            }
            return 0;
        }
        public Student UpdateStudentFromDb(long id, Student student)
        {
            var studentToUpdate = GetStudentFromDb(id);
            if (studentToUpdate != null)
            {
                studentToUpdate.Grade = student.Grade;
                studentToUpdate.Name = student.Name;
                studentToUpdate.studentSubjects = student.studentSubjects;
                _context.Entry(studentToUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }
            return studentToUpdate;
        }

        
    }
}
