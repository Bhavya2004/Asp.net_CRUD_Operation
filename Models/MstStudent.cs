using System;
using System.Collections.Generic;

namespace CRUD_with_sql.Models
{
    public partial class MstStudent
    {
        public int StudentId { get; set; }
        public int BranchId { get; set; }
        public int CityId { get; set; }
        public string StudentName { get; set; } = null!;
        public string MobileNoStudent { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? MobileNoFather { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Age { get; set; }
        public bool? IsActive { get; set; }
        public string? Gender { get; set; }
        public string? Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public virtual MstBranch Branch { get; set; } = null!;
        public virtual LocCity City { get; set; } = null!;
    }
}
