$(function () {
    $(document).on("click", "#lastBtn", function (e) {
        history.go(-1);
        e.preventDefault();
        return false;
    });

});

