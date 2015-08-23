//二个标签
var tID=2;
function ShowTab(ID){
 if(ID!=tID){
  document.getElementById("Tab"+tID).className='';
  document.getElementById("Tab"+ID).className="js_on";
  document.getElementById("box"+tID).style.display='none';
  document.getElementById("box"+ID).style.display='';
  tID=ID;
 }
}
