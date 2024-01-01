using System;
using System.Collections.Generic;

namespace CRUD_with_sql.Models
{
    public partial class LocCountry
    {
        public LocCountry()
        {
            LocCities = new HashSet<LocCity>();
            LocStates = new HashSet<LocState>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; } = null!;
        public string CountryCode { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public virtual ICollection<LocCity> LocCities { get; set; }
        public virtual ICollection<LocState> LocStates { get; set; }
    }
}
