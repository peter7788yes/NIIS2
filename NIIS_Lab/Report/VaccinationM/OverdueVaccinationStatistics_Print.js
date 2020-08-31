(function () {

    "use strict";

    $(function () {
        $("#divReport").load("/Report/VaccinationM/VaccinationDetail_Print.html", function () {

            if (rt > 1) {
                $("#sec2").show();
            }
            else {
                $("#sec1").show();
            }
        });
    });

})();