using System.ComponentModel.DataAnnotations;

namespace CRUD_with_sql.Models
{
    public class CityModel
    {
        public int? CityId { get; set; }

        [Required]
        public String? CityName { get; set; }
        public int? CountryID { get; set; }
        public int? StateID { get; set; }
        [Required]
        public String? CityCode { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? Modified { get; set; }
    }

    public class CountryDropDownModel
    {
        public int CountryId { get; set; }
        public String CountryName { get; set; }
    }

    public class StateDropDownModel
    {
        public int StateId { get; set; }
        public String StateName { get; set; }
    }
    public class LOC_CityDropDownModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
    }

    public class LOC_CityFilterModel
    {
        public int? CountryID { get; set; }
        public int? StateID { get; set; }
        public string? CityName { get; set; }
        public string? CityCode { get; set; }
    }
}
