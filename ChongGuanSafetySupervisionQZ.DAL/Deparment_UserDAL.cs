using ChongGuanSafetySupervisionQZ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.DAL
{
    public class Deparment_UserDAL
    {
        public ResultData<QZ_Deparment> QueryDeparmentByUser(QZ_User qZ_User)
        {
            string message = "没有该部门信息";

            var query = from du in ModelQZ.DatabaseContext.QZ_Deparment_User
                        join d in ModelQZ.DatabaseContext.QZ_Deparment
                        on du.DeparmentId equals d.DeparmentId
                        where du.UserId == qZ_User.UserId
                        select d;

            QZ_Deparment data = query.FirstOrDefault();

            if (data != null)
            {
                message = string.Empty;
            }

            ResultData<QZ_Deparment> result = new ResultData<QZ_Deparment> { IsSuccessed = data != null, Message = message, Data = data };

            return result;
        }

        public async Task<ResultData<QZ_Deparment_User>> Add(QZ_Deparment qZ_Deparment, QZ_User qZ_User)
        {
            var query = from du in ModelQZ.DatabaseContext.QZ_Deparment_User
                        where du.DeparmentId == qZ_Deparment.DeparmentId && du.UserId == qZ_User.UserId
                        select du;


            QZ_Deparment_User data = query.FirstOrDefault();

            if (data == null)
            {
                data = ModelQZ.DatabaseContext.QZ_Deparment_User.Add(new QZ_Deparment_User
                {
                    DeparmentId = qZ_Deparment.DeparmentId,
                    UserId = qZ_User.UserId,
                    IsDeleteId = 0,
                    CreateTime = DateTime.Now.ToString(),
                    ModifyTime = DateTime.Now.ToString()
                });

                await ModelQZ.DatabaseContext.SaveChangesAsync();
            }

            ResultData<QZ_Deparment_User> result = new ResultData<QZ_Deparment_User> { IsSuccessed = true, Data = data };

            return result;
        }
    }
}
