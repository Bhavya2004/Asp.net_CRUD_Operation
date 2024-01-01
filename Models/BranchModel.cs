using System.ComponentModel.DataAnnotations;

namespace CRUD_with_sql.Models
{
	public class BranchModel
	{
		public int? BranchId { get; set; }

		[Required]
		public String? BranchName { get; set; }
        [Required]
        public String? BranchCode { get; set; }

		public DateTime? Created { get; set; }

		public DateTime? Modified { get; set; }
	}
}
