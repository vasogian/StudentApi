using Microsoft.EntityFrameworkCore;
using StudentApi.Models;
namespace StudentApi.Services
{
    public class TeacherService
    {
        private readonly StudentContext _studentContext;
        public TeacherService(StudentContext studentContext)
        {
            _studentContext = studentContext;
        }


        public void AddTeacher(Teacher teacher)
        {
            _studentContext.Teachers.Add(teacher);
            _studentContext.SaveChanges();

        }

        public Teacher GetTeacher(int id)
        {
            var selectTeacher = _studentContext.Teachers
                .Include(x => x.subjects)
                .FirstOrDefault(x => x.Id == id);
            return selectTeacher;
        }

        public List<Teacher> GetAllTeachers()
        {
            var getAllTeachers = _studentContext.Teachers
                .Include(x=>x.subjects)
                .ToList();
            return getAllTeachers;
        }

        public int DeleteTeacher(int id)
        {
            var teacherToDelete = GetTeacher(id);
            if (teacherToDelete != null)
            {
                _studentContext.Remove(teacherToDelete);
                _studentContext.SaveChanges();

                return 1;
            }
            return 0;
        }

        public Teacher UpdateStudent(int id, Teacher teacher)
        {
            var teacherToUpdate = GetTeacher(id);
            if (teacherToUpdate != null)
            {
                teacherToUpdate.Name = teacher.Name;
                teacherToUpdate.subjects = teacher.subjects;
                _studentContext.Entry(teacherToUpdate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _studentContext.SaveChanges();
            }
            return teacherToUpdate;

        }
    }
}
