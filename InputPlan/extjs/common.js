function getCookie(name)
{
	cookie = " "+document.cookie;
	offset = cookie.indexOf(" "+name+"=");

	if (offset == -1) return null;

	offset += name.length+2;
	end = cookie.indexOf(";", offset)

	if (end == -1) end = cookie.length;
	  return unescape(cookie.substring(offset, end));
}

function setCookie(name, value, expires, path, domain, secure) {
	document.cookie =
	name+"="+escape(value)+
		(expires ? "; expires="+expires.toGMTString() : "")+
		(path    ? "; path="   +path   : "")+
		(domain  ? "; domain=" +domain : "")+
		(secure  ? "; secure" : "");
}

function setCookieLT(name, value, lifetime, path, domain, secure) {
	if (lifetime)
		lifetime = new Date(Date.parse(new Date())+lifetime*1000);
	setCookie(name, value, lifetime, path, domain, secure);
}

function delCookie(name, path, domain) {
	if (getCookie(name)) {
		var date = new Date("January 01, 2000 00:00:01");
		setCookie(name, "", date, path, domain);
	}
}
