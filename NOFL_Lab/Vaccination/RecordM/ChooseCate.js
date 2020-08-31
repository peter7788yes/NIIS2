$(function () {
    $(document).on("click", "#closeBtn", function (e) {
        window.close();
        e.preventDefault();
        return false;
    });
});


var popUpWindow = function (url, title, w, h) {
    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
}

var openWindowWithPost = function (url, title, w, h, keys, values) {
    var newWindow = popUpWindow(url, title, w, h);
    if (!newWindow) return false;
    var html = "";
    html += "<html><head></head><body><form id='formid' method='post' action='" + url + "'>";
    keys = keys || [];
    values = values || [];
    if (keys && values && (keys.length == values.length))
        for (var i = 0; i < keys.length; i++)
            html += "<input type='hidden' name='" + keys[i] + "' value='" + values[i] + "'/>";
    html += "</form><script type='text/javascript'>document.getElementById(\"formid\").submit()</script></body></html>";
    newWindow.document.write(html);
    return newWindow;
}


//var getQueryVariable = function (variable) {
//    var query = window.location.search.substring(1);
//    var vars = query.split("&");
//    for (var i = 0; i < vars.length; i++) {
//        var pair = vars[i].split("=");
//        if (pair[0] == variable) {
//            return pair[1];
//        }
//    }
//}

var goHealth = function ()
{
    var keys = [];
    var values = [];
    keys[0] = "c";
    keys[1] = "i";
    keys[2] = "r";
    keys[3] = "a";
    values[0] = CC;
    values[1] = II;
    values[2] = RR;
    values[3] = AA;
    openWindowWithPost("/Vaccination/RecordM/ApplyHealth.aspx", "ApplyHealth", 920, 450, keys, values);

    //var iValue = getQueryVariable("i");
    //popUpWindow("/Vaccination/RecordM/ApplyHealth.aspx?i=1", "ApplyHealth", 920,450)
}

var goRecord = function () {

    var keys = [];
    var values = [];
    keys[0] = "c";
    keys[1] = "i";
    keys[2] = "r";
    keys[3] = "a";
    values[0] = CC;
    values[1] = II;
    values[2] = RR;
    values[3] = AA;
    openWindowWithPost("/Vaccination/RecordM/ApplyRecord.aspx", "ApplyRecord", 930, 330, keys, values);

    //var iValue = getQueryVariable("i");
    //popUpWindow("/Vaccination/RecordM/ApplyRecord.aspx?i=1", "ApplyRecord", 930, 330)
}

var goEffect = function () {

    var keys = [];
    var values = [];
    keys[0] = "c";
    keys[1] = "i";
    keys[2] = "r";
    keys[3] = "a";
    values[0] = CC;
    values[1] = II;
    values[2] = RR;
    values[3] = AA;
    openWindowWithPost("/Vaccination/RecordM/ApplyEffect.aspx", "ApplyEffect", 930, 630, keys, values);

    //var iValue = getQueryVariable("i");
    //popUpWindow("/Vaccination/RecordM/ApplyEffect.aspx?i=1", "ApplyEffect", 930, 630)
}