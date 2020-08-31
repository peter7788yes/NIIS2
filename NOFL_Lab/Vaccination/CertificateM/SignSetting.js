$(function () {
    $(document).on("click", "#clearBtn", function (e) {
        document.querySelector("#tbLocation").value = "";
        document.querySelector("#hfLocationID").value = "";
        document.querySelector("#tbP").value = "";
        document.querySelector("#tbD").value = "";
        document.querySelector("#tbS").value = "";
        document.querySelector("#tbE").value = "";
        document.querySelector("#tbC").value = "";
        e.preventDefault();
        return false;
    });
});

angular.module("MyApp", [])
         .controller("MyController", ["$scope", "$http", function ($scope, $http) {

             $scope.VM = {};

             $scope.openOrgs = function (record) {
                 popUpWindow("/SelectSingleOrg.aspx", "SelectSingleOrg", 930, 450);
           
             };

             $scope.changeData = function () {
                 var postData = {};
                 postData["O"] = document.querySelector('#hfLocationID').value;

                 $http({
                     method: 'POST',
                     url: "/Vaccination/CertificateM/SignSetting_GetDataOP.aspx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
                .success(function (data, status, headers, config) {
                    //console.log(data);
                    document.querySelector('#tbLocation').value = data.N;
                    document.querySelector('#hfLocationID').value = data.O;
                    document.querySelector('#tbP').value = data.P;
                    document.querySelector('#tbD').value = data.D;
                    document.querySelector('#tbS').value = data.S;
                    document.querySelector('#tbE').value = data.E;
                    document.querySelector('#tbC').value = data.C;
                })
                .error(function (data, status, headers, config) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });

             };

             var popUpWindow = function (url,target , title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }
}]);

var getCode = function (code) {
    var element = document.querySelector('#tbLocation');
    element.value = code.text;
    element.focus();
    var element = document.querySelector('#hfLocationID');
    element.value = code.id;

    var controllerElement = document.querySelector('section');
    var controllerScope = angular.element(controllerElement).scope();
    controllerScope.$apply(function () {
        controllerScope.changeData();
    });
};