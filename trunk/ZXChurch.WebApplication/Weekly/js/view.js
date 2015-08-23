function loadWeekly() {
    var id = weeklyid;
    var o = $('#weekly-count');
    if (id != '' && id != undefined) {
        $.ajax({ type: "POST",
            url: 'WebHandler.ashx', data: 'Action=read-weekly-count&id=' + id + '&t=' + Math.random(),
            async: true,
            error: function () {
                o.html('系统出显错误');
            },
            success: function (msg) {
                var result = eval("result=" + msg);
                if (result.result == '1') {
                    o.html(result.count);
                } 

            }
        });
    }
    else {
        o.html("错误的参数！");
    }
}
$(document).ready(function () {
    loadWeekly();
});
