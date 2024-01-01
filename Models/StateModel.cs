using System.ComponentModel.DataAnnotations;

namespace Data_Annotation.Models
{
    public class StateModel
    {

        public int? StateID { get; set; }

        [Required]
        public String? StateName { get; set; }
        [Required]
        public String? StateCode { get; set; }

        public int? CountryId { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }

    public class StateDropDownModel
    {
        public int StateId { get; set; }
        public String StateName { get; set; }
    }
    public class LOC_StateFilterModel
    {
        public string? StateName { get; set; }
        public string? StateCode { get; set; }
        public int? CountryID { get; set; }
    }
}