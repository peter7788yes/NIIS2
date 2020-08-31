
function checkTabsHeight() {
    $('.tab').css('height', $($('.here').children()[0]).height() + $($('.here').children()[1]).height());
}

$(function () {




    var OrgsMenuOrigin = sessionStorage.getItem("OrgsMenuOrigin");

    if (OrgsMenuOrigin == data) {
        var myHtml = sessionStorage.getItem("OrgsMenu", data);
        $("#ulRoot").html(myHtml);
        $("a").removeClass("here");
        $("li").removeClass("hereblock");
    }
    else {

        $.each(JSON.parse(data), function (index, item) {

            if (item.P == JSON.parse(data)[0].P) {
                $("#ulRoot").append('<li id="x' + item.I + '"><a href="javascript:void(0);"><input id="cbx' + item.I + '" type="checkbox" class="cbx" value="' + item.I + '"/>' + item.O + '</a></li>');
            }
            else {
                var parent = $("#x" + item.P + " ul");
                if (parent.length == 0) {

                    $("#x" + item.P).append('<ul class="line"></ul>');
                }
                $("#x" + item.P + " ul:first").append('<li id="x' + item.I + '"><a href="javascript:void(0);"><input id="cbx' + item.I + '" type="checkbox" class="cbx" value="' + item.I + '"/>' + item.O + '</a></li>');

            }
        });

        $.each($("li"), function (index, item) {
            if ($(item).children('ul').length != 0) {
                $(item).addClass('less');
            }
        });
    }


    sessionStorage.setItem("OrgsMenuOrigin", data);
    sessionStorage.setItem("OrgsMenu", $("#ulRoot").html());


    $(document).on("click", ".cbs", function (e) {
        e.preventDefault();
        return false;
    });

    $(document).on("click", ".cbx", function (e) {
        e.preventDefault();
        return false;
    });


    $(document).on("click", "#div1 a", function (e) {
        var hasChecked = $(this).find('input:first').prop("checked");
        if (hasChecked == false) {
            $(this).find('input:first').prop("checked", true);
        }
        else {
            $(this).find('input:first').prop("checked", false);
        }
        e.preventDefault();
        return false;
    });

    $(document).on("click", "#ulRoot a", function (e) {
        //alert(1);
        var hasChecked = $(this).find('input:first').prop("checked");
        if (hasChecked == false) {
            $(this).find('input:first').prop("checked", true);
        }
        else {
            $(this).find('input:first').prop("checked", false);
        }
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


    $('#tab2TableDiv').css('margin', '0px');
    checkTabsHeight();

    $(document).on("click", "input:checkbox", function (e) {
        e.stopImmediatePropagation();
        e.preventDefault();
        return false;
    });



    $(document).on("click", "#selectAllA", function (e) {
        $(".cbs").prop("checked", true);
        e.preventDefault();
        return false;
    });

    $(document).on("click", "#cancelBtn1", function (e) {
        $(".cbs").prop("checked", false);
        e.preventDefault();
        return false;
    });

    $(document).on("click", "#selectAllB", function (e) {
        $(".cbx").prop("checked", true);
        e.preventDefault();
        return false;
    });

    $(document).on("click", "#cancelBtn2", function (e) {
        $(".cbx").prop("checked", false);
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


    $(document).on("click", "#saveBtn1,#saveBtn2", function (e) {
        var rtn = [];
        var RoleVM1 = {};
        var RoleVM2 = {};
        RoleVM1["RoleType"] = 1;
        RoleVM1["RoleIDs"] = [];
        RoleVM1["TextAry"] = [];

        RoleVM2["RoleType"] = 2;
        RoleVM2["RoleIDs"] = [];
        RoleVM2["TextAry"] = [];


        $.each($(".cbs"), function (index, item) {
            if ($(item).prop('checked') == true) {
                RoleVM1["RoleIDs"].push($(item).val());
                RoleVM1["TextAry"].push($("#" + $(item).attr('id')).parent().text().trim());
            }
        });

        $.each($(".cbx"), function (index, item) {
            if ($(item).prop('checked') == true) {
                RoleVM2["RoleIDs"].push($(item).val());
                RoleVM2["TextAry"].push($("#" + $(item).attr('id')).parent().text().trim());
            }
        });

        rtn.push(RoleVM1);
        rtn.push(RoleVM2);

        console.log(JSON.stringify(rtn));

        window.opener.getCodes(rtn);
        window.close();
        e.preventDefault();
        return false;
    });

});
