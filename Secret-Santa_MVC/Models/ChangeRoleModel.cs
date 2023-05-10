using Microsoft.AspNetCore.Identity;

namespace Secret_Santa_MVC.Models
{
    public class ChangeRoleModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<IdentityRole<long>> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRoleModel()
        {
            AllRoles = new List<IdentityRole<long>>();
            UserRoles = new List<string>();
        }
    }
}
