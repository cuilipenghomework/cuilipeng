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
    public partial class frmAddData : Form
    {
        public frmSearchStudent fr = new frmSearchStudent();
        public string type;
        public frmAddData(frmSearchStudent fr1)
        {
            this.fr = fr1;
            InitializeComponent();
        }
        public frmAddData()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 加载窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        string conString = "Data Source=.;Initial Catalog=MySchool;User ID=sa;Password=1";
        private void frmAddData_Load(object sender, EventArgs e)
        {
            SqlConnection con=new SqlConnection(conString);
            string sql = string.Format("select * from grade");
            SqlDataAdapter adapter=new SqlDataAdapter(sql,con);
            DataSet ds=new DataSet();
            adapter.Fill(ds,"grade");
            this.lblNo.Visible = false;
            this.txtStudentno.Visible = false;
            this.cbGrade.DisplayMember = "gradename";
            this.cbGrade.ValueMember = "gradeid";
            this.cbGrade.DataSource=ds.Tables["grade"];
            if (type=="修改")
            {
                this.Text = "修改数据";
                this.btnAdd.Text = "修改";
                this.lblNo.Visible = true;
                this.txtStudentno.Visible = true;
                SqlConnection con1 = new SqlConnection(conString);
                con1.Open();
                sql = string.Format("select * from student where studentname='{0}'",fr.studentname);
                SqlCommand com = new SqlCommand(sql,con1);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    this.txtStudentno.Text = reader["studentno"].ToString();
                    this.txtStudentName.Text = reader[2].ToString();
                    this.cbGrade.SelectedValue =Convert.ToInt32(reader["gradeid"]);
                    this.txtPwd.Text = reader["loginpwd"].ToString();
                    this.dtpBornDate.Value = Convert.ToDateTime(reader["borndate"]);
                    this.txtPhone.Text = reader["phone"].ToString();
                    this.txtAddress.Text = reader["address"].ToString();
                    this.txtEmail.Text = reader["email"].ToString();
                    this.txtIdentity.Text = reader["identitycard"].ToString();
                    if (reader["gender"].ToString()=="女")
                    {
                        this.rbFemale.Checked = true;
                    }
                }
                reader.Close();
                con.Close();
            }
        }
        /// <summary>
        /// 点击添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (type == "修改")
            {
                SqlConnection con1 = new SqlConnection(conString);
                con1.Open();
                string sql1 = string.Format("update student set studentname='{0}',gradeid='{1}',phone='{2}' where studentname='{3}'", this.txtStudentName.Text, this.cbGrade.SelectedValue, this.txtPhone.Text,fr.studentname);
                SqlCommand com1 = new SqlCommand(sql1, con1);
                int i = com1.ExecuteNonQuery();
                con1.Close();
                if (i > 0)
                {
                    fr.Updated();
                    MessageBox.Show("修改成功");
                }
            }
            else
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                string sex = this.rbMale.Checked ? "男" : "女";
                string sql = string.Format("insert into student values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", this.txtStudentName.Text, this.txtPwd.Text, sex, this.cbGrade.SelectedValue.ToString(), this.txtPhone.Text, this.txtAddress.Text, this.dtpBornDate.Value, this.txtEmail.Text, this.txtIdentity.Text);
                SqlCommand com = new SqlCommand(sql, con);
                try
                {
                    int result1 = com.ExecuteNonQuery();
                    if (result1 > 0)
                    {
                        MessageBox.Show("修改成功！");
                    }
                    else
                    {
                        MessageBox.Show("修改失败！");
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
