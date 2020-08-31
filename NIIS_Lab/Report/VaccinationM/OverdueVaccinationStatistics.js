$(function () {
    $(document).on("click", "#addBtn", function (e) {
        location.href = "/Vaccination/ParameterM/LocationSetting_Add.aspx";
        e.preventDefault();
        return false;
    });
});

angular.module("MyApp", [])
         .controller("MyController", ["$scope", function ($scope) {

             $scope.VM = {};
             $scope.VM.locationObj = {};

             $scope.openOrgs = function (record) {
                 popUpWindow("/SelectSingleOrg.aspx", "SelectSingleOrg", 620, 450);

             };

             var popUpWindow = function (url, title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }

             var openWindowWithPost = function (url, title, w, h, keys, values) {
                 var newWindow = popUpWindow(url, title, w, h);
                 if (!newWindow) return false;
                 var html = "";
                 html += "<html><head></head><body><form id='formid' method='post' action='" + url + "' autocomplete='off'>";
                 keys = keys || [];
                 values = values || [];
                 if (keys && values && (keys.length == values.length))
                     for (var i = 0; i < keys.length; i++)
                         html += "<input type='hidden' name='" + keys[i] + "' value='" + values[i] + "'/>";
                 html += "</form><script type='text/javascript'>document.getElementById(\"formid\").submit()</script></body></html>";
                 newWindow.document.write(html);
                 return newWindow;
             }


             var doPOST = function (url, keys, values) {
                 keys = keys || [];
                 values = values || [];
                 var html = "";
                 html += "<html><head></head><body><form id='formid' method='post' action='" + url + "' autocomplete='off'>";
                 if (keys && values && (keys.length == values.length))
                     for (var i = 0; i < keys.length; i++)
                         html += "<input type='hidden' name='" + keys[i] + "' value='" + values[i] + "'/>";
                 html += "</form><script type='text/javascript'>document.getElementById(\"formid\").submit()</script></body></html>";
                 //console.log(html);
                 document.write(html);
             };

             $scope.goPrint = function () {
                 var keys = [];
                 var values = [];
                 keys[0] = '1';
                 values[0] = 1;
                 keys[1] = 'rt';
                 var isChecked = $("#rd1").prop('checked');
                 values[1] = isChecked == true ? 1 : 2;
                 openWindowWithPost("/Report/VaccinationM/OverdueVaccinationStatistics_Print.aspx", "OverdueVaccinationStatistics", 930, 630, keys, values);
             };

}]);



var getCode = function (code) {
    var element = document.querySelector('#tbLocation');
    element.value = code.text;
    element.focus();
    var controllerElement = document.querySelector('section');
    var controllerScope = angular.element(controllerElement).scope();
    controllerScope.$apply(function () {
        controllerScope.VM.locationObj = code;
    });
};