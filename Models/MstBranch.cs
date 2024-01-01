using System;
using System.Collections.Generic;

namespace CRUD_with_sql.Models
{
    public partial class MstBranch
    {
        public MstBranch()
        {
            MstStudents = new HashSet<MstStudent>();
        }

        public int BranchId { get; set; }
        public string BranchName { get; set; } = null!;
        public string BranchCode { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public virtual ICollection<MstStudent> MstStudents { get; set; }
    }
}
