namespace Secret_Santa_MVC.Models.Identity
{
    public class ScopeAttribute : Attribute
    {
        public string Scope { get; set; }
        public ScopeAttribute(string scope) 
        {
            Scope = scope;
        }
    }
}
