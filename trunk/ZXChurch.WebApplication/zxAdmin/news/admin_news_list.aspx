<%@ Page Language="C#" AutoEventWireup="true"  Inherits="ZXChurch.Admin.News.AdminNewsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>文章管理</title>
    <link href="../../css/admin/styles/base.css" rel="stylesheet" type="text/css" />
    <link href="../../css/admin/styles/common.css" rel="stylesheet" type="text/css" />
    <link href="../../css/admin/styles/admin.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/jQueryDialog/dialog.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link href="../../Scripts/jQueryDialog/dialog.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jQueryDialog/html5.js" type="text/javascript"></script>
    <script src="../../Scripts/jQueryDialog/dialog.js" type="text/javascript"></script>
    <script src="../script/admin_news_list.js" type="text/javascript"></script>
</head>
<body onload="ShowTab(<%=type%>)">
 <form id="form1" runat="server">
    <div class="welcomBox">
<div class=" welcomCont listPadding">
    	<p class=" mb5 c_grey postion">您的当前位置：<a href="#">后台管理</a> > <a href="">文章管理</a></p>
		<h4 class="f14 listTits" id="listTits_tab">
        	<a class="js_on" href="?type=1" id="Tab1" >文章管理</a>
            <a href="?type=2" id="Tab2" onclick="ShowTab(2)">分类管理</a>
        </h4>
        <div id="box1" class="listCon listCon_padding">
        <table width="100%" border="0" class="tableBox" id="Table1">
               <thead>
               	  <tr>
                    <td colspan="8" class="listTit_sub">                        
                        <h5 class="fl f14 fb">文章管理</h5><span class="f12 fb"><a href="javascript:;" onclick="ShowSaveNews(0);">新增文章</a></span>              
                    </td>
                  </tr>  
                  <tr>
                    <td colspan="8" class="findRow">
                        <div class="fl pl10 findRow_left">
                            <input type="checkbox" name="" value="全选/取消" /><span class="f12 ml5">全选/取消</span>
                            <select class=" f13 deleteSel"><option>批量删除</option></select>
                             
                            <input type="button" class="f12 deleteBtn" value="批量操作" />
                        </div>
                        <div class="fr pr10 findRow_right">                            
                            <asp:DropDownList runat="server" ID="ddlNewsType"  CssClass="f13 normalSel"></asp:DropDownList>                            
                            <asp:TextBox runat="server" ID="txtSearchKey" CssClass="f12 searchTxt"/>                            
                            <asp:Button runat="server" ID="btSearch" CssClass="findBtn" Text="查 询" />
                        </div>
                    </td>
                  </tr>               
                  <tr class="f12 tableTit">
                    <th width="3.7"% class="pl10 pr10 fb bl1ff br1ce"><input type="checkbox" name="" /></th>
                    <th width="8.3%" class="tc fb br1ce">编号</th>
                    <th width="8.3%" class="tc fb br1ce">分类</th>
                    <th width="25.4%" class="tc fb br1ce">文章名称</th>
                    <th width="10.7%" class="tc fb br1ce">查看次数</th>
                    <th width="10.6%" class="tc fb br1ce">添加时间</th>
                    <th width="8%" class="tc fb br1ce">显示状态</th>
                    <th class="tc fb br1ff">操作</th>
                  </tr>
               </thead>  
               <tbody id="Tbody1">                 
                  <asp:Repeater ID="reptListNews" runat="server">
                   <ItemTemplate>
                  <tr class="f12 data_item">
                    <td class="pl10 pr10"><input type="checkbox" name="" /></td>
                    <td class="tc"><%#Eval("ID") %></td>
                    <td class="tc"><%#ddlNewsType.Items.FindByValue(Eval("CID").ToString())%></td>
                    <td class="tc"><%#Eval("title")%></td>
                    <td class="tc"><%#Eval("DisplayOrder")%></td>
                    <td class="tc"><%#Convert.ToDateTime(Eval("AddTime")).ToShortDateString()%></td> 
                    <td class="tc"><%#BindIsShow(int.Parse(Eval("IsShow").ToString()))%></td>  
                    <td class="tc pr10 c_blue2 br1ff"><a class="mr20 modifyIco"  href="javascript:;" onclick="ShowSaveNews(<%#Eval("id")%>);">修改</a><a name="aa" class="mr20 configIco" href="javascript:;" onclick="EditNewsStatus(1,<%=++i%>,'id=<%#Eval("id")%>&IsShow=<%#Eval("IsShow")%>')">配置</a>                    
                     <asp:LinkButton runat="server" CssClass="mr20 deleteIco" ID="btDelete" Text="删除" CommandArgument='<%#Eval("id") %>' OnClientClick="return DeleteConfirm();" OnClick="btDeleteNews_Click" />
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
                            <asp:Literal runat="server" ID="litPagerNews" />                               
                            </div>
                        </div>
                    </td>
                  </tr>
              </tfoot>   
            </table>
        </div>
        <div class="listCon listCon_padding" id="box2" style="display:none;">
        	<table width="100%" border="0" class="tableBox">
               <thead>
               	  <tr>
                    <td colspan="8" class="listTit_sub">                        
                        <h5 class="fl f14 fb">分类管理</h5><span class="f12 fb"><a href="javascript:void(0);" onclick="ShowSaveNewTypeDialog(0)">新增分类</a></span>              
                    </td>
                  </tr>                                   
                  <tr class="f12 tableTit">
                    <th width="3.7"% class="pl10 pr10 fb bl1ff br1ce"></th>
                    <th width="8.3%" class="tc fb br1ce">编号</th>
                    <th width="21.4%" class="tc fb br1ce">分类名称</th>
                    <th width="10.6%" class="tc fb br1ce">添加时间</th>
                    <th class="tc fb br1ff">操作</th>
                  </tr>
               </thead>  
               <tbody id="tab">
               <asp:Repeater ID="reptListType" runat="server">
                    <ItemTemplate>
                  <tr class="f12 data_item">
                    <td class="pl10 pr10"><input type="checkbox" name="" /></td>
                    <td class="tc"><%#Eval("id") %></td>
                    <td class="tc"><%#Eval("TypeName")%></td>
                    <td class="tc"><%#Convert.ToDateTime(Eval("AddTime")).ToShortDateString()%></td>   
                    <td class="tc pr10 c_blue2 br1ff"><a class="mr20 modifyIco"  href="javascript:void(0);" onclick="ShowSaveNewTypeDialog(<%#Eval("id")%>)">修改</a><%--<a class="mr20 configIco" href="#">配置</a>--%>                    
                     <asp:LinkButton runat="server" CssClass="mr20 deleteIco" ID="btDelete" Text="删除" CommandArgument='<%#Eval("id") %>' OnClientClick="return DeleteConfirm();" OnClick="btDeleteType_Click" />
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
                            <asp:Literal runat="server" ID="litPagerType" />                               
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
