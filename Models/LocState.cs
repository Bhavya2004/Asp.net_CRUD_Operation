using System;
using System.Collections.Generic;

namespace CRUD_with_sql.Models
{
    public partial class LocState
    {
        public LocState()
        {
            LocCities = new HashSet<LocCity>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; } = null!;
        public int CountryId { get; set; }
        public string StateCode { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public virtual LocCountry Country { get; set; } = null!;
        public virtual ICollection<LocCity> LocCities { get; set; }
    }
}
