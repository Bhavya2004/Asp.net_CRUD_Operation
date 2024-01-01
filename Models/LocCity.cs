using System;
using System.Collections.Generic;

namespace CRUD_with_sql.Models
{
    public partial class LocCity
    {
        public LocCity()
        {
            MstStudents = new HashSet<MstStudent>();
        }

        public int CityId { get; set; }
        public string CityName { get; set; } = null!;
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string Citycode { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public DateTime Modified { get; set; }

        public virtual LocCountry Country { get; set; } = null!;
        public virtual LocState State { get; set; } = null!;
        public virtual ICollection<MstStudent> MstStudents { get; set; }
    }
}
