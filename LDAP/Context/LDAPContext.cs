using System;
using LDAP.Models;
using Microsoft.EntityFrameworkCore;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace LDAP.Context
{
    public class LDAPContext:DbContext 
    {
        public LDAPContext(DbContextOptions options):base(options) 
        {
            InitializeLDAPConnection();
        }
        private DirectoryEntry ldapConnection;
        private PrincipalContext principalContext;
        private void InitializeLDAPConnection()
        {
            string ldapPath = "LDAP://OU=Hauni Hungaria,DC=HUNGARIA,DC=KOERBER,DC=DE";
            //ldapConnection = new DirectoryEntry(ldapPath);
            principalContext=new PrincipalContext(ContextType.Domain,ldapPath);
        }

        public DbSet<Users> Users { get; set; }
        public void SearchLDAP(string filter)
        {
            string[] propertiesToLoad = { "displayName", "sAMAccountName", "department", "mail", "extensionattribute", "manager" };
            try
            {
                DirectorySearcher searcher = new DirectorySearcher(ldapConnection, filter);
                searcher.PropertiesToLoad.AddRange(propertiesToLoad);
                SearchResultCollection results = searcher.FindAll();
                foreach (SearchResult result in results)
                {
                    foreach (string property in propertiesToLoad)
                    {
                        if (result.Properties.Contains(property))
                        {
                            Console.WriteLine($"{property}: {result.Properties[property][0]}");
                        }
                        else
                        {
                            Console.WriteLine($"{property}: Nincs találat");
                        }

                    }
                    Console.WriteLine("-----");
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }
        }
       
    }
}
