<%@ Page validateRequest="false" Language="C#" AutoEventWireup="true"  Inherits="ZXChurch.Admin.News.AdminNewsSave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>文章编辑</title>
    <link href="../../css/admin/styles/base.css" rel="stylesheet" type="text/css" />
    <link href="../../css/admin/styles/common.css" rel="stylesheet" type="text/css" />
    <link href="../../css/admin/styles/admin.css" rel="stylesheet" type="text/css" />
     <link href="../../Scripts/jQueryDialog/dialog.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link href="../../Scripts/jQueryDialog/dialog.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jQueryDialog/html5.js" type="text/javascript"></script>
    <script src="../../Scripts/jQueryDialog/dialog.js" type="text/javascript"></script>     
   
     <script language="javascript" type="text/javascript">
         function OnSave() {
             var DialogObj = dialog('正在保存……', { title: '系统提示' });
         }
         function setSetting() {
             var o = document.getElementById('tbothersetting');
             if (o.style.display == 'none') o.style.display = '';
             else o.style.display = 'none';
         }
     </script>
      <script src="../../ueditor/editor_config.js" type="text/javascript"></script>
      <script src="../../ueditor/editor_all.js" type="text/javascript"></script>
   
</head>
<body>
    <form id="form1" runat="server" onsubmit="OnSave()">
    <div class="welcomBox">
    <div class=" welcomCont listPadding">
    	<p class=" mb5 c_grey postion">您的当前位置：<a href="#">后台管理</a> > <a href="#">文章管理</a> > <a href="#">文章编辑</a></p>
       <div class="listTit">
        	<div class=" fl listTit_l"></div>
            <div class=" fl listTit_c"><h4 class="fl f14 fb">文章编辑</h4></div>
            <div class=" fr listTit_r"></div>
        </div>
        <div class="listCon pt10">        	
			<table width="100%" border="0" class="f14 tableForm">
              <tr>
                <td width="27%" class="tr pr10">标题:</td>
                <td>               
                <asp:TextBox runat="server" ID="txtNewsTitle" CssClass="nameIpt" />
                </td>
              </tr>
              <tr>
                <td class="tr pr10">分类:</td>
                <td>                	
                    <asp:DropDownList runat="server" ID="ddlNewsType" CssClass="f13 registSel">
                    </asp:DropDownList>
                </td>
              </tr>
              <tr>
              <td class="tr pr10">附加选项:</td>
                <td>  <a href="javascript:;" onclick="setSetting()">显示/隐藏更多设置</a>              	                   
                </td>
              </tr>
              <tbody style=" display:none;" id="tbothersetting">
                  <tr>
                    <td class="tr pr10">标题颜色:</td>
                    <td><asp:TextBox runat="server" ID="txtNewsTitleColor" CssClass="numIpt" /></td>
                  </tr>
                  <tr>
                    <td class="tr pr10">图片:</td>
                    <td><asp:TextBox runat="server" ID="txtNewsPicture" CssClass="numIpt" /></td>
                  </tr>
                  <tr>
                    <td class="tr pr10">来源网站:</td>
                    <td><asp:TextBox runat="server" ID="txtNewsFrom" CssClass="numIpt" /></td>
                  </tr>
                  <tr>
                    <td class="tr pr10">作者:</td>
                    <td><asp:TextBox runat="server" ID="txtNewsAuthor" CssClass="numIpt" /></td>
                  </tr>
                   <tr>
                    <td class="tr pr10">置顶:</td>
                    <td><asp:TextBox runat="server" ID="txtNewsTopWeight" CssClass="numIpt" /></td>
                  </tr>
                  <tr>
                    <td class="tr pr10">精华:</td>
                    <td><asp:TextBox runat="server" ID="txtNewsEilWeigth" CssClass="numIpt" /></td>
                  </tr>
                  <tr>
                    <td class="tr pr10">查看次数:</td>
                    <td><asp:TextBox runat="server" ID="txtNewsDisplayOrder" CssClass="numIpt" /></td>
                  </tr>
                  <tr>
                <td class="tr pr10">显示状态:</td>
                <td>                	
                    <asp:DropDownList runat="server" ID="DropDownListIsShow" CssClass="f13 registSel">
                    <asp:ListItem Text="显示" Value="1" />
                    <asp:ListItem Text="隐藏" Value="0" />
                    </asp:DropDownList>
                </td>
              </tr>  
              </tbody>
                <tr>
                <td colspan="2" align="center">
                <asp:TextBox runat="server" Width="800px" ID="txtNewsMessage" TextMode="MultiLine"></asp:TextBox>
                <script type="text/javascript">
                    UE.getEditor('txtNewsMessage', {
                        theme: "default", //皮肤
                        lang: 'zh-cn', //语言
                        initialFrameWidth:780
                    });
    </script>
                </td>
                </tr>         
              <tr>
                <td colspan="2" class="tc">               
                <asp:Button runat="server" ID="btSave" Text="确定" CssClass="mr50 okBtn" />
                <input type="button" class=" canelBtn" value="取消" onclick="parent.DialogObj.close();" /></td>
              </tr>                    
            </table>
         

      </div>
      
    </div>
</div>
    </form>
</body>
</html>

