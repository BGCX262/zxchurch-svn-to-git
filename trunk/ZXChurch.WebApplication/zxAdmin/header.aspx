<%@ Page Language="C#" AutoEventWireup="true" Inherits="ZXChurch.Admin.AdminBasePage" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>管理</title>
<meta name="keywords" content="管理" />
<meta name="description" content="管理" />
<link href="../css/admin/styles/base.css" rel="stylesheet" type="text/css" />
<link href="../css/admin/styles/common.css" rel="stylesheet" type="text/css" />
<link href="../css/admin/styles/admin.css" rel="stylesheet" type="text/css" />
</head>

<body>
<div class="w headBox">
	<h1 class="fl f_ws f22 c_white headLeft">中兴教会网站后台管理系统</h1>
    <p class="fr f12 tr c_white headRight">
    	<a class=" mr10 userCenter" href="#">用户中心</a><a class=" mr10 modifyPsw" href="user/admin_user_resetpwd.aspx" target="mainwin">修改密码</a><a class=" mr10 exitSys" href="logout.aspx">退出系统</a>
    </p>
    <p class="fr f12 mb10 c_white mt40 headCont"><a class=" mr8 homePage" href="#">首页</a>|<a class=" mr8 ml8 backIocn" href="#">后退</a>|<a class=" mr8 ml8 forwardIcon" href="#">前进</a>|<a class=" mr8 ml8 flashIcon" href="#">刷新</a>|<a class=" mr8 ml8 helpIcon" href="#">帮助</a></p>
</div>
<div class="w navBox">
	<ul class="fl f12 c_fblue" id="navBox">
    	<li class="fl tc nonce"><a href="leftmenu.aspx" target="leftwin">系统管理</a></li>
       <%-- <li class="fl tc "><a href="#">方法管理</a></li>--%>
        <li class="fl tc "><a href="leftusermenu.aspx" target="leftwin">用户管理</a></li>
    </ul>
    <p class="fr f12 pr5 c_fblue"><span class="adminIcon"></span><span>欢迎您：<strong class="fb"><%=ZXChurch.Admin.AdminManage.AdminName%></strong></span><span>角色：<em>管理员</em></span><span>今天是<%=DateTime.Now.ToString("yyyy年MM月dd日")%></span><span><%=DateTime.Now.DayOfWeek %></span></p>
</div>
<script language="javascript" type="text/javascript">
    (function () {
        var olist = document.getElementById('navBox').getElementsByTagName('a');
        for (var i = 0; i < olist.length; i++) {
            olist[i].onclick = function () {
                for (var k = 0; k < olist.length; k++)
                    olist[k].parentNode.className = 'fl tc';
                this.parentNode.className = 'fl tc nonce';
            };
        }
    })();
</script>
</body>
</html>

