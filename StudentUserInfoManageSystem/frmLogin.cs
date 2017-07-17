using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StudentUserInfoManageSystem
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 点击关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 点击登录
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIn_Click(object sender, EventArgs e)
        {
            string loginId = this.txtLoginId.Text.Trim();
            string loginPwd = this.txtLoginPwd.Text.Trim();
            if (string.IsNullOrEmpty(loginId))
            {
                MessageBox.Show("请输入登录名");
                this.txtLoginId.Focus();
                return;
            }
            if (string.IsNullOrEmpty(loginPwd))
            {
                MessageBox.Show("请输入密码");
                this.txtLoginPwd.Focus();
                return;
            }
            string conString = "Data Source=.;Initial Catalog=MySchool;User ID=sa;Password=1";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            string sql = string.Format("select count(*) from admin where loginid='{0}' and loginpwd='{1}'",loginId,loginPwd);
            SqlCommand com = new SqlCommand(sql,con);
            int result = Convert.ToInt32(com.ExecuteScalar());
            if (result>0)
            {
                frmMain fr = new frmMain();
                fr.loginId = loginId;
                this.Hide();
                fr.ShowDialog();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("系统异常，登录失败");
            }
            con.Close();
        }
    }
}
