<%@ Page Language="C#" AutoEventWireup="true" Inherits="ZXChurch.Admin.AdminBasePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>后台登录</title>
<meta name="keywords" content="后台登录" />
<meta name="description" content="后台登录" />
<link href="../css/admin/styles/base.css" rel="stylesheet" type="text/css" />
<link href="../css/admin/styles/common.css" rel="stylesheet" type="text/css" />
<link href="../css/admin/styles/admin.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">        if (self != top) top.location = self.location;    </script>
</head>

<body id="login">
<div class="pr bc loginBox">
	<h1 class="pa f_ws">后台管理系统</h1>
    <div class="pr loginBox_user">
    	<h2 class="none">用户登录</h2>
        <div class="pa userForm">
             <form action="#" method="post" onsubmit="login();return false;">
                <p><label>用户名：</label><input type="text" name="username"  id="login_name" class="iptBox" /></p>
                <p><label>密&nbsp;&nbsp;码：</label><input type="password" name="userpsw"  id="login_pswd"  class="iptBox" /></p>
                <p>
                	<label>验证码：</label><input type="text" name="code" id="login_code" class="codeBox" onkeypress="if(event.keyCode==13){login();}" />
                    <img id="checkcode" src="checkcode.aspx" width="65" height="20" />
                    <a class="c_white" href="javascript:;" onclick="document.getElementById('checkcode').src='checkcode.aspx?t='+ Math.random();">不清楚，换一张</a>
                    <span class="cb f12 c_red" id="login_tip"></span> 
                </p>                
                <div><input type="button" id="btlogin" class="loginBtn"  onclick="login();"/></div>
            </form>
        </div>
    </div>
</div>
  <script language="javascript" type="text/javascript">
      function login() {        
          var uname = document.getElementById('login_name').value;
          var upswd = document.getElementById('login_pswd').value;
          var ucode = document.getElementById('login_code').value;
          if (uname != '' && upswd != '') {
              var btlogin = document.getElementById('btlogin');
              btlogin.disabled = "disabled";
              document.getElementById('login_pswd').value = '';
              var tempTitle = document.title;
              document.title = '正在登陆……';
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
                          window.location = 'default.aspx';
                      } else {
                          $('#login_tip').html(result.msg);
                          btlogin.disabled = "";
                          document.title = tempTitle;
                          document.getElementById('checkcode').src = 'checkcode.aspx?t='+Math.random();
                      }

                  }
              });
          } else {
              $('#login_tip').html('用户名、密码和验证码不能空！');
              return false;
          }
      }
        </script>
</body>
</html>
