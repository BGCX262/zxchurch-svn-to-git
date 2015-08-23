<%@ Page Language="C#" AutoEventWireup="true" Inherits="ZXChurch.Admin.AdminBasePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<script type="text/javascript">  if (top != self) top.location = self.location; </script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>系统后台首页</title>
<meta name="keywords" content="系统后台首页" />
<meta name="description" content="系统后台首页" />   
<link href="../css/admin/styles/base.css" rel="stylesheet" type="text/css" />
<link href="../css/admin/styles/common.css" rel="stylesheet" type="text/css" />
<link href="../css/admin/styles/admin.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div id="wrap">
    <div id="main">
        <table width="100%" border="0">
          <tr>
            <td><iframe src="header.aspx" width="100%" height="103" frameborder="0" scrolling="no"></iframe></td>
          </tr>
        </table>
        <table width="100%" border="0">
          <tr>
            <td width="200" class="fixwidth"><iframe name="leftwin" src="leftmenu.aspx" width="206" height="720" frameborder="0" scrolling="no" allowTransparency="true"></iframe></td>
            <td class="vt">
                <table width="100%" border="0">
                  <tr>
                    <td><iframe name="mainwin" src="welcome.aspx" width="100%" height="720" frameborder="0" scrolling="auto" allowTransparency="true"></iframe></td>
                  </tr>
                </table>
            </td>
          </tr>
        </table>
    </div>
</div>
<div id="footer">
	<iframe src="footer.aspx" width="100%" height="30" frameborder="0" scrolling="no"></iframe>
</div>

</body>
</html>

