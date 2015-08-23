using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.IO;

namespace ZXChurch.Admin.Web
{
    /// <summary>
    /// 首页index.html 管理
    /// </summary>
    public class AdminIndexManage:AdminBasePage
    {
        #region 页面变量
        #endregion

        #region 页面控件       
        private Button btSave;
        private TextBox txtIndexMessage;
        #endregion

        #region 页面事件
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadWebControl();
            PageChack();

            if (!IsPostBack)
            {
                BindData();
            }

        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (new DBNews().WebIndexMessageSave(txtIndexMessage.Text))
            {
                #region 文件生成
                string tempPath = Server.MapPath("templates/index.html");
                StringBuilder sb = new StringBuilder(1000);
                if (File.Exists(tempPath))
                {
                    sb.Append(File.ReadAllText(tempPath, Encoding.UTF8));
                    sb.Replace("{Message}", txtIndexMessage.Text);
                    string FileDir = Server.MapPath("/");
                    try
                    {
                        File.WriteAllText(FileDir + "/index.html", sb.ToString());
                    }
                    catch
                    {

                    }
                }

                #endregion

                Alert("保存成功",Request.Url.ToString());
            }
            else
            {
                Alert("保存失败");
            }
        }

        #endregion

        #region 页面方法

        private void BindData()
        {
            txtIndexMessage.Text = new DBNews().WebIndexMessageGet();
        }

        /// <summary>
        /// 加载用户控件
        /// </summary>
        private void LoadWebControl()
        {
            btSave = (Button)this.FindControl("btSave");
            btSave.Click += new EventHandler(btSave_Click);
            txtIndexMessage = (TextBox)this.FindControl("txtIndexMessage");
        }
        /// <summary>
        /// 身份权限判断
        /// </summary>
        private void PageChack()
        {            
        }
        #endregion
    }
}
