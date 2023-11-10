namespace LDAP.Models
{
    public class Users
    {
        public string Name { get; set; }
        public string sAMAccountName { get; set; }
        public string department { get; set; }
        public string mail { get; set; }
        public string extensionAttribute { get; set; }
        public string manager { get; set; }
    }
}
