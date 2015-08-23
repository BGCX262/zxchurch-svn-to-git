using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Net;
using ZXChurch.Common;
using System.IO;
namespace ZXChurch.Weekly
{
    /// <summary>
    /// 周报保存页面类
    /// </summary>
    public class WebWeeklySave: System.Web.UI.Page
    {
        #region 页面变量
        private int intId = 0;
        #endregion

        #region 页面控件
        private TextBox txtTitle;
        private TextBox txtURL;
        private TextBox txtMessage;
        private Button btSave;
        #endregion

        #region 页面事件
        protected void Page_Load(object sender, EventArgs e)
        {
            intId = RequestHelper.RequestInt("id");
            LoadWebControl();
            PageChack();

            if (!IsPostBack)
            {
                BindData();
            }
            
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Trim().Length == 0) { Common.PageHelper.Alert(this.Page, "请填写标题"); return; }
            if (new DB().SaveWeekly(intId, txtTitle.Text, txtURL.Text, txtMessage.Text))
            {

                #region 文件生成
                string tempPath = Server.MapPath("templates/view.html");
                StringBuilder sb = new StringBuilder(1000);
                if (File.Exists(tempPath))
                    sb.Append(File.ReadAllText(tempPath, Encoding.UTF8));
                if (intId == 0) { intId = new DB().GetMaxWeeklyID(); }
                if (intId > 0)
                {
                    Dictionary<string, string> dic = new DB().GetWeeklyInfoById(intId);
                    foreach (string key in dic.Keys)
                        sb.Replace("{" + key + "}", dic[key]);
                    string FileDir = Server.MapPath("/weekly/");
                    try
                    {
                        File.WriteAllText(FileDir + "/" + intId + ".html", sb.ToString());
                    }
                    catch
                    {

                    }
                }
                #endregion

                Response.Redirect("admin.aspx");
                ZXChurch.Admin.AdminManage.SaveWebLog(" 周报管理", txtTitle.Text + "保存成功");
            }
            else
            {
                Common.PageHelper.Alert(this.Page,"保存失败"); return;
            }
        }

        
        #endregion

        #region 页面方法

        private void BindData()
        {
            if (intId > 0)
            {
                List<string> values = new DB().GetWeeklyInfo(intId);
                if (values.Count >= 3)
                {
                    txtTitle.Text = values[1];
                    txtURL.Text = values[2];
                    txtMessage.Text = values[3];
                }
            }           
            
        }

        /// <summary>
        /// 加载用户控件
        /// </summary>
        private void LoadWebControl()
        {
          txtTitle = (TextBox)this.FindControl("txtTitle");
          txtURL= (TextBox)this.FindControl("txtURL") ;
          txtMessage= (TextBox)this.FindControl("txtMessage") ;
          btSave= (Button)this.FindControl("btSave") ;
          btSave.Click += new EventHandler(btSave_Click);
        }
        /// <summary>
        /// 身份权限判断
        /// </summary>
        private void PageChack()
        {
            if (Session[ZXChurch.Admin.SessionKeys.WeeklyAdminSession] == null)
            {
                Common.PageHelper.Alert(this.Page,"登陆超时","admin.aspx");
            }
            else
            {
                if (ZXChurch.Admin.AdminManage.AdminID > 0)
                {
                    if (new DB().CheckAdmin())
                    {
                       
                    }
                    else
                    {
                        Common.PageHelper.Alert(this.Page, "无权限访问", "index.html");
                        return;
                    }
                }
            }            
        }
        #endregion
    }
}
