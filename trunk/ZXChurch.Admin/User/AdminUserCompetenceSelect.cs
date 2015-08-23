using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace ZXChurch.Admin.User
{
    public class AdminUserCompetenceSelect:AdminBasePage
    {
        #region 页面变量
        #endregion

        #region 页面控件       
        private Repeater reptList;     
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

        protected void btDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((LinkButton)sender).CommandArgument);
            if (id > 0)
            {
                if (new DB().DeleteAdmin(id))
                {
                    //Alert("删除成功",);
                    Response.Redirect(Request.Url.ToString());
                }
                else
                {
                    Alert("删除失败");
                }
            }
            else
            {
                Alert("参数错误，删除失败！");
            }
        }

        #endregion

        #region 页面方法

        private void BindData()
        {
           
            reptList.DataSource = new DB().GetDataList();
            reptList.DataBind();
            
           

        }

        /// <summary>
        /// 加载用户控件
        /// </summary>
        private void LoadWebControl()
        {
            //MultiViewMonthlyBulletin = (MultiView)this.FindControl("MultiViewMonthlyBulletin");
            reptList = (Repeater)this.FindControl("reptList");            

        }
        /// <summary>
        /// 身份权限判断
        /// </summary>
        private void PageChack()
        {
            //if (Session[ZXChurch.Admin.SessionKeys.MonthlyBulletinSession] != null)
            //{
            //    MultiViewMonthlyBulletin.ActiveViewIndex = 1;
            //}
            //else
            //{
            //    if (ZXChurch.Admin.AdminManage.AdminID > 0)
            //    {
            //        if (new DB().CheckAdmin())
            //        {
            //            MultiViewMonthlyBulletin.ActiveViewIndex = 1;
            //        }
            //        else
            //        {
            //            MultiViewMonthlyBulletin.ActiveViewIndex = 0;
            //            Page.ClientScript.RegisterStartupScript(this.GetType(), "js", string.Format("<script type=\"text/javascript\">{0};</script>", "alert('没有权限！');"));
            //        }
            //    }
            //}
            //litAdminName.Text = ZXChurch.Admin.AdminManage.AdminName;
        }
        #endregion
    }
}
