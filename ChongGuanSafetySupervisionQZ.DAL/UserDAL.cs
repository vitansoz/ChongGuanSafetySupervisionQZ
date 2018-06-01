using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChongGuanSafetySupervisionQZ.Model;

namespace ChongGuanSafetySupervisionQZ.DAL
{
    public class UserDAL
    {
        public bool Login(QZ_User qZ_User)
        {
            var query = from u in ModelQZ.DatabaseContext.QZ_User
                        where (u.LoginName == qZ_User.LoginName && u.LoginPwd == qZ_User.LoginPwd)
                        select u;


            return query.Count() == 1;
        }
    }
}
