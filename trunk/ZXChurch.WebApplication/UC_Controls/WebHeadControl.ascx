<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebHeadControl.ascx.cs" Inherits="ZXChurch.WebApplication.UC_Controls.WebHeadControl" %>
<link href="../css/common.css" rel="stylesheet" type="text/css" />
<div class="header-wrap">    
            <div class="header-top">
                <div class="left">
                    <ul>
                        <li><!--<a target="_blank" href="http://www.gospeltimes.cn/">资讯站 gospeltimes.cn</a></li><li>
                            <a target="_blank" href="http://3g.gospeltimes.cn">手机站</a>--></li></ul>
                </div>
                <div class="logo">
                    <h1>
                        <a href="/">
                            <img class="png24" src="../css/images/logo.gif"></a></h1>
                </div>
                <div class="right right-head">
                    <a onclick="this.style.behavior='url(#default#homepage)';this.setHomePage('http://www.zxchurch.org');"
                        class="a-bg" href="http://www.zxchurch.org">设为首页</a> <a onclick="window.external.AddFavorite(location.href,document.title);"
                            class="a-bg" href="#">收藏本站</a></div>
                <div class="clear">
                </div>
            </div>
            <div class="nav">
                <div class="left">
                    <ul>
                        <li><a target="_blank" class="current" href="/">首 页</a></li>
                        <li><a target="_blank" href="/MonthlyBulletin/default.aspx">月 报</a></li>
                        <!--<li><a target="_blank" href="/M1_ministry.html">事 工</a></li>
                        <li><a target="_blank" href="/M1_theology.html">神 学</a></li>
                        <li><a target="_blank" href="/M1_society.html">社 会</a></li>
                        <li><a target="_blank" href="/M1_culture.html">文 化</a></li>
                        <li><a target="_blank" href="/M1_family.html">家 庭</a></li>
                        <li><a target="_blank" href="/M1_business.html">商 业</a></li>
                        <li><a target="_blank" href="/M1_global.html">环 球</a></li>
                        <li><a target="_blank" href="/M1_column.html">专 栏</a></li>
                        <li><a target="_blank" href="/index.php?r=SpecialTopic/list">专 题</a></li>-->
                        <div style="clear: both;"></div>
                    </ul>
                </div>
                <div class="right">
                    <!--/*<form method="get" action="/search.php"> <input type="text" x-webkit-speech="" autofocus="" id="keywords" class="input-form" value="请输入关键词" alt="请输入关键词" name="keywords" style="color: rgb(255, 255, 255);"> <input type="image" src="/images/bt-search.gif"></form>*/-->
                </div>
                <div class="clear">
                </div>
            </div>
        </div>