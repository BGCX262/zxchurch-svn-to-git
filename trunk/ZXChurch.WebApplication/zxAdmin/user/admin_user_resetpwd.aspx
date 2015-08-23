<%@ Page Language="C#" AutoEventWireup="true" Inherits="ZXChurch.Admin.User.AdminUserResetpwd" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>修改密码</title>
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
     </script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="OnSave()">
    <div class="welcomBox">
    <div class=" welcomCont listPadding">
    	<p class=" mb5 c_grey postion">您的当前位置：<a href="#">后台管理</a> > <a href="#">用户管理</a> > <a href="#">修改密码</a></p>
       <div class="listTit">
        	<div class=" fl listTit_l"></div>
            <div class=" fl listTit_c"><h4 class="fl f14 fb">修改密码</h4></div>
            <div class=" fr listTit_r"></div>
        </div>
        <div class="listCon pt10">        	
			<table width="100%" border="0" class="f14 tableForm">
              <tr>
                <td width="27%" class="tr pr10">原始密码:</td>
                <td>               
                <asp:TextBox runat="server" TextMode="Password" ID="txtOldPSWD" CssClass="numIpt" />
                </td>
              </tr>
              <tr>
                <td class="tr pr10">新密码:</td>
                <td><asp:TextBox runat="server" TextMode="Password" ID="txtNewPSWD" CssClass="numIpt" /></td>
              </tr>              
              <tr>
                <td class="tr pr10">重复密码:</td>
                <td class="c_blue2"><asp:TextBox runat="server" TextMode="Password" ID="txtPSWD" CssClass="numIpt" /></td>
              </tr> 
              <tr>
                <td colspan="2" class="tc">               
                <asp:Button runat="server" ID="btSave" Text="确定" CssClass="mr50 okBtn" />
                <input type="button" class=" canelBtn" value="取消" onclick="window.location='../welcome.aspx';" /></td>
              </tr>                    
            </table>

      </div>
      
    </div>
</div>
    </form>
</body>
</html>
