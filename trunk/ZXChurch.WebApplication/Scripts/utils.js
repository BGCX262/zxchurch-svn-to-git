//====================================================================================
//======================================Form表单验证 Begin============================
//====================================================================================
//验证表单 供外部调用
//prompt=""  required="1"  valuetype
function CheckForm(objForm) {
    return _F_FR_Check_Rquired(objForm);
}
var _str_CanBeNull = "至少有一项不能为空!";
var _str_WrongTextType = "输入的数据类型有误!";
var _str_ErrorTitle = "错误信息提示";
var _str_AlertTile = "提示信息";
var _int_Width = 300;
var _int_Height = 150;
var CurrentObject;

function ShowSysTip(content) {
    close_dialog();
    show_dialoginfo("系统提示", content, _int_Width, _int_Height)
}

function ShowError(content) {
    close_dialog();
    show_dialoginfo(_str_ErrorTitle, content, _int_Width, _int_Height)
}
//cs文件里调用的提示函数
function ShowMessage(content) {
    //show_dialoginfo("",content,_int_Width,_int_Height)
    alert(content);
}
function _F_FR_Check_Rquired(frm) {
    var bResult = true
    var fm_len = frm.length;
    var fmObj;
    var tempDate, tempYear, tempMonth, tempDay, m;
    var temp;
    var aaGetYear;
    var tempStr;
    for (var i = 0; i < fm_len; i++) {
        //begin for loop
        fmObj = frm.elements[i];
        //alert(fmObj);
        if (fmObj.disabled == false) {
            if (_FR_Check_Required_Object(fmObj) == false)
                return false;
        }
    }
    return true;
}
function $_ID(id) {
    return document.getElementById(id);
}
function _F_FR_Check_RquiredAlert(obj) {
    var _l_msgLeastNotNull = _str_CanBeNull;
    var errcontent = "";
    if (obj.getAttribute("prompt") != null) {


        if (obj.type == "select-one")
            errcontent = "请选择" + obj.getAttribute("prompt");
        else
            errcontent = "请输入" + obj.getAttribute("prompt");
    }
    else {
        errcontent = _l_msgLeastNotNull;
    }
    ShowError(errcontent);

    CurrentObject = obj;
    try {
        obj.focusout();
    }
    catch (e) {

    }
}
function _F_FR_Check_TextTypeAlert(obj, string) {
    var errcontent = "";
    var _l_msgWrongTextType = _str_WrongTextType;
    if (obj.getAttribute("prompt") != null) {
        errcontent = obj.getAttribute("prompt") + "输入错误," + string;
        //alert(obj.prompt+"输入错误,"+string);			
    }
    else {
        errcontent = _l_msgWrongTextType;
        //alert(_l_msgWrongTextType);
    }
    ShowError(errcontent);

    CurrentObject = obj;
    try {
        obj.focusout();
    }
    catch (e) {

    }
}


function F_LS_GetSelectVal(obj) {
    if (obj.selectedIndex >= 0)
        return obj.item(obj.selectedIndex).value;
    else
        return false;
}

