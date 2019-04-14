using ChongGuanDotNetUtils.Helpers;
using ChongGuanSafetySupervisionQZ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.DAL
{
    public class PartyDAL
    {
        public async Task<ResultData<QZ_Party>> Add(QZ_Party qZ_Party)
        {
            string message = "添加当事人失败";

            try
            {
                qZ_Party.PartyId = Guid.NewGuid().ToString();

                qZ_Party.CreateTime = DateTime.Now.ToString();
                qZ_Party.ModifyTime = DateTime.Now.ToString();

                ModelQZ.DatabaseContext.QZ_Party.Add(qZ_Party);
                await ModelQZ.DatabaseContext.SaveChangesAsync();

                message = string.Empty;
            }
            catch (Exception ex)
            {
            }
            ResultData<QZ_Party> result = new ResultData<QZ_Party> { IsSuccessed = true, Message = message, Data = qZ_Party };
            return result;
        }

        public ResultData<IEnumerable<QZ_Party>> Qurey(string partyId = "", string createUserId = "", string createDepartmentId = "", string partyCard = "")
        {

            var query = from e in ModelQZ.DatabaseContext.QZ_Party
                        where (partyId == "" || e.PartyId == partyId) &&
                        (createUserId == "" || e.CreateUserId == createUserId) &&
                        (createDepartmentId == "" || e.CreateDepartmentId == createDepartmentId) &&
                        (partyCard == "" || e.PartyCard == partyCard)
                        select e;

            IEnumerable<QZ_Party> data = query.ToList();

            List<QZ_Party> resultData = new List<QZ_Party>();

            foreach (var d in data)
            {
                QZ_Party t = new QZ_Party();
                ReflectionHelper.CopyProperties<QZ_Party>(d, t, new String[] { });
                resultData.Add(t);
            }

            ResultData<IEnumerable<QZ_Party>> result = new ResultData<IEnumerable<QZ_Party>> { IsSuccessed = true, Message = "", Data = resultData };

            return result;
        }

        public async Task<ResultData<QZ_Party>> Delete(QZ_Party qZ_Party)
        {
            string message = "当事人不存在";

            var query = from e in ModelQZ.DatabaseContext.QZ_Party
                        where e.PartyId == qZ_Party.PartyId
                        select e;

            QZ_Party data = query.FirstOrDefault();

            if (data != null)
            {
                ReflectionHelper.CopyProperties<QZ_Party>(qZ_Party, data, new String[] { "PartyId" });

                data.ModifyTime = DateTime.Now.ToString();
                data.IsDeleteId = 1;

                ModelQZ.DatabaseContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                await ModelQZ.DatabaseContext.SaveChangesAsync();

                message = string.Empty;
            }

            ResultData<QZ_Party> result = new ResultData<QZ_Party> { IsSuccessed = data != null, Message = message, Data = data };

            return result;
        }

        public async Task<ResultData<QZ_Party>> Update(QZ_Party qZ_Party)
        {
            string message = "在押人员信息不存在";

            var query = from e in ModelQZ.DatabaseContext.QZ_Party
                        where (e.PartyId == qZ_Party.PartyId || e.PartyCard == qZ_Party.PartyCard)
                        select e;

            QZ_Party data = query.FirstOrDefault();

            if (data != null)
            {
                ReflectionHelper.CopyProperties<QZ_Party>(qZ_Party, data, new String[] { "PartyId" });

                data.ModifyTime = DateTime.Now.ToString();

                ModelQZ.DatabaseContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                await ModelQZ.DatabaseContext.SaveChangesAsync();

                message = string.Empty;
            }

            ResultData<QZ_Party> result = new ResultData<QZ_Party> { IsSuccessed = data != null, Message = message, Data = data };

            return result;
        }

        public async Task<ResultData<QZ_Party>> UpdateByCard(QZ_Party qZ_Party)
        {
            try
            {
                string message = "在押人员信息不存在";

                var query = from e in ModelQZ.DatabaseContext.QZ_Party
                            where (e.PartyCard == qZ_Party.PartyCard)
                            select e;

                QZ_Party data = query.FirstOrDefault();

                if (data != null)
                {
                    ReflectionHelper.CopyProperties<QZ_Party>(qZ_Party, data, new String[] { "PartyId", "CreateUserId", "CreateDepartmentId","CreateTime" });

                    data.ModifyTime = DateTime.Now.ToString();

                    ModelQZ.DatabaseContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    await ModelQZ.DatabaseContext.SaveChangesAsync();

                    message = string.Empty;
                }

                ResultData<QZ_Party> result = new ResultData<QZ_Party> { IsSuccessed = data != null, Message = message, Data = data };

                return result;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                throw ex;
            }
        }
    }
}
