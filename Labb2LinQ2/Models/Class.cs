using System.ComponentModel.DataAnnotations;

namespace Labb2LinQ2.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        public string ClassName { get; set; }

        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}
