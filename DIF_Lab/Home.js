window.onbeforeunload = function () { return '請由"登出"按鈕，登出系統'; };

var changeMenuPath = function (pathname)
{
    try {
        window.frames["leftFrame"].window.changeMenuPath(pathname);
    }
    catch (e) {

    }
};


if (self != top) {
    window.parent.location.href = '/Home.aspx';
}



