using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySchool1.DAL;
using MySchool.Models;

namespace MySchool1
{
    public partial class frmStudentInfo : Form
    {
        public frmStudentInfo()
        {
            InitializeComponent();
            this.dgvAllInfo.AutoGenerateColumns = false;
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //DataSet ds = StudentService.GetAllInfo("Students");
            //this.dgvAllInfo.DataSource = ds.Tables["Students"];
            string sql="select * from student";
            Updated(sql);
            List<Grade> grList = StudentService.GetAllGrade();
            this.cbGrade.DisplayMember = "gradename";
            this.cbGrade.ValueMember = "gradeid";
            this.cbGrade.DataSource = grList;
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void Updated(string sql)
        {
            List<Student> stulist = StudentService.GetAllStudents(sql);
            this.dgvAllInfo.DataSource = new BindingList<Student>(stulist);
        }
        /// <summary>
        /// 点击添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddInfo fr = new frmAddInfo(this);
            fr.ShowDialog();
        }
        /// <summary>
        /// 点击查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select * from student where gradeid='{0}'",this.cbGrade.SelectedValue);
            Updated(sql);
        }
    }
}
