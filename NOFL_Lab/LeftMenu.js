$(function () {

    var heightLightAry = [];

    var leftMenuOrigin = sessionStorage.getItem("leftMenuOrigin");

    if (leftMenuOrigin == data) {
        var myHtml=sessionStorage.getItem("leftMenu", data);
        $("#ulRoot").html(myHtml);
        $("a").removeClass("here");
        $("li").removeClass("hereblock");
    }
    else
    {

        $.each(JSON.parse(data), function (index, item) {
            //var myStyle = "";
            if (item.PageUrl != "") {
                //myStyle = "color:blue;";
                heightLightAry.push("x"+item.ID);
            }


            if (item.PID == 0) {
                //$("#ulRoot").append('<li id="x' + item.ID + '"><a href="javascript:void(0);" style="' + myStyle + '"' + ' pageurl="' + item.PageUrl + '">' + item.ModuleName + '</a></li>');
                $("#ulRoot").append('<li id="x' + item.ID + '"><a href="javascript:void(0);" pageurl="' + item.PageUrl + '">' + item.ModuleName + '</a></li>');
            }
            else {
                var parent = $("#x" + item.PID + " ul");
                if (parent.length == 0) {

                    $("#x" + item.PID).append('<ul class="line"></ul>');
                }
                //$("#x" + item.PID + " ul:first").append('<li id="x' + item.ID + '"><a href="javascript:void(0);" style="' + myStyle + '"' + ' pageurl="' + item.PageUrl + '">' + item.ModuleName + '</a></li>');
                $("#x" + item.PID + " ul:first").append('<li id="x' + item.ID + '"><a href="javascript:void(0);"  pageurl="' + item.PageUrl + '">' + item.ModuleName + '</a></li>');

            }
        });

        $.each($("li"), function (index, item) {
            if ($(item).children('ul').length != 0) {
                $(item).addClass('less');
            }
        });
    }

    $.each(heightLightAry, function (index, item) {
        $("#" + item).children('a:first').css("color", "blue");
    });

    //sessionStorage.removeItem("leftMenuOrigin");
    //sessionStorage.removeItem("leftMenu");
    sessionStorage.setItem("leftMenuOrigin", data);
    sessionStorage.setItem("leftMenu", $("#ulRoot").html());



    $(document).on("click", "a", function (e) {

        var myHtml = "";
        var liAry = $(this).parents('li');
        //var liAry = $(this).closest('li');
        $.each(liAry.get().reverse(), function (index, item) {
            var PageUrl = $(item).children('a:first').attr("pageurl");
            var ModuleName = $(item).children('a:first').text();
            var href = PageUrl == "" ? "javascript:void(0);" : PageUrl;
            if (index != liAry.length-1)
                myHtml += '<a href="' + href + '">' + ModuleName + "</a>";
            else
                myHtml += ModuleName;
        });
        //sessionStorage.removeItem("menuPath");
        sessionStorage.setItem("menuPath", myHtml + '<img id="loading" alt="載入中..." src="/images/loading001.gif" style="display:none;margin-left:5px;" />');

        window.parent.document.getElementById('cpIframe').src = $(this).attr("pageurl");
        $("a").removeClass("here");
        $("li").removeClass("hereblock");
        $(this).addClass("here");
        $(this).parents('ul:first').parents('li:first').addClass("hereblock");
        //$(this).closest('ul').closest('li').addClass("hereblock");

       

        e.preventDefault();
        return false;
    });

    $(document).on("click", ".add", function (e) {
        $(this).removeClass("add");
        $(this).addClass("less");
        $(this).children("ul:first").removeClass("hidden");
        sessionStorage.setItem("leftMenu", $("#ulRoot").html());
        e.preventDefault();
        return false;
    });

    $(document).on("click", ".less", function (e) {
        $(this).removeClass("less");
        $(this).addClass("add");
        $(this).children("ul:first").addClass("hidden");
        sessionStorage.setItem("leftMenu", $("#ulRoot").html());
        e.preventDefault();
        return false;
    });

    if (p != "") {
        $("a[pageurl='" + p + "']").trigger("click");
    }
});