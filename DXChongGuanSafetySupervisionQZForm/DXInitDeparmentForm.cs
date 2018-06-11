using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace DXChongGuanSafetySupervisionQZForm
{
    public partial class DXInitDeparmentForm : DevExpress.XtraEditors.XtraForm
    {
        public DXInitDeparmentForm()
        {
            InitializeComponent();

            this.TransparencyKey = Color.Red;
        }
    }
}