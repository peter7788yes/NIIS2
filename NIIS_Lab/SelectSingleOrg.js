
function checkTabsHeight() {
    $('.tab').css('height', $($('.here').children()[0]).height() + $($('.here').children()[1]).height());
}

$(function () {

    var OrgsMenuOrigin = sessionStorage.getItem("SingleOrgMenuOrigin");

    if (OrgsMenuOrigin == data) {
        var myHtml = sessionStorage.getItem("SingleOrgMenu", data);
        $("#ulRoot").html(myHtml);
        $("a").removeClass("here");
        $("li").removeClass("hereblock");
    }
    else {

        $.each(JSON.parse(data), function (index, item) {

            if (item.P == 0) {
                $("#ulRoot").append('<li id="x' + item.I + '"><a href="javascript:void(0);" data-id="' + item.I + '">' + item.O + '</a></li>');
            }
            else {
                var parent = $("#x" + item.P + " ul");
                if (parent.length == 0) {

                    $("#x" + item.P).append('<ul class="line"></ul>');
                }
                $("#x" + item.P + " ul:first").append('<li id="x' + item.I + '"><a href="javascript:void(0);" data-id="' + item.I + '">' + item.O + '</a></li>');

            }
        });

        $.each($("li"), function (index, item) {
            if ($(item).children('ul').length != 0) {
                $(item).addClass('less');
            }
        });
    }

   
    sessionStorage.setItem("SingleOrgMenuOrigin", data);
    sessionStorage.setItem("SingleOrgMenu", $("#ulRoot").html());


    $(document).on("click", ".orgs", function (e) {
        e.preventDefault();
        return false;
    });


    

    $(document).on("click", "#ulRoot a", function (e) {
        var rtn = {};
        var text = "";
        var id = 0;
        
        rtn["text"] = $(this).text();
        rtn["id"] = $(this).attr('data-id');

        window.opener.getCode(rtn);
        window.close();
        e.preventDefault();
        return false;
    });

    $(document).on("click", ".add", function (e) {
        $(this).removeClass("add");
        $(this).addClass("less");
        $(this).children("ul:first").removeClass("hidden");
        e.preventDefault();
        return false;
    });

    $(document).on("click", ".less", function (e) {
        $(this).removeClass("less");
        $(this).addClass("add");
        $(this).children("ul:first").addClass("hidden");
        e.preventDefault();
        return false;
    });


    $('#tab2TableDiv').css('margin','0px');
    checkTabsHeight();

 

   
    
    $(document).on("click", "#selectAllB", function (e) {
        $(".cbx").prop("checked", true);
        e.preventDefault();
        return false;
    });

   
    $(document).on("click", "#selectAllB", function (e) {
        $(".cbx").prop("checked", true);
        e.preventDefault();
        return false;
    });



    $(document).on("click", ".tabBtn", function (e) {
        $('.tabBtn').removeClass("here");
        $(this).addClass("here");
        var AorB = $(this).attr("data-tab");
        $(".tabs").hide();
        $("#tab" + AorB).show();

        checkTabsHeight();
        e.preventDefault();
        return false;
    });
});
