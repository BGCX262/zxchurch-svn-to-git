using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace ZXChurch.WebLog
{
    /// <summary>
    /// 记录操作日志
    /// </summary>
    class DB
    {
        public OleDbConnection getConn()
        {
            string connstr = System.Configuration.ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            OleDbConnection tempconn = new OleDbConnection(connstr);
            return (tempconn);
        }
        public void SaveLog(string SysName, int AdminId,  string AdminName,  string Doing  )
        {
            using (OleDbConnection conn = getConn())
            {
                conn.Open();
                string strCom = " INSERT INTO syslog([sysname],[adminid],[adminname],[doing],[addtime] ) VALUES(@sysname,@adminid,@adminname,@doing,'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                OleDbCommand myCommand = new OleDbCommand(strCom, conn);
                myCommand.CommandText = strCom;
                myCommand.Parameters.AddWithValue("@sysname", SysName);
                myCommand.Parameters.AddWithValue("@adminid", AdminId);
                myCommand.Parameters.AddWithValue("@adminname", AdminName);
                myCommand.Parameters.AddWithValue("@doing", Doing);
                myCommand.ExecuteNonQuery();
            }
        }
    }
}
