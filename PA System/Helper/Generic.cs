using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;

namespace PA_System.Helper
{
    public class Generic
    {
        public static string GetCurrentLogonUserName()
        {
            PrincipalContext pContext = new PrincipalContext(ContextType.Domain);

            UserPrincipal currentUser = UserPrincipal.Current;

            return currentUser.SamAccountName;
        }
    }
}