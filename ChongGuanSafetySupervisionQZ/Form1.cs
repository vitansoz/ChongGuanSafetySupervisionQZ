using ChongGuanSafetySupervisionQZ.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChongGuanSafetySupervisionQZ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //ChongGuanSafetySupervisionQZ.DAL.AreasDAL areasDAL = new DAL.AreasDAL();
            //var t = areasDAL.Query();
            //int sb = t.Count();
            //foreach (var a in t)
            //{

            //}

            this.Load += Form1_Load;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ChongGuanSafetySupervisionQZ.DAL.UserDAL userDAL = new DAL.UserDAL();
            userDAL.Login(new QZ_User { LoginName = "mayun", LoginPwd = "dashabi" });

        }

        private async void simpleButton1_Click(object sender, EventArgs e)
        {


            ChongGuanSafetySupervisionQZ.DAL.AreasDAL areasDAL = new DAL.AreasDAL();

            QZ_Areas qZ_Areas = new QZ_Areas { AreaId = DateTime.Now.Ticks.ToString(), AreaLevel = 1, AreaName = "fuck", AreaPid = "1" };
            var t = await areasDAL.Add(qZ_Areas);


            BindingList<QZ_Areas> tt = new BindingList<QZ_Areas>();
            tt.Add(t);

            gridControl1.DataSource = tt;
            gridControl1.RefreshDataSource();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChongGuanSafetySupervisionQZ.DAL.AreasDAL areasDAL = new DAL.AreasDAL();
            var t = areasDAL.Query(DateTime.Now.Second % 2);
            BindingList<QZ_Areas> tt = new BindingList<QZ_Areas>(t.ToList());

            gridControl1.DataSource = tt;
            gridControl1.RefreshDataSource();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChongGuanSafetySupervisionQZ.DAL.UserDAL userDAL = new DAL.UserDAL();

            if (!userDAL.Login(new QZ_User { LoginName = "mayun", LoginPwd = "dashabi" }))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("傻逼", "登陆失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("马云", "就是大傻逼！！！！", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
