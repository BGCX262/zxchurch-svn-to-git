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
    public class DBNews
    {
        public OleDbConnection getConn()
        {
            string connstr = System.Configuration.ConfigurationManager.ConnectionStrings["NewsDB"].ConnectionString;
            OleDbConnection tempconn = new OleDbConnection(connstr);
            return (tempconn);
        }

        #region 文章分类后台操作
        /// <summary>
        /// 获取文章类别列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataSet GetDataListByNewsType()
        {
            DataSet ds = new DataSet();
            using (OleDbConnection conn = getConn())
            {
                OleDbCommand myCommand = new OleDbCommand("SELECT [ID],[TypeName],[AddTime] FROM [NewsType]", conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(myCommand);
                adapter.Fill(ds);
            }
            return ds;
        }
        /// <summary>
        /// 获取文章类别列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataSet GetDataListByNewsType(int pageIndex, int pageSize, out int recordCount)
        {
            recordCount = 0;
            string strSql = "SELECT TOP " + pageSize + " * FROM [NewsType] ORDER BY [ID] DESC";
            if (pageIndex > 1)
            {
                strSql = "SELECT  * FROM [NewsType]"
                    + " WHERE ID >="
                    + " (SELECT MIN([ID]) FROM(SELECT TOP " + (pageSize * pageIndex) + " [ID] FROM  [NewsType] ORDER BY [ID] DESC))"
                    + "  AND ID<"
                    + " (SELECT MIN([ID]) FROM(SELECT TOP " + (pageSize * pageIndex - pageSize) + " [ID] FROM  [NewsType] ORDER BY [ID] DESC))"
                    + " ORDER BY [ID] DESC";
            }
            DataSet ds = new DataSet();
            using (OleDbConnection conn = getConn())
            {
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(myCommand);
                adapter.Fill(ds);

                conn.Open();
                myCommand.CommandText = "SELECT COUNT([ID]) FROM [NewsType]";
                OleDbDataReader reader = myCommand.ExecuteReader();

                if (reader.Read())
                {
                    recordCount = int.Parse(reader[0].ToString());
                }
            }
            return ds;
        }
        /// <summary>
        /// 保存文章类别信息 2013-04-05
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newsCategoryName"></param>
        /// <returns></returns>
        public bool SaveNewsType(int id, string newsCategoryName)
        {
            bool result = false;
            int intR = 0;
            if (id > 0)
            {
                #region 编辑信息 ID,newsCategoryName，
                using (OleDbConnection conn = getConn())
                {
                    conn.Open();
                    string strCom = " UPDATE [NewsType] SET [TypeName]=@TypeName WHERE ID=@ID";
                    OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                    myCommand.Parameters.AddWithValue("@newsCategoryName", newsCategoryName);
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
                    string strCom = " INSERT INTO [NewsType] ([TypeName],[AddTime]) VALUES(@newsCategoryName,'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                    myCommand.CommandText = strCom;
                    myCommand.Parameters.AddWithValue("@newsCategoryName", newsCategoryName);
                    intR = myCommand.ExecuteNonQuery();
                }
                #endregion
            }

            if (intR > 0) result = true;
            return result;
        }

        /// <summary>
        /// 根据id获取文章类别信息 2013-04-05
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newsCategoryName"></param>
        /// <returns></returns>
        public List<string> GetNewsTypeId(int id)
        {
            List<string> values = new List<string>();
            using (OleDbConnection conn = getConn())
            {
                conn.Open();
                string strSql = "SELECT [ID], [TypeName] FROM [NewsType] WHERE [ID]=@ID";
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
        /// 根据id删除文章类别信息 2013-04-05
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newsCategoryName"></param>
        /// <returns></returns>
        public bool DeleteNewsType(int id)
        {
            bool result = false;
            int intR = 0;
            if (id > 0)
            {
                using (OleDbConnection conn = getConn())
                {
                    conn.Open();
                    string strCom = " DELETE FROM [NewsType]   WHERE ID=@ID";
                    OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                    myCommand.CommandText = strCom;
                    myCommand.Parameters.AddWithValue("@ID", id);
                    intR = myCommand.ExecuteNonQuery();
                }
                if (intR > 0) result = true;
            }
            return result;
        }
        #endregion

        #region 文章后台操作
        /// <summary>
        /// 保存文章信息 2013-04-05
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newsCategoryName"></param>
        /// <returns></returns>
        public bool NewsSave(int id, int newsType, int isShow, string txtNewsTitle, string txtNewsTitleColor, string txtNewsPicture, string txtNewsFrom, string txtNewsAuthor, int txtNewsTopWeight, int txtNewsEilWeigth, int txtNewsDisplayOrder, string txtNewsMessage)
        {
            bool result = false;
            int intR = 0;
            if (id > 0)
            {
                #region 编辑信息 ID,newsCategoryName，
                using (OleDbConnection conn = getConn())
                {
                    conn.Open();
                    string strCom = " UPDATE [News] SET [CID]=@CID,[TColor]=@TColor,[TPic]=@TPic,[TFrom]=@TFrom,[Author]=@Author,[TopWeight]=@TopWeight,[EilWeigth]=@EilWeigth,[DisplayOrder]=@DisplayOrder,[IsShow]=@IsShow,[Title]=@Title,[Message]=@Message,[AdminId]=@AdminId,[AdminName]=@AdminName WHERE ID=@ID";
                    OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                    myCommand.Parameters.AddWithValue("@CID", newsType);
                    myCommand.Parameters.AddWithValue("@TColor", txtNewsTitleColor);
                    myCommand.Parameters.AddWithValue("@TPic", txtNewsPicture);
                    myCommand.Parameters.AddWithValue("@TFrom", txtNewsFrom);
                    myCommand.Parameters.AddWithValue("@Author", txtNewsAuthor);
                    myCommand.Parameters.AddWithValue("@TopWeight", txtNewsTopWeight);
                    myCommand.Parameters.AddWithValue("@EilWeigth", txtNewsEilWeigth);
                    myCommand.Parameters.AddWithValue("@DisplayOrder", txtNewsDisplayOrder);
                    myCommand.Parameters.AddWithValue("@IsShow", isShow);
                    myCommand.Parameters.AddWithValue("@Title", txtNewsTitle);
                    myCommand.Parameters.AddWithValue("@Message", txtNewsMessage);
                    myCommand.Parameters.AddWithValue("@AdminId", newsType);
                    myCommand.Parameters.AddWithValue("@AdminName", newsType);
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
                    string strCom = " INSERT INTO [News] ([CID],[TColor],[TPic],[TFrom],[Author],[TopWeight],[EilWeigth],[DisplayOrder],[IsShow],[Title],[Message],[AdminId],[AdminName],[AddTime]) VALUES(@CID,@TColor,@TPic,@TFrom,@Author,@TopWeight,@EilWeigth,@DisplayOrder,@IsShow,@Title,@Message,@AdminId,@AdminName,'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                    myCommand.CommandText = strCom;
                    myCommand.Parameters.AddWithValue("@CID", newsType);
                    myCommand.Parameters.AddWithValue("@TColor", txtNewsTitleColor);
                    myCommand.Parameters.AddWithValue("@TPic", txtNewsPicture);
                    myCommand.Parameters.AddWithValue("@TFrom", txtNewsFrom);
                    myCommand.Parameters.AddWithValue("@Author", txtNewsAuthor);
                    myCommand.Parameters.AddWithValue("@TopWeight", txtNewsTopWeight);
                    myCommand.Parameters.AddWithValue("@EilWeigth", txtNewsEilWeigth);
                    myCommand.Parameters.AddWithValue("@DisplayOrder", txtNewsDisplayOrder);
                    myCommand.Parameters.AddWithValue("@IsShow", isShow);
                    myCommand.Parameters.AddWithValue("@Title", txtNewsTitle);
                    myCommand.Parameters.AddWithValue("@Message", txtNewsMessage);
                    myCommand.Parameters.AddWithValue("@AdminId", AdminManage.AdminID);
                    myCommand.Parameters.AddWithValue("@AdminName", AdminManage.AdminName);
                    intR = myCommand.ExecuteNonQuery();
                }
                #endregion
            }

            if (intR > 0) result = true;
            return result;
        }
        /// <summary>
        /// 修改文章显示状态 2013-04-05
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newsCategoryName"></param>
        /// <returns></returns>
        public bool updateByNewsIsShow(int id, int isShow)
        {
            bool result = false;
            int intR = 0;
            if (id > 0)
            {
                #region 编辑信息 ID,newsCategoryName，
                using (OleDbConnection conn = getConn())
                {
                    conn.Open();
                    string strCom = " UPDATE [News] SET [IsShow]=@IsShow WHERE ID=@ID";
                    OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                    myCommand.Parameters.AddWithValue("@IsShow", isShow);
                    myCommand.Parameters.AddWithValue("@ID", id);
                    intR = myCommand.ExecuteNonQuery();
                }
                #endregion
            }
            if (intR > 0) result = true;
            return result;
        }
        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataSet GetDataListByNews(int pageIndex, int pageSize,int cid,string searchkey, out int recordCount)
        {
            recordCount = 0;
            string coloum = " [ID],[CID],[IsShow],[Title],[TColor],[TPic],[TFrom],[Author],[TopWeight],[EilWeigth],[DisplayOrder],[AdminId],[AdminName],[AddTime] ";//列表不需要查,[Message]
            string conditon = "";
            if (cid > 0) {
                conditon = " AND [CID]="+cid;
            }
            if (searchkey.Trim().Length > 0) {
                conditon += " AND [Title] like'%"+searchkey+"%'";
            }
            string strSql = "SELECT TOP " + pageSize + coloum + "  FROM [News] WHERE [ID]>0 " + conditon + "  ORDER BY [ID] DESC";
            if (pageIndex > 1)
            {
                strSql = "SELECT  " + coloum + " FROM [News]"
                    + " WHERE ID >="
                    + " (SELECT MIN([ID]) FROM(SELECT TOP " + (pageSize * pageIndex) + " [ID] FROM  [News] WHERE [ID]>0 " + conditon + "  ORDER BY [ID] DESC))"
                    + "  AND ID<"
                    + " (SELECT MIN([ID]) FROM(SELECT TOP " + (pageSize * pageIndex - pageSize) + " [ID] FROM  [News] WHERE [ID]>0 " + conditon + "  ORDER BY [ID] DESC))"
                    + conditon 
                    + " ORDER BY [ID] DESC";
            }
            DataSet ds = new DataSet();
            using (OleDbConnection conn = getConn())
            {
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(myCommand);
                adapter.Fill(ds);

                conn.Open();
                myCommand.CommandText = "SELECT COUNT([ID]) FROM [News]";
                OleDbDataReader reader = myCommand.ExecuteReader();

                if (reader.Read())
                {
                    recordCount = int.Parse(reader[0].ToString());
                }
            }
            return ds;
        }

        /// <summary>
        /// 根据id删除文章类别信息 2013-04-05
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newsCategoryName"></param>
        /// <returns></returns>
        public bool DeleteNews(int id)
        {
            bool result = false;
            int intR = 0;
            if (id > 0)
            {
                using (OleDbConnection conn = getConn())
                {
                    conn.Open();
                    string strCom = " DELETE FROM [News] WHERE ID=@ID";
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
        /// 根据id获取文章类别信息 2013-04-05
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newsCategoryName"></param>
        /// <returns></returns>
        public List<string> GetNewsId(int id)
        {
            List<string> values = new List<string>();
            using (OleDbConnection conn = getConn())
            {
                conn.Open();
                string strSql = "SELECT [CID],[IsShow],[Title],[TColor],[TPic],[TFrom],[Author],[TopWeight],[EilWeigth],[DisplayOrder],[Message],[AdminId],[AdminName],[AddTime] FROM [News] WHERE [ID]=@ID";
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
        /// 获取最新ID
        /// </summary>
        /// <returns></returns>
        public int GetNewsMaxId()
        {
            int intMaxId = 0;
            using (OleDbConnection conn = getConn())
            {
                conn.Open();
                string strSql = "SELECT Max(ID) FROM [News] ";
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                OleDbDataReader reader = myCommand.ExecuteReader();

                if (reader.Read())
                {
                    intMaxId = TypeHelper.ToInt(reader[0]);
                }
                reader.Close();               
            }
            return intMaxId;
        }

        public Dictionary<string, string> GetNewsById(int id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            using (OleDbConnection conn = getConn())
            {
                conn.Open();
                string strSql = "SELECT [ID], [CID],[IsShow],[Title],[TColor],[TPic],[TFrom],[Author],[TopWeight],[EilWeigth],[DisplayOrder],[Message],[AdminId],[AdminName],[AddTime] FROM [News] WHERE [ID]=@ID";
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                myCommand.Parameters.AddWithValue("@ID", id);
                OleDbDataReader reader = myCommand.ExecuteReader();

                if (reader.Read())
                {
                    dic.Add("ID", reader["id"].ToString());
                    dic.Add("CID", reader["cid"].ToString());
                    dic.Add("IsShow", reader["IsShow"].ToString());
                    dic.Add("Title", reader["Title"].ToString());
                    dic.Add("TFrom", reader["TFrom"].ToString());
                    dic.Add("Author", reader["Author"].ToString());
                    dic.Add("Message", reader["Message"].ToString());
                    dic.Add("AddTime", TypeHelper.ToDateTime(reader["AddTime"]).ToString("yyyy-MM-dd"));
                    dic.Add("DisplayOrder", reader["DisplayOrder"].ToString());
                }
            }
            return dic;
        }

        #endregion

        #region index.html首页后台操作
        /// <summary>
        /// 获取首页内容
        /// </summary>
        /// <returns></returns>
        public string WebIndexMessageGet()
        {
            string str = "";
            using (OleDbConnection conn = getConn())
            {
                conn.Open();
                string strSql = "SELECT [Message] FROM [IndexWeb] WHERE [Title]='web_index'";
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);               
                OleDbDataReader reader = myCommand.ExecuteReader();

                if (reader.Read())
                {
                    str = reader["Message"].ToString();
                }
            }
            return str;
        }
        /// <summary>
        /// 保存首页内容
        /// </summary>
        /// <param name="indexMsg"></param>
        /// <returns></returns>
        public bool WebIndexMessageSave(string indexMsg)
        {
            bool result = false;
            int intR = 0;
            
            #region 编辑信息 ID,newsCategoryName，
            using (OleDbConnection conn = getConn())
            {
                conn.Open();
                string strCom = " UPDATE [IndexWeb] SET [Message]=@Message WHERE [Title]='web_index'";
                OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                myCommand.Parameters.AddWithValue("@Message", indexMsg);
                intR = myCommand.ExecuteNonQuery();
            }
            #endregion
          
            if (intR > 0) result = true;
            return result;
        }

        #endregion

        #region  数据表初始化操作
        /// <summary>
        /// 初始化文章分类表
        /// </summary>
        /// <returns></returns>
        public bool ResetNewsTypeTable()
        {
            bool result = false;
            using (OleDbConnection conn = getConn())
            {
                conn.Open();
                OleDbCommand myCommand = new OleDbCommand("", conn);
                try
                {
                    string strCom = " drop table NewsType";
                    myCommand.CommandText = strCom;
                    myCommand.ExecuteNonQuery();
                }
                catch
                {

                }
                string strCreateSql = "Create Table [NewsType] ([ID] int identity(1,1) Primary key,[TypeName] VarChar(20) Not Null,[AddTime] DateTime Default '2013-01-01')";
                myCommand.CommandText = strCreateSql;
                myCommand.ExecuteNonQuery();
                #region 数据初始化
                string strInserSql = " INSERT INTO [NewsType] ([TypeName],[AddTime]) VALUES('灵粮分享','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                myCommand.CommandText = strInserSql;
                myCommand.ExecuteNonQuery();

                strInserSql = " INSERT INTO [NewsType] ([TypeName],[AddTime]) VALUES('婚姻家庭','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                myCommand.CommandText = strInserSql;
                myCommand.ExecuteNonQuery();

                strInserSql = " INSERT INTO [NewsType] ([TypeName],[AddTime]) VALUES('亲子关系','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                myCommand.CommandText = strInserSql;
                myCommand.ExecuteNonQuery();

                strInserSql = " INSERT INTO [NewsType] ([TypeName],[AddTime]) VALUES('生活百科','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                myCommand.CommandText = strInserSql;
                myCommand.ExecuteNonQuery();

                strInserSql = " INSERT INTO [NewsType] ([TypeName],[AddTime]) VALUES('书网文摘','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                myCommand.CommandText = strInserSql;
                myCommand.ExecuteNonQuery();

                strInserSql = " INSERT INTO [NewsType] ([TypeName],[AddTime]) VALUES('信仰随笔','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                myCommand.CommandText = strInserSql;
                myCommand.ExecuteNonQuery();

                strInserSql = " INSERT INTO [NewsType] ([TypeName],[AddTime]) VALUES('灵程感悟','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                myCommand.CommandText = strInserSql;
                myCommand.ExecuteNonQuery();

                strInserSql = " INSERT INTO [NewsType] ([TypeName],[AddTime]) VALUES('信仰诗选','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                myCommand.CommandText = strInserSql;
                myCommand.ExecuteNonQuery();

                strInserSql = " INSERT INTO [NewsType] ([TypeName],[AddTime]) VALUES('时事新闻','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                myCommand.CommandText = strInserSql;
                myCommand.ExecuteNonQuery();
                #endregion
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 初始化文章表数据
        /// </summary>
        /// <returns></returns>
        public bool ResetNewsTable()
        {
            bool result = false;
            using (OleDbConnection conn = getConn())
            {
                conn.Open();
                OleDbCommand myCommand = new OleDbCommand("", conn);
                try
                {
                    string strCom = " drop table [News]";
                    myCommand.CommandText = strCom;
                    myCommand.ExecuteNonQuery();
                }
                catch
                {

                }
                string strCreateSql = "Create Table [News] ([ID] int identity(1,1) Primary key,[CID] int,[TColor] VarChar(20),[TPic] VarChar(250),[TFrom] VarChar(25),[Author] VarChar(10),[TopWeight] int,[EilWeigth] int,[DisplayOrder] int,[IsShow] int,[Title] VarChar(20) Not Null,[Message] text,[AdminId] int,[AdminName] VarChar(25),[AddTime] DateTime Default '2013-01-01')";
                myCommand.CommandText = strCreateSql;
                myCommand.ExecuteNonQuery();

                result = true;
            }
            return result;
        }
        /// <summary>
        /// 首页初始化
        /// </summary>
        /// <returns></returns>
        public bool ResetIndexPage()
        {
            bool result = false;
            using (OleDbConnection conn = getConn())
            {
                conn.Open();
                OleDbCommand myCommand = new OleDbCommand("", conn);
                try
                {
                    string strCom = " drop table [IndexWeb]";
                    myCommand.CommandText = strCom;
                    myCommand.ExecuteNonQuery();
                }
                catch
                {

                }
                string strCreateSql = "Create Table [IndexWeb] ([ID] int identity(1,1) Primary key,[Title] VarChar(20) Not Null,[Message] text,[AdminId] int,[AdminName] VarChar(25),[AddTime] DateTime Default '2013-01-01')";
                myCommand.CommandText = strCreateSql;
                myCommand.ExecuteNonQuery();

                #region 数据初始化
                string strInserSql = " INSERT INTO [IndexWeb] ([Title],[Message],[AddTime]) VALUES('web_index','系统正在更新中……','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                myCommand.CommandText = strInserSql;
                myCommand.ExecuteNonQuery();
                #endregion

                result = true;
            }
            return result;
        }
        #endregion

        #region 前台显示文章方法
        /// <summary>
        /// 前台网页显示文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<string> WebGetNewsId(int id)
        {
            List<string> values = new List<string>();
            using (OleDbConnection conn = getConn())
            {
                conn.Open();
                string strSql = "SELECT [ID], [Title],[Message],[AddTime],[DisplayOrder],[TFrom] FROM [News] WHERE [IsShow]=1 AND [ID]=@ID";
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
        ///前台页面获取文章列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataSet WebGetDataListByNews(int pageIndex, int pageSize, out int recordCount)
        {
            recordCount = 0;
            string coloum = " [ID],[Title],[AddTime] ";//列表不需要查,[Message]
            string strSql = "SELECT TOP " + pageSize + coloum + "  FROM [News] WHERE [IsShow]=1 ORDER BY [ID] DESC";
            if (pageIndex > 1)
            {
                strSql = "SELECT  " + coloum + " FROM [News]"
                    + " WHERE [IsShow] =1 AND ID >="
                    + " (SELECT MIN([ID]) FROM(SELECT TOP " + (pageSize * pageIndex) + " [ID] FROM  [News] WHERE [IsShow]=1 ORDER BY [ID] DESC))"
                    + "  AND ID<"
                    + " (SELECT MIN([ID]) FROM(SELECT TOP " + (pageSize * pageIndex - pageSize) + " [ID] FROM  [News] WHERE [IsShow]=1 ORDER BY [ID] DESC))"
                    + " ORDER BY [ID] DESC";
            }
            DataSet ds = new DataSet();
            using (OleDbConnection conn = getConn())
            {
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(myCommand);
                adapter.Fill(ds);

                conn.Open();
                myCommand.CommandText = "SELECT COUNT([ID]) FROM [News] WHERE [IsShow]=1";
                OleDbDataReader reader = myCommand.ExecuteReader();

                if (reader.Read())
                {
                    recordCount = int.Parse(reader[0].ToString());
                }
            }
            return ds;
        }
        /// <summary>
        /// 前台页面分类查询文章列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="cid"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataSet WebGetDataListByNews(int pageIndex, int pageSize, int cid, out int recordCount)
        {
            recordCount = 0;
            if (cid <= 0) return WebGetDataListByNews(pageIndex, pageSize, out recordCount);

            string coloum = " [ID],[Title],[AddTime] ";//列表不需要查,[Message]
            string strSql = "SELECT TOP " + pageSize + coloum + "  FROM [News] WHERE [IsShow]=1 AND [CID]=" + cid + " ORDER BY [ID] DESC";
            if (pageIndex > 1)
            {
                strSql = "SELECT  " + coloum + " FROM [News]"
                    + " WHERE [IsShow]=1 AND [CID]=" + cid + " AND ID >="
                    + " (SELECT MIN([ID]) FROM(SELECT TOP " + (pageSize * pageIndex) + " [ID] FROM  [News] WHERE [IsShow]=1 AND [CID]=" + cid + " ORDER BY [ID] DESC))"
                    + "  AND ID<"
                    + " (SELECT MIN([ID]) FROM(SELECT TOP " + (pageSize * pageIndex - pageSize) + " [ID] FROM  [News] WHERE [IsShow]=1 AND [CID]=" + cid + " ORDER BY [ID] DESC))"
                    + " ORDER BY [ID] DESC";
            }
            DataSet ds = new DataSet();
            using (OleDbConnection conn = getConn())
            {
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(myCommand);
                adapter.Fill(ds);

                conn.Open();
                myCommand.CommandText = "SELECT COUNT([ID]) FROM [News] WHERE [IsShow]=1 AND [Cid]=" + cid;
                OleDbDataReader reader = myCommand.ExecuteReader();

                if (reader.Read())
                {
                    recordCount = int.Parse(reader[0].ToString());
                }
            }
            return ds;
        }

        /// <summary>
        /// 更新文章查看次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateNewsDisplayOrder(int id)
        {
            bool result = false;
            int intR = 0;
            if (id > 0)
            {
                using (OleDbConnection conn = getConn())
                {
                    conn.Open();
                    string strCom = " UPDATE [News] SET [DisplayOrder]=[DisplayOrder]+1   WHERE ID=@ID";
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
        /// 查看文章次数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isadd">是否+1</param>
        /// <returns></returns>
        public int GetNewsDisplayOrder(int id, bool isadd)
        {
            int intR = 0;
            using (OleDbConnection conn = getConn())
            {
                conn.Open();
             
                string strCom = " ";
               
                OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                if (isadd)
                {
                    myCommand.CommandText = " UPDATE [News] SET [DisplayOrder]=[DisplayOrder]+1   WHERE ID=@ID"; ;
                    myCommand.Parameters.AddWithValue("@ID", id);
                    intR = myCommand.ExecuteNonQuery();
                }

                myCommand.CommandText = "SELECT DisplayOrder FROM [News] WHERE ID=@ID";
                myCommand.Parameters.AddWithValue("@ID", id);
                intR = TypeHelper.ToInt(myCommand.ExecuteScalar());

            }
            return intR;

        }
        /// <summary>
        /// 按发布时间排序
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public DataSet WebGetDataListOrderByIDDESC(int count)
        {
            string coloum = " [ID],[Title],[AddTime]";
            string strSql = "SELECT TOP " + count + coloum + "  FROM [News] WHERE [IsShow]=1  ORDER BY [ID] DESC";
            
            DataSet ds = new DataSet();
            using (OleDbConnection conn = getConn())
            {
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(myCommand);
                adapter.Fill(ds);
            }
            return ds;
        }
        /// <summary>
        /// 按查看次数排序
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public DataSet WebGetDataListOrderByDisplayOrderDESC(int count)
        {
            string coloum = " [ID],[Title],[AddTime] ";
            string strSql = "SELECT TOP " + count + coloum + "  FROM [News] WHERE [IsShow]=1  ORDER BY [DisplayOrder] DESC";

            DataSet ds = new DataSet();
            using (OleDbConnection conn = getConn())
            {
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(myCommand);
                adapter.Fill(ds);
            }
            return ds;
        }
        /// <summary>
        /// 按置顶排序
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public DataSet WebGetDataListByCidOrderByTopWeight(int cid, int count)
        {
            string coloum = " [ID],[Title],[AddTime] ";
            string condition = "WHERE [IsShow]=1  ";
            if (cid > 0) { condition += " AND [Cid]=" + cid; }
            string strSql = "SELECT TOP " + count + coloum + "  FROM [News] " + condition + "  ORDER BY [TopWeight] DESC";

            DataSet ds = new DataSet();
            using (OleDbConnection conn = getConn())
            {
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(myCommand);
                adapter.Fill(ds);
            }
            return ds;
        }

        /// <summary>
        /// 按精华排序
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public DataSet WebGetDataListByCidOrderByEilWeigth(int cid, int count)
        {
            string coloum = " [ID],[Title],[AddTime] ";
            string condition = " WHERE [IsShow]=1 AND [EilWeigth]>0 ";
            if (cid > 0) { condition += " AND [Cid]=" + cid; }
            string strSql = "SELECT TOP " + count + coloum + "  FROM [News]   " + condition + "  ORDER BY [EilWeigth] DESC";

            DataSet ds = new DataSet();
            using (OleDbConnection conn = getConn())
            {
                OleDbCommand myCommand = new OleDbCommand(strSql, conn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(myCommand);
                adapter.Fill(ds);
            }
            return ds;
        }
        #endregion

    }
}