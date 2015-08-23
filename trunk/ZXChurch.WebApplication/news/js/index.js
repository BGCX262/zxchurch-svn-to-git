var requestURL = '../../zxcommon/NewsHandler.ashx';
var cid = UrlParm.parm("cid");
function LoadNewTypeData() {
    var o = $('#news-type-list');
    $.ajax({ type: "POST",
        url: requestURL, data: 'Action=read-newstype-list',
        async: true,
        error: function () {
            o.html('系统出显错误');
        },
        success: function (msg) {
            var result = eval("result=" + msg);
            if (result.result == '1') {
                var num = 0;
                var dataHtml = '', temp = '';

                for (var i in result.list) {
                    var item = result.list[i];
                    if (cid == item.cid) {
                        $('#breadcrumb').append('&nbsp;>&nbsp;<a href="javascript:;">' + item.cname + '</a>')
                        item.cname = '<font color="red">' + item.cname + '</font>';
                       
                     }
                    temp += templateFetch($('#newstype-template').val(), item);
                    num++;
                }
                if (num != 0) {
                    dataHtml = temp;
                }
                o.html(dataHtml);
            } else {
                o.html(result.msg);
            }

        }
    });

}
function LoadDataByPage(v) {
    var o = $('#pageListCt');
   
    o.html('<p class="mb10">正在努力加载……</p>');
    var pageSize = 25; /// <reference path="" />

    $.ajax({ type: "POST",
        url: requestURL, data: 'Action=read-news-list&cid='+cid+'&pagesize=' + pageSize + '&p=' + v,
        async: true,
        error: function () {
            o.html('系统出显错误');
        },
        success: function (msg) {
            var result = eval("result=" + msg);
            if (result.result == '1') {
                //o.html(result.count);
                var num = 0;
                var dataHtml = '', temp = '';

                for (var i in result.list) {
                    var item = result.list[i];
                    temp += templateFetch($('#list-template').val(), item);
                    num++;
                }
                if (num != 0) {
                    dataHtml = '<ul>'+temp+'</ul>';
                }

                if (parseInt(result.count) > pageSize) {
                    var pageHtml = '<div class="f12 lh200 page">';
                    var tmp = '';
                    if (parseInt(result.page) > 1) {
                        pageHtml += '<a href="javascript:;" onclick="LoadDataByPage(' + (parseInt(result.page) - 1) + ')">上一页</a>';
                    }

                    if (parseInt(result.pagecount) <= 10) {
                        for (var i = 1; i <= parseInt(result.pagecount); i++) {
                            if (i == parseInt(result.page))
                                pageHtml += '<b>' + i + '</b>';
                            else
                                pageHtml += '<a href="javascript:;" onclick="LoadDataByPage(' + i + ')">' + i + '</a>';
                        }
                    }

                    if (parseInt(result.page) < parseInt(result.pagecount)) {
                        pageHtml += '<a href="javascript:;" onclick="LoadDataByPage(' + (parseInt(result.page) + 1) + ')">下一页</a>';
                    }

                    dataHtml += pageHtml + '</div>';
                }
                o.html( dataHtml);
            } else {
                o.html(result.msg);
            }

        }
    });
}

function LoadNewsNewTopN() {
    var o = $('#news-new-topN');
    $.ajax({ type: "POST",
        url: requestURL, data: 'Action=read-news-newtop&c=10',
        async: true,
        error: function () {
            o.html('系统出显错误');
        },
        success: function (msg) {
            var result = eval("result=" + msg);
            if (result.result == '1') {
                var num = 0;
                var dataHtml = '', temp = '';
                for (var i in result.list) {
                    var item = result.list[i];
                    temp += templateFetch($('#news-top-template').val(), item);
                    num++;
                }
                if (num != 0) {
                    dataHtml = temp;
                }
                o.html(dataHtml);
            } else {
                o.html(result.msg);
            }

        }
    });
}
function LoadNewsDisplayOrderTopN() {
    var o = $('#news-displayorder-topN');
    $.ajax({ type: "POST",
        url: requestURL, data: 'Action=read-news-displaytop&c=10',
        async: true,
        error: function () {
            o.html('系统出显错误');
        },
        success: function (msg) {
            var result = eval("result=" + msg);
            if (result.result == '1') {
                var num = 0;
                var dataHtml = '', temp = '';
                for (var i in result.list) {
                    var item = result.list[i];
                    temp += templateFetch($('#news-top-template').val(), item);
                    num++;
                }
                if (num != 0) {
                    dataHtml = temp;
                }
                o.html(dataHtml);
            } else {
                o.html(result.msg);
            }

        }
    });
}
$(document).ready(function () { LoadDataByPage(1); LoadNewTypeData(); LoadNewsNewTopN(); LoadNewsDisplayOrderTopN(); });