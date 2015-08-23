using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using ZXChurch.Admin;
using System.Data;
using ZXChurch.Common;

namespace ZXChurch.Weekly
{
    /// <summary>
    /// 月报数据操作
    /// </summary>
    public class DB
    {
        public OleDbConnection getConn()
        {
            string connstr = System.Configuration.ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            OleDbConnection tempconn = new OleDbConnection(connstr);
            return (tempconn);
        }
        /// <summary>
        /// 判断管理员是否有权限
        /// </summary>
        /// <returns></returns>
        public bool CheckAdmin()
        {
            bool result = false;
            int id = 0;
            OleDbConnection conn = getConn(); //getConn():得到连接对象
            string strCom = "Select * from Users WHERE  UserID=@uid AND userType=1";//判断栏目用户表中是否有此管理员
            OleDbCommand myCommand = new OleDbCommand(strCom, conn);
            myCommand.Parameters.AddWithValue("@uid", ZXChurch.Admin.AdminManage.AdminID);
            conn.Open();
            OleDbDataReader reader;
            reader = myCommand.ExecuteReader(); //执行command并得到相应的DataReader
            //下面把得到的值赋给tempnote对象
            if (reader.Read())
            {
                result = true;
                System.Web.HttpContext.Current.Session.Add(SessionKeys.WeeklyAdminSession, reader["id"]);
                id = int.Parse(reader["id"].ToString());
            }
            reader.Close();
            conn.Close();

            if (id > 0)
            {

                conn.Open();
                strCom = " UPDATE Users SET LoginTime='" + DateTime.Now.ToString("yyyy-MM-dd") + "'  WHERE ID=@ID";

                myCommand.Parameters.Clear();
                myCommand.CommandText = strCom;
                myCommand.Parameters.AddWithValue("@ID", id);
                int intR = myCommand.ExecuteNonQuery();
                conn.Close();
            }

            return result;
        }
        #region 周报相关操作

        public DataSet GetDataList(int pageIndex,int pageSize,out int recordCount)
        {
            recordCount = 0;
            string strSql = "SELECT TOP "+pageSize+" [ID], [Flag], [URL], [AddTime], [UserName], [Title] FROM [weekly] ORDER BY [ID] DESC";
            if (pageIndex > 1)
            {
                strSql = "SELECT  [ID], [Flag], [URL], [AddTime], [UserName], [Title] FROM [weekly]"
                    +" WHERE ID >="
                    + " (SELECT MIN([ID]) FROM(SELECT TOP " + (pageSize * pageIndex) + " [ID] FROM  [weekly] ORDER BY [ID] DESC))"
                    +"  AND ID<"
                    + " (SELECT MIN([ID]) FROM(SELECT TOP " + (pageSize * pageIndex - pageSize) + " [ID] FROM  [weekly] ORDER BY [ID] DESC))"
                    +" ORDER BY [ID] DESC";
            }
            DataSet ds = new DataSet();
            using (OleDbConnection conn = getConn())
            {          
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(myCommand);              
                adapter.Fill(ds);

                conn.Open();
                myCommand.CommandText = "SELECT COUNT([ID]) FROM [weekly]";
                OleDbDataReader reader= myCommand.ExecuteReader();
               
                if (reader.Read())
                {
                    recordCount = int.Parse(reader[0].ToString());
                }
            }
            


            return ds;
        }

        public bool SaveWeekly(int id,string title,string url,string message)
        {
            bool result = false;
            int intR = 0;
            if (id > 0)
            {
                #region UPDATE
                using (OleDbConnection conn = getConn())
                {
                    conn.Open();
                    string strCom = " UPDATE weekly SET [URL]=@URL,[Title]=@Title,[Message]=@Message  WHERE ID=@ID";
                    OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                    myCommand.CommandText = strCom;
                    myCommand.Parameters.AddWithValue("@URL", url);
                    myCommand.Parameters.AddWithValue("@Title", title);
                    myCommand.Parameters.AddWithValue("@Message", message);
                    myCommand.Parameters.AddWithValue("@ID", id);
                    
                    intR = myCommand.ExecuteNonQuery();
                }
                #endregion
            }
            else
            {
                #region INSERT
                using (OleDbConnection conn = getConn())
                {
                    conn.Open();
                    string strCom = " INSERT INTO weekly([Flag], [URL], [AddTime], [UserName], [Title],[Message] ) VALUES(0,@URL,'" + DateTime.Now.ToString("yyyy-MM-dd") + "',@UserName,@Title,@Message)";
                    OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                    myCommand.CommandText = strCom;
                    myCommand.Parameters.AddWithValue("@URL", url);
                    myCommand.Parameters.AddWithValue("@UserName", ZXChurch.Admin.AdminManage.AdminName);
                    myCommand.Parameters.AddWithValue("@Title", title);
                    myCommand.Parameters.AddWithValue("@Message", message);
                    
                    intR = myCommand.ExecuteNonQuery();
                }
                #endregion
            }

            if (intR > 0) result = true;
            return result;
        }