function _FR_Check_Required_Object(fmObj) {
    if (fmObj.getAttribute("required") == "1") {
        if (fmObj.type == "select-one") {
            var selectValue = ""; //2010-05-19新增的检测
            if (fmObj.defaultValue) {
                selectValue = fmObj.defaultValue;
            } //新增内容结束
            if (F_LS_GetSelectVal(fmObj) == selectValue) {
                _F_FR_Check_RquiredAlert(fmObj);
                return false;
            }
        }
        else if ((fmObj.type == "text") || (fmObj.type == "password") || (fmObj.type == "textarea")) {
            if (fmObj.value.replace(/^\s+|\s+$/g, "") == "") {
                _F_FR_Check_RquiredAlert(fmObj);
                return false;
            }
        }
    }
    if (fmObj.value != "") {
        if (fmObj.getAttribute("changedot") == 1)
            fmObj.value = fmObj.value.replace(",", "，");
        if (fmObj.getAttribute("valuetype") == "N") {
            if (isNaN(fmObj.value)) {
                _F_FR_Check_TextTypeAlert(fmObj, "需要是数字");
                return false;
            }
        }
        else if (fmObj.getAttribute("valuetype") == "CN") {
            if (!InChinese(fmObj.value)) {
                _F_FR_Check_TextTypeAlert(fmObj, "需要是中文");
                return false;
            }
        }
        else if (fmObj.getAttribute("valuetype") == "money") {
            if (!Ismoney(fmObj.value)) {
                _F_FR_Check_TextTypeAlert(fmObj, "价格的格式不对");
                return false;
            }
        }
        else if (fmObj.getAttribute("valuetype") == "Discount") {
            if (!IsDiscount(fmObj.value)) {
                _F_FR_Check_TextTypeAlert(fmObj, "打折格式不对");
                return false;
            }
        }
        else if (fmObj.getAttribute("valuetype") == "Email") {
            if (!IsEMail(fmObj.value)) {
                _F_FR_Check_TextTypeAlert(fmObj, "电子邮件类型错误！");
                return false;
            }
        }
        else if (fmObj.getAttribute("valuetype") == "EnKong") {
            if (!IsEnKong(fmObj.value)) {
                _F_FR_Check_TextTypeAlert(fmObj, "含有特殊字符,类型只能为英文字母、数字和下划线");
                return false;
            }
        }
        else if (fmObj.getAttribute("valuetype") == "Domain") {
            if (!IsDomain(fmObj.value)) {
                _F_FR_Check_TextTypeAlert(fmObj, "含有特殊字符,类型只能为英文字母、数字和中横线");
                return false;
            }
        }
        else if (fmObj.getAttribute("valuetype") == "Valid") {
            if (!IsValid(fmObj.value)) {
                _F_FR_Check_TextTypeAlert(fmObj, "含有特殊字符,类型只能为英文字母、中文字符、数字和下划线");
                return false;
            }
            //return false;
        }
        else if (fmObj.getAttribute("valuetype") == "Http" && fmObj.value != "http://") {
            if (!IsHttp(fmObj.value)) {
                _F_FR_Check_TextTypeAlert(fmObj, "标准格式为:http://www.yorkbbs.ca");
                return false;
            }
            //return false;
        } else if (fmObj.getAttribute("valuetype") == "CanadaPost") {
            if (!IsCanadaPost(fmObj)) {
                _F_FR_Check_TextTypeAlert(fmObj, "如：M2J 4A6");
                return false;
            }
            //return false;
        } else if (fmObj.getAttribute("valuetype") == "CanadaTel") {
            if (!IsCanadaTel(fmObj)) {
                _F_FR_Check_TextTypeAlert(fmObj, "如：1234567890");
                return false;
            }
        }


        if (fmObj.valuelength != null) {

            if (fmObj.value.length > parseInt(fmObj.valuelength)) {
                _F_FR_Check_TextTypeAlert(fmObj, "字符长度不能大于" + fmObj.getAttribute("valuelength"));
                return false;
            }
        }

    }

    if ((fmObj.getAttribute("valuetype") == "D") && ((fmObj.getAttribute("required") == "1") || (fmObj.value != ""))) {
        temp = fmObj.value;
        temp = temp.replace("-", "/");
        temp = temp.replace("-", "/");
        tempDate = new Date(temp);

        m = temp.indexOf("/");

        tempYear = temp.substr(0, m);

        m = temp.lastIndexOf("/") + 1;


        tempDay = parseFloat(temp.substr(m));


        aaGetYear = tempDate.getYear();

        if (aaGetYear < 1900) aaGetYear = 1900 + aaGetYear;

        if ((tempDay != tempDate.getDate()) || (tempYear != aaGetYear)) {
            ShowError("无效的日期。正确的格式为:2008-1-1");
            fmObj.focus()
            return false;
        }
    }
    return true;
}



//判断输入的是不是合法邮件地址
function IsEMail(emailStr) {
    var emailPat = /^([A-Za-z0-9_\-\.]+)@([A-Za-z0-9]+)[\.]+([A-Za-z0-9]{2,3})$/;
    var matchArray = emailStr.match(emailPat);
    //alert(matchArray[1]);
    if (matchArray == null) {
        //alert("E-mail邮件地址错误！");
        //show_dialoginfo(_str_ErrorTitle,"E-mail邮件地址错误！",_int_Width,_int_Height)
        return false;
    }
    return true;
}


//判断输入的是不是正常的字符串
function IsValid(String) {
    var Letters = "><'<>/\\～！·＃￥％…；‘’：“”—＊（　）—＋｜－＝、／。，？《》↑↓⊙●★☆■♀ 『』◆◥◤◣ Ψ ※ →№←㊣∑⌒ 〖〗 ＠ξζω□ ∮〓※ ▓∏卐【 】▲△√ ∩¤々 ♀♂∞ ㄨ ≡↘↙ ＆◎Ю┼┏ ┓田 ┃▎○╪┗┛ ∴ ①②③④⑤⑥⑦⑧ \"";
    var i;
    var c;
    for (i = 0; i < String.length; i++) {
        c = String.charAt(i);
        if (Letters.indexOf(c) >= 0)
            return false;
    }
    return true;
}

//判断输入的是不是正常的字符串
function IsHttp(String) {
    var myReg = /^http:\/\/([A-Za-z0-9_-]+)[\.]{1}[\S\s]+/;
    var matchArray = String.match(myReg);
    //alert(matchArray[1]);
    if (matchArray == null) {
        //alert("E-mail邮件地址错误！");
        //show_dialoginfo(_str_ErrorTitle,"E-mail邮件地址错误！",_int_Width,_int_Height)
        return false;
    }
    return true;
}

function Ismoney(str) {
    var re = /^[\d.]*$/;
    return re.test(str);
}
function IsDiscount(str) {
    var re = /^([1-9]|([0-9][.]([0-9]{1,2})))$/;
    return re.test(str);
}



