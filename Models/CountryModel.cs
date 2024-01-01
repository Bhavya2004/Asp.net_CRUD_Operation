using System.ComponentModel.DataAnnotations;

namespace Data_Annotation.Models
{
    public class CountryModel
    {

        public int? CountryId { get; set; }

        [Required]
        public String? CountryName { get; set; }
        [Required]
        public String? CountryCode { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }

    public class CountryDropDownModel
    {
        public int CountryId { get; set; }
        public String CountryName { get; set;}
    }
}