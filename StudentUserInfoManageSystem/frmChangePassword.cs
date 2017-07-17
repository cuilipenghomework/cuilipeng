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
    public partial class frmChangePassword : Form
    {
        public string loginId;
        public frmChangePassword()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void brnPreserve_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(loginId);
            if (this.txtInitialPwd.Text.Trim()=="")
            {
                this.error.SetError(this.txtInitialPwd,"请输入原密码");
                this.txtInitialPwd.Focus();
                return;
            }
            this.error.Clear();
            string conString = "Data Source=.;Initial Catalog=MySchool;User ID=sa;Password=1";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            string sql = string.Format("select loginpwd from admin where loginid='{0}'", loginId);
            SqlCommand com = new SqlCommand(sql, con);
            SqlDataReader reader = com.ExecuteReader();
            while(reader.Read())
            {
                if (this.txtInitialPwd.Text.Trim()!=reader[0].ToString())
                {
                    this.error.SetError(this.txtInitialPwd,"请正确输入原密码");
                    this.txtInitialPwd.Focus();
                    this.txtInitialPwd.SelectAll();
                    return;
                }
            }
            reader.Close();
            con.Close();
            this.error.Clear();
            if (this.txtNewPwd1.Text.Trim() == "")
            {
                this.error.SetError(this.txtNewPwd1, "请输入新密码");
                this.txtNewPwd1.Focus();
                return;
            }
            else if (this.txtNewPwd2.Text.Trim()!=this.txtNewPwd1.Text.Trim())
            {
                this.error.SetError(this.txtNewPwd2, "两次密码输入不一致");
                this.txtNewPwd2.Focus();
                return;
            }
            this.error.Clear();
            con.Open();
            string sql1 = string.Format("update admin set loginpwd='{0}' where loginid='{1}'",this.txtNewPwd1.Text.Trim(),loginId);
            SqlCommand com1 = new SqlCommand(sql1, con);
            int result = com1.ExecuteNonQuery();
            if (result>0)
            {
                MessageBox.Show("修改成功！");
            }
            else
            {
                MessageBox.Show("修改异常");
            }
            con.Close();
        }
        /// <summary>
        /// 点击取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