        public bool DeleteWeekly(int id)
        {
            bool result = false;
            int intR = 0;
            if (id > 0)
            {
                using (OleDbConnection conn = getConn())
                {
                    conn.Open();
                    string strCom = " DELETE FROM weekly   WHERE ID=@ID";
                    OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                    myCommand.CommandText = strCom;               
                    myCommand.Parameters.AddWithValue("@ID", id);
                    intR = myCommand.ExecuteNonQuery();
                }
                if (intR > 0) result = true;
            }
            return result;
        }

        public List<string> GetWeeklyInfo(int id)
        {
            List<string> values = new List<string>();
            using (OleDbConnection conn = getConn())
            {
                conn.Open();
                string strSql = "SELECT [ID], [Title],[URL],[Message], [Flag],  [AddTime], [UserName] FROM [Weekly] WHERE [ID]=@ID";
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                myCommand.Parameters.AddWithValue("@ID", id);
                OleDbDataReader reader = myCommand.ExecuteReader();

                if (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                        values.Add(reader[i].ToString());
                }
            }
            return values;
        }
        /// <summary>
        /// 更新查看次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateWeeklyFlag(int id)
        {
            bool result = false;
            int intR = 0;
            if (id > 0)
            {
                using (OleDbConnection conn = getConn())
                {
                    conn.Open();
                    string strCom = " UPDATE weekly SET [Flag]=[Flag]+1   WHERE ID=@ID";
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
        /// 更新周报查看次数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="add">是否更新</param>
        /// <returns></returns>
        public int GetWeeklyCount(int id, bool add)
        {
            int intR = 0;
            using (OleDbConnection conn = getConn())
            {
                conn.Open();

                string strCom = " ";

                OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                if (add)
                {
                    myCommand.CommandText = " UPDATE weekly SET [Flag]=[Flag]+1  WHERE ID=@ID"; ;
                    myCommand.Parameters.AddWithValue("@ID", id);
                    intR = myCommand.ExecuteNonQuery();
                }

                myCommand.CommandText = "SELECT Flag FROM [weekly] WHERE ID=@ID";
                myCommand.Parameters.AddWithValue("@ID", id);
                intR = TypeHelper.ToInt(myCommand.ExecuteScalar());

            }
            return intR;
        }

        /// <summary>
        /// 获取最大ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxWeeklyID()
        {
            int intMaxId = 0;
            using (OleDbConnection conn = getConn())
            {
                conn.Open();
                string strSql = "SELECT MAX([ID]) FROM [Weekly] ";
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                OleDbDataReader reader = myCommand.ExecuteReader();

                if (reader.Read())
                {
                    intMaxId = TypeHelper.ToInt(reader[0]);
                }
            }

            return intMaxId;
        }
        /// <summary>
        /// 获取周报信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetWeeklyInfoById(int id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            using (OleDbConnection conn = getConn())
            {
                conn.Open();
                string strSql = "SELECT [ID], [Title],[URL],[Message], [Flag],  [AddTime], [UserName] FROM [Weekly] WHERE [ID]=@ID";
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                myCommand.Parameters.AddWithValue("@ID", id);
                OleDbDataReader reader = myCommand.ExecuteReader();

                if (reader.Read())
                {
                    dic.Add("ID", reader["ID"].ToString());
                    dic.Add("Title", reader["Title"].ToString());
                    dic.Add("URL", reader["URL"].ToString());
                    dic.Add("Flag", reader["Flag"].ToString());
                    dic.Add("Message", reader["Message"].ToString());
                    dic.Add("AddTime", TypeHelper.ToDateTime(reader["AddTime"]).ToString("yyyy-MM-dd"));
                }
            }

            return dic;
        }
        #endregion


        #region 管理员操作接口
        /// <summary>
        /// 修改管理员状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userType">0=禁用，1=有效</param>
        /// <returns></returns>
        public bool UpDateUser(int id, int userType)
        {
            bool result = false;
            int intR = 0;
            if (id > 0)
            {
                using (OleDbConnection conn = getConn())
                {
                    conn.Open();
                    string strCom = " UPDATE Users SET UserType=@UserType  WHERE ID=@ID";
                    OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                    myCommand.CommandText = strCom;
                    myCommand.Parameters.AddWithValue("@UserType", userType);
                    myCommand.Parameters.AddWithValue("@ID", id);
                    intR = myCommand.ExecuteNonQuery();
                }
                if (intR > 0) result = true;
            }
            return result;
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteUser(int id)
        {
            bool result = false;
            int intR = 0;
            if (id > 0)
            {
                using (OleDbConnection conn = getConn())
                {
                    conn.Open();
                    string strCom = " DELETE FROM Users   WHERE ID=@ID";
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
        /// 保存管理员
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="username"></param>
        /// <param name="usertype">0=禁用，1=有效</param>
        /// <returns></returns>
        public bool SaveUser(int userid, string username, int usertype)
        {
            bool result = false;
            int intR = 0;


            if (userid > 0)
            {
                #region UPDATE
                using (OleDbConnection conn = getConn())
                {
                    conn.Open();

                    string strUpDateCom = " UPDATE [Users] SET [UserName]=@UserName,[UserType]=@UserType  WHERE UserID=@UserID";
                    string strInsertCom = " INSERT INTO Users([UserId], [UserName], [UserType], [LoginTime] ) VALUES(@UserID,@UserName,@UserType,'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    OleDbCommand myCommand = new OleDbCommand("", conn);

                    myCommand.CommandText = "SELECT [ID] FROM [Users]  WHERE [UserId]="+userid;
                    IDataReader reader = myCommand.ExecuteReader();
                    if (reader.Read()) { intR = 1; } reader.Close(); 
                    if (intR == 1)
                    {
                        myCommand.CommandText = strUpDateCom;
                        myCommand.Parameters.AddWithValue("@UserName", username);
                        myCommand.Parameters.AddWithValue("@UserType", usertype);
                        myCommand.Parameters.AddWithValue("@UserID", userid);
                    }
                    else
                    {
                        myCommand.CommandText = strInsertCom;
                        myCommand.Parameters.AddWithValue("@UserID", userid);
                        myCommand.Parameters.AddWithValue("@UserName", username);
                        myCommand.Parameters.AddWithValue("@UserType", usertype);                       
                    
                    }

                    intR = myCommand.ExecuteNonQuery();
                }
                #endregion
            }            

            if (intR > 0) result = true;
            return result;
        }
        /// <summary>
        /// 获取管理员列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataSet GetUserList(int pageIndex, int pageSize, out int recordCount)
        {
            recordCount = 0;
            string strSql = "SELECT TOP " + pageSize + " * FROM [Users] ORDER BY [ID] DESC";
            if (pageIndex > 1)
            {
                strSql = "SELECT  * FROM [Users]"
                    + " WHERE ID >="
                    + " (SELECT MIN([ID]) FROM(SELECT TOP " + (pageSize * pageIndex) + " [ID] FROM  [Users] ORDER BY [ID] DESC))"
                    + "  AND ID<"
                    + " (SELECT MIN([ID]) FROM(SELECT TOP " + (pageSize * pageIndex - pageSize) + " [ID] FROM  [Users] ORDER BY [ID] DESC))"
                    + " ORDER BY [ID] DESC";
            }
            DataSet ds = new DataSet();
            using (OleDbConnection conn = getConn())
            {
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(myCommand);
                adapter.Fill(ds);

                conn.Open();
                myCommand.CommandText = "SELECT COUNT([ID]) FROM [Users]";
                OleDbDataReader reader = myCommand.ExecuteReader();

                if (reader.Read())
                {
                    recordCount = int.Parse(reader[0].ToString());
                }
            }



            return ds;
        }
        #endregion
    }
}
