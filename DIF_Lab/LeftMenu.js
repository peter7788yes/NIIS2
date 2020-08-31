$(function () {
    var heightLightAry = [];

    var leftMenuOrigin = sessionStorage.getItem("leftMenuOrigin");

    if (leftMenuOrigin == data) {
        var myHtml = sessionStorage.getItem("leftMenu", data);
        $("#ulRoot").html(myHtml);
        $("span").removeClass("here");
        $("li").removeClass("hereblock");
    }
    else {
        $.each(dataObj, function (index, item) {
            //var myStyle = "";
            if (item.PageUrl != "") {
                //myStyle = "color:blue;";
                heightLightAry.push("x" + item.ID);
            }


            if (item.PID == 0) {
                //$("#ulRoot").append('<li id="x' + item.ID + '"><a href="javascript:void(0);" style="' + myStyle + '"' + ' pageurl="' + item.PageUrl + '">' + item.ModuleName + '</a></li>');
                //$("#ulRoot").append('<li id="x' + item.ID + '"><a href="javascript:void(0);" pageurl="' + item.PageUrl + '">' + item.ModuleName + '</a></li>');
                $("#ulRoot").append('<li id="x' + item.ID + '"><span class="leftReplaceA" pageurl="' + item.PageUrl + '">' + item.ModuleName + '</span></li>');
            }
            else {
                var parent = $("#x" + item.PID + " ul");
                if (parent.length == 0) {

                    $("#x" + item.PID).append('<ul class="line"></ul>');
                }
                //$("#x" + item.PID + " ul:first").append('<li id="x' + item.ID + '"><a href="javascript:void(0);" style="' + myStyle + '"' + ' pageurl="' + item.PageUrl + '">' + item.ModuleName + '</a></li>');
                $("#x" + item.PID + " ul:first").append('<li id="x' + item.ID + '"><span class="leftReplaceA" pageurl="' + item.PageUrl + '">' + item.ModuleName + '</span></li>');

            }
        });

        $.each($("li"), function (index, item) {
            if ($(item).children('ul').length != 0) {
                $(item).addClass('less');
            }
        });
    }

    $.each(heightLightAry, function (index, item) {
        $("#" + item).children('span:first').css("color", "blue");
    });

    //sessionStorage.removeItem("leftMenuOrigin");
    //sessionStorage.removeItem("leftMenu");
    sessionStorage.setItem("leftMenuOrigin", data);
    sessionStorage.setItem("leftMenu", $("#ulRoot").html());



    $(document).on("click", "span", function (e) {
        var self = $(this);
        var url = self.attr("pageurl");
        if (url == "" || url == undefined) {
            return;
        }
        var myHtml = "";
        var liAry = $(this).parents('li');

        try {
            setTimeout(function () {
                if (moving == false) {
                    moving = true;
                    $('html, body').animate({
                        scrollTop: self.offset().top - 200
                    }, 1000, function () {
                        // Animation complete.
                        moving = false;
                    });
                }
            }, 0);

        }
        catch (e) {

        }


        //var liAry = $(this).closest('li

        //$.each(liAry.get().reverse(), function (index, item) {
        //    var PageUrl = $(item).children('span:first').attr("pageurl");
        //    var ModuleName = $(item).children('span:first').text();
        //    var href = PageUrl == "" ? "javascript:void(0);" : PageUrl;
        //    if (index != liAry.length-1)
        //        myHtml += '<a href="' + href + '">' + ModuleName + "</a>";
        //    else
        //        myHtml += ModuleName;
        //});

        //sessionStorage.removeItem("menuPath");
        //var lastPage = sessionStorage.getItem("menuPath");
        //if (lastPage != undefined) {
        //    sessionStorage.setItem("lastMenuPath", lastPage);
        //}

        //sessionStorage.setItem("menuPath", myHtml + '<img id="loading" alt="載入中..." src="/images/loading001.gif" style="display:none;margin-left:5px;" />');
        //window.location.replace('url');
        window.parent.document.getElementById('mainFrame').src = $(this).attr("pageurl");
        $("span").removeClass("here");
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


var moving = false;

var changeMenuPath = function (pathname) {

    pathname = pathname || "";
    var purePathname = pathname.replace(/\//g, "").replace(/\./g, "").replace(/_/g, "").replace(/-/g, "");
    //console.log(purePathname);
    var isFound = false;
    var isLoop = true;

    //console.log(dataObj);
    $.each(dataObj, function (index, item) {

        if (isLoop == true && pathname == item.PageUrl) {
            isFound = true;
            var $myA = $('#x' + item.ID).children('span:first');
            var self = $($myA);
            $("span").removeClass("here");
            $("li").removeClass("hereblock");
            $myA.addClass("here");
            $myA.parents('ul:first').parents('li:first').addClass("hereblock");

            try {
                setTimeout(function () {
                    if (moving == false) {
                        moving = true;
                        $('html, body').animate({
                            scrollTop: self.offset().top - 200
                        }, 1000, function () {
                            // Animation complete.
                            moving = false;
                        });
                    }
                }, 0);
            }
            catch (e) {

            }

            var myHtml = "";
            var liAry = $myA.parents('li');

            $.each(liAry.get().reverse(), function (index2, item2) {
                var PageUrl = $(item2).children('span:first').attr("pageurl");
                var ModuleName = $(item2).children('span:first').text();
                var href = PageUrl == "" ? "javascript:void(0);" : PageUrl;
                if (index2 != liAry.length - 1)
                    //myHtml += '<a href="' + href + '">' + ModuleName + "</a>";
                    myHtml += '<span class="pathReplaceA">' + ModuleName + "</span>";
                else
                    myHtml += ModuleName;
            });

            //console.log(myHtml);

            // remeber last menupath
            try {
                var divElement = window.parent.window.frames["mainFrame"].window.document.getElementsByClassName('path')[0];
                myHtml += '<img id="loading" src="/images/loading001.gif" style="display:none;margin-left:5px;" />'
                //myHtml += '<img id="loading" class="lazy" data-original="/images/loading001.gif"  style="display:none;margin-left:5px;" />'
                divElement.innerHTML = myHtml;
                sessionStorage.setItem("menuPath", myHtml);
                sessionStorage.setItem("menuPathID", item.ID);
            }
            catch (e) {
            }
            isLoop = false;
        }
    });

    if (isFound == false) {
        var pathObjString = sessionStorage.getItem("pathObjString");
        if (pathObjString != null) {
            try {
                var pathObj = JSON.parse(pathObjString);
                if (pathObj[purePathname] == undefined) {
                    var lastPage = sessionStorage.getItem("menuPath");
                    var lastPageID = sessionStorage.getItem("menuPathID");
                    var divElement = window.parent.window.frames["mainFrame"].window.document.getElementsByClassName('path')[0];
                    divElement.innerHTML = lastPage;
                    pathObj[purePathname] = {};
                    pathObj[purePathname].menuPath = lastPage;
                    pathObj[purePathname].menuPathID = lastPageID;
                    sessionStorage.setItem("pathObjString", JSON.stringify(pathObj));

                }
                else {
                    var divElement = window.parent.window.frames["mainFrame"].window.document.getElementsByClassName('path')[0];
                    divElement.innerHTML = pathObj[purePathname].menuPath;
                    var $myA = $('#x' + pathObj[purePathname].menuPathID).children('span:first');
                    $("span").removeClass("here");
                    $("li").removeClass("hereblock");
                    $myA.addClass("here");
                    $myA.parents('ul:first').parents('li:first').addClass("hereblock");
                    var self = $($myA);
                    try {
                        setTimeout(function () {
                            if (moving == false) {
                                moving = true;
                                $('html, body').animate({
                                    scrollTop: self.offset().top - 200
                                }, 1000, function () {
                                    // Animation complete.
                                    moving = false;
                                });
                            }
                        }, 0);
                    }
                    catch (e) {

                    }
                }

            }
            catch (e) {
            }
        }
        else {
            try {
                var pathObj = {};
                var lastPage = sessionStorage.getItem("menuPath");
                var lastPageID = sessionStorage.getItem("menuPathID");
                var divElement = window.parent.window.frames["mainFrame"].window.document.getElementsByClassName('path')[0];
                divElement.innerHTML = lastPage;
                pathObj[purePathname] = {};
                pathObj[purePathname].menuPath = lastPage;
                pathObj[purePathname].menuPathID = lastPageID;
                sessionStorage.setItem("pathObjString", JSON.stringify(pathObj));

            }
            catch (e) {
            }
        }
    }
};

//$(function () {
//    //$("img").lazyload({ effect: "fadeIn" });
//    $("img.lazy").lazyload();
//});