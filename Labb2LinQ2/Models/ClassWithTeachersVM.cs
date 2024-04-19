namespace Labb2LinQ2.Models
{
    public class ClassWithTeachersVM
    {
        public IEnumerable<ClassWithTeachers> Classes { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
    }
}