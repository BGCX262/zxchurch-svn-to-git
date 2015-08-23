using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZXChurch.Admin.News
{
    /// <summary>
    /// 
    /// </summary>
    public class AdminNewsCategory:AdminBasePage
    {
        #region 页面变量
        private int intAdminId = 0;
        private string strAction = "";
        #endregion

        #region 页面控件
        //private TextBox txtUserName;
        //private TextBox txtTrueName;
        //private TextBox txtPSWD;
        //private DropDownList ddlUserType;
        //private Button btSave;
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
           

        }
        #endregion

        #region 页面方法

        private void BindData()
        {
            //ddlUserType.DataSource = AdminManage.GetAdminTypeList;
            //ddlUserType.DataValueField = "key";
            //ddlUserType.DataTextField = "value";
            //ddlUserType.DataBind();

            //List<string> values = new DB().GetAdminId(intAdminId);
            //if (values.Count >= 4)
            //{
            //    txtUserName.Text = values[1];
            //    txtTrueName.Text = values[2];
            //    ddlUserType.SelectedValue = values[3];
            //}
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
