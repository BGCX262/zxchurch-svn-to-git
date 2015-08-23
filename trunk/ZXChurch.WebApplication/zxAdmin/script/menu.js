$(document).ready(function(){
	//页面中的DOM已经装载完成时，执行的代码
	$(".menuBox > h3").click(function(){
		$(".menuBox > div").not($(this).next("div")).each(function(i){
		   $(this).slideUp();
		   $(this).prev("h3").css("background-image", "url('../css/admin/images/collapsed.gif')")
		 }); 
		//找到主菜单项对应的子菜单项
		var ulNode = $(this).next("div");
		ulNode.slideToggle();
		changeIcon($(this));
	});
});

/**
 * 修改主菜单的指示图标
 */
function changeIcon(mainNode) {
	if (mainNode) {
		if (mainNode.css("background-image").indexOf("collapsed.gif") >= 0) {
		    mainNode.css("background-image", "url('../css/admin/images/expanded.gif')");
		} else {
		    mainNode.css("background-image", "url('../css/admin/images/collapsed.gif')");
		}
	}
}
