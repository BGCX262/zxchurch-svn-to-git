<%@ Page Language="C#" AutoEventWireup="true" Inherits="ZXChurch.Admin.User.AdminUserList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户管理</title>
    <link href="../../css/admin/styles/base.css" rel="stylesheet" type="text/css" />
    <link href="../../css/admin/styles/common.css" rel="stylesheet" type="text/css" />
    <link href="../../css/admin/styles/admin.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/jQueryDialog/dialog.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link href="../../Scripts/jQueryDialog/dialog.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jQueryDialog/html5.js" type="text/javascript"></script>
    <script src="../../Scripts/jQueryDialog/dialog.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var DialogObj = null;
        function ShowSaveUserDialog(id) { //添加用户
            DialogObj = dialog({ type: 'iframe', value: 'admin_user_save.aspx?id='+id, height: '300px', width: '420px' }, { title: '添加用户' });

        }
        function CloseSaveUserDialog() {
            if (DialogObj != null) DialogObj.close();
        }
        function DeleteConfirm() {
            if (confirm('你确定要删除吗？')) {
                DialogObj = dialog('正在删除……', { title: '系统提示' });  
                return true;
            }
            else
                return false;
        }
    </script>
</head>

<body>
    <form id="form1" runat="server">
    <div class="welcomBox">
<div class=" welcomCont listPadding">
    	<p class=" mb5 c_grey postion">您的当前位置：<a href="#">用户管理中心</a>        
        </p>
		<h4 class="f14 listTits">        	
            <a class="js_on" href="#" id="Tab2" >用户管理中心</a>          
        </h4>        
        <div class="listCon listCon_padding" id="box2" style="display:block;">
        	<table width="100%" border="0" class="tableBox">
               <thead>
               	  <tr>
                    <td colspan="8" class="listTit_sub">                        
                        <h5 class="fl f14 fb">用户管理</h5>
                        <span class="f12 fb">
                        <a href="javascript:void(0);" onclick="ShowSaveUserDialog(0)">添加用户</a></span>              
                    </td>
                  </tr>  
                     
                  <tr class="f12 tableTit">
                    <th width="3.7"% class="pl10 pr10 fb bl1ff br1ce"><%--<input type="checkbox" name="" />--%></th>
                    <th width="8.3%" class="tc fb br1ce">编号</th>
                    <th width="10.3%" class="tc fb br1ce">名称</th>
                    <th class="10.3%">真实名</th>
                    <th class="tc fb br1ce">类型</th>
                    <th width="10.7%" class="tc fb br1ce">时间</th>                  
                    <th width="15.7%" class="tc fb br1ff">操作</th>
                  </tr>
               </thead>  
               <tbody id="tab">
                   <asp:Repeater ID="reptList" runat="server">
                    <ItemTemplate>
                  <tr class="f12 data_item">
                    <td class="pl10 pr10"></td>
                    <td class="tc"><%#Eval("id") %></td>
                    <td class="tc"><%#Eval("UserName")%></td>
                    <td class="tc"><%#Eval("TrueName")%> </td>
                    <td class="tc"><%#ZXChurch.Admin.AdminManage.GetAdminTypeList[int.Parse(Eval("UserType").ToString())]%></td>
                    <td class="tc"><%#Convert.ToDateTime(Eval("AddTime")).ToShortDateString()%></td>                    
                    <td class="tc pr10 c_blue2 br1ff"><a class="mr20 modifyIco"  href="javascript:void(0);" onclick="ShowSaveUserDialog(<%#Eval("id") %>)">修改</a><%--<a class="mr20 configIco" href="#">配置</a>--%>                    
                    <asp:LinkButton runat="server" CssClass="mr20 deleteIco" ID="btDelete" Text="删除" CommandArgument='<%#Eval("id") %>' OnClientClick="return DeleteConfirm();" OnClick="btDelete_Click" />
                    </td>
                  </tr>
                  </ItemTemplate>
                </asp:Repeater>                                  
               </tbody> 
               <tfoot>       
              
                  <tr>
                    <td colspan="8">
                        <div class="f12 tablePage">                    	
                            <div class="fr pr10 c_grey tableBottom_r">
                            <asp:Literal runat="server" ID="litPager" />                               
                            </div>
                        </div>
                    </td>
                  </tr>
              </tfoot>     
            </table>
      </div>          
    </div>
</div>
    </form>
</body>
</html>
