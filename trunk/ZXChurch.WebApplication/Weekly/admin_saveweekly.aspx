<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false"  Inherits="ZXChurch.Weekly.WebWeeklySave" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>周报编辑</title>
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
      <script src="../../ueditor/editor_config.js" type="text/javascript"></script>
      <script src="../../ueditor/editor_all_min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="OnSave()">
    <div class="welcomBox">
    <div class=" welcomCont listPadding">
    	<p class=" mb5 c_grey postion">您的当前位置：<a href="#">周报管理</a> > <a href="#">周报编辑</a></p>
       <div class="listTit">
        	<div class=" fl listTit_l"></div>
            <div class=" fl listTit_c"><h4 class="fl f14 fb">周报编辑</h4></div>
            <div class=" fr listTit_r"></div>
        </div>
        <div class="listCon pt10">        	
			<table width="100%" border="0" class="f14 tableForm">
              <tr>
                <td width="27%" class="tr pr10">标题:</td>
                <td>               
                <asp:TextBox runat="server" ID="txtTitle" CssClass="nameIpt" />
                </td>
              </tr>
               <tr>
                <td  class="tr pr10">下载地址:</td>
                <td>               
                <asp:TextBox runat="server" ID="txtURL" CssClass="nameIpt" />
                </td>
              </tr>           
                <tr>
                <td colspan="2" align="center">
                <asp:TextBox runat="server" Width="98%" ID="txtMessage" TextMode="MultiLine"></asp:TextBox>
                <script type="text/javascript">
                    UE.getEditor('txtMessage', {
                        theme: "default", //皮肤
                        lang: 'zh-cn' //语言
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
