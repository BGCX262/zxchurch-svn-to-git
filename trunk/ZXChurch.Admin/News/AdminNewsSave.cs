using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZXChurch.Common;
using System.Web.UI.WebControls;
using System.IO;

namespace ZXChurch.Admin.News
{
    public class AdminNewsSave:AdminBasePage
    { 
        #region 页面变量
        private int intNewsId = 0;
        private string strAction = "";
        #endregion

        #region 页面控件
        private TextBox txtNewsTitle;
        private DropDownList ddlNewsType;
        private DropDownList DropDownListIsShow;
        private TextBox txtNewsTitleColor;
        private TextBox txtNewsPicture;
        private TextBox txtNewsFrom;
        private TextBox txtNewsAuthor;
        private TextBox txtNewsTopWeight;
        private TextBox txtNewsEilWeigth;
        private TextBox txtNewsDisplayOrder;
        private TextBox txtNewsMessage;
        private Button btSave;
        #endregion

        #region 页面事件
        protected void Page_Load(object sender, EventArgs e)
        {
            intNewsId = RequestHelper.RequestInt("id");
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
            if (txtNewsTitle.Text.Trim().Length == 0) { Alert("请填写文章标题"); return; }
            int newsType = int.Parse(ddlNewsType.SelectedValue);
            int isShow = int.Parse(DropDownListIsShow.SelectedValue);
            if (new DBNews().NewsSave(intNewsId, newsType, isShow, txtNewsTitle.Text.Trim(), txtNewsTitleColor.Text.Trim(), txtNewsPicture.Text.Trim(), txtNewsFrom.Text.Trim(), txtNewsAuthor.Text.Trim(), TypeHelper.ToInt(txtNewsTopWeight.Text.Trim()), TypeHelper.ToInt(txtNewsEilWeigth.Text.Trim()), TypeHelper.ToInt(txtNewsDisplayOrder.Text.Trim()), txtNewsMessage.Text.Trim()))
            {

                if (isShow == 1)
                {
                    #region 文件生成
                    string tempPath = Server.MapPath("templates/view.html");
                    StringBuilder sb = new StringBuilder(1000);
                    if (File.Exists(tempPath))
                        sb.Append(File.ReadAllText(tempPath, Encoding.UTF8));
                    if (intNewsId == 0) { intNewsId = new DBNews().GetNewsMaxId(); }
                    if (intNewsId > 0)
                    {
                        Dictionary<string, string> dic = new DBNews().GetNewsById(intNewsId);
                        foreach (string key in dic.Keys)
                            sb.Replace("{" + key + "}", dic[key]);
                        string FileDir = Server.MapPath("/news/");
                        try
                        {
                            File.WriteAllText(FileDir + "/" + intNewsId + ".html", sb.ToString());
                        }
                        catch
                        {

                        }
                    }
                    #endregion
                }
                else if(intNewsId>0)
                {
                    #region 删除原来的文件
                    string FileDir = Server.MapPath("/news/");
                    try
                    {
                        File.Delete(FileDir + "/" + intNewsId + ".html");
                    }
                    catch { }
                    #endregion
                }

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
            ddlNewsType.DataSource = new DBNews().GetDataListByNewsType();
            ddlNewsType.DataValueField = "ID";
            ddlNewsType.DataTextField = "TypeName";
            ddlNewsType.DataBind();
            List<string> values = new DBNews().GetNewsId(intNewsId);
            Console.WriteLine(values.Count);
            if (values.Count >=13)
            {
                ddlNewsType.Text = values[0];
                DropDownListIsShow.Text = values[1];
                txtNewsTitle.Text = values[2];
                txtNewsTitleColor.Text = values[3];
                txtNewsPicture.Text = values[4];
                txtNewsFrom.Text = values[5];
                txtNewsAuthor.Text = values[6];
                txtNewsTopWeight.Text = values[7];
                txtNewsEilWeigth.Text = values[8];
                txtNewsDisplayOrder.Text = values[9];
                txtNewsMessage.Text = values[10];
            }
        }

        /// <summary>
        /// 加载用户控件
        /// </summary>
        private void LoadWebControl()
        {
            txtNewsTitle = (TextBox)this.FindControl("txtNewsTitle");
            txtNewsTitleColor = (TextBox)this.FindControl("txtNewsTitleColor");
            txtNewsPicture = (TextBox)this.FindControl("txtNewsPicture");
            txtNewsFrom = (TextBox)this.FindControl("txtNewsFrom");
            txtNewsAuthor = (TextBox)this.FindControl("txtNewsAuthor");
            txtNewsTopWeight = (TextBox)this.FindControl("txtNewsTopWeight");
            txtNewsEilWeigth = (TextBox)this.FindControl("txtNewsEilWeigth");
            txtNewsDisplayOrder = (TextBox)this.FindControl("txtNewsDisplayOrder");
            txtNewsMessage = (TextBox)this.FindControl("txtNewsMessage");
            ddlNewsType = (DropDownList)this.FindControl("ddlNewsType");
            DropDownListIsShow = (DropDownList)this.FindControl("DropDownListIsShow");
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
