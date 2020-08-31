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
             $scope.VM.locationObj = [];
             $scope.VM.agencyObj = {};
             $scope.VM.agencyObj.AN = "";
             $scope.VM.agencyObj.I = 0;
             $scope.openOrgs = function (record) {
                 popUpWindow("/SelectOrgs.aspx", "SelectOrgs", 820, 450);

             };

             $scope.openSelectAgency = function (record) {
                 popUpWindow("/Report/VaccinationM/VaccinationDetail_SelectAgency.aspx", "SelectAgency", 930, 450);

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
                 var isChecked=$("#rd1").prop('checked');
                 values[1] = isChecked == true ? 1:2;
                 openWindowWithPost("/Report/VaccinationM/VaccinationDetail_Print.aspx", "VaccinationDetail_Print", 930, 630, keys, values);
             };
}]);


var getCodes = function (code) {
    var TextAry = [];
    $.each(code, function (index,item) {
        TextAry=TextAry.concat(item.TextAry);
    });
    var element = document.querySelector('#tbLocation');
    element.value = TextAry;
    element.focus();
    var controllerElement = document.querySelector('section');
    var controllerScope = angular.element(controllerElement).scope();
    controllerScope.$apply(function () {
        controllerScope.VM.locationObj = code;
    });
};

var getAgency = function (code) {
    var element = document.querySelector('#tbAgency');
    element.value = code.AN;
    element.focus();
    var controllerElement = document.querySelector('section');
    var controllerScope = angular.element(controllerElement).scope();
    controllerScope.$apply(function () {
        controllerScope.VM.agencyObj = code;
    });
};