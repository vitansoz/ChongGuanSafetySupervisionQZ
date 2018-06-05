using ChongGuanSafetySupervisionQZ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.DAL
{
    public class Role_UserDAL
    {
        public ResultData<QZ_Role> QueryRoleByUser(QZ_User qZ_User)
        {
            string message = "获取角色信息失败";

            var query = from ru in ModelQZ.DatabaseContext.QZ_Role_User
                        join r in ModelQZ.DatabaseContext.QZ_Role
                        on ru.RoleId equals r.RoleId
                        where ru.UserId == qZ_User.UserId
                        select r;

            QZ_Role data = query.FirstOrDefault();

            if (data != null)
            {
                message = string.Empty;
            }

            ResultData<QZ_Role> result = new ResultData<QZ_Role> { IsSuccessed = data != null, Message = message, Data = data };

            return result;
        }

        public async Task<ResultData<QZ_Role_User>> Add(QZ_Role qZ_Role, QZ_User qZ_User)
        {
            var query = from ru in ModelQZ.DatabaseContext.QZ_Role_User
                        where ru.RoleId == qZ_Role.RoleId && ru.UserId == qZ_User.UserId
                        select ru;


            QZ_Role_User data = query.FirstOrDefault();

            if (data == null)
            {
                data = ModelQZ.DatabaseContext.QZ_Role_User.Add(new QZ_Role_User
                {
                    RoleId = qZ_Role.RoleId,
                    UserId = qZ_User.UserId,
                    IsDeleteId = 0,
                    CreateTime = DateTime.Now.ToString(),
                    ModifyTime = DateTime.Now.ToString()
                });

                await ModelQZ.DatabaseContext.SaveChangesAsync();
            }

            ResultData<QZ_Role_User> result = new ResultData<QZ_Role_User> { IsSuccessed = true, Data = data };

            return result;
        }
    }
}
