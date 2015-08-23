using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Web.Security;
using System.Data;
using ZXChurch.Common;

namespace ZXChurch.Admin
{
    public class DB
    {
        public OleDbConnection getConn()
        {
            string connstr = System.Configuration.ConfigurationManager.ConnectionStrings["Admin"].ConnectionString;
            OleDbConnection tempconn = new OleDbConnection(connstr);
            return (tempconn);
        }
        /// <summary>
        /// 管理员登陆
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool Login(string name, string pwd)
        {
            bool result = false;
            pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "MD5").ToLower();
           
            string strCom = "Select * from Admin WHERE username=@username AND PSWD=@PSWD";
            using (OleDbConnection conn = getConn())//getConn():得到连接对象
            {
                OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                myCommand.Parameters.AddWithValue("@username", name);
                myCommand.Parameters.AddWithValue("@PSW", pwd);
                conn.Open();
                OleDbDataReader reader;
                reader = myCommand.ExecuteReader(); //执行command并得到相应的DataReader
                //下面把得到的值赋给tempnote对象
                if (reader.Read())
                {
                    System.Web.HttpContext.Current.Session.Add(SessionKeys.AdminSession, reader["username"] + "|" + reader["id"] + "|" + reader["UserType"]);
                    result = true;
                    //context.Response.Write("{result:'ok'}");
                }
                //else
                //{
                    // context.Response.Write("{result:'error'}");
                //}

                reader.Close();
            }
            return result;
        }
        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataSet GetDataList(int pageIndex, int pageSize, out int recordCount)
        {
            recordCount = 0;
            string strSql = "SELECT TOP " + pageSize + " * FROM [Admin] ORDER BY [ID] DESC";
            if (pageIndex > 1)
            {
                strSql = "SELECT  * FROM [Admin]"
                    + " WHERE ID >="
                    + " (SELECT MIN([ID]) FROM(SELECT TOP " + (pageSize * pageIndex) + " [ID] FROM  [Admin] ORDER BY [ID] DESC))"
                    + "  AND ID<"
                    + " (SELECT MIN([ID]) FROM(SELECT TOP " + (pageSize * pageIndex - pageSize) + " [ID] FROM  [Admin] ORDER BY [ID] DESC))"
                    + " ORDER BY [ID] DESC";
            }
            DataSet ds = new DataSet();
            using (OleDbConnection conn = getConn())
            {
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(myCommand);
                adapter.Fill(ds);

                conn.Open();
                myCommand.CommandText = "SELECT COUNT([ID]) FROM [Admin]";
                OleDbDataReader reader = myCommand.ExecuteReader();

                if (reader.Read())
                {
                    recordCount = int.Parse(reader[0].ToString());
                }
            }
            return ds;
        }
        /// <summary>
        /// 有效的管理员列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataList()
        {
            DataSet ds = new DataSet();
            using (OleDbConnection conn = getConn())
            {
                OleDbCommand myCommand = new OleDbCommand("SELECT * FROM [Admin] WHERE [UserType]>"+AdminType.NonAdmin.GetHashCode(), conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(myCommand);
                adapter.Fill(ds);
            }
            return ds;
        }
        /// <summary>
        /// 保存用户信息 2013-03-18
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <param name="truename"></param>
        /// <param name="usertype"></param>
        /// <param name="pswd"></param>
        /// <returns></returns>
        public bool Save(int id, string username, string truename, int usertype, string pswd)
        {
            bool result = false;
            int intR = 0;
            if (id > 0)
            {
                #region 编辑信息 ID,UserName,TrueName,AddTime,UserType,PSWD,
                using (OleDbConnection conn = getConn())
                {
                    conn.Open();
                    string strCom = " UPDATE [Admin] SET [UserName]=@UserName,[TrueName]=@TrueName,[UserType]=@UserType,[PSWD]=@PSWD  WHERE ID=@ID";
                    if (pswd == "") {
                        strCom = " UPDATE [Admin] SET [UserName]=@UserName,[TrueName]=@TrueName,[UserType]=@UserType WHERE ID=@ID";
                    }
                    OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                    myCommand.Parameters.AddWithValue("@UserName", username);
                    myCommand.Parameters.AddWithValue("@TrueName", truename);
                    myCommand.Parameters.AddWithValue("@UserType", usertype);
                    if(pswd!="")
                        myCommand.Parameters.AddWithValue("@PSWD", FormsAuthentication.HashPasswordForStoringInConfigFile(pswd, "MD5").ToLower());
                    myCommand.Parameters.AddWithValue("@ID", id);

                    intR = myCommand.ExecuteNonQuery();
                }
                #endregion
            }
            else
            {
                #region 添加信息
                using (OleDbConnection conn = getConn())
                {
                    conn.Open();
                    string strCom = " INSERT INTO [Admin] ([UserName],[TrueName],[AddTime],[UserType],[PSWD] ) VALUES(@UserName,@TrueName,'"+DateTime.Now.ToString("yyyy-MM-dd")+"',@UserType,@PSWD )";
                    OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                    myCommand.CommandText = strCom;
                    myCommand.Parameters.AddWithValue("@UserName", username);
                    myCommand.Parameters.AddWithValue("@TrueName", truename);
                    myCommand.Parameters.AddWithValue("@UserType", usertype);
                    myCommand.Parameters.AddWithValue("@PSWD", pswd);

                    intR = myCommand.ExecuteNonQuery();
                }
                #endregion
            }

            if (intR > 0) result = true;
            return result;
        }

        public List<string> GetAdminId(int id)
        {
            List<string> values = new List<string>();
            using (OleDbConnection conn = getConn())
            {
                conn.Open();
                string strSql = "SELECT [ID], [UserName],[TrueName],[UserType] FROM [Admin] WHERE [ID]=@ID";
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                myCommand.Parameters.AddWithValue("@ID", id);
                OleDbDataReader reader = myCommand.ExecuteReader();

                if (reader.Read())
                {
                   for(int i=0; i<reader.FieldCount;i++)
                    values.Add(reader[i].ToString());
                }
            }
            return values;
        }

        public bool DeleteAdmin(int id)
        {
            bool result = false;
            int intR = 0;
            if (id > 0)
            {
                using (OleDbConnection conn = getConn())
                {
                    conn.Open();
                    string strCom = " DELETE FROM [Admin]   WHERE ID=@ID";
                    OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                    myCommand.CommandText = strCom;
                    myCommand.Parameters.AddWithValue("@ID", id);
                    intR = myCommand.ExecuteNonQuery();
                }
                if (intR > 0) result = true;
            }
            return result;
        }
        /// <summary>
        /// 修改密码    2013-03-19
        /// </summary>
        /// <param name="name"></param>
        /// <param name="oldpwd"></param>
        /// <param name="newpwd"></param>
        /// <returns></returns>
        public bool ResetPswd(string name, string oldpwd, string newpwd)
        {
            bool result = false;
            oldpwd = FormsAuthentication.HashPasswordForStoringInConfigFile(oldpwd, "MD5").ToLower();
            newpwd = FormsAuthentication.HashPasswordForStoringInConfigFile(newpwd, "MD5").ToLower();

            string strCom = "UPDATE Admin SET PSWD=@NewPSWD WHERE username=@username AND PSWD=@PSWD";
            using (OleDbConnection conn = getConn())//getConn():得到连接对象
            {
                OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                myCommand.Parameters.AddWithValue("@NewPSWD", newpwd);
                myCommand.Parameters.AddWithValue("@username", name);
                myCommand.Parameters.AddWithValue("@PSW", oldpwd);
                conn.Open();
                int intC = myCommand.ExecuteNonQuery(); //执行command
                if (intC > 0) result = true;
            }
            return result;
        }
        #region 系统初始化操作
        
        /// <summary>
        /// 重设超级管理员
        /// </summary>
        /// <returns></returns>
        public bool ResetSuperAdmin()
        {
            bool result = false;
            using (OleDbConnection conn = getConn())
            {
               
                string strCom = " SELECT [ID] FROM  [Admin] WHERE  [UserName]='zhongxing'";
                
                OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                 conn.Open();
                OleDbDataReader reader;
                int id = 0;
                reader = myCommand.ExecuteReader();
                if (reader.Read())
                {
                    id = TypeHelper.ToInt(reader["id"]);
                }
                reader.Close();

                if (id > 0)
                {
                    strCom = "UPDATE [Admin] SET [PSWD]='" + FormsAuthentication.HashPasswordForStoringInConfigFile("zhongxing@" + DateTime.Now.Year, "MD5").ToLower() + "',[UserType]=1 WHERE [ID]=" + id;
                }
                else
                {
                    strCom = " INSERT INTO [Admin] ([UserName],[TrueName],[AddTime],[UserType],[PSWD] ) VALUES('zhongxing','zhongxing','" + DateTime.Now.ToString("yyyy-MM-dd") + "',1,'" + FormsAuthentication.HashPasswordForStoringInConfigFile("zhongxing@" + DateTime.Now.Year, "MD5").ToLower() + "' )";
                }
               
                myCommand.CommandText = strCom;
                myCommand.ExecuteNonQuery();
                conn.Close();
            }
            return result;
        }
        #endregion
    }
}