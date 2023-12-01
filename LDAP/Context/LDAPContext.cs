
using System;
using System.DirectoryServices;
using LDAP.Controllers;
using LDAP.Interface;
using LDAP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LDAP.Context
{
    public class LDAPContext : Interface1 //Icrud
    {
        private DirectoryEntry ldapConnection;

       

        public DbSet<Users> Users { get; set; }

        private void InitializeLDAPConnection()
        {
            //string ldapPath = "LDAP://OU=Hauni Hungaria,DC=HUNGARIA,DC=KOERBER,DC=DE";
            DirectoryEntry ldapPath =new DirectoryEntry("LDAP://OU=Hauni Hungaria,DC=HUNGARIA,DC=KOERBER,DC=DE");
            ldapConnection = new DirectoryEntry(ldapPath);
           
        }

        
        public List<Users> GetAllUsers()
        {
            SearchResultCollection results;
            DirectorySearcher ds = null;
            ds= new DirectorySearcher(ldapConnection);
           
            ds.PropertiesToLoad.Add("displayName");
            ds.PropertiesToLoad.Add("sAMAccountName");
            ds.PropertiesToLoad.Add("department");
            ds.PropertiesToLoad.Add("mail");
            ds.PropertiesToLoad.Add("extensionattribute5");
            ds.PropertiesToLoad.Add("manager");

            ds.Filter = "(&(objectCategory=User))";

            results = ds.FindAll();
           List<Users> users = new List<Users>();
            foreach (SearchResult result in results)
            {
                Users user = new Users();
                user.Name = result.Properties.Contains("displayName") ? result.Properties["displayName"][0].ToString() : string.Empty;
                user.sAMAccountName = result.Properties.Contains("sAMAccountName") ? result.Properties["sAMAccountName"][0].ToString() : string.Empty;
                user.department = result.Properties.Contains("department") ? result.Properties["department"][0].ToString() : string.Empty;
                user.mail = result.Properties.Contains("mail") ? result.Properties["mail"][0].ToString() : string.Empty;
                user.extensionAttribute= result.Properties.Contains("extensionattribute5") ? result.Properties["extensionattribute5"][0].ToString() : string.Empty;
                user.manager= result.Properties.Contains("manager") ? result.Properties["manager"][0].ToString() : string.Empty;
                users.Add(user);
                //Console.WriteLine(result.Properties["displayName"][0].ToString());
                //Console.WriteLine(result.Properties["sAMAccountName"][0].ToString());
                //Console.WriteLine(result.Properties["sAMAccountName"][0].ToString());
            }

            return users;
        }
        public User GetUser(string username)
        {
            DirectorySearcher ds = new DirectorySearcher(ldapConnection);
            SearchResult result;
            //SearchResultCollection results;
            //DirectorySearcher ds = null;
            //ds = new DirectorySearcher(ldapConnection);



            ds.Filter = "(&(objectCategory=User)(sAMAccountName=" + username +"))";

            var sr = ds.FindOne();
           
            //if(sr != null)
            //{
               
            //}
            return BUser(sr);

           
        }
        public User BUser(SearchResult result)
        {
            User user = new User();
            user.Name = result.Properties.Contains("displayName") ? result.Properties["displayName"][0].ToString() : string.Empty;
            user.sAMAccountName = result.Properties.Contains("sAMAccountName") ? result.Properties["sAMAccountName"][0].ToString() : string.Empty;
            user.department = result.Properties.Contains("department") ? result.Properties["department"][0].ToString() : string.Empty;
            user.mail = result.Properties.Contains("mail") ? result.Properties["mail"][0].ToString() : string.Empty;
            user.extensionAttribute = result.Properties.Contains("extensionattribute5") ? result.Properties["extensionattribute5"][0].ToString() : string.Empty;
            user.manager = result.Properties.Contains("manager") ? result.Properties["manager"][0].ToString() : string.Empty;
            return user;
        }
       


        User Interface1.GetUser(string username)
        {
            DirectorySearcher ds = new DirectorySearcher(ldapConnection);
            SearchResult result;
            //SearchResultCollection results;
            //DirectorySearcher ds = null;
            //ds = new DirectorySearcher(ldapConnection);



            ds.Filter = "(&(objectCategory=User)(sAMAccountName=" + username + "))";

            var sr = ds.FindOne();

            //if(sr != null)
            //{

            //}
            return BUser(sr);
        }

        public void SearchUserByAccountName(string accountName)
        {
            throw new NotImplementedException();
        }
    }
}
