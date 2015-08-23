using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZXChurch.Admin
{
    public class ReLoadDB : System.Web.UI.Page
    {
        #region 页面变量
        #endregion

        #region 页面控件
        
        private int ac=Common.RequestHelper.RequestInt("ac");
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

      
        #endregion

        #region 页面方法

        private void BindData()
        {
            
            
            switch (ac)
            {
                case 1: new DBNews().ResetNewsTypeTable(); Response.Write("初始化文章分类表"); break;
                case 2: new DBNews().ResetNewsTable(); Response.Write("初始化文章表"); break;
                case 4: new DBNews().ResetIndexPage(); Response.Write("初始化首页表"); break;
                case 3: new DB().ResetSuperAdmin(); Response.Write("重设超级管理员"); break;
                default: break;
            }
                        

        }

        /// <summary>
        /// 加载用户控件
        /// </summary>
        private void LoadWebControl()
        {            
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
