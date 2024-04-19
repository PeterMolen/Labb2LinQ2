using System.ComponentModel.DataAnnotations;

namespace Labb2LinQ2.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
        public virtual ICollection<Teaching>? Teachings { get; set; }
    }
}
