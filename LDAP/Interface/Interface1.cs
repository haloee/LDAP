using LDAP.Controllers;
using LDAP.Models;

namespace LDAP.Interface
{
    public interface Interface1
    {
        List<Users> GetAllUsers();
        void SearchUserByAccountName(string accountName);
       public User GetUser(string username);
    }
}
