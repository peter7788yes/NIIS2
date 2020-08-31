$(function () {




    $(document).on("click", ".AddVac,.tbVac", function (e) {
        var iWidth = 620;  //視窗的寬度;
        var iHeight = 508; //視窗的高度;
        var iTop = (window.screen.availHeight - 30 - iHeight) / 2;  //視窗的垂直位置;
        var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;   //視窗的水平位置;
        window.open("/CaseVisit/ChooseVacList.aspx?" + "hdid=" + HFID + "&tbid=" + VCID, "ChooseVacList", "width=620,height=508,top=" + iTop + ",left=" + iLeft + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes");

    });
    $(document).on("click", "#btnBack", function (e) {
        history.go(-1);

    });

    $(document).on("click", ".btnSave", function (e) {
        $(".VisitResult").change();
    });


    $(document).on("click", ".delFile", function (e) {
        //
        var imgo = $(this);

        var fileid = $(this).attr("id").replace("img_del_", "");

        $.ajax({
            cache: false,
            type: "POST",
            url: "/CaseVisit/VisitCaseContentOP.aspx",
            data: { 'action': 'DelVisitFile', 'VisitFileID': fileid },
            async: false
        })
        .done(function (response) {
            var reply = eval(response);
            if (reply.RetCode == '1') {
                $(imgo).parent().html('');
            }

        });
    });

     

//    $(".tbVac").change(function () {
//  //     _postback();
//    });



    $(document).on("change", ".VisitResult", function (e) {



    });



});

var UpdateVal = function (code) { 
    $("#" + HFID).val(code.VacIDs);
    $("#" + VCID).val(code.VacCodes);
    $(".tbVac").change();
};