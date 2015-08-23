using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXChurch.Common;
using System.Data;
using System.Web.UI.WebControls;

namespace ZXChurch.Admin.News
{
    public class AdminNewsList:AdminBasePage
    {
        #region 页面变量
        private string strAction = "";
        public int type=RequestHelper.RequestInt("type", 1);
        public int i = 2;
        private int cid = RequestHelper.RequestInt("ddlNewsType",0);
        private string searchkey = RequestHelper.RequestString("txtSearchKey").Replace("'", "");
        #endregion

        #region 页面控件
        private Repeater reptListType;
        private Repeater reptListNews;
        private Literal litPagerNews;
        private Literal litPagerType;
        private Literal litAdminName;
        private DropDownList ddlNewsType;
        private TextBox txtSearchKey;
        private Button btSearch;
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

        protected void btSearch_Click(object sender, EventArgs e)
        {
            LoadNewsList(1);

        }
        protected void btDeleteType_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((LinkButton)sender).CommandArgument);
            if (id > 0)
            {
                if (new DBNews().DeleteNewsType(id))
                {
                    Alert("删除成功");
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
        protected void btDeleteNews_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((LinkButton)sender).CommandArgument);
            if (id > 0)
            {
                if (new DBNews().DeleteNews(id))
                {
                    Alert("删除成功");
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
            if (type == 1)
            {
                ddlNewsType.DataSource = new DBNews().GetDataListByNewsType();
                ddlNewsType.DataTextField = "TypeName";
                ddlNewsType.DataValueField = "id";                
                ddlNewsType.DataBind();
                ddlNewsType.Items.Insert(0, new ListItem("全部", "0"));

                LoadNewsList(RequestHelper.RequestInt("page", 1));

            }
            else if (type == 2)
            { 
              LoadTypeList();
            }
           
        }
        private void LoadNewsList(int p) 
        {
            ddlNewsType.SelectedValue = cid.ToString();
            txtSearchKey.Text = searchkey;

            string strURL ="";
            if (cid > 0) { strURL = "&ddlNewsType=" + cid; }
            if (searchkey != "") { strURL += "&txtSearchKey=" + searchkey; }
            int count = 0;
            int intPageSize = 25;
            int intPageIndex = p;
            if (intPageIndex <= 0) intPageIndex = 1;
            reptListNews.DataSource = new DBNews().GetDataListByNews(intPageIndex, intPageSize, cid,searchkey, out count);
            reptListNews.DataBind();
            if (count > intPageSize)
            {
                StringBuilder sb = new StringBuilder();

                int intPageCount = (count + intPageSize - 1) / intPageSize;
                /*
                   <span class="totalPage">共<em class="c_red">100</em>条记录,每页<em>10</em>条</span>
                 */
                sb.AppendFormat("<span class=\"totalPage\">共<em class=\"c_red\">{0}</em>条记录,每页<em>{1}</em>条</span>", count, intPageSize);
                if (intPageIndex > 1 && intPageCount < intPageIndex)
                    intPageIndex = intPageCount;
                if (intPageCount > 1 && intPageCount < 10)
                {
                    for (int i = 1; i < intPageCount+1; i++)
                    {
                        if (intPageIndex == i)
                            sb.AppendFormat("<a  class=\"on\"  href=\"javascript:void(0);\">{0}</a>", i);
                        else
                            sb.AppendFormat("<a href=\"{1}?page={0}&type=1{2}\"  class=\"numPage\" >{0}</a>", i, Request.Url.AbsolutePath, strURL);
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
                                sb.AppendFormat("<a  class=\"numPage\"  href=\"{1}?page={0}&type=1{2}\">{0}</a>", i, Request.Url.AbsolutePath, strURL);
                        }
                    }
                    else if (intPageIndex > 10)
                    {
                        for (int i = intPageIndex - 5; i < intPageIndex + 5; i++)
                        {
                            if (intPageIndex == i)
                                sb.AppendFormat("<a class=\"on\"  href=\"javascript:void(0);\">{0}</a>", i);
                            else
                                sb.AppendFormat("<a  class=\"numPage\"  href=\"{1}?page={0}&type=1{2}\">{0}</a>", i, Request.Url.AbsolutePath, strURL);
                        }
                    }
                }
                litPagerNews.Text = sb.ToString();
            }
        }
        private void LoadTypeList()
        {

            reptListType.DataSource = new DBNews().GetDataListByNewsType(); 
            reptListType.DataBind();
            
        }
        public string BindNewTypeName(int intNewTypeId)
        {
            string s = "";
            List<string> values = new DBNews().GetNewsTypeId(intNewTypeId);
            if (values.Count >= 2)
            {
               s = values[1];
            }
            else
            {
                s ="暂无";
            }
            return s;
        }
        public string BindIsShow(int intIsShow)
        {
            string s = "";
            if (intIsShow == 1)
            {
                s = "显示";
            }
            else
            {
                s = "隐藏";
            }
            return s;
        }

        /// <summary>
        /// 加载用户控件
        /// </summary>
        private void LoadWebControl()
        {
            reptListType = (Repeater)this.FindControl("reptListType");
            reptListNews = (Repeater)this.FindControl("reptListNews");
            litPagerNews = (Literal)this.FindControl("litPagerNews");
            litPagerType = (Literal)this.FindControl("litPagerType");
            litAdminName = (Literal)this.FindControl("litAdminName");
            ddlNewsType = (DropDownList)this.FindControl("ddlNewsType");
            txtSearchKey = (TextBox)this.FindControl("txtSearchKey");
            btSearch = (Button)this.FindControl("btSearch");
            btSearch.Click += new EventHandler(btSearch_Click);
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
