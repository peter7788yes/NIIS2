$(function () {

    var hfCateID = sessionStorage.getItem("hfCateID");
    var hfCateName = sessionStorage.getItem("hfCateName");
    var tbName = sessionStorage.getItem("tbName");
    var tbDesp = sessionStorage.getItem("tbDesp");
    var rbLevel = sessionStorage.getItem("rbLevel");

    if (hfCateID != undefined) {
        $("#hfCateID").val(hfCateID);
    }

    $("#ddlCate")[0].value = $("#hfCateID").val();

    if (hfCateName != undefined) {
        $("#hfCateName").val(hfCateName);
    }

    if (tbName != undefined){
        $("#tbName").val(tbName);
    }

    if (tbDesp != undefined) {
        $("#tbDesp").val(tbDesp);
    }

    if (rbLevel != undefined) {
        $("#" + rbLevel).prop("checked",true);
    }

    $(document).on("click", "#lastBtn", function (e) {
        //location.href = "/System/PowerM/RolePowerSetting.aspx";
        history.go(-1);
        e.preventDefault();
        return false;
    });

    $(document).on("submit", "form", function (e) {
        var tbName = $.trim($("#tbName").val());
        var hasRbLevel = $('input:radio:checked').length;
        var alertString = "";

        if (tbName == "" )
        {
            alertString += "角色名稱:必填\n";
        }

        if (hasRbLevel == "0") {
            alertString += "所屬層級:必填";
        }

        if (alertString != "")
        {
            alert(alertString);
            e.preventDefault();
            return false;
        }

       sessionStorage.setItem("tbName", tbName);
       sessionStorage.setItem("tbDesp", $.trim($("#tbDesp").val()));
       
       if (hasRbLevel > 0)
       {
           sessionStorage.setItem("rbLevel", $($('input:radio:checked')[0]).attr("ID"));
       }
    });

   

    $(document).on("change", "#ddlCate", function (e) {
        $("#hfCateID").val($("#ddlCate")[0].value);
        $("#hfCateName").val($("#ddlCate")[0].selectedOptions[0].text);
    });

    $("#hfCateID").val($("#ddlCate")[0].value);
    $("#hfCateName").val($("#ddlCate")[0].selectedOptions[0].text);
});

