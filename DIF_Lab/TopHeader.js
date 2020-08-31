document.querySelector("#btnLogout").addEventListener("click", function () {
    window.parent.onbeforeunload = null;
});

//$(function () {
//    //$("img").lazyload({ effect: "fadeIn" });
//    $("img.lazy").lazyload();
//});

(function poll() {
    setTimeout(function () {
        $.ajax({
            type: "POST", url: "/Ashx/GetOnlineUserOP.ashx", success: function (data) {
                $("#lblOnlineUser").text("線上人數: "+data[0].OnlineUserCount+" 人");
            }, dataType: "json", complete: poll
        });
    }, 30000);
})();


