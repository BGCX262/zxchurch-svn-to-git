﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Net;
using ZXChurch.Common;

namespace ZXChurch.Weekly
{
    public partial class WebAdmin : System.Web.UI.Page
    {
        #region 页面变量
        #endregion

        #region 页面控件
        private MultiView MultiViewMonthlyBulletin;  
        private Repeater reptList;
        private Literal litPager;
        private Literal litAdminName;
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
            int count=0;
            int intPageSize = 20;
            int intPageIndex = RequestHelper.RequestInt("page",1);
            if (intPageIndex <= 0) intPageIndex = 1;
            reptList.DataSource = new DB().GetDataList(intPageIndex, intPageSize, out count);
            reptList.DataBind();
            if (count > intPageSize)
            {
                StringBuilder sb = new StringBuilder();
               
                int intPageCount = (count + intPageSize - 1) / intPageSize;
                /*
                   <span class="totalPage">共<em class="c_red">100</em>条记录,每页<em>10</em>条</span>
                 */
                sb.AppendFormat("<span class=\"totalPage\">共<em class=\"c_red\">{0}</em>条记录,每页<em>{1}</em>条</span>",count,intPageSize);
                if (intPageIndex > 1 && intPageCount < intPageIndex) 
                    intPageIndex = intPageCount;
                if (intPageCount>1 && intPageCount < 10)
                {
                    for (int i = 1; i < intPageCount; i++)
                    {
                        if (intPageIndex == i)
                            sb.AppendFormat("<a  class=\"on\"  href=\"javascript:void(0);\">{0}</a>", i);
                        else
                            sb.AppendFormat("<a href=\"{1}?page={0}\"  class=\"numPage\" >{0}</a>", i, Request.Url.AbsolutePath); 
                    }
                }
                else if (intPageCount > 10)
                {
                    if (intPageIndex < 10)
                    {
                        for (int i = 1; i < 10; i++)
                        {
                            if (intPageIndex == i)
                                sb.AppendFormat("<a  class=\"on\"  href=\"javascript:void(0);\">{0}</a>", i);
                            else
                                sb.AppendFormat("<a  class=\"numPage\"  href=\"{1}?page={0}\">{0}</a>", i, Request.Url.AbsolutePath);
                        }
                    }
                    else if (intPageIndex > 10)
                    {
                        for (int i = intPageIndex - 5; i < intPageIndex+5; i++)
                        {
                            if (intPageIndex == i)
                                sb.AppendFormat("<a class=\"on\"  href=\"javascript:void(0);\">{0}</a>", i);
                            else
                                sb.AppendFormat("<a  class=\"numPage\"  href=\"{1}?page={0}\">{0}</a>", i, Request.Url.AbsolutePath);
                        }
                    }
                }
               
                litPager.Text = sb.ToString();
            }
            
        }

        /// <summary>
        /// 加载用户控件
        /// </summary>
        private void LoadWebControl()
        {
            MultiViewMonthlyBulletin = (MultiView)this.FindControl("MultiViewMonthlyBulletin");
            reptList = (Repeater)this.FindControl("reptList");
            litPager = (Literal)this.FindControl("litPager");
            litAdminName = (Literal)this.FindControl("litAdminName");

        }
        /// <summary>
        /// 身份权限判断
        /// </summary>
        private void PageChack()
        {
            if (Session[ZXChurch.Admin.SessionKeys.WeeklyAdminSession] != null)
            {
                MultiViewMonthlyBulletin.ActiveViewIndex = 1;
            }
            else
            {
                if (ZXChurch.Admin.AdminManage.AdminID > 0)
                {
                    if (new DB().CheckAdmin())
                    {
                        MultiViewMonthlyBulletin.ActiveViewIndex = 1;
                    }
                    else
                    {
                        MultiViewMonthlyBulletin.ActiveViewIndex = 0;
                        Common.PageHelper.Alert(this.Page, "没有权限");
                        return;
                    }
                }
            }
            litAdminName.Text = ZXChurch.Admin.AdminManage.AdminName;
        }
        #endregion
    }
}