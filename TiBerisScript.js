var d=document.createElement('div');//Create div element
d.style.marginLeft='auto';//Add to div element styles
d.style.marginRight='auto';
d.style.border='2px solid black';
d.style.width='1169px';
d.style.height='162px';

//Add to div child html elements
d.innerHTML = "<a href='http:www.yandex.by'>" +
"	<img src='https://www.przone.ru/images/programs/ximg1207-588.png.pagespeed.ic.jFu73NGc_U.png' width='154' height='115' alt='lorem'>" +
"</a>"+
"<a href='http:www.google.by'>" +
"	<img src='http://4.bp.blogspot.com/-3c_ysEB34xk/U5IiL0glYmI/AAAAAAAAqjg/w141idwpEL0/s1600/A%2Bque%2Bapunta%2BGoogle%2BPanda%2B4.0%2Bsolonuevas.png' width='154' height='115' alt='lorem'>" +
"</a>";
//document.body.appendChild(d);
var articleDiv = document.querySelector("div.tbs");
articleDiv.appendChild(d);
