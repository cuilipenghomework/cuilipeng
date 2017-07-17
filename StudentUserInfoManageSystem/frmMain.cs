using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentUserInfoManageSystem
{
    public partial class frmMain : Form
    {
        public string loginId;//验证登录名
        public frmMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 点击退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// 点击修改密码按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiChangePwd_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(loginId);
            frmChangePassword fr = new frmChangePassword();
            fr.loginId = loginId;
            fr.ShowDialog();
            //fr.loginId = loginId;
            fr.MdiParent = this;
        }
        /// <summary>
        /// 点击增加数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAddData_Click(object sender, EventArgs e)
        {
            frmAddData fr = new frmAddData();
            //fr.MdiParent = this;
            fr.ShowDialog();
            fr.MdiParent = this;
        }
        /// <summary>
        /// 点击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiChange_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 点击查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSearchData_Click(object sender, EventArgs e)
        {
            frmSearchStudent fs = new frmSearchStudent();
            fs.ShowDialog();
            fs.MdiParent = this;
        }
    }
}
