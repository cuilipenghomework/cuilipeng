using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MySchoolBase
{
    class Program
    {
        static void Main(string[] args)
        {
            QuerySuccess();
            ShowMenu();
            //Console.ReadLine();
        }
        /// <summary>
        /// 显示菜单
        /// </summary>
        private static void ShowMenu()
        {
            Console.WriteLine("--------请选择操作键--------");
            Console.WriteLine("1.查询学生数量");
            Console.WriteLine("2.查看学生用户列表");
            Console.WriteLine("3.查询指定学生姓名");
            Console.WriteLine("4.查询指定学生的所有基本信息");
            Console.WriteLine("5.插入年级信息");
            Console.WriteLine("6.修改学生生日信息");
            Console.WriteLine("7.删除学生信息");
            Console.WriteLine("8.退出");
            Console.WriteLine("----------------------------");
            switch (Console.ReadLine())
            {
                case "1":
                    OueryStudentNum();
                    ShowMenu();
                    break;
                case "2":
                    QueryStudentInfo();
                    ShowMenu();
                    break;
                case "3":
                    OueryStudentName();
                    ShowMenu();
                    break;
                case "4":
                    QueryAllInfo();
                    ShowMenu();
                    break;
                case "5":
                    InsertInfo();
                    ShowMenu();
                    break;
                case "6":
                    UpdateBorndateInfo();
                    ShowMenu();
                    break;
                case "7":
                    DeleteStudentInfo();
                    ShowMenu();
                    break;
                case "8":
                    break;
                default:
                    ShowMenu();
                    break;
            }
        }
        /// <summary>
        /// 删除学生信息
        /// </summary>
        private static void DeleteStudentInfo()
        {
            SqlConnection con = null;
            try
            {
                string conString = "Data Source=.;DataBase=MySchoolNew;user id=sa;pwd=1";
                con = new SqlConnection(conString);
                con.Open();
                Console.WriteLine("请输入学号：");
                string num = Console.ReadLine();
                string sql = string.Format("select studentno,studentname from student where studentno='{0}'",num);
                SqlCommand com = new SqlCommand(sql,con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine("确实要删除学号为{0},姓名为：{1}的学生记录吗？（y/n）",reader[0],reader[1]);
                    string record = Console.ReadLine();
                    reader.Close();
                    con.Close();
                    if (record.ToLower()=="y")
                    {
                        con.Open();
                        StringBuilder sql1 = new StringBuilder();
                        sql1.AppendFormat("delete student where studentno='{0}'",num);
                        SqlCommand com1= new SqlCommand(sql1.ToString(), con);
                        int result = com1.ExecuteNonQuery();
                        if (result > 0)
                        {
                            Console.WriteLine("删除成功!");
                        }
                        else
                        {
                            Console.WriteLine("修改失败");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 修改学生生日信息
        /// </summary>
        private static void UpdateBorndateInfo()
        {
            try
            {
                Console.WriteLine("请输入学生学号：");
                string number = Console.ReadLine();
                Console.WriteLine("请输入修改后的生日（XXXX-XX-XX）");
                DateTime ta = Convert.ToDateTime(Console.ReadLine());
                SqlConnection con = null;          
                string conString = "Data Source=.;DataBase=MySchoolNew;user id=sa;pwd=1";
                con = new SqlConnection(conString);
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("update student set borndate='{0}' where studentno='{1}'",ta,number);
                SqlCommand com = new SqlCommand(sql.ToString(), con);
                int result = com.ExecuteNonQuery();
                if (result>0)
                {
                    Console.WriteLine("修改成功!");
                }
                else
                {
                    Console.WriteLine("修改失败");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 插入年级信息
        /// </summary>
        private static void InsertInfo()
        {
            SqlConnection con = null;
            try
            {
                string conString = "Data Source=.;DataBase=MySchoolNew;user id=sa;pwd=1";
                con = new SqlConnection(conString);
                con.Open();
                Console.WriteLine("请输入待插入的年级名称：");
                string num = Console.ReadLine();
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("select gradename from grade");
                //string sql = string.Format("select * from student");
                SqlCommand com = new SqlCommand(sql.ToString(), con);
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    if (reader[0].ToString()==num)
                    {
                        Console.WriteLine("名称已经存在");
                        reader.Close();
                        InsertInfo();
                    }
                }
                reader.Close();
                con.Close();
                con.Open();
                string sql1 = string.Format("Insert into grade(gradename) values('{0}')",num);
                SqlCommand com1 = new SqlCommand(sql1,con);
                int result = com1.ExecuteNonQuery();
                if (result>0)
                {
                    Console.WriteLine("插入成功！");
                }
                else
                {
                    Console.WriteLine("插入失败");
                }
            }
            catch (Exception)
            {
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 查询学生的所有基本信息
        /// </summary>
        private static void QueryAllInfo()
        {
            SqlConnection con = null;
            try
            {
                string conString = "Data Source=.;DataBase=MySchoolNew;user id=sa;pwd=1";
                con = new SqlConnection(conString);
                con.Open();
                Console.WriteLine("请输入学生姓名：");
                string num = Console.ReadLine();
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat(@"select StudentName 姓名,Sex 性别,GradeName 年级,phone 电话,Address 地址,BornDate 出生日期,Email 邮箱 from student
inner join Grade on Student.GradeId=Grade.GradeId where StudentName like '{0}'","%"+num+"%");
                //string sql = string.Format("select * from student");
                SqlCommand com = new SqlCommand(sql.ToString(), con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine(reader[0]+"\t"+reader[1]+"\t"+reader[2]+"\t"+reader[3]+"\t"+reader[4]+"\t"+reader[5]+"\t"+reader[6]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 根据学号查询学生姓名
        /// </summary>
        private static void OueryStudentName()
        {
            SqlConnection con = null;
            try
            {
                string conString = "Data Source=.;DataBase=MySchoolNew;user id=sa;pwd=1";
                con = new SqlConnection(conString);
                con.Open();
                Console.WriteLine("请输入学号");
                string num = Console.ReadLine();
                StringBuilder sql = new StringBuilder();
                sql.AppendFormat("select studentname from student where studentno='{0}'",num);
                //string sql = string.Format("select * from student");
                SqlCommand com = new SqlCommand(sql.ToString(), con);
                SqlDataReader reader = com.ExecuteReader();
                if(reader.Read())
                {
                    Console.WriteLine( "学号是："+num+"的学生姓名为：{0}", reader[0]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 查询学生用户列表
        /// </summary>
        private static void QueryStudentInfo()
        {
            SqlConnection con = null;
            //try
            //{
                string conString = "Data Source=.;DataBase=MySchoolNew;user id=sa;pwd=1";
                con = new SqlConnection(conString);
                con.Open();
            //    string sql = string.Format("select * from student");
            //    SqlCommand com = new SqlCommand(sql, con);
            //    SqlDataReader reader = com.ExecuteReader();
            //    while(reader.Read())
            //    {
            //        Console.WriteLine("{0}\t{1}\t{2}",reader[0],reader[1],reader[2]);
            //    }
            //    reader.Close();
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //finally
            //{
            //    con.Close();
            //}

            string sql = string.Format("select * from student");
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            adapter.Fill(ds,"student");
            foreach (DataRow item in ds.Tables["student"].Rows)
            {
                Console.WriteLine("{0}\t{1}\t{2}",item[0],item[1],item[2]);
            }
        }
        /// <summary>
        /// 查询学生数量
        /// </summary>
        private static void OueryStudentNum()
        {
            SqlConnection con = null;
            try
            {
                string conString = "Data Source=.;DataBase=MySchoolNew;user id=sa;pwd=1";
                con = new SqlConnection(conString);
                con.Open();
                string sql = string.Format("select count(*) from student");
                SqlCommand com = new SqlCommand(sql, con);
                int result = Convert.ToInt32(com.ExecuteScalar());
                if (result > 0)
                {
                    Console.WriteLine("学生数量为："+result);
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// 判断是否登录成功
        /// </summary>
        private static void QuerySuccess()
        {
            Console.WriteLine("请输入用户名");
            string userName = Console.ReadLine();
            Console.WriteLine("请输入密码：");
            string password = Console.ReadLine();
            string conString = "Data Source=.;DataBase=MySchoolNew;user id=sa;pwd=1";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            string sql = string.Format("select * from admin1 where userid='{0}' and password='{1}'", userName, password);
            SqlCommand com = new SqlCommand(sql, con);
            int result = Convert.ToInt32(com.ExecuteScalar());
            if (result > 0)
            {
                Console.WriteLine("登录成功！");
                return;
            }
            else
            {
                Console.WriteLine("登录失败！");
                QuerySuccess();
            }
            con.Close();
        }
    }
}
