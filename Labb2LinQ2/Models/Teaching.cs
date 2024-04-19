using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Labb2LinQ2.Models
{
    public class Teaching
    {
        [Key]
        public int TeachingId { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }

        [ForeignKey("Class")]
        public int ClassId { get; set; }

        //navigation
        public virtual Class? Class { get; set; }
        public virtual Teacher? Teacher { get; set; }
    }
}
