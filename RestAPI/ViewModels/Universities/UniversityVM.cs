using RestAPI.Model;

namespace RestAPI.ViewModels.Universities
{
    public class UniversityVM
    {
        public Guid? Guid { get; set; } 
        public string Code { get; set; }
        public string Name { get; set; }

        public static UniversityVM ToVM(University university) {
            return new UniversityVM
            {
                Guid = university.Guid,
                Code = university.Code,
                Name = university.Name
            };
        }

        public static University ToModel(UniversityVM universityVM) {
            return new University()
            {
                Guid = (Guid)universityVM.Guid,
                Code = universityVM.Code,
                Name = universityVM.Name,
                CreatedDate= DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
        /*public static University ToUpdate(UniversityVM universityVM)
        {
            return new University()
            {
                Guid = (Guid)universityVM.Guid,
                Code = universityVM.Code,
                Name = universityVM.Name,
                ModifiedDate = DateTime.Now
            };
        }*/
    }
}
