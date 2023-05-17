namespace RestAPI.Model
{
    public class Education : BaseEntity
    {
        public string Major { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }
        public Guid UniversityGuid { get; set; }
    }
}
