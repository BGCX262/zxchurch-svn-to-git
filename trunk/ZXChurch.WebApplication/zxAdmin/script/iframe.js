function iFrameHeight() {   
	var ifm1 = document.getElementById("sidepage");
	var subWeb1 = document.frames ? document.frames["sidepage"].document : ifm1.contentDocument;   
	if(ifm1 != null && subWeb1 != null) {
	   ifm1.height = subWeb1.body.scrollHeight;
	}   
}   

function iFrameHeight2() {   
	var ifm = document.getElementById("iframepage");
	var subWeb = document.frames ? document.frames["iframepage"].document : ifm.contentDocument;   
	if(ifm != null && subWeb != null) {
	   ifm.height = subWeb.body.scrollHeight;
	}   
}   

