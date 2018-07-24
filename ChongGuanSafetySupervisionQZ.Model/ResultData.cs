 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.Model
{
    public  class ResultData<T>
    {
        public bool IsSuccessed;
        public string Message;
        public T Data;
    }
}
