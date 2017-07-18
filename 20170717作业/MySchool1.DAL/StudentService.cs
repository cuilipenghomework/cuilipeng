using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySchool.Models;
using System.Data;
using System.Data.SqlClient;

namespace MySchool1.DAL
{
    public class StudentService
    {
        /// <summary>
        /// 学生列表
        /// </summary>
        /// <returns></returns>
        public static List<Student> GetAllStudents(string sql)
        {
            
            SqlDataReader reader=SqlHelper.ExecuteReader(sql);
            List<Student> studentList = new List<Student>();
            while (reader.Read())
            {
                Student stu = new Student()
                {
                    StudentNo = Convert.ToInt32(reader[0]),
                    LoginPwd = reader[1].ToString(),
                    StudentName = reader[2].ToString(),
                    Gender = reader[3].ToString(),
                    GradeId = Convert.ToInt32(reader[4]),
                    Phone = reader[5].ToString(),
                    Address = reader[6].ToString(),
                    BornDate = Convert.ToDateTime(reader[7]),
                    Email = reader[8].ToString(),
                    IdentityCard = reader[9].ToString()
                };
                studentList.Add(stu);
            }
            reader.Close();
            return studentList;
        }
        /// <summary>
        /// 班级列表
        /// </summary>
        /// <returns></returns>
        public static List<Grade> GetAllGrade()
        {
            string sql = "select * from Grade";
            SqlDataReader reader = SqlHelper.ExecuteReader(sql);
            List<Grade> gradeList = new List<Grade>();
            while (reader.Read())
            {
                Grade gr = new Grade()
                {
                    GradeId=Convert.ToInt32(reader[0]),
                    GradeName=reader[1].ToString(),
                };
                gradeList.Add(gr);
            }
            reader.Close();
            return gradeList;
        }
        /// <summary>
        /// Admin列表
        /// </summary>
        /// <returns></returns>
        public static List<Admin> GetAllAdmin()
        {
            string sql = "select * from admin";
            SqlDataReader reader = SqlHelper.ExecuteReader(sql);
            List<Admin> adminList = new List<Admin>();
            while (reader.Read())
            {
                Admin ad= new Admin()
                {
                    loginId=reader[0].ToString(),
                    loginPwd=reader[1].ToString()
                };
                adminList.Add(ad);
            }
            reader.Close();
            return adminList;
        }
        /// <summary>
        /// 得到临时数据表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataSet GetAllInfo(string tableName)
        {
            string sql = "select * from student";
            DataSet ds=SqlHelper.ExexuteDataSet(sql,tableName);
            return ds;
        }
        public static string AddReault(string sql)
        {
            int a=SqlHelper.ExecuteNonQuery(sql);
            if (a>0)
            {
                return "添加成功";
            }
            return "失败";
        }
    }
}
