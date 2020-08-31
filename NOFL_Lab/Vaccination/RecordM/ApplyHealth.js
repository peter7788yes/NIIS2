﻿$(function () {
    $(document).on("click", "#closeBtn", function (e) {
        window.close();
        e.preventDefault();
        return false;
    });

    $(document).on("click", "#saveBtn", function (e) {
        var postData = {};
        postData["AD"] = $("#tbDate").val();
        postData["AW"] = $("#cbAllow").prop("checked");
        postData["AU"] = $("#selectUser")[0].value;
        postData["OS"] = $("#tbOther").val();
        postData["RD"] = url('?i');
        postData["AH"] = "";
        var AH = [];
        $.each($(".cbs"), function (index, item) {
            if ($(item).prop("checked") == true) {
                AH.push($(item).val());
            }
        });
        postData["AH"] = AH.toString();
        //console.log(postData);


            $.ajax({
                cache: false,
                type: "POST",
                url: "/Vaccination/RecordM/ApplyHealth_AddOP.aspx",
                data: postData
            })
           .done(function (data) {
               if(data.chk>0)
               {
                   alert('儲存成功');
               }
               else
               {
                   alert('儲存失敗');
               }
           })
           .fail(function (jqXHR, textStatus) {

           });

        e.preventDefault();
        return false;
    });

    $.each(userAry, function (index, item) {
        var $option = $("<option/>");
        $option.val(item.U);
        $option.text(item.N);
        console.log($option);
        $($("#selectUser")[0]).append($option);
    });

    var aryLengh = stateAry.length;
    var aryNumber ="";
    $.each(stateAry.reverse(), function (index, item) {
        switch (aryLengh)
        {
            case 1:
                aryNumber = "一";
                break;
            case 2:
                aryNumber = "二";
                break;
            case 3:
                aryNumber = "三";
                break;
            case 4:
                aryNumber = "四";
                break;
            case 5:
                aryNumber = "五";
                break;
            case 6:
                aryNumber = "六";
                break;
            case 7:
                aryNumber = "七";
                break;
            case 8:
                aryNumber = "八";
                break;
            case 9:
                aryNumber = "九";
                break;
            case 10:
                aryNumber = "十";
                break;

        }
        var $tr = $("<tr/>");
        var $td = $("<td/>");
        var $input = $("<input class='cbs' type='checkbox'/>").val(item.EV);
        var $label = $("<label/>");
        $label.append($input).append(aryNumber + "." + item.EN);
        $td.append($label);
        $tr.append($td);
        $("#tb").prepend($tr);
        aryLengh--;
    });
});

