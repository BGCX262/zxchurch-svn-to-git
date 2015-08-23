<%@ Page Language="C#" AutoEventWireup="true"  Inherits="ZXChurch.Admin.AdminBasePage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>用户管理</title>
<meta name="keywords" content="用户管理" />
<meta name="description" content="用户管理" />
<link href="../css/admin/styles/base.css" rel="stylesheet" type="text/css" />
<link href="../css/admin/styles/common.css" rel="stylesheet" type="text/css" />
<link href="../css/admin/styles/admin.css" rel="stylesheet" type="text/css" />
<link type="text/css" rel="stylesheet" href="../css/admin/styles/xtree.css" />
    <script src="../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script type="text/javascript" src="script/menu.js"></script>
<script type="text/javascript" src="script/xtree.js"></script>
<script type="text/javascript" src="script/xmlextras.js"></script>
<script type="text/javascript" src="script/xloadtree.js"></script>
</head>

<body>
<div class="sideBar">
    <h2 class="sideTit"><span class="none">导航菜单</span></h2>
    <div class="sideCont">
        <div class="bc pt6 menuBox">
            <h3 class="pr f12 tc c_white tit"><a href="#">用户管理</a></h3>
            <div class="pb8 navMenu" style="display:none;">
            	<script type="text/javascript">
            	    /// XP Look
            	    webFXTreeConfig.rootIcon = "../css/admin/images/xp/folder.png";
            	    webFXTreeConfig.openRootIcon = "../css/admin/images/xp/openfolder.png";
            	    webFXTreeConfig.folderIcon = "../css/admin/images/xp/folder.png";
            	    webFXTreeConfig.openFolderIcon = "../css/admin/images/xp/openfolder.png";
            	    webFXTreeConfig.fileIcon = "../css/admin/images/xp/file.png";
            	    webFXTreeConfig.lMinusIcon = "../css/admin/images/xp/Lminus.png";
            	    webFXTreeConfig.lPlusIcon = "../css/admin/images/xp/Lplus.png";
            	    webFXTreeConfig.tMinusIcon = "../css/admin/images/xp/Tminus.png";
            	    webFXTreeConfig.tPlusIcon = "../css/admin/images/xp/Tplus.png";
            	    webFXTreeConfig.iIcon = "../css/admin/images/xp/I.png";
            	    webFXTreeConfig.lIcon = "../css/admin/images/xp/L.png";
            	    webFXTreeConfig.tIcon = "../css/admin/images/xp/T.png";
            	    //var tree = new WebFXLoadTree("WebFXLoadTree", "tree1.xml");
            	    //tree.setBehavior("classic");

            	    var rti;
            	    var tree = new WebFXTree("系统功能菜单");
                    <%if(ZXChurch.Admin.AdminManage.CheckSysAdmin){ %>
            	    tree.add(new WebFXLoadTreeItem("权限管理", "", "user/admin_user_competence.aspx", "mainwin"));
            	    tree.add(new WebFXLoadTreeItem("用户管理","", "user/admin_user_list.aspx","mainwin"));
            	    tree.add(new WebFXLoadTreeItem("新增用户","","user/admin_user_save.aspx?ac=add", "mainwin"));
                    <%} %>
            	    tree.add(new WebFXLoadTreeItem("修改密码", "", "user/admin_user_resetpwd.aspx", "mainwin"));
            	  
            	   
            	    //tree.add(new WebFXTreeItem("", "treexitong.xml"));

            	    document.write(tree);
					
					</script>
            </div>
        </div>       
    </div>
</div>
</body>
</html>

