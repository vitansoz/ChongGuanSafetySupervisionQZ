using ChongGuanSafetySupervisionQZ.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
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

            QZ_Areas qZ_Areas = new QZ_Areas { AreaId = "1", AreaLevel = 1, AreaName = "fuck", AreaPid = "1" };
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
            BindingList<QZ_Areas> tt = new BindingList<QZ_Areas>(t.Data.ToList());

            gridControl1.DataSource = tt;
            gridControl1.RefreshDataSource();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            ChongGuanSafetySupervisionQZ.DAL.UserDAL userDAL = new DAL.UserDAL();

            ResultData<QZ_User> result = await userDAL.Add(new QZ_User
            {
                AreaCode = "shit",
                IsDeleteId = 0,
                LoginName = "张晓坤" + DateTime.Now.Ticks,
                IsForbidden = 0,
                LoginPwd = "123456",
                UserAge = "18",
                UserCard = "123",
                UserCode = "sb123",
                UserEmail = "sb@126.com",
                UserFingerImageFilePath = "sb",
                UserLawCard = "sb",
                UserName = "张晓坤",
                UserPhone = "1334324",
                UserSex = "sy",
                UserPhotoFilePath = "sss"
            });

            if(result.IsSuccessed)
            {
                XtraMessageBox.Show(result.Data.UserName, result.Data.UserId.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                XtraMessageBox.Show("sb", result.Message, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ChongGuanSafetySupervisionQZ.DAL.AreasDAL areasDAL = new DAL.AreasDAL();
            var areaList = areasDAL.Query();

            BindingList<QZ_Areas> tt = new BindingList<QZ_Areas>(areaList.Data.ToList());

            //this.treeListLookUpEdit1.

            //this.treeListLookUpEdit1.DataBindings.Clear();
            this.treeListLookUpEdit1.Properties.DataSource = tt;
            this.treeListLookUpEdit1.Properties.TreeList.KeyFieldName = "AreaId";
            this.treeListLookUpEdit1.Properties.TreeList.ParentFieldName = "AreaPid";
            this.treeListLookUpEdit1.Properties.DisplayMember = "AreaName";
            this.treeListLookUpEdit1.Properties.ValueMember = "AreaId";
            this.treeListLookUpEdit1.EditValue = tt[0].AreaId;

            this.treeListLookUpEdit1.Properties.TreeList.Columns[0].Caption = "部门所在地";
            this.treeListLookUpEdit1.Properties.TreeList.Columns[1].Visible= false;

        }

        private void treeListLookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            this.label1.Text = this.treeListLookUpEdit1.EditValue.ToString();
        }

        private void treeListLookUpEdit1_Popup(object sender, EventArgs e)
        {
            foreach (TreeListNode node in this.treeListLookUpEdit1TreeList.Nodes)
            {
                node.Expanded = false;
                this.SetStyle(node);
            }
        }

        public void SetStyle(TreeListNode node)
        {
            for (int index = 0; index < node.Nodes.Count; ++index)
            {
                node.Nodes[index].Expanded = false;
                this.SetStyle(node.Nodes[index]);
            }
        }
    }
}
