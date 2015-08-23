var requestURL = 'zxcommon/NewsHandler.ashx';
function loadTopNews() { //加载最新文章
    var ___temp = '<li><p><span class="tail">{addtime}</span><a href="/news/{id}.html" title="{title}">{title}</a></p></li>';
    var o = $('#index-top-news');
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

function loadcidtag() {
    var ___temp = '<li><p><a title="{title}" href="/news/{id}.html">{title}</a></p></li>';
    $('.list-paddingleft-2').each(function () {
        var o = $(this), cid = o.attr('cid');
        if (cid) {
            var __parmData = 'Action=read-news-cid-top&c=10&cid=' + cid;
            if (cid == "hot") { __parmData = 'Action=read-news-displaytop&c=14'; }
            else if (cid == "recommend") { __parmData = 'Action=read-news-index-recom&c=5'; }
            $.ajax({ type: "POST",
                url: requestURL, data: __parmData,
                async: true,
                success: function (msg) {
                    var result = eval("result=" + msg);
                    if (result.result == '1') {
                        var dataHtml = '';
                        for (var i in result.list) {
                            var item = result.list[i];
                            dataHtml += templateFetch(___temp, item);
                        }
                        o.html(dataHtml);
                    }

                }
            });
        }
    });
}
$(document).ready(function () {
    loadTopNews(); loadcidtag();
});

