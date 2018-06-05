using ChongGuanDotNetUtils.Helpers;
using ChongGuanSafetySupervisionQZ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.DAL
{
    public class EventDAL
    {
        public async Task<ResultData<QZ_Event>> Add(QZ_Event qZ_Event)
        {
            string message = "添加信息失败";

            qZ_Event.EventId = Guid.NewGuid().ToString();

            qZ_Event.CreateTime = DateTime.Now.ToString();
            qZ_Event.ModifyTime = DateTime.Now.ToString();

            ModelQZ.DatabaseContext.QZ_Event.Add(qZ_Event);
            await ModelQZ.DatabaseContext.SaveChangesAsync();

            message = string.Empty;

            ResultData<QZ_Event> result = new ResultData<QZ_Event> { IsSuccessed = true, Message = message, Data = qZ_Event };
            return result;
        }

        public ResultData<IEnumerable<QZ_Event>> Qurey(string eventId = "", string createUserId = "", string createDepartmentId = "")
        {

            var query = from e in ModelQZ.DatabaseContext.QZ_Event
                        where (eventId == "" || e.EventId == eventId) &&
                        (createUserId == "" || e.CreateUserId == createUserId) &&
                        (createDepartmentId == "" || e.CreateDepartmentId == createDepartmentId)
                        select e;

            IEnumerable<QZ_Event> data = query.ToList();
            ResultData<IEnumerable<QZ_Event>> result = new ResultData<IEnumerable<QZ_Event>> { IsSuccessed = true, Message = "", Data = data };

            return result;
        }

        public async Task<ResultData<QZ_Event>> Delete(QZ_Event qZ_Event)
        {
            string message = "事件不存在";

            var query = from e in ModelQZ.DatabaseContext.QZ_Event
                        where e.EventId == qZ_Event.EventId
                        select e;

            QZ_Event data = query.FirstOrDefault();

            if(data != null)
            {
                ReflectionHelper.CopyProperties<QZ_Event>(qZ_Event, data, new String[] { "EventId"});

                data.ModifyTime = DateTime.Now.ToString();
                data.IsDeleteId = 1;

                ModelQZ.DatabaseContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                await ModelQZ.DatabaseContext.SaveChangesAsync();

                message = string.Empty;
            }

            ResultData<QZ_Event> result = new ResultData<QZ_Event> { IsSuccessed = data != null, Message = message, Data = data };

            return result;
        }

        public async Task<ResultData<QZ_Event>> Update(QZ_Event qZ_Event)
        {
            string message = "事件不存在";

            var query = from e in ModelQZ.DatabaseContext.QZ_Event
                        where e.EventId == qZ_Event.EventId
                        select e;

            QZ_Event data = query.FirstOrDefault();

            if (data != null)
            {
                ReflectionHelper.CopyProperties<QZ_Event>(qZ_Event, data, new String[] { "EventId" });

                data.ModifyTime = DateTime.Now.ToString();

                ModelQZ.DatabaseContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                await ModelQZ.DatabaseContext.SaveChangesAsync();

                message = string.Empty;
            }

            ResultData<QZ_Event> result = new ResultData<QZ_Event> { IsSuccessed = data != null, Message = message, Data = data };

            return result;
        }



    }
}
