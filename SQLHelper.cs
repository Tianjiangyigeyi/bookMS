using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace bookMS
{
    public class SQLHelper
    {
        public static string connectionString = "server=.;database=Library_db;user=sa;password=123456";
        public static bool OpenSQL()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 执行增删改操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>
        /// 受影响的行数
        /// </returns>
        public static int Update(string sql)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                return cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                //写入日志
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// 获得单一结果查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static object GetSingleResult(string sql)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                //写入日志
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// 返回一个结果集的查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static SqlDataReader GetReader(string sql)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch(Exception ex)
            {
                connection.Close();
                throw ex;
            }

        }
    }
}
