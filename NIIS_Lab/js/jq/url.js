/*! NIIS 2016-05-09 */

window.url=function(){function a(a){return!isNaN(parseFloat(a))&&isFinite(a)}function b(a){return decodeURIComponent(a.replace(/\+/g," "))}return function(c,d){var e=d||window.location.toString();if(!c)return e;c=c.toString(),"//"===e.substring(0,2)?e="http:"+e:1===e.split("://").length&&(e="http://"+e),d=e.split("/");var f={auth:""},g=d[2].split("@");1===g.length?g=g[0].split(":"):(f.auth=g[0],g=g[1].split(":")),f.protocol=d[0],f.hostname=g[0],f.port=g[1]||("https"===f.protocol.split(":")[0].toLowerCase()?"443":"80"),f.pathname=(d.length>3?"/":"")+d.slice(3,d.length).join("/").split("?")[0].split("#")[0];var h=f.pathname;"/"===h.charAt(h.length-1)&&(h=h.substring(0,h.length-1));var i=f.hostname,j=i.split("."),k=h.split("/");if("hostname"===c)return i;if("domain"===c)return/^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$/.test(i)?i:j.slice(-2).join(".");if("sub"===c)return j.slice(0,j.length-2).join(".");if("port"===c)return f.port;if("protocol"===c)return f.protocol.split(":")[0];if("auth"===c)return f.auth;if("user"===c)return f.auth.split(":")[0];if("pass"===c)return f.auth.split(":")[1]||"";if("path"===c)return f.pathname;if("."===c.charAt(0)){if(c=c.substring(1),a(c))return c=parseInt(c,10),j[0>c?j.length+c:c-1]||""}else{if(a(c))return c=parseInt(c,10),k[0>c?k.length+c:c]||"";if("file"===c)return k.slice(-1)[0];if("filename"===c)return k.slice(-1)[0].split(".")[0];if("fileext"===c)return k.slice(-1)[0].split(".")[1]||"";if("?"===c.charAt(0)||"#"===c.charAt(0)){var l=e,m=null;if("?"===c.charAt(0)?l=(l.split("?")[1]||"").split("#")[0]:"#"===c.charAt(0)&&(l=l.split("#")[1]||""),!c.charAt(1))return l?b(l):l;c=c.substring(1),l=l.split("&");for(var n=0,o=l.length;o>n;n++)if(m=l[n].split("="),m[0]===c)return(m[1]?b(m[1]):m[1])||"";return null}}return""}}(),"undefined"!=typeof jQuery&&jQuery.extend({url:function(a,b){return window.url(a,b)}});
//# sourceMappingURL=url.js.map