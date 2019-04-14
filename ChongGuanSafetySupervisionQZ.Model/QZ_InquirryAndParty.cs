using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChongGuanSafetySupervisionQZ.Model
{
    public class QZ_InquiryAndParty
    {
        public QZ_Inquiry Inquiry { get; set; }
        public QZ_Party Party { get; set; }

        //public string Test
        //{
        //    get { return "fuck"; }
        //}

        public double ItemWidth { get; set; }
        public double ItemHeight
        {
            get;
            set;
        }
    }
}
