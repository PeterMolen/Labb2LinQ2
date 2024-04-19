using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Labb2LinQ2.Models
{
    public enum Grade
    {
        A, B, C, D, E, F
    }
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }
        public Grade? Grade { get; set; }

        //Fk
        [ForeignKey("Class")]
        public int FkClassId { get; set; }

        [ForeignKey("Student")]
        public int FkStudentId { get; set; }
        [ForeignKey("Teacher")]
        public int FkTeacherId { get; set; }

        //navigation
        public virtual Class? Class { get; set; }
        public virtual Student? Student { get; set; }
        public virtual Teacher? Teacher { get; set; }

    }
}
