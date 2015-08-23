var DialogObj = null;
var MBURL = '../../weekly/WebHandler.ashx';
function ShowTab(i) {//权限选项卡切换
    var AList = document.getElementById('listTits_tab').getElementsByTagName('A');
    for (var k = 0; k < AList.length; k++) {
        if ((k + 1) == parseInt(i)) {
            AList[k].className = 'js_on';
            $('#box' + (k + 1)).show();
        }
        else {
            AList[k].className = AList[k].className.replace('js_on', '');
            $('#box' + (k + 1)).hide();
        }
    }
    LoadTabData(i);
}
function LoadTabData(i) { //加载选择的选项卡显示的数据
    switch (i) {
        case 1: LoadMonthlyBulletin(1); break; //加载月报管理员
        default:
            alert('错误的参数'); return;
            break;
    }
}
function templateFetch(str, obj) {
    var retval = str;
    //其实是顺次对 str 执行正则表达式的替换
    for (i in obj) {
        var re = new RegExp('\\{' + i + '\\}', 'g');
        retval = retval.replace(re, obj[i]);
    }
    return retval;
}

function ShowSelectUserDialog(v) { //选择管理用户
    DialogObj = dialog({ type: 'iframe', value: 'admin_user_competence_select.aspx?id=' + v, height: '300px', width: '420px' }, { title: '添加管理员' });

}

function InsertUser(v, val) { //添加管理员
    var POSTURL = '';
    v = parseInt(v);
    switch (v) {
        case 1: POSTURL = MBURL; break;  //月报管理员
        default:
            alert('错误的参数'); return;
            break;
    }
    //DialogObj = dialog('正在执行操作……', { title: '系统提示' }); 
    $.ajax({ type: "POST",
        url: POSTURL, data: 'Action=sys-admin&do=save&' + val,
        async: true,
        error: function () {
            DialogObj = dialog('系统出显错误', { title: '操作提示', time: 1500 });
        },
        success: function (msg) {
            var result = eval("result=" + msg);
            if (result.result == '1') {
                DialogObj.close();
                DialogObj = dialog('保存成功', { title: '操作提示', time: 1500 });
                LoadTabData(v);
            } else {
                //alert(result.msg);
                DialogObj = dialog(result.msg, { title: '操作提示', time: 1500 });
            }

        }
    });
}

function EditUser(v, val,parms) {//编辑用户
    var POSTURL = '';
    v = parseInt(v);
    switch (v) {
        case 1: POSTURL = MBURL; break; //月报管理员
        default:
            alert('错误的参数');
            break;
    }
    DialogObj = dialog('正在执行操作……', { title: '系统提示' });
    $.ajax({ type: "POST",
        url: POSTURL, data: 'Action=sys-admin&do=update&' + val,
        async: true,
        error: function () {
            DialogObj = dialog('系统出显错误', { title: '操作提示', time: 1500 });
        },
        success: function (msg) {
            var result = eval("result=" + msg);
            if (result.result == '1') {
                DialogObj.close();
                DialogObj = dialog('保存成功', { title: '操作提示', time: 1500 });
                //LoadTabData(v);
                // if (parms != null) 
                {
                    //alert(parms);
                    var o_typeId = parms.trname + 'usertype-' + parms.id;
                    //alert(o_typeId);
                    var o_configId = parms.trname + 'config-' + parms.id;
                    parms.usertype = parms.usertype == '1' ? '0' : '1';
                    document.getElementById(o_typeId).innerHTML = parms.usertype == '1' ? '<font color="red">禁止</font>' : '<font color="green">有效</font>';
                    document.getElementById(o_configId).setAttribute("onclick", templateFetch("EditUser(1,'id={id}&usertype={usertype}',{id:'{id}',usertype:{usertype},trname:'{trname}'})", parms));
                }
            } else {
                //alert(result.msg);
                DialogObj = dialog(result.msg, { title: '操作提示', time: 1500 });
            }

        }
    });
}
function DeleteUser(v, val,parms) { //删除用户
    var POSTURL = '';

    switch (parseInt(v)) {
        case 1: POSTURL = MBURL; break; //月报管理员
        default:
            alert('错误的参数');
            break;
    }
    if (confirm('确定要删除吗？')) {
        DialogObj = dialog('正在执行操作……', { title: '系统提示' });
        $.ajax({ type: "POST",
            url: POSTURL, data: 'Action=sys-admin&do=delete&' + val,
            async: true,
            error: function () {
                DialogObj = dialog('系统出显错误', { title: '操作提示', time: 1500 });
            },
            success: function (msg) {
                var result = eval("result=" + msg);
                if (result.result == '1') {
                    DialogObj.close();
                    DialogObj = dialog('删除成功', { title: '操作提示', time: 1500 });
                    $('#' + parms.trid).remove();
                    //LoadTabData(v);
                } else {
                    //alert(result.msg);
                    DialogObj = dialog(result.msg, { title: '操作提示', time: 1500 });
                }

            }
        });
    }
}

function LoadMonthlyBulletin(v) {//月报管理员

    var o = $('#MB-data-list'), p = $('#MB-tablePage');
    o.html('<tr> <td colspan="8"><p class="mb10">正在加载数据……</p></td></tr>');
    p.html('');
    var pageSize = 4;
    $.ajax({ type: "POST",
        url: MBURL, data: 'Action=sys-admin&do=list&pagesize=' + pageSize + '&p=' + v,
        async: true,
        error: function () {
            o.html('系统出显错误');
        },
        success: function (msg) {
            var result = eval("result=" + msg);
            if (result.result == '1') {
                var dataHtml = '', temp = '';

                for (var i in result.list) {
                    var item = result.list[i];
                    item.type = item.usertype == '0' ? '1' : '0';
                    item.usertype = item.usertype == '0' ? '<font color="red">禁止</font>' : '<font color="green">有效</font>';
                    item.trname = 'MB-data-';
                    item.trid = 'MB-data-'+item.id;
                    temp += templateFetch($('#MB-data-template').val(), item);
                }
                o.html(temp);
                var pageHtml = '<span class="totalPage">共<em class="c_red">' + result.count + '</em>条记录,每页<em>' + pageSize + '</em>条</span>';
                if (parseInt(result.count) > pageSize) {

                    var tmp = '';
                    if (parseInt(result.page) > 1) {
                        pageHtml += '<a class="numPage"  href="javascript:;" onclick="LoadMonthlyBulletin(' + (parseInt(result.page) - 1) + ')">上一页</a>';
                    }
                    if (parseInt(result.pagecount) <= 10) {
                        for (var i = 1; i <= parseInt(result.pagecount); i++) {
                            if (i == parseInt(result.page))
                                pageHtml += '<b>' + i + '</b>';
                            else
                                pageHtml += '<a class="numPage"  href="javascript:;" onclick="LoadMonthlyBulletin(' + i + ')">' + i + '</a>';
                        }
                    }

                    if (parseInt(result.page) < parseInt(result.pagecount)) {
                        pageHtml += '<a class="numPage"  href="javascript:;" onclick="LoadMonthlyBulletin(' + (parseInt(result.page) + 1) + ')">下一页</a>';
                    }
                }
                p.html(pageHtml);
                //o.html(dataHtml);
            } else {
                //o.html(result.msg);
                DialogObj = dialog(result.msg, { title: '系统提示' });
            }

        }
    });
}       