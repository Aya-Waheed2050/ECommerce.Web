using System.ComponentModel.DataAnnotations;

namespace AdminDashBoard.Models.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }


        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }

    }
}
