using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MySchool1.DAL
{
    public class SqlHelper
    {
        //连接数据库字符串
        public static readonly string conString="Data Source=.;DataBase=MySchool;user id=sa;pwd=1";
        /// <summary>
        /// 获取多行多列值
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql)
        {
            SqlConnection con=new SqlConnection(conString);
            con.Open();
            SqlCommand com = new SqlCommand(sql,con);
            SqlDataReader reader = com.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
        /// <summary>
        /// 执行增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql)
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand com = new SqlCommand(sql,con);
            int result = com.ExecuteNonQuery();
            con.Close();
            return result;
        }
        /// <summary>
        /// 返回临时数据表
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataSet ExexuteDataSet(string sql,string tableName)
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(conString);
            SqlDataAdapter adapter = new SqlDataAdapter(sql,con);
            adapter.Fill(ds,tableName);
            return ds;
        }
    }
}
