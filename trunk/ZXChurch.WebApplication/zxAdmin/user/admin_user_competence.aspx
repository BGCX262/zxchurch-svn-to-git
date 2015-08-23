<%@ Page Language="C#" AutoEventWireup="true"  Inherits="ZXChurch.Admin.User.AdminUserCompetence" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户权限管理</title>
    <link href="../../css/admin/styles/base.css" rel="stylesheet" type="text/css" />
    <link href="../../css/admin/styles/common.css" rel="stylesheet" type="text/css" />
    <link href="../../css/admin/styles/admin.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/jQueryDialog/dialog.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link href="../../Scripts/jQueryDialog/dialog.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jQueryDialog/html5.js" type="text/javascript"></script>
    <script src="../../Scripts/jQueryDialog/dialog.js" type="text/javascript"></script>
   <script type="text/javascript" src="js/admin_user_competence.js"></script>
</head>
<body>
    <div class="welcomBox">
<div class=" welcomCont listPadding">
    	<p class=" mb5 c_grey postion">您的当前位置：<a href="#">首页</a> > <a href="#">用户权限管理</a></p>
		<h4 class="f14 listTits" id="listTits_tab">
        	<a href="javascript:;" id="Tab1" onclick="ShowTab(1)">周报权限管理</a>
            <%--<a href="javascript:;" id="Tab2" onclick="ShowTab(2)">前台用户中心</a>
            <a href="javascript:;" id="Tab3" onclick="ShowTab(3)">后台添加</a>
            <a href="javascript:;" id="Tab4" onclick="ShowTab(4)">后台修改联系信息</a>
            <a href="javascript:;" id="Tab5" onclick="ShowTab(5)">后台修改问卷</a>
            <a href="javascript:;" id="Tab6" onclick="ShowTab(6)">后台修改产品</a>--%>
        </h4>
        <div class="listCon listCon_padding" id="box1" style="display:none;">
        <table width="100%" border="0" class="tableBox">
               <thead>
               	  <tr>
                    <td colspan="8" class="listTit_sub">                        
                        <h5 class="fl f14 fb">周报权限管理</h5><span class="f12 fb"><a href="javascript:;" onclick="ShowSelectUserDialog(1)">添加管理员</a></span>              
                    </td>
                  </tr>                        
                  <tr class="f12 tableTit">
                    <th width="3.7"% class="pl10 pr10 fb bl1ff br1ce"><%--<input type="checkbox" name="" />--%></th>
                    <th width="8.3%" class="tc fb br1ce">编号</th>
                    <th width="8.3%" class="tc fb br1ce">管理员ID</th>
                    <th width="21.4%" class="tc fb br1ce">登陆名</th>
                    <th width="10.7%" class="tc fb br1ce">登陆时间</th>
                    <th width="8%" class="tc fb br1ce">状态</th>
                    <th class="tc fb br1ff">操作</th>
                  </tr>
               </thead>  
               <tbody id="MB-data-list" class="data_list">                                          
               </tbody> 
               <tfoot>                       
                  <tr>
                    <td colspan="8">
                        <div class="f12 tablePage">                    	
                            <div class="fr pr10 c_grey tableBottom_r"  id="MB-tablePage">
                              
                            </div>
                        </div>
                    </td>
                  </tr>
              </tfoot>     
            </table>
           <textarea id="MB-data-template" style="display:none;">
            <tr class="f12 data_item" id="MB-data-{id}">
                    <td class="pl10 pr10"></td>
                    <td class="tc">{id}</td>
                    <td class="tc">{userid}</td>
                    <td class="tc">{username}</td>                   
                    <td class="tc">{logintime}</td>
                    <td class="tc" id="MB-data-usertype-{id}">{usertype}</td>
                    <td class="tc pr10 c_blue2 br1ff"><a id="MB-data-config-{id}" class="mr20 configIco" href="javascript:;" onclick="EditUser(1,'id={id}&usertype={type}',{id:'{id}',usertype:{type},trname:'{trname}'})">配置</a><a class="mr20 deleteIco" href="javascript:;" onclick="DeleteUser(1,'id={id}',{trid:'{trid}'});">删除</a></td>
                  </tr>    
           </textarea>
        
        </div>
        <div class="listCon listCon_padding" id="box2" style="display:none;">
        	<table width="100%" border="0" class="tableBox">
               <thead>
               	  <tr>
                    <td colspan="8" class="listTit_sub">                        
                        <h5 class="fl f14 fb">流程管理1</h5><span class="f12 fb"><a href="#">新增流程</a></span>              
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
                            <select class=" f13 normalSel"><option>正常</option></select>
                            <input type="text" class="f12 searchTxt" value="问题名称/代码" />
                            <input type="button" class="findBtn" value="查 询" />
                        </div>
                    </td>
                  </tr>               
                  <tr class="f12 tableTit">
                    <th width="3.7"% class="pl10 pr10 fb bl1ff br1ce"><input type="checkbox" name="" /></th>
                    <th width="8.3%" class="tc fb br1ce">编号</th>
                    <th width="8.3%" class="tc fb br1ce">代码</th>
                    <th width="21.4%" class="tc fb br1ce">流程名称</th>
                    <th width="10.7%" class="tc fb br1ce">胸卡类型</th>
                    <th width="10.6%" class="tc fb br1ce">调用css</th>
                    <th width="8%" class="tc fb br1ce">状态</th>
                    <th class="tc fb br1ff">操作</th>
                  </tr>
               </thead>  
               <tbody id="tab">
                  <tr class="f12">
                    <td class="pl10 pr10"><input type="checkbox" name="" /></td>
                    <td class="tc">001</td>
                    <td class="tc">Q001</td>
                    <td class="tc">工作情况调查2</td>
                    <td class="tc">EQ01</td>
                    <td class="tc">css1</td>
                    <td class="tc">正常</td>
                    <td class="tc pr10 c_blue2 br1ff"><a class="mr20 modifyIco" href="#">修改</a><a class="mr20 configIco" href="#">配置</a><a class="mr20 deleteIco" href="#">删除</a></td>
                  </tr>
                  <tr class="f12">
                    <td class="pl10 pr10"><input type="checkbox" name="" /></td>
                    <td class="tc">001</td>
                    <td class="tc">Q001</td>
                    <td class="tc">工作情况调查2</td>
                    <td class="tc">EQ01</td>
                    <td class="tc">css1</td>
                    <td class="tc">正常</td>
                    <td class="tc pr10 c_blue2 br1ff"><a class="mr20 modifyIco" href="#">修改</a><a class="mr20 configIco" href="#">配置</a><a class="mr20 deleteIco" href="#">删除</a></td>
                  </tr>
                  <tr class="f12">
                    <td class="pl10 pr10"><input type="checkbox" name="" /></td>
                    <td class="tc">001</td>
                    <td class="tc">Q001</td>
                    <td class="tc">工作情况调查2</td>
                    <td class="tc">EQ01</td>
                    <td class="tc">css1</td>
                    <td class="tc">正常</td>
                    <td class="tc pr10 c_blue2 br1ff"><a class="mr20 modifyIco" href="#">修改</a><a class="mr20 configIco" href="#">配置</a><a class="mr20 deleteIco" href="#">删除</a></td>
                  </tr>
                  <tr class="f12">
                    <td class="pl10 pr10"><input type="checkbox" name="" /></td>
                    <td class="tc">001</td>
                    <td class="tc">Q001</td>
                    <td class="tc">工作情况调查2</td>
                    <td class="tc">EQ01</td>
                    <td class="tc">css1</td>
                    <td class="tc">正常</td>
                    <td class="tc pr10 c_blue2 br1ff"><a class="mr20 modifyIco" href="#">修改</a><a class="mr20 configIco" href="#">配置</a><a class="mr20 deleteIco" href="#">删除</a></td>
                  </tr>
                  <tr class="f12">
                    <td class="pl10 pr10"><input type="checkbox" name="" /></td>
                    <td class="tc">001</td>
                    <td class="tc">Q001</td>
                    <td class="tc">工作情况调查2</td>
                    <td class="tc">EQ01</td>
                    <td class="tc">css1</td>
                    <td class="tc">正常</td>
                    <td class="tc pr10 c_blue2 br1ff"><a class="mr20 modifyIco" href="#">修改</a><a class="mr20 configIco" href="#">配置</a><a class="mr20 deleteIco" href="#">删除</a></td>
                  </tr>
                  <tr class="f12">
                    <td class="pl10 pr10"><input type="checkbox" name="" /></td>
                    <td class="tc">001</td>
                    <td class="tc">Q001</td>
                    <td class="tc">工作情况调查2</td>
                    <td class="tc">EQ01</td>
                    <td class="tc">css1</td>
                    <td class="tc">正常</td>
                    <td class="tc pr10 c_blue2 br1ff"><a class="mr20 modifyIco" href="#">修改</a><a class="mr20 configIco" href="#">配置</a><a class="mr20 deleteIco" href="#">删除</a></td>
                  </tr>
                  <tr class="f12">
                    <td class="pl10 pr10"><input type="checkbox" name="" /></td>
                    <td class="tc">001</td>
                    <td class="tc">Q001</td>
                    <td class="tc">工作情况调查2</td>
                    <td class="tc">EQ01</td>
                    <td class="tc">css1</td>
                    <td class="tc">正常</td>
                    <td class="tc pr10 c_blue2 br1ff"><a class="mr20 modifyIco" href="#">修改</a><a class="mr20 configIco" href="#">配置</a><a class="mr20 deleteIco" href="#">删除</a></td>
                  </tr>
                  <tr class="f12">
                    <td class="pl10 pr10"><input type="checkbox" name="" /></td>
                    <td class="tc">001</td>
                    <td class="tc">Q001</td>
                    <td class="tc">工作情况调查2</td>
                    <td class="tc">EQ01</td>
                    <td class="tc">css1</td>
                    <td class="tc">正常</td>
                    <td class="tc pr10 c_blue2 br1ff"><a class="mr20 modifyIco" href="#">修改</a><a class="mr20 configIco" href="#">配置</a><a class="mr20 deleteIco" href="#">删除</a></td>
                  </tr>
                  <tr class="f12">
                    <td class="pl10 pr10"><input type="checkbox" name="" /></td>
                    <td class="tc">001</td>
                    <td class="tc">Q001</td>
                    <td class="tc">工作情况调查2</td>
                    <td class="tc">EQ01</td>
                    <td class="tc">css1</td>
                    <td class="tc">正常</td>
                    <td class="tc pr10 c_blue2 br1ff"><a class="mr20 modifyIco" href="#">修改</a><a class="mr20 configIco" href="#">配置</a><a class="mr20 deleteIco" href="#">删除</a></td>
                  </tr>
                  <tr class="f12">
                    <td class="pl10 pr10"><input type="checkbox" name="" /></td>
                    <td class="tc">001</td>
                    <td class="tc">Q001</td>
                    <td class="tc">工作情况调查2</td>
                    <td class="tc">EQ01</td>
                    <td class="tc">css1</td>
                    <td class="tc">正常</td>
                    <td class="tc pr10 c_blue2 br1ff"><a class="mr20 modifyIco" href="#">修改</a><a class="mr20 configIco" href="#">配置</a><a class="mr20 deleteIco" href="#">删除</a></td>
                  </tr>
                  <tr class="f12">
                    <td class="pl10 pr10"><input type="checkbox" name="" /></td>
                    <td class="tc">001</td>
                    <td class="tc">Q001</td>
                    <td class="tc">工作情况调查2</td>
                    <td class="tc">EQ01</td>
                    <td class="tc">css1</td>
                    <td class="tc">正常</td>
                    <td class="tc pr10 c_blue2 br1ff"><a class="mr20 modifyIco" href="#">修改</a><a class="mr20 configIco" href="#">配置</a><a class="mr20 deleteIco" href="#">删除</a></td>
                  </tr>
                  <tr class="f12">
                    <td class="pl10 pr10"><input type="checkbox" name="" /></td>
                    <td class="tc btnone">001</td>
                    <td class="tc btnone">Q001</td>
                    <td class="tc btnone">工作情况调查2</td>
                    <td class="tc btnone">EQ01</td>
                    <td class="tc btnone">css1</td>
                    <td class="tc btnone">正常</td>
                    <td class="tc pr10 c_blue2 br1ff btnone"><a class="mr20 modifyIco" href="#">修改</a><a class="mr20 configIco" href="#">配置</a><a class="mr20 deleteIco" href="#">删除</a></td>
                  </tr>                  
               </tbody> 
               <tfoot>       
                  <tr>
                    <td colspan="8" class="btnone">
                        <div class="f12 tableOp">
                            <div class="fl pl10 findRow_left">
                                <input type="checkbox" name="" value="全选/取消" /><span class="f12 ml5">全选/取消</span>
                                <select class=" f13 deleteSel"><option>批量删除</option></select>
                                <input type="button" class="f12 deleteBtn" value="批量操作" />
                            </div>                                                
                        </div>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="8">
                        <div class="f12 tablePage">                    	
                            <div class="fr pr10 c_grey tableBottom_r">
                                <span class="totalPage">共<em class="c_red">100</em>条记录,每页<em>10</em>条</span>
                                <a class="mr10" href="#">首页</a>
                                <a class="mr10" href="#">上一页</a>
                                <a class="numPage" href="#">1</a>
                                <a class="numPage" href="#">2</a>
                                <a class="numPage" href="#">3</a>...<a class="numPage" href="#">10</a><a class="ml10 mr10" href="#">下一页</a><a class="mr10" href="#">尾页</a><span>转至第<input type="text" class="goPage" />页</span><input type="button" class="goBtn" />
                            </div>
                        </div>
                    </td>
                  </tr>
              </tfoot>     
            </table>
      </div>  
        <div id="box3" style="display:none;">cccccc</div>
        <div id="box4" style="display:none;">dddddd</div>
        <div id="box5" style="display:none;">eeeeee</div>
        <div id="box6" style="display:none;">ffffff</div>
    </div>
</div>
<script language="javascript" type="text/javascript">
    ShowTab(1);
</script>
</body>
</html>
