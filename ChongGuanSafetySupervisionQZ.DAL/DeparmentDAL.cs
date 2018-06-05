using ChongGuanSafetySupervisionQZ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.DAL
{
    public class DeparmentDAL
    {
        public async Task<ResultData<QZ_Deparment>> Add(QZ_Deparment qZ_Deparment)
        {
            //string message = "添加部门信息失败";

            var query = from d in ModelQZ.DatabaseContext.QZ_Deparment
                        where d.DeparmentId == qZ_Deparment.DeparmentId
                        select d;

            QZ_Deparment data = query.FirstOrDefault();

            if (data == null)
            {
                qZ_Deparment.CreateTime = DateTime.Now.ToString();
                qZ_Deparment.ModifyTime = DateTime.Now.ToString();

                data = ModelQZ.DatabaseContext.QZ_Deparment.Add(qZ_Deparment);
                await ModelQZ.DatabaseContext.SaveChangesAsync();

                //message = string.Empty;

            }

            ResultData<QZ_Deparment> result = new ResultData<QZ_Deparment> { IsSuccessed = true, Data = data };

            return result;
        }

        public ResultData<QZ_Deparment> Query(QZ_Deparment qZ_Deparment)
        {
            string message = "没有该部门信息";

            var query = from d in ModelQZ.DatabaseContext.QZ_Deparment
                        where d.DeparmentId == qZ_Deparment.DeparmentId
                        select d;

            QZ_Deparment data = query.FirstOrDefault();

            if (data != null)
            {
                message = string.Empty;
            }

            ResultData<QZ_Deparment> result = new ResultData<QZ_Deparment> { IsSuccessed = data != null, Message = message, Data = data };

            return result;
        }
    }
}
