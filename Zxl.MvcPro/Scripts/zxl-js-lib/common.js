/************** 基于UA判断浏览器类型，版本号 *****************/
function getBrowserInfo() {
    
    var type, version;
    var ua = navigator.userAgent.toLowerCase();
    var s;
    (s = ua.match(/msie ([\d.]+)/)) ? (type = "ie", version = s[1]) :
    (s = ua.match(/firefox\/([\d.]+)/)) ? (type = "firefox" , version = s[1]):
    (s = ua.match(/chrome\/([\d.]+)/)) ? (type = "chrome" , version = s[1]):
    (s = ua.match(/opera.([\d.]+)/)) ? (type = "opera" , version = s[1]):
    (s = ua.match(/version\/([\d.]+).*safari/)) ? (type = "safari", version = s[1]) : (type = "unkown", version = "unkown");
    var o = {};
    o.Type = type;
    o.Version = version;
    return o;
} /**************************************************/