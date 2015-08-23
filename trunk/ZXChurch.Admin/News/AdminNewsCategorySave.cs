using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXChurch.Common;
using System.Web.UI.WebControls;

namespace ZXChurch.Admin.News
{
    public class AdminNewsCategorySave:AdminBasePage
    { 
        #region 页面变量
        private int intNewTypeId = 0;
        private string strAction = "";
        #endregion

        #region 页面控件
        private TextBox txtCategoryName;
        private Button btSave;
        #endregion

        #region 页面事件
        protected void Page_Load(object sender, EventArgs e)
        {
            intNewTypeId = RequestHelper.RequestInt("id");
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
            if (txtCategoryName.Text.Trim().Length == 0) { Alert("请填写文章类别名称"); return; }
            if (new DBNews().SaveNewsType(intNewTypeId, txtCategoryName.Text.Trim()))
            {
                ExecuteJs("parent.CloseDialog();");
                return;
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
            List<string> values = new DBNews().GetNewsTypeId(intNewTypeId);
            if (values.Count >= 2)
            {
                txtCategoryName.Text = values[1];
            }
        }

        /// <summary>
        /// 加载用户控件
        /// </summary>
        private void LoadWebControl()
        {
            txtCategoryName = (TextBox)this.FindControl("txtUserName");
            btSave = (Button)this.FindControl("btSave");
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
