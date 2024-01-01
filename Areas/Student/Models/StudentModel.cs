using System.ComponentModel.DataAnnotations;

namespace CRUD_with_sql.Areas.Student.Models
{
    public class StudentModel
    {
            public int? StudentID { get; set; }
        [Required]
        public string StudentName { get; set; }
        [Required]
        public string MobileNoStudent { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string MobileNoFather { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string BirthDate { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public string  Gender { get; set; }
        [Required]
        public string Password { get; set; }
            public int? BranchID { get; set; }
            public int? CityID { get; set; }
            public int? CountryID { get; set; }
    }
}
