using PA_System.Helper;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;

namespace PA_System.Infrastructure
{
    public class ADUsers
    {
        public static string sDomain = string.Empty;

        public string GroupName { get; set; }

        public List<string> GroupList { get; set; }

        public string DomanName
        {
            get
            {
                return "fgdc01.futuregrowth.co.za";
            }

        }

        public string ServiceUSer
        {
            get
            {
                return @"fgportal";
            }

        }
        
        //Get Ad group list - this method accepts username returns only the list of groups where passed user has access to
        public List<string> GetAdGroupsForUser(string userName, string domainName = null)
        {
            var result = new List<string>(); 

            var list = new List<string>();

            if (userName.Contains('\\') || userName.Contains('/'))
            {
                domainName = userName.Split(new char[] { '\\', '/' })[0];

                userName = userName.Split(new char[] { '\\', '/' })[1];
            }

            var userInformation = string.Empty;

            using (PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, domainName))
            {

                using (UserPrincipal user = UserPrincipal.FindByIdentity(domainContext, userName))
                {
                    using (var searcher = new DirectorySearcher(new DirectoryEntry("LDAP://" + domainContext.Name)))
                    {
                        foreach (var item in user.GetGroups())
                        {
                            userInformation += "Groups \t" + item + "\n";
                        }

                        searcher.Filter = String.Format("(&(objectCategory=group)(member={0}))", user.DistinguishedName);

                        searcher.SearchScope = SearchScope.Subtree;

                        searcher.PropertiesToLoad.Add("cn");

                        foreach (SearchResult entry in searcher.FindAll())
                        {
                            if (entry.Properties.Contains("cn"))
                            {
                                result.Add(entry.Properties["cn"][0].ToString());
                            }
                        }
                    }

                }


            }

            return result;
        }

        //Pass a group name as a parameter and returns user information
        public List<Employee> GetUserPerByAdGroup(string gName)
        {
            var userList = new List<Employee>();

            sDomain = this.DomanName;

            using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, sDomain))
            {
                GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, gName);

                if (group != null)
                {
                    // iterate over the group's members
                    foreach (Principal p in group.GetMembers())
                    {
                        //Initializing an object which will be called mostly to populate user dropdown lists
                        var user = UserPrincipal.FindByIdentity(ctx, p.SamAccountName);

                        var employeeObject = new Employee();

                        try
                        {
                            employeeObject.EmployeeFirstName = string.IsNullOrWhiteSpace(user.GivenName) ? user.Name : user.GivenName;

                            employeeObject.EmployeeSurname = user.Surname;

                            employeeObject.EmployeeNumber = user.VoiceTelephoneNumber;

                            employeeObject.EmailAddress = user.EmailAddress;

                            employeeObject.UserName = p.SamAccountName;

                            employeeObject.DisplayName = user.GivenName + " " + user.Surname;

                            //Adding an object to a class
                            if (!string.IsNullOrWhiteSpace(employeeObject.DisplayName))
                            {
                                userList.Add(employeeObject);
                            }
                        }
                        catch (Exception)
                        {
                            
                        }                        
                    }
                }
            }

            //There are instances (for example raters) where a group becomes a user hence a group list is appended into a user information
            var ADGroupList = new ADUsers().GetAdGroupsForUser(Generic.GetCurrentLogonUserName(), new ADUsers().DomanName);

            for (int i = 0; i < ADGroupList.Count; i++)
            {
                userList.Add(new Employee() { DisplayName = ADGroupList[i] });
            }

            return userList.OrderBy(c => c.DisplayName).ToList();
        }

        public string GetEmailAddress(string employeeName)
        {
            try
            {
                var list = !string.IsNullOrWhiteSpace("DG-Futuregrowth") ? new ADUsers().GetUserPerByAdGroup("DG-Futuregrowth") : new List<PA_System.Infrastructure.Employee>();

                var email = string.Empty;

                if (list.Count == 0)
                {
                    return string.Empty;
                }

                if (!string.IsNullOrWhiteSpace(list.Where(c => c.DisplayName.ToLower() == employeeName.ToLower()).First().EmailAddress))
                {
                  return  email = list.Where(c => c.DisplayName.ToLower() == employeeName.ToLower()).First().EmailAddress;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        

            return "";
        }
    }
}