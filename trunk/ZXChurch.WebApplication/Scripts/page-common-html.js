function __pageHeader() {
    var url = window.location.href.toLowerCase();
    
    var sbHTML = '<div class="header clearfix">'
    + '<span class="logo"><img src="http://www.zxchurch.org/templates/default/images/logo.png" style="margin-top:3px" /></span>'
    + '<div class="fr" style="width:625px">'
    + '<a href="#" class="fl" target="_blank"><img src="/css/images/570.jpg"></a>'
    + '</div>'
    + '</div>'
    + '<div class="nav">'
    + '<ul class="mainnav">'
    + '<li class="first"><strong><a href="/index.html">网站首页</a></strong></li>'
    + '<li  ' + (url.indexOf('/about.html') > 0 ? 'class="current"' : '') + '><a  href="/about.html" target="_self">我们的信仰</a></li>'
    + '<li  ' + (url.indexOf('/weekly/') > 0 ? 'class="current"' : '') + '><a  href="/weekly/index.html" target="_self">星&nbsp;&nbsp;&nbsp;光</a></li>'
    + '<li class=""><a  href="http://www.zxchurch.org/showforum-4.aspx" target="_blank">诗篇歌曲</a></li>'
    + '<li class=""><a  href="http://www.zxchurch.org/showforum-8.aspx" target="_blank">资源下载</a></li>'
    + '<li ' + (url.indexOf('/news/') > 0 ? 'class="current"' : '') + '><a  href="/news/index.html" target="_self">主内文章</a></li>'
    + '<li ' + (url.indexOf('/churchuactivities.html') > 0 ? 'class="current"' : '') + '><a href="/churchuactivities.html">教会活动</a></li>'
    + '<li  ' + (url.indexOf('/worshipprogram.html') > 0 ? 'class="current"' : '') + '><a href="/worshipprogram.html">敬拜程序</a></li>'
    + '<li ' + (url.indexOf('/contact.html') > 0 ? 'class="current"' : '') + '><a href="/contact.html">联系我们</a></li>'
    + '</ul>'
    + '</div>';
    document.write(sbHTML);
}

function __pageFooter() {
    var sbHTML = '<div class="footer">'
                + '<div class="links"><a target="_blank" href="/about.html">我们的信仰</a>|<a target="_blank" href="/contact.html">联系我们</a>|<a target="_blank" href="http://www.zxchurch.org/index.aspx">交流论坛</a></div>'
                + '版权所有 &copy; 基督教中兴教会 All Rrights Reserved.  豫ICP备1300581 '
                + '</div>'
                + '<script src="/Scripts/google-analytics.js" type="text/javascript"></script>'
                + '';
    document.write(sbHTML);
}