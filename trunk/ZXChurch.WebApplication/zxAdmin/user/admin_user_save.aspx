<%@ Page Language="C#" AutoEventWireup="true"  Inherits="ZXChurch.Admin.User.AdminUserSave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户编辑</title>
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
           var  DialogObj = dialog('正在保存……', { title: '系统提示' });  
         }
     </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="OnSave()">
    <div class="welcomBox">
    <div class=" welcomCont listPadding">
    	<p class=" mb5 c_grey postion">您的当前位置：<a href="#">后台管理</a> > <a href="#">用户管理</a> > <a href="#">编辑用户</a></p>
       <div class="listTit">
        	<div class=" fl listTit_l"></div>
            <div class=" fl listTit_c"><h4 class="fl f14 fb">编辑用户</h4></div>
            <div class=" fr listTit_r"></div>
        </div>
        <div class="listCon pt10">        	
			<table width="100%" border="0" class="f14 tableForm">
              <tr>
                <td width="27%" class="tr pr10">登陆名:</td>
                <td>               
                <asp:TextBox runat="server" ID="txtUserName" CssClass="numIpt" />
                </td>
              </tr>
              <tr>
                <td class="tr pr10">真实名:</td>
                <td><asp:TextBox runat="server" ID="txtTrueName" CssClass="numIpt" /></td>
              </tr>
              <tr>
                <td class="tr pr10">用户类型:</td>
                <td>                	
                    <asp:DropDownList runat="server" ID="ddlUserType" CssClass="f13 registSel">
                    </asp:DropDownList>
                </td>
              </tr>
              <tr>
                <td class="tr pr10">密码:</td>
                <td class="c_blue2"><asp:TextBox runat="server" TextMode="Password" ID="txtPSWD" CssClass="numIpt" /></td>
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
