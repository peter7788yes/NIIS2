$(function () {

    $(document).on("click", "#lastBtn", function (e) {
        location.href = "/Login.aspx";
        e.preventDefault();
        return false;
    });

});