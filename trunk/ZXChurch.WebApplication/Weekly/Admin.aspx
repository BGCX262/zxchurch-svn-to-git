<%@ Page Language="C#" AutoEventWireup="true"  Inherits="ZXChurch.Weekly.WebAdmin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>周报管理</title>
    <link href="../css/admin/styles/base.css" rel="stylesheet" type="text/css" />
    <link href="../css/admin/styles/common.css" rel="stylesheet" type="text/css" />
    <link href="../css/admin/styles/admin.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/jQueryDialog/dialog.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jQueryDialog/dialog.js" type="text/javascript"></script>
    <script src="../Scripts/jQueryDialog/html5.js" type="text/javascript"></script>
    
</head>
<body>
    
    <div>
    <asp:MultiView ID="MultiViewMonthlyBulletin" runat="server" ActiveViewIndex="0">
    <asp:View runat="server" ID="LoginView" >
    <div id="login">
    <div class="pr bc loginBox">
	<h1 class="pa f_ws">周报后台管理</h1>
    <div class="pr loginBox_user">
    	<h2 class="none">用户登陆</h2>
        <div class="pa userForm">
            <form action="#" method="post" onsubmit="login();return false;">
                <p><label>用户名：</label><input type="text" name="username"  id="login_name" class="iptBox" /></p>
                <p><label>密&nbsp;&nbsp;码：</label><input type="password" name="userpsw"  id="login_pswd"  class="iptBox" /></p>
                <p>
                	<label>验证码：</label><input type="text" name="code" id="login_code" class="codeBox" onkeypress="if(event.keyCode==13){login();}" />
                    <img id="checkcode" src="WebHandler.ashx?action=CheckCode" width="65" height="20" />
                    <a class="c_white" href="javascript:;" onclick="document.getElementById('checkcode').src='WebHandler.ashx?action=CheckCode&t='+ Math.random();">不清楚，换一张</a>
                    <span class="cb f12 c_red" id="login_tip"></span> 
                </p>                
                <div><input type="button" id="btlogin" class="loginBtn"  onclick="login();"/></div>
            </form>
        </div>
    </div>
</div>
    </div>       
        <script language="javascript" type="text/javascript">
            function login() {
                var DialogObj = null;
                var uname = document.getElementById('login_name').value;
                var upswd = document.getElementById('login_pswd').value;
                var ucode = document.getElementById('login_code').value;
                if (uname != '' && upswd != '') {
                    var btlogin = document.getElementById('btlogin');
                    btlogin.disabled = "disabled";
                    document.getElementById('login_pswd').value = '';
                    DialogObj = dialog('正在登陆……', { title: '系统提示' });                  
                    $.ajax({ type: "POST",
                        url: '../zxcommon/AdminHandler.ashx', data: 'Action=login&username=' + uname + '&pwd=' + upswd + '&checkcode=' + ucode,
                        async: true,
                        error: function () {
                            alert('系统出显错误');
                            btlogin.disabled = "";
                            btlogin.value = " ";
                        },
                        success: function (msg) {
                            var result = eval("result=" + msg);
                            if (result.result == 'ok') {
                                window.location='admin.aspx';
                            } else {
                                $('#login_tip').html(result.msg);
                                btlogin.disabled = "";
                                DialogObj.close();
                                document.getElementById('checkcode').src = 'WebHandler.ashx?action=CheckCode';
                            }

                        }
                    });
                } else {
                    $('#login_tip').html('用户名、密码和验证码不能空！');
                return false;
                }
            }
        </script>
    
    </asp:View>
    <asp:View runat="server"  ID="ManageView">
    
    <textarea id="SaveHTML" style=" display:none;">
    
    <table style="width:270px">
        <tr>
        <td>标题</td>
        <td>
        <input type="hidden" id="save_id" />
        <input type="text" id="save_title" /> </td>
        </tr>
        <tr>
        <td>下载地址</td>
        <td><input type="text" id="save_url" /></td>
        </tr>
        <tr>
        <td colspan="2" align="center">
        <input type="button" id="bt_save" value="保 存"  onclick="SaveMsg()" />&nbsp;&nbsp;&nbsp;
        <input type="button"  value="关 闭"  onclick="CloseSaveDialog()" />
        </td>
        <td></td>
        </tr>
    </table>
    
    </textarea>
  
    <script language="javascript" type="text/javascript">
        var DialogObj=null;
        function ShowSaveDialog() {
            DialogObj = dialog($('#SaveHTML').val(), { title: '保存月报' });
           
        }
        function ShowEditDialog(id) {
            DialogObj = dialog($('#SaveHTML').val(), { title: '编辑月报' });
            $('#save_title').val($('#' + id + '_title').val());
            $('#save_url').val($('#'+id+'_url').val());
            $('#save_id').val(id);
        }
        function DeleteMsg(id) {
            if (confirm('你确定要删除这个信息吗？')) {
                DialogObj = dialog('正在执行删除……', { title: '操作提示' });
                $.ajax({ type: "POST",
                    url: 'WebHandler.ashx', data: 'Action=delete&id=' + id,
                    async: true,
                    error: function () {
                        alert('系统出显错误');
                    },
                    success: function (msg) {
                        var result = eval("result=" + msg);
                        if (result.result == 'ok') {
                            DialogObj = dialog('删除成功', { title: '操作提示' });                         
                            window.location = window.location;

                        } else {                          
                            DialogObj.close();
                        }

                    }
                });
            }
        }
        function CloseSaveDialog() {
            DialogObj.close();
        }
        function SaveMsg() {
                       
            var title = $('#save_title').val();
            var url = $('#save_url').val();
            var id = $('#save_id').val();
            var bt = $('#bt_save');

            bt.val('正在保存');
            bt.attr('disabled', 'disabled');
            $.ajax({ type: "POST",
                url: 'WebHandler.ashx', data: 'Action=save&id=' + id + '&downloadurl=' + encodeURI(url) + '&title=' + title,
                async: true,
                error: function () {                   
                    DialogObj = dialog('系统出显错误', { title: '操作提示',time:1500 });   
                    bt.attr('disabled', '');
                    bt.val("保 存");
                },
                success: function (msg) {
                    var result = eval("result=" + msg);
                    if (result.result == 'ok') {
                        DialogObj.close();
                        DialogObj = dialog('保存成功', { title: '操作提示', time: 1500 });   

                    } else {
                        //alert(result.msg);
                        DialogObj = dialog(result.msg, { title: '操作提示', time: 1500 });  
                        bt.attr('disabled', '');
                        bt.val("保 存");
                    }

                }
            });

        }
        function Logout() {
            if (confirm('你确定要退出吗？')) {
                window.location = 'WebHandler.ashx?action=logout';
            }
        }

    </script>
     
      <div class="welcomBox">
