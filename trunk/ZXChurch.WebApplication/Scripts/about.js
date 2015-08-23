var requestURL = 'zxcommon/NewsHandler.ashx';
var ___temp = '<li><a href="/news/{id}.html" title="{title}">{title}</a></li>';
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
                    temp += templateFetch(___temp, item);
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
                    temp += templateFetch(___temp, item);
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
$(document).ready(function () {LoadNewsNewTopN(); LoadNewsDisplayOrderTopN(); });