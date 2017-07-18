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
    public partial class frmAddInfo : Form
    {
        public frmStudentInfo frm = new frmStudentInfo();
        public frmAddInfo(frmStudentInfo fr)
        {
            this.frm = fr;
            InitializeComponent();
        }
        /// <summary>
        /// 点击添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Student stu = new Student()
            {
                LoginPwd = this.txtPwd.Text.Trim(),
                StudentName = this.txtStudentName.Text.Trim(),
                Gender = this.rbMale.Checked? "男":"女",
                GradeId = this.cbGrade.SelectedIndex+1,
                Phone = this.txtPhone.Text.Trim(),
                Address = this.txtAddress.Text.Trim(),
                BornDate = Convert.ToDateTime(this.dtpBornDate.Value),
                Email = this.txtEmail.Text.Trim(),
                IdentityCard = this.txtIdentity.Text.Trim()
            };
            List<Student> stuList = new List<Student>();
            stuList.Add(stu);
            string sql = "";
            foreach (Student item in stuList)
            {
                sql = string.Format(@"insert into student values('{0}',
                '{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",item.LoginPwd,item.StudentName,item.Gender,item.GradeId,item.Phone,item.Address,item.BornDate,item.Email,item.IdentityCard);
            }
            string a=StudentService.AddReault(sql);
            MessageBox.Show("添加成功");
            frm.Updated("select * from student");
        }
    }
}