//判断输入的是不是合法的字符的函数
function IsEnKong(argValue) {
    var flag1 = false;
    var compStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_-1234567890";
    var length2 = argValue.length;
    for (var iIndex = 0; iIndex < length2; iIndex++) {
        var temp1 = compStr.indexOf(argValue.charAt(iIndex));
        if (temp1 == -1) {
            flag1 = false;
            break;
        }
        else {
            flag1 = true;
        }
    }
    return flag1;
}

//判断输入的是不是合法的字符的函数
function IsDomain(argValue) {
    var flag1 = false;
    var compStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-1234567890";
    var length2 = argValue.length;
    for (var iIndex = 0; iIndex < length2; iIndex++) {
        var temp1 = compStr.indexOf(argValue.charAt(iIndex));
        if (temp1 == -1) {
            flag1 = false;
            break;
        }
        else {
            flag1 = true;
        }
    }
    return flag1;
}

//判断输入的是不是包含中文
function InChinese(String) {
    var Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890`=~!@#$%^&*()_+[]{}\\|/?.>,<;:'\-?<>/～！·＃￥％…；‘’：“”—＊（　）—＋｜－＝、／。，？《》↑↓⊙●★☆■♀ 『』◆◥◤◣ Ψ ※ →№←㊣∑⌒ 〖〗 ＠ξζω□ ∮〓※ ▓∏卐【 】▲△√ ∩¤々 ♀♂∞ ㄨ ≡↘↙ ＆◎Ю┼┏ ┓田 ┃▎○╪┗┛ ∴ ①②③④⑤⑥⑦⑧ \"";
    var i;
    var c;
    for (i = 0; i < String.length; i++) {
        c = String.charAt(i);
        if (Letters.indexOf(c) < 0)
            return true;
    }
    return false;
}

//验证文字长度
function CheckWordLength(obj, obj2, MaxLengh) {
    var NowLength = obj.value.length;
    if (NowLength < MaxLengh) {
        obj2.innerHTML = "总限定字数[<span class=Red>" + MaxLengh + "</span>] 目前ds还可输入[<span class=Green>" + (MaxLengh - NowLength) + "</span>]";
    }
    else {
        //alert("总字数不能超过"+MaxLengh+",自动清除!")
        obj.value = obj.value.substring(0, MaxLengh)
        obj2.innerHTML = "总限定字数[<span class=Red>" + MaxLengh + "</span>] <span class=Red>目前已达上限</span>";
    }
}
/**
* 去除字符串str头尾的空格
* @param str 字符串
* @return str去除头尾空格后的字符串。
*/
function trim(str) {
    if (str == null) return "";

    // 去除前面所有的空格
    while (str.charAt(0) == ' ') {
        str = str.substring(1, str.length);
    }

    // 去除后面的空格
    while (str.charAt(str.length - 1) == ' ') {
        str = str.substring(0, str.length - 1);
    }

    return str;
}

function IsImage(fileURL, isAlert) {
    //本程序用来验证后缀，如果还有其它格式，可以添加在right_type;
    var right_type = new Array(".gif", ".jpg", ".jpeg", ".png");
    var right_typeLen = right_type.length;
    var imgUrl = fileURL.toLowerCase();
    var postfixLen = imgUrl.length;
    var len4 = imgUrl.substring(postfixLen - 4, postfixLen);
    var len5 = imgUrl.substring(postfixLen - 5, postfixLen);
    var isImage = false;
    for (i = 0; i < right_typeLen; i++) {
        if ((len4 == right_type[i]) || (len5 == right_type[i])) {
            isImage = true;
        }
    }
    if (!isImage) {
        if (isAlert == null || isAlert == true) { ShowError(" 您选择图片格式不正确！<br/>支持格式为:gif|jpg|jpeg|png"); }

        //show_dialoginfo(_str_ErrorTitle,"您选择图片格式不正确！",_int_Width,_int_Height)
        return false;
    }
    return true;
}

function IsIE() {
    return document.all && window.external;
}
//是否是加拿大的邮编
function IsCanadaPost(obj) {
    var retPost = /^((([a-zA-Z][0-9]){3}))$/;
    if (retPost.test(obj.value)) {
        obj.value = obj.value.substring(0, 3).toUpperCase() + " " + obj.value.substring(3).toUpperCase();
        return true;
    } else {
        retPost = /^([a-zA-Z][0-9][a-zA-Z][ ][0-9][a-zA-Z][0-9])$/;
        if (retPost.test(obj.value)) {
            obj.value = obj.value.toUpperCase();
            return true;
        }
    }
    return false;
}

function IsCanadaTel(obj) {
    var retTel = /^[1-9][0-9]{9}/;
    if (retTel.test(obj.value)) {
        FormatPhone(obj);
        return true;
    } else {
        retTel = /^[1-9][0-9]{2}-[0-9]{3}-[0-9]{4}/;
        if (retTel.test(obj.value))
            return true;
    }
    return false;
}
//格式化电话号码
function FormatPhone(obj) {
    var valueTel = obj.value;
    valueTel = valueTel.substring(0, 3) + "-" + valueTel.substring(3, 6) + "-" + valueTel.substring(6);
    obj.value = valueTel;
}
//获取IE下图片地址
function getIEPath(obj) {
    if (obj) {
        if (IsIE()) {
            obj.select();
            // IE下取得图片的本地路径
            return document.selection.createRange().text;
        }
        return obj.value;
    }
}
//====================================================================================
//======================================Form表单验证 End==============================
//====================================================================================

//====================================================================================
//===================================弹出窗口 Begin===================================
//====================================================================================
function show_dialog(url, width, height, title) {
    var doch = document.documentElement.clientHeight;
    if (doch == 0) { doch = document.body.clientHeight; }

    var wt = (doch - height) / 2;

    var wl = (document.body.clientWidth - width) / 2;

    if (title == undefined) {
        title = '';
    }


    var wh = document.documentElement.scrollTop;

    //alert(wh);

    var body_width = document.body.clientWidth + 'px';
    var body_height;

    if (document.documentElement.scrollHeight > document.documentElement.clientHeight) {
        body_height = document.documentElement.scrollHeight + wh + 'px';
    }
    else {
        body_height = document.documentElement.clientHeight + wh + 'px';
    }

    //alert(body_width);


    var bg_obj = createDiv("show_bg", "#D5D5D5", "0px", "0px", "absolute", body_width, body_height, "999");
    bg_obj.style.top = '0px';
    bg_obj.style.left = '0px';
    bg_obj.style.filter = 'progid:DXImageTransform.Microsoft.Alpha(opacity=60,finishOpacity=100,style=0)';
    bg_obj.style.opacity = 0.8;
    document.body.appendChild(bg_obj);



    dvs = createDiv("show_dialog", "#ffffff", "0px", "0px", "absolute", "0px", "0px", "10000");
    dvs.style.top = wt + wh + 'px';
    dvs.style.left = wl + 'px';

    dvs.style.width = width + 10;
    dvs.style.height = height + 50;
    dvs.style.textAlign = "center";
    document.body.appendChild(dvs);
    dvs.innerHTML = '<div id="show_dialog_title" style="width:' + width + 'px;" onmousedown="javascript:startDrag(this)" onmouseup="javascript:stopDrag(this)" onmousemove="drag(this)"><div id="show_dialog_title_text" >' + title + '</div>'
	+ '<div id="show_dialog_close" ><a href="javascript:void(0);" onclick="close_dialog()" > 关闭 <img src="/App_Themes/cmsyork/images/dialog_close.gif"  onclick="close_dialog()"/></a></div></div>'
	+ '<div id="show_dialog_content" style="width:' + width + 'px;"><iframe  marginheight="0"  marginwidth="0"  frameborder="0"  scrolling="auto"  width="' + (width) + 'px"  height="' + (height) + 'px" src="' + url + '"></iframe></div>';

}

function show_messageinfo(title, content, width, height) {
    if (document.getElementById('show_bg') != null) {
        return;
    }
    var doch = document.documentElement.clientHeight;
    if (doch == 0) { doch = document.body.clientHeight; }
    var wt = (doch - height) / 2;
    var wl = (document.body.clientWidth - width) / 2;

    var wh = document.documentElement.scrollTop;
    alert(wh);
}


function show_dialoginfo(title, content, width, height) {



    if (document.getElementById("show_bg") != null) {
        return;
    }

    var doch = document.documentElement.clientHeight;
    if (doch == 0) { doch = document.body.clientHeight; }
    var wt = (doch - height) / 2;
    var wl = (document.body.clientWidth - width) / 2;




    var wh = document.documentElement.scrollTop;

    //alert(wh);

    var body_width = document.body.clientWidth + 'px';
    var body_height;

    if (document.documentElement.scrollHeight > document.documentElement.clientHeight) {
        body_height = document.documentElement.scrollHeight + wh + 'px';
    }
    else {
        body_height = document.documentElement.clientHeight + wh + 'px';
    }

    //alert(body_height);


    var bg_obj = createDiv("show_bg", "#D5D5D5", "0px", "0px", "absolute", body_width, body_height, "999");
    bg_obj.style.filter = 'progid:DXImageTransform.Microsoft.Alpha(opacity=60,finishOpacity=100,style=0)';
    bg_obj.style.opacity = 0.8;
    bg_obj.style.top = '0px';
    bg_obj.style.left = '0px';
    document.body.appendChild(bg_obj);





    dvs = createDiv("show_dialog", "#ffffff", "0px", "0px", "absolute", "0px", "0px", "10000");


    dvs.style.top = wt + wh + 'px';
    dvs.style.left = wl + 'px';
    //dvs.style.border = "1px solid #069";

    dvs.style.width = width + 10;
    dvs.style.height = height;

    dvs.style.textAlign = "center";

    document.body.appendChild(dvs);

    dvs.focus();

    //alert(width);

    dvs.innerHTML = '<div id="show_dialog_title" style="width:' + width + 'px;" onmousedown="javascript:startDrag(this)" onmouseup="javascript:stopDrag(this)" onmousemove="drag(this)"><div id="show_dialog_title_text" >' + title + '</div>'


	+ '<div id="show_dialog_close" ><a href="javascript:void(0);" onclick="close_dialog()" > 关闭 <img src="/App_Themes/cmsyork/images/dialog_close.gif"  onclick="close_dialog()"/></a></div></div>'

	+ '<div id="show_dialog_content" style="color:red;width:' + width + 'px;height:' + (height - 80) + 'px;">' + content + '！</div>'

	+ '<div id="show_dialog_buttom" style="width:' + width + 'px;"><input id="show_dialog_btok" style="height:25px;line-height:25px;" type="button" value="确定" onclick="close_dialog();" ></div>';
    try {
        blur
        $("#show_dialog_btok").blur();
        $("#show_dialog_btok").focus(); //2011.05.06 晓举焦点默认到确定按钮上去
    }
    catch (errr)
    { }
}


function reset() {
    window.location = window.location;
}

function close_dialog() {

    try {
        var bg_obj = document.getElementById("show_bg");
        var dvs = document.getElementById("show_dialog");
        document.body.removeChild(bg_obj);
        document.body.removeChild(dvs);
        setTimeout(CurrentFocus, 5);
    }
    catch (e) {

    }

}

function CurrentFocus() {
    try {
        if (CurrentObject != null)// if (CurrentObject != null && CurrentObject.style.Dispaly != "none")
            CurrentObject.focus();
    }
    catch (e) {

    }
}

function createDiv(dvid, bgcolor, lft, tp, pos, wdth, hgt, zindex) {
    var newdv = document.createElement("div");
    if (dvid != "") {
        newdv.id = dvid;
    }
    if (bgcolor != "") {
        newdv.style.backgroundColor = bgcolor;
    }
    if (lft != "") {
        newdv.style.left = lft;
    }
    if (tp != "") {
        newdv.style.top = tp;
    }
    if (pos != "") {
        newdv.style.position = pos;
    }
    if (wdth != "" && wdth != "0px") {
        newdv.style.width = wdth;
    }
    if (hgt != "" && hgt != "0px") {
        newdv.style.height = hgt;
    }
    if (zindex != "") {
        newdv.style.zIndex = zindex;
    }
    return newdv;
}
document.onkeydown = EnterKeyClose;
function EnterKeyClose(event) {
    if (event != null && event != undefined)
        if (event.keyCode == 13) {
            close_dialog();
        }
}



function getEvent() //同时兼容ie和ff的写法 
{
    if (document.all) return window.event;
    func = getEvent.caller;
    while (func != null) {
        var arg0 = func.arguments[0];
        if (arg0) {
            if ((arg0.constructor == Event || arg0.constructor == MouseEvent) || (typeof (arg0) == "object" && arg0.preventDefault && arg0.stopPropagation)) {
                return arg0;
            }
        }
        func = func.caller;
    }
    return null;
}


//可以打包为js文件;   
var x0 = 0, y0 = 0, x1 = 0, y1 = 0;
var offx = 6, offy = 6;
var moveable = false;
var hover = 'orange', normal = '#3366bb'; //color;   
var index = 10000; //z-index;   
//开始拖动;



function startDrag(obj) {
    //锁定标题栏; 
    if (document.all) {
        obj.setCapture();
    }


    //定义对象;   
    var win = obj.parentNode;
    var sha = obj;

    //alert(win);
    //alert(sha);   

    var e = getEvent();
    //记录鼠标和层位置;  
    if (document.all) {
        x0 = e.clientX;
        y0 = e.clientY;
    }
    else {
        x0 = e.pageX;
        y0 = e.pageY;
    }
    x1 = parseInt(win.style.left);
    y1 = parseInt(win.style.top);

    //alert(x1);


    //alert(x0);

    //记录颜色;   
    //normal   =   obj.style.backgroundColor;
    //改变风格;   
    //obj.style.backgroundColor   =   hover;   

    //obj.nextSibling.style.color   =   hover;   
    //sha.style.left   =   x1   +   offx;   
    //sha.style.top   =   y1   +   offy;
    //alert(event.clientX); 
    obj.style.cursor = "move";
    moveable = true;

}
//拖动;   
function drag(obj) {
    var win = obj.parentNode;
    var sha = obj;
    obj.style.cursor = "move";
    //alert(win);

    var e = getEvent();
    //记录鼠标和层位置;

    if (moveable) {

        //alert(win.style.left);

        if (document.all) {
            win.style.left = x1 + e.clientX - x0;
            win.style.top = y1 + e.clientY - y0;
        }
        else {
            win.style.left = (x1 + e.pageX - x0) + "px";
            win.style.top = (y1 + e.pageY - y0) + "px";
            //alert(x1   +   e.pageX   -   x0);
        }
        //sha.style.left   =   parseInt(win.style.left)   +   offx;   
        //sha.style.top   =   parseInt(win.style.top)   +   offy;   
    }
}
//停止拖动;   
function stopDrag(obj) {
    var win = obj.parentNode;
    var sha = obj;
    //obj.style.backgroundColor   =   normal;   
    //obj.nextSibling.style.color   =   normal;   
    //sha.style.left   =   obj.parentNode.style.left;   
    //sha.style.top   =   obj.parentNode.style.top;   
    //放开标题栏;   

    obj.style.cursor = "default";
    if (document.all) {
        obj.releaseCapture();
    }
    moveable = false;
}
//获得焦点;   
function getFocus(obj) {
    index = index + 2;
    var idx = index;
    obj.style.zIndex = idx;
    obj.nextSibling.style.zIndex = idx - 1;
}
//====================================================================================
//===================================弹出窗口 End=====================================
//====================================================================================


//====================================================================================
//======================================选中复选框 Begin==============================
//====================================================================================
function CheckAll(form) {

    for (var i = 0; i < form.elements.length; i++) {
        var e = form.elements[i];

        if (e.type == "checkbox" && e.name != 'chkall' && e.name == 'delid') {
            e.checked = form.chkall.checked;
        }
    }
}

//选择所有复选框
function SelectAll(form) {
    for (var i = 0; i < form.elements.length; i++) {
        var currentElement = form.elements[i];
        if (currentElement.type == "checkbox") {
            if (!currentElement.disabled)
                form.elements[i].checked = "checked";
        }
    }
}
//取消选择所有复选框
function UnSelectAll(form) {
    for (var i = 0; i < form.elements.length; i++) {
        var currentElement = form.elements[i];
        if (currentElement.type == "checkbox") {
            if (form[i].checked) {
                form.elements[i].checked = "";
            }
            else {
                ////                if (!currentElement.disabled) {
                ////                    currentElement.checked = "checked";
                ////                }
            }
        }
    }
}
//判断是否选择
function IsCheckBoxChecked(form) {
    for (var i = 0; i < form.elements.length; i++) {
        if (form.elements[i].type == "checkbox") {
            if (form.elements[i].checked) {
                return true;
            }
        }
    }
    alert("你没有选择任何操作选项");
    return false;
}
//====================================================================================
//======================================选中复选框 End==============================
//====================================================================================

//====================================================================================
//======================================日历控件 Begin================================
//====================================================================================

String.prototype.Format = function () {
    var tmpStr = this;
    var iLen = arguments.length;
    for (var i = 0; i < iLen; i++) {
        tmpStr = tmpStr.replace(new RegExp("\\{" + i + "\\}", "g"), arguments[i]);
    }
    return tmpStr;
}
Calendar = {
    //region Property
    today: new Date(),
    year: 2005,
    month: 8,
    date: 21,
    curPosX: 0,
    curPosY: 0,
    curCapture: null,
    curDay: null,
    //endregion

    //region Method
    display:
		function (o, e, d) {
		    with (Calendar) {
		        o = typeof (o) == "object" ? o : document.getElementById(o);


		        if (window.event) {
		            curPosX = document.body.scrollLeft + event.x;
		            curPosY = document.body.scrollTop + event.y;
		        } else {
		            curPosX = e.pageX;
		            curPosY = e.pageY;
		        }

		        if (o.value == "" && d) o.value = d;
		        with (document.getElementById("Calendar__")) {
		            if (o != curCapture) {
		                curCapture = o;
		                if (style.display == "block") {
		                    style.left = curPosX + "px";
		                    style.top = curPosY + "px";
		                }
		                else load();
		            }
		            else {
		                if (style.display == "block") style.display = "none";
		                else load();
		            }
		        }
		    }
		},
    close:
		function () {
		    with (document.getElementById("Calendar__")) {
		        style.display = "none";
		    }
		},
    load:
		function () {
		    with (Calendar) {
		        curDay = loadDate(curCapture.value);
		        with (curDay) {
		            year = getFullYear();
		            month = getMonth() + 1;
		            date = getDate();
		        }
		        init();
		    }
		},
    init:
		function () {
		    with (Calendar) {
		        with (new Date(year, month - 1, date)) {
		            year = getFullYear();
		            month = getMonth() + 1;
		            date = getDate();
		            setDate(1);
		            var first = getDay();
		            setMonth(getMonth() + 1, 0)
		            paint(first, getDate());
		        }
		    }
		},
    paint:
		function (first, last) {
		    var calendar = document.getElementById("Calendar__");
		    var grid = document.getElementById("dataGrid__");
		    var i, l;
		    l = Math.ceil((first + last) / 7);
		    if (!document.all) {
		        //calendar.style.height = (62 + 19 * Math.ceil((first + last)/7)) + "px";
		    }
		    grid.innerHTML = new Array(l * 7 + 1).join("<li><a></a></li>");
		    with (Calendar) {
		        var strDate = "{0}-{1}".Format(year, month);
		        var isTodayMonth = ((year == today.getFullYear()) && (month == today.getMonth() + 1));
		        var isCurdayMonth = ((year == curDay.getFullYear()) && (month == curDay.getMonth() + 1));
		        var todayDate = today.getDate();
		        for (i = 0; i < last; i++) {
		            grid.childNodes[first + i].innerHTML = '<a href="{2}-{1}"{0} onclick="Calendar.setValue({1});return false">{1}</a>'.Format(((i + 1) == todayDate && isTodayMonth) ? ' class="today"' : isCurdayMonth && (i + 1) == curDay.getDate() ? ' class="curDay"' : '', i + 1, strDate);
		        }
		        document.getElementById("dateText__").innerHTML = '<a href="' + (year - 1) + '年" onclick="Calendar.turn(-12);return false" title="上一年"><<</a> <a href="上一月" onclick="Calendar.turn(-1);return false" title="上一月"><</a> ' + year + " - " + month + ' <a href="下一月" onclick="Calendar.turn(1);return false" title="下一月">></a> <a href="' + (year + 1) + '年" onclick="Calendar.turn(12);return false" title="下一年">>></a>';
		        with (calendar) {
		            style.left = Calendar.curPosX + "px";
		            style.top = Calendar.curPosY + "px";
		            style.display = "block";

		        }
		    }
		},
    turn:
		function (num) {
		    Calendar.month += num;
		    Calendar.date = 1;
		    Calendar.init();
		},
    setValue:
		function (val) {
		    with (Calendar) {
		        curCapture.value = "{0}-{1}-{2}".Format(year, month, val);
		        document.getElementById("Calendar__").style.display = "none";
		    }
		},
    loadDate:
		function (op, formatString) {
		    formatString = formatString || "ymd";
		    var m, year, month, day;
		    switch (formatString) {
		        case "ymd":
		            m = op.match(new RegExp("^((\\d{4})|(\\d{2}))([-./])(\\d{1,2})\\4(\\d{1,2})$"));
		            if (m == null) return new Date();
		            day = m[6];
		            month = m[5] * 1;
		            year = (m[2].length == 4) ? m[2] : GetFullYear(parseInt(m[3], 10));
		            break;
		        case "dmy":
		            m = op.match(new RegExp("^(\\d{1,2})([-./])(\\d{1,2})\\2((\\d{4})|(\\d{2}))$"));
		            if (m == null) return new Date();
		            day = m[1];
		            month = m[3] * 1;
		            year = (m[5].length == 4) ? m[5] : GetFullYear(parseInt(m[6], 10));
		            break;
		        default:
		            break;
		    }
		    if (!parseInt(month)) return new Date();
		    month = month == 0 ? 12 : month;
		    var date = new Date(year, month - 1, day);
		    return (typeof (date) == "object" && year == date.getFullYear() && month == (date.getMonth() + 1) && day == date.getDate()) ? date : new Date();
		    function GetFullYear(y) { return ((y < 30 ? "20" : "19") + y) | 0; }
		},
    toString: function () { return true; }
    //endregion
}
var __calendar_html = "<style>";
__calendar_html += "#Calendar__ {background-color:#F9F7F7;width:155px;position:absolute;display:none;padding-bottom:1px;text-align:center;}";
__calendar_html += "#Calendar__ ul{list-style-type:none;}";
__calendar_html += "#Calendar__ ul li{display:block;width:20px;margin:1px;background-color:#fff;text-align:center;float:left;font:11px;font-family: \"宋体\", serif,Arial, Helvetica, sans-serif;}";
__calendar_html += "#Calendar__ ul li a{height:20px;display:block;background-color:#fff;line-height:20px;text-decoration:none;color:#333}";
__calendar_html += "#Calendar__ ul li a:hover{background:#336699;color:#FFF}";
__calendar_html += "#Calendar__ #dateText__{font:12px;text-align:center;height:20px;padding-top:5px;color:Green;float:left;width:137px;}";
__calendar_html += "#Calendar__ #dateText__ a{font:12px;text-decoration:none; color:#06c}";
__calendar_html += "#Calendar__ #close__{font:12px;text-align:center;height:20px;padding-top:5px;margin-right:1px;}";
__calendar_html += "#Calendar__ #close__ a{font:12px;text-decoration:none; color:#06c}";
__calendar_html += "#Calendar__ #head__ li a{font:bold 12px;clear:both;}";
__calendar_html += "#Calendar__ #dataGrid__{}";
__calendar_html += "#Calendar__ #dataGrid__ li a:hover{background:#dedede url(/plus/calendar/check.gif) right bottom no-repeat;color:red}";
__calendar_html += "#Calendar__ #dataGrid__ .today{background:url(/plus/calendar/today.gif) center no-repeat;color:Red;}";
__calendar_html += "#Calendar__ #dataGrid__ .curDay{background:#dedede url(/plus/calendar/check.gif) right bottom no-repeat;color:Green;}";
__calendar_html += "</style>";
__calendar_html += "<div id=\"Calendar__\">";
__calendar_html += "<div id=\"dateText__\"></div><div id=\"close__\" style=\"float:right;\"><a href=\"#\"><img src=\"/App_Themes/cmsyork/images/date_close.gif\" onclick=\"Calendar.close()\"></a>&nbsp;</div>";
__calendar_html += "<ul id=\"head__\" onclick=\"return false\">";
__calendar_html += "<li><a href=\"#\">日</a></li><li><a href=\"#\">一</a></li><li><a href=\"#\">二</a></li><li><a href=\"#\">三</a></li><li><a href=\"#\">四</a></li><li><a href=\"#\">五</a></li><li><a href=\"#\">六</a></li>";
__calendar_html += "</ul>";
__calendar_html += "<ul id=\"dataGrid__\"></ul>";
__calendar_html += "</div>";
document.write(__calendar_html);
//====================================================================================
//======================================日历控件 End==================================
//====================================================================================

//====================================================================================
//======================================获取页面url的参数  Begin======================
//====================================================================================
UrlParm = function () { // url参数   
    var data, index;
    (function init() {
        data = [];
        index = {};

        var u = window.location.search.substr(1);

        if (u != '') {
            var parms = decodeURIComponent(u).split('&');
            for (var i = 0, len = parms.length; i < len; i++) {
                if (parms[i] != '') {
                    var p = parms[i].split("=");
                    if (p.length == 1 || (p.length == 2 && p[1] == '')) {// p | p=   
                        data.push(['']);
                        index[p[0]] = data.length - 1;
                    } else if (typeof (p[0]) == 'undefined' || p[0] == '') { // =c | =   
                        data[0] = [p[1]];
                    } else if (typeof (index[p[0]]) == 'undefined') { // c=aaa   
                        data.push([p[1]]);
                        index[p[0]] = data.length - 1;
                    } else {// c=aaa   
                        data[index[p[0]]].push(p[1]);
                    }
                }
            }
        }
    })();
    return {
        // 获得参数,类似request.getParameter()   
        parm: function (o) { // o: 参数名或者参数次序   
            try {
                return (typeof (o) == 'number' ? data[o][0] : data[index[o]][0]);
            } catch (e) {
            }
        },
        //获得参数组, 类似request.getParameterValues()   
        parmValues: function (o) { //  o: 参数名或者参数次序   
            try {
                return (typeof (o) == 'number' ? data[o] : data[index[o]]);
            } catch (e) { }
        },
        //是否含有parmName参数   
        hasParm: function (parmName) {
            return typeof (parmName) == 'string' ? typeof (index[parmName]) != 'undefined' : false;
        },
        // 获得参数Map ,类似request.getParameterMap()   
        parmMap: function () {
            var map = {};
            try {
                for (var p in index) { map[p] = data[index[p]]; }
            } catch (e) { }
            return map;
        }
    }
} ();

//====================================================================================
//======================================获取页面url的参数  End  ======================
//====================================================================================
//===========兼容IE与Firefox的js 复制代码 Begin=================//
copy2Clipboard = function (txt) {
    if (window.clipboardData) {
        window.clipboardData.clearData();
        window.clipboardData.setData("Text", txt);
    }
    else if (navigator.userAgent.indexOf("Opera") != -1) {
        window.location = txt;
    }
    else if (window.netscape) {
        try {
            netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
        }
        catch (e) {
            alert("您的firefox安全限制限制您进行剪贴板操作，请打开'about:config'将signed.applets.codebase_principal_support'设置为true'之后重试，相对路径为firefox根目录/greprefs/all.js");
            return false;
        }
        var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
        if (!clip) return;
        var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
        if (!trans) return;
        trans.addDataFlavor('text/unicode');
        var str = new Object();
        var len = new Object();
        var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
        var copytext = txt; str.data = copytext;
        trans.setTransferData("text/unicode", str, copytext.length * 2);
        var clipid = Components.interfaces.nsIClipboard;
        if (!clip) return false;
        clip.setData(trans, null, clipid.kGlobalClipboard);
    }
}
//===========兼容IE与Firefox的js 复制代码  End=================//
//==================JavaScript判断浏览器类型及版本 Begin===========================================//
function navigator_check() {
    var Sys = {};
    var ua = navigator.userAgent.toLowerCase();
    var s;
    (s = ua.match(/msie ([\d.]+)/)) ? Sys.ie = s[1] :
        (s = ua.match(/firefox\/([\d.]+)/)) ? Sys.firefox = s[1] :
        (s = ua.match(/chrome\/([\d.]+)/)) ? Sys.chrome = s[1] :
        (s = ua.match(/opera.([\d.]+)/)) ? Sys.opera = s[1] :
        (s = ua.match(/maxthon\/.([\d.]+)/)) ? Sys.maxthon = s[1] :
        (s = ua.match(/version\/([\d.]+).*safari/)) ? Sys.safari = s[1] : 0;

    //document.title = ua;

    //以下进行测试
    //if (Sys.ie) document.write('IE: ' + Sys.ie);
    //if (Sys.firefox) document.write('Firefox: ' + Sys.firefox);
    // if (Sys.chrome) document.write('Chrome: ' + Sys.chrome);
    //if (Sys.opera) document.write('Opera: ' + Sys.opera);
    //if (Sys.safari) document.write('Safari: ' + Sys.safari);
    //document.write(ua);
    return Sys;
}
//==================JavaScript判断浏览器类型及版本 End===========================================//