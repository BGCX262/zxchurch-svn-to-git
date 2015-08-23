var NEWS = 'WebHandler.ashx';
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

}
var DialogObj = null;
function ShowSaveNewTypeDialog(id) { //添加文章分类
    DialogObj = dialog({ type: 'iframe', value: 'admin_news_category_save.aspx?id=' + id, height: '300px', width: '420px' }, { title: '保存类别' });

}
function CloseDialog() {
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
function EditNewsStatus(v, i, val) {//编辑文章
    var POSTURL = '';
    v = parseInt(v);
    i = parseInt(i);
    switch (v) {
        case 1: POSTURL = NEWS; break;
        default:
            alert('错误的参数');
            break;
    }
    DialogObj = dialog('正在执行操作……', { title: '系统提示' });
    $.ajax({ type: "POST",
        url: POSTURL, data: 'Action=sys-admin&do=update&' + val,
        async: true,
        error: function () {
            DialogObj.close();
            DialogObj = dialog('系统出显错误', { title: '操作提示', time: 1500 });
        },
        success: function (msg) {
            var result = eval("result=" + msg);
            if (result.result == '1') {
                DialogObj.close();
                DialogObj = dialog('保存成功', { title: '操作提示', time: 1500 });
                if (result.IsShow == 1) {
                    Table1.rows[i].cells[6].innerText = "显示";
                } else {
                    Table1.rows[i].cells[6].innerText = "隐藏";
                }

                var data = Table1.rows[i].cells[7].getElementsByTagName("a")[1].onclick;


            } else {
                DialogObj.close();
                DialogObj = dialog(result.msg, { title: '操作提示', time: 1500 });
            }
        }
    });
}

function ShowSaveNews(id) {
    DialogObj = dialog({ type: 'iframe', value: 'admin_news_save.aspx?id=' + id, height: '580px', width: '900px' }, { title: '保存文章' });
}