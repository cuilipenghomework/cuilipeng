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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 点击登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            List<Admin> adList=StudentService.GetAllAdmin();
            if (string.IsNullOrEmpty(this.txtId.Text.Trim()))
            {
                return;
            }
            if (string.IsNullOrEmpty(this.txtPwd.Text.Trim()))
            {
                return;
            }
            foreach (Admin item in adList)
            {
                if (item.loginId==this.txtId.Text.Trim())
                {
                    if (item.loginPwd==this.txtPwd.Text.Trim())
                    {
                        frmStudentInfo fr = new frmStudentInfo();
                        this.Hide();
                        fr.ShowDialog();
                        this.Dispose();
                    }
                }
            }
        }
    }
}
