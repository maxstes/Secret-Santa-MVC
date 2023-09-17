namespace Secret_Santa_MVC.Data.Entities
{
    public class ChangePasswordViewModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
