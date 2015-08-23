function LoadDataByPage(v) {
    var o = $('#data-list-show');
    o.html('<p class="mb10">正在努力加载……</p>');
    var pageSize = 25;
    $.ajax({ type: "POST",
        url: 'WebHandler.ashx', data: 'Action=read-list&pagesize=' + pageSize + '&p=' + v,
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
                    if (num == 4) {
                        dataHtml += '<ul class="f14">' + temp + '</ul>';
                        temp = '';
                        num = 0;
                    }
                }
                if (num != 0) {
                    dataHtml += '<ul class="f14">' + temp + '</ul>';
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
                o.html(dataHtml);
            } else {
                o.html(result.msg);
            }

        }
    });
}


$(document).ready(function () { LoadDataByPage(1); });