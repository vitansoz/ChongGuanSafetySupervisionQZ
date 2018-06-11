using ChongGuanSafetySupervisionQZ.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.DAL
{
    public class AreasDAL
    {
        public ResultData<IEnumerable<QZ_Areas>> Query(int areaLevel = 0)
        {
            var query = from a in ModelQZ.DatabaseContext.QZ_Areas
                        where (areaLevel == 0 || a.AreaLevel == areaLevel)
                        select a;

            IEnumerable<QZ_Areas> data = query.ToList();


            ResultData<IEnumerable<QZ_Areas>> result = new ResultData<IEnumerable<QZ_Areas>> { IsSuccessed = true, Message = string.Empty, Data = data };
            return result;
        }

        public async Task<QZ_Areas> Add(QZ_Areas areas)
        {
            ModelQZ.DatabaseContext.QZ_Areas.Add(areas);
            await ModelQZ.DatabaseContext.SaveChangesAsync();

            return areas;
        }      
    }
}
