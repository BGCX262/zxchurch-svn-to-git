using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXChurch.Common;
using System.Web.UI.WebControls;

namespace ZXChurch.Admin.User
{
    public class AdminUserSave:AdminBasePage
    {
        #region 页面变量
        private int intAdminId = 0;
        private string strAction = "";
        #endregion

        #region 页面控件
        private TextBox txtUserName;
        private TextBox txtTrueName;
        private TextBox txtPSWD;
        private DropDownList ddlUserType;
        private Button btSave;
        #endregion

        #region 页面事件
        protected void Page_Load(object sender, EventArgs e)
        {
            intAdminId = RequestHelper.RequestInt("id");
            strAction = RequestHelper.RequestString("ac");
            LoadWebControl();
            PageChack();

            if (!IsPostBack)
            {
                BindData();
            }

        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim().Length == 0) { Alert("请填写登陆名"); return; }
            if (txtTrueName.Text.Trim().Length == 0) { Alert("请填写真实名"); return; }
            if (intAdminId == 0)
            {
                if (txtPSWD.Text.Length == 0) { Alert("添加用户需要填写密码"); return; }
            }
            int userType = int.Parse(ddlUserType.SelectedValue);

            if (new DB().Save(intAdminId, txtUserName.Text.Trim(), txtTrueName.Text.Trim(), userType, txtPSWD.Text))
            {
                if (strAction != "")
                {
                    Response.Redirect("admin_user_list.aspx");
                }
                else
                {
                    ExecuteJs("parent.location.reload(true);");
                }
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
            ddlUserType.DataSource = AdminManage.GetAdminTypeList;
            ddlUserType.DataValueField = "key";
            ddlUserType.DataTextField = "value";
            ddlUserType.DataBind();

            List<string> values = new DB().GetAdminId(intAdminId);
            if (values.Count >= 4)
            {
                txtUserName.Text = values[1];
                txtTrueName.Text = values[2];
                ddlUserType.SelectedValue = values[3];
            }
        }

        /// <summary>
        /// 加载用户控件
        /// </summary>
        private void LoadWebControl()
        {
            txtUserName = (TextBox)this.FindControl("txtUserName"); 
            txtTrueName=(TextBox)this.FindControl("txtTrueName");
            txtPSWD=(TextBox)this.FindControl("txtPSWD");
            ddlUserType= (DropDownList)this.FindControl("ddlUserType");           
            btSave=(Button)this.FindControl("btSave");
            btSave.Click += new EventHandler(btSave_Click);

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
