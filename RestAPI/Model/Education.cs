using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI.Model
{
    [Table("tb_m_educations")]
    public class Education : BaseEntity
    {
        [Column("major",TypeName ="nvarchar(100)")]
        public string Major { get; set; }
        [Column("degree", TypeName = "nvarchar(10)")]
        public string Degree { get; set; }
        [Column("gpa")]
        public float GPA { get; set; }
        [Column("university_guid")]
        public Guid UniversityGuid { get; set; }

        // kardinalitas
        public University? University { get; set; }
        public Employee? Employee { get; set; }

    }
}
