using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace ZXChurch.Admin.User
{
    public class AdminUserResetpwd:AdminBasePage
    {
        #region 页面变量
        private int intAdminId = 0;
        private string strAction = "";
        #endregion

        #region 页面控件
        private TextBox txtOldPSWD;
        private TextBox txtNewPSWD;
        private TextBox txtPSWD;
        private Button btSave;
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
            if (txtOldPSWD.Text.Length == 0) { Alert("请输入原始密码！"); return; }
            if (txtNewPSWD.Text != txtPSWD.Text) { Alert("新密码输入不一致，请修改！"); return; }
            if (txtOldPSWD.Text == txtPSWD.Text) { Alert("新密码与原始密码相同，请修改！"); return; }
            
            if (new DB().ResetPswd(AdminManage.AdminName,txtOldPSWD.Text, txtPSWD.Text))
            {
                Alert("修改密码成功！", "../welcome.aspx");
            }
            else
            {
                Alert("修改密码失败！");
            }

        }
        #endregion

        #region 页面方法

        private void BindData()
        {
            
        }

        /// <summary>
        /// 加载用户控件
        /// </summary>
        private void LoadWebControl()
        {
            txtOldPSWD = (TextBox)this.FindControl("txtOldPSWD");
            txtNewPSWD = (TextBox)this.FindControl("txtNewPSWD");
            txtPSWD = (TextBox)this.FindControl("txtPSWD");
            btSave = (Button)this.FindControl("btSave");
            btSave.Click += new EventHandler(btSave_Click);

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
