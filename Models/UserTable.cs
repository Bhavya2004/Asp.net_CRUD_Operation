using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUD_with_sql.Models
{
    public partial class UserTable
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public bool IsActive { get; set; }
    }
}
