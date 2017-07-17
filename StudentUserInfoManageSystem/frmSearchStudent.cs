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
    public partial class frmSearchStudent : Form
    {
        public frmSearchStudent()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 加载界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        string conString = "Data Source=.;Initial Catalog=MySchool;User ID=sa;Password=1";
        private void frmSearchStudent_Load(object sender, EventArgs e)
        {
            this.dgvShow.AutoGenerateColumns = false;
            SqlConnection con = new SqlConnection(conString);      
            DataSet ds = new DataSet();          
            string sql = "select * from grade";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql,con);
            adapter1.Fill(ds,"grade");
            DataRow dr = ds.Tables["grade"].NewRow();
            dr[0] = -1;
            dr[1] = "全部";
            ds.Tables["grade"].Rows.InsertAt(dr,0);
            this.cbGrade.DisplayMember = "gradename";
            this.cbGrade.ValueMember = "gradeid";
            this.cbGrade.DataSource=ds.Tables["grade"];
            Updated();
        }
        /// <summary>
        /// 绑定控件
        /// </summary>
        public void Updated()
        {
            SqlConnection con1 = new SqlConnection(conString);
            DataSet ds1 = new DataSet();
            string sql1 = string.Format(@"select studentname,phone,grade.gradename from student,grade where student.gradeid=grade.gradeid 
            and studentname like '%{0}%' and phone like '%{1}%'", this.txtName.Text.Trim(), this.txtPhone.Text.Trim());
            if (Convert.ToInt32(this.cbGrade.SelectedValue) != -1)
            {
                sql1 += string.Format("and student.gradeid = '{0}'", Convert.ToInt32(this.cbGrade.SelectedValue));
            }
            SqlDataAdapter adapter = new SqlDataAdapter(sql1, con1);
            adapter.Fill(ds1, "students");
            this.dgvShow.DataSource = ds1.Tables["students"];
        }
        /// <summary>
        /// 点击查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Updated();
        }
        /// <summary>
        /// 点击删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result=MessageBox.Show("是否删除","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            string name=this.dgvShow.SelectedRows[0].Cells[0].Value.ToString();
            if(result==DialogResult.OK)
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                string sql = string.Format("delete from grade where gradename='{0}'", this.dgvShow.SelectedRows[0].Cells[1].Value.ToString());
                SqlCommand com = new SqlCommand(sql,con);
                com.ExecuteNonQuery();
                sql = string.Format("delete from student where studentname='{0}'", name);
                com.CommandText = sql;
                int i = com.ExecuteNonQuery();
                con.Close();
                if (i>0)
                {
                    Updated();
                }
            }
        }
        /// <summary>
        ///  点击修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public string studentname;
        public void btnChange_Click(object sender, EventArgs e)
        {
            studentname = this.dgvShow.SelectedRows[0].Cells[0].Value.ToString();
            //MessageBox.Show(studentname);
            frmAddData frm = new frmAddData(this);
            frm.type = "修改";
            frm.ShowDialog();
            //frm.MdiParent = this;
        }
    }
}
