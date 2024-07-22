using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class Helper
    {
        public const string PathImageuser = "/Images/Users";
        public const string PathSaveImageuser = "Images/Users";

        public const string MsgType = "msgType";
        public const string Title = "title";
        public const string Msg = "msg";

        public const string Success = "success";
        public const string Error = "error";


        //Data Default User
        public const string Email = "superadmin@domain.com";
        public const string UserName = "superadmin@domain.com";
        public const string Name = "SuperAdmin";
        public const string Password = "SuperAdmin@p@$$w0rd123456";


        //Data Default User
        public const string EmailBasic = "basicuser@domain.com";
        public const string UserNameBasic = "basicuser@domain.com";
        public const string NameBasic = "basicuser";
        public const string PasswordBasic = "basicuser@p@$$w0rd123456";




        public const string Permission = "Permission";


        public enum eurrentStaut
        {
            Active =1,
            Delete =0
        }

        public const string Save = "Save";
        public const string Update = "Update";
        public const string Delete = "Delete";



        public enum Roles
        {
            SuperAdmin,
            Admin,
            Basic
        }

        public enum PermissionModuleName
        {
            Home,
            Accounts,
            Roles,
            Registers,
            Categories
        }
    }
}
