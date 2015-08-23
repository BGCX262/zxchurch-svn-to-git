<%@ Page Language="C#" AutoEventWireup="true"  Inherits="ZXChurch.Admin.User.AdminUserCompetenceSelect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择管理员</title>
    <link href="../../css/admin/styles/base.css" rel="stylesheet" type="text/css" />
    <link href="../../css/admin/styles/common.css" rel="stylesheet" type="text/css" />
    <link href="../../css/admin/styles/admin.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/jQueryDialog/dialog.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link href="../../Scripts/jQueryDialog/dialog.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jQueryDialog/html5.js" type="text/javascript"></script>
    <script src="../../Scripts/jQueryDialog/dialog.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/utils.js"></script>  
    <script language="javascript" type="text/javascript">
        function saveseluser() {
            var userList = document.getElementsByName('seladmin');
            var isCheck = false;
            for (var i = 0; i < userList.length; i++)
                if (userList[i].checked) {
                    //alert(UrlParm.parm('id'));
                    isCheck = true;
                    var DialogObj = dialog('正在执行操作……', { title: '系统提示' }); 
                    parent.InsertUser(UrlParm.parm('id'),userList[i].value); break;
                }

                if (!isCheck) alert('请选择管理员');
        }
    </script>
</head>
<body>
    <div class="listCon listCon_padding" >
        	<table width="100%" border="0" class="tableBox">
               <thead> 
                  <tr class="f12 tableTit">
                    <th width="20%" class="pl10 pr10 fb bl1ff br1ce"></th>
                    <th width="20%" class="tc fb br1ce">编号</th>
                    <th width="20%" class="tc fb br1ce">名称</th>
                    <th>真实名</th>                    
                  </tr>
               </thead>  
               <tbody id="tab">
                   <asp:Repeater ID="reptList" runat="server">
                    <ItemTemplate>
                  <tr class="f12">
                    <td class="pl10 pr10"><input type="radio" name="seladmin" value="userid=<%#Eval("id") %>&username=<%#Eval("UserName")%>" />                    
                    </td>
                    <td class="tc"><%#Eval("id") %></td>
                    <td class="tc"><%#Eval("UserName")%></td>
                    <td class="tc"><%#Eval("TrueName")%> </td>                    
                  </tr>
                  </ItemTemplate>
                </asp:Repeater>                                  
               </tbody> 
               <tfoot>       
              <tr>
                    <td colspan="4" class="btnone">
                        <div class="f12 tableOp">
                            <div class="fl pl10 findRow_left">                                                               
                                <input type="button" class="f12 deleteBtn" value="保存选择" onclick="saveseluser();"/>

                                <input type="button" class=" canelBtn" value="取消" onclick="parent.DialogObj.close();" />
                            </div>                                                
                        </div>
                    </td>
                  </tr>
              </tfoot>     
            </table>
      </div>
</body>
</html>
