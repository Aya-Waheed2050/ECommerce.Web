using System.ComponentModel.DataAnnotations;

namespace AdminDashBoard.Models.Roles
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Name Is Required")]
        [StringLength(256, ErrorMessage = "Role Name Cant not Exceed 256 Characters")]
        public string Name { get; set; }
    }
}
