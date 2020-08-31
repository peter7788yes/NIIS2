$(function () {

    var tbName = sessionStorage.getItem("tbName");
    var tbDesp = sessionStorage.getItem("tbDesp");
    var rbLevel = sessionStorage.getItem("rbLevel");

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
        location.href = "/System/PowerM/RolePowerSetting.aspx";
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
});

