
using LDAP.Models;

namespace LDAP.Interface
{
    public interface Icrud<T> where T : class
    {
        List<Users> GetAllUsers();

    }
}
