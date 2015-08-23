using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace ZXChurch.Admin.User
{
    /// <summary>
    /// 用户权限管理
    /// </summary>
    public class AdminUserCompetence:AdminBasePage
    {
        #region 页面变量
        #endregion

        #region 页面控件
        //private MultiView MultiViewMonthlyBulletin;
        //private Repeater reptList;
        //private Literal litPager;
        //private Literal litAdminName;
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
            //int id = int.Parse(((LinkButton)sender).CommandArgument);
            //if (id > 0)
            //{
            //    if (new DB().DeleteAdmin(id))
            //    {
            //        //Alert("删除成功",);
            //        Response.Redirect(Request.Url.ToString());
            //    }
            //    else
            //    {
            //        Alert("删除失败");
            //    }
            //}
            //else
            //{
            //    Alert("参数错误，删除失败！");
            //}
        }

        #endregion

        #region 页面方法

        private void BindData()
        {
            //int count = 0;
            //int intPageSize = 20;
            //int intPageIndex = RequestHelper.RequestInt("page", 1);
            //if (intPageIndex <= 0) intPageIndex = 1;
            //reptList.DataSource = new DB().GetDataList(intPageIndex, intPageSize, out count).Tables[0];
            ////ID,UserName,TrueName,AddTime,UserType,PSWD, 
            //reptList.DataBind();
            //if (count > intPageSize)
            //{
            //    StringBuilder sb = new StringBuilder();

            //    int intPageCount = (count + intPageSize - 1) / intPageSize;
            //    /*
            //       <span class="totalPage">共<em class="c_red">100</em>条记录,每页<em>10</em>条</span>
            //     */
            //    sb.AppendFormat("<span class=\"totalPage\">共<em class=\"c_red\">{0}</em>条记录,每页<em>{1}</em>条</span>", count, intPageSize);
            //    if (intPageIndex > 1 && intPageCount < intPageIndex)
            //        intPageIndex = intPageCount;
            //    if (intPageCount > 1 && intPageCount < 10)
            //    {
            //        for (int i = 1; i < intPageCount; i++)
            //        {
            //            if (intPageIndex == i)
            //                sb.AppendFormat("<a  class=\"on\"  href=\"javascript:void(0);\">{0}</a>", i);
            //            else
            //                sb.AppendFormat("<a href=\"{1}?page={0}\"  class=\"numPage\" >{0}</a>", i, Request.Url.AbsolutePath);
            //        }
            //    }
            //    else if (intPageCount > 10)
            //    {
            //        if (intPageIndex < 10)
            //        {
            //            for (int i = 1; i < 10; i++)
            //            {
            //                if (intPageIndex == i)
            //                    sb.AppendFormat("<a  class=\"on\"  href=\"javascript:void(0);\">{0}</a>", i);
            //                else
            //                    sb.AppendFormat("<a  class=\"numPage\"  href=\"{1}?page={0}\">{0}</a>", i, Request.Url.AbsolutePath);
            //            }
            //        }
            //        else if (intPageIndex > 10)
            //        {
            //            for (int i = intPageIndex - 5; i < intPageIndex + 5; i++)
            //            {
            //                if (intPageIndex == i)
            //                    sb.AppendFormat("<a class=\"on\"  href=\"javascript:void(0);\">{0}</a>", i);
            //                else
            //                    sb.AppendFormat("<a  class=\"numPage\"  href=\"{1}?page={0}\">{0}</a>", i, Request.Url.AbsolutePath);
            //            }
            //        }
            //    }

            //    litPager.Text = sb.ToString();
            //}

        }

        /// <summary>
        /// 加载用户控件
        /// </summary>
        private void LoadWebControl()
        {
            //MultiViewMonthlyBulletin = (MultiView)this.FindControl("MultiViewMonthlyBulletin");
            //reptList = (Repeater)this.FindControl("reptList");
            //litPager = (Literal)this.FindControl("litPager");
            //litAdminName = (Literal)this.FindControl("litAdminName");

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
