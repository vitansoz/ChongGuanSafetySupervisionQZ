using ChongGuanSafetySupervisionQZ.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.DAL
{
    public class TalkTypeDAL
    {
        public ResultData<IEnumerable<QZ_TalkType>> Query(int id = -1)
        {
            var query = from a in ModelQZ.DatabaseContext.QZ_TalkType
                        where (id == -1 || a.TalkTypeId == id)
                        select a;

            IEnumerable<QZ_TalkType> data = query.ToList();


            ResultData<IEnumerable<QZ_TalkType>> result = new ResultData<IEnumerable<QZ_TalkType>> { IsSuccessed = true, Message = string.Empty, Data = data };
            return result;
        }

    }
}
