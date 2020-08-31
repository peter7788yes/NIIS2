$(function () {

    var checkPWD =function(){
        var val1 = $("#tbPWD").val();
        var val2 = $("#tbPWD2").val();
        if (val1 != val2) {
            $("#notPWD").show();
        }
        else
        {
            $("#notPWD").hide();
        }
    };
    $(document).on("blur", "#tbPWD", function (e) {

        checkPWD();

        e.preventDefault();
        return false;
    });

    $(document).on("blur", "#tbPWD2", function (e) {
        checkPWD();

        e.preventDefault();
        return false;
    });

    
   
});

