using System.ComponentModel.DataAnnotations;
using AdminDashBoard.Models.Roles;

namespace AdminDashBoard.Models.Users
{
    public class UserRoleViewModel
    {
        [Display(Name = "User Id")]
        public string UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }
        public List<UpdateRoleViewModel> Roles { get; set; }
    }
}