<div class=" welcomCont listPadding">
    	<p class=" mb5 c_grey postion">您的当前位置：<a href="#">周报管理中心</a>
        <span  style=" float:right;">你好，<asp:Literal runat="server" ID="litAdminName" />
        <a href="javascript:void(0);" onclick="Logout()">退出</a></span>
        </p>
		<h4 class="f14 listTits">        	
            <a class="js_on" href="#" id="Tab2" >周报管理中心</a>          
        </h4>        
        <div class="listCon listCon_padding" id="box2" style="display:block;">
        	<table width="100%" border="0" class="tableBox">
               <thead>
               	  <tr>
                    <td colspan="8" class="listTit_sub">                        
                        <h5 class="fl f14 fb">周报管理</h5><span class="f12 fb"><a href="admin_saveweekly.aspx" >添加信息</a></span>              
                    </td>
                  </tr>  
                     
                  <tr class="f12 tableTit">
                    <th width="3.7"% class="pl10 pr10 fb bl1ff br1ce"><%--<input type="checkbox" name="" />--%></th>
                    <th width="8.3%" class="tc fb br1ce">编号</th>
                    <th width="20.3%" class="tc fb br1ce">名称</th>
                    <th class="tc fb br1ce">下载地址</th>
                    <th width="10.7%" class="tc fb br1ce">时间</th>                  
                    <th width="15.7%" class="tc fb br1ff">操作</th>
                  </tr>
               </thead>  
               <tbody id="tab">
                   <asp:Repeater ID="reptList" runat="server">
                    <ItemTemplate>
                  <tr class="f12">
                    <td class="pl10 pr10"></td>
                    <td class="tc"><%#Eval("id") %></td>
                    <td class="tc"><%#Eval("Title")%>
                      <input type="hidden" id="<%#Eval("id") %>_title"  value="<%#Eval("Title")%>"/>
                      <input type="hidden" id="<%#Eval("id") %>_url"  value="<%#Eval("URL")%>"/></td>
                    <td class="tc"><a href=" <%#Eval("URL")%>" target="_blank"> <%#Eval("URL")%></a></td>
                    <td class="tc"><%#Eval("AddTime")%></td>                    
                    <td class="tc pr10 c_blue2 br1ff"><a class="mr20 modifyIco"  href="admin_saveweekly.aspx?id=<%#Eval("id") %>" >修改</a><%--<a class="mr20 configIco" href="#">配置</a>--%><a class="mr20 deleteIco" href="javascript:void(0);" onclick="DeleteMsg(<%#Eval("id") %>)">删除</a></td>
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
    </asp:View>
    </asp:MultiView>
    </div>
    
</body>
</html>
