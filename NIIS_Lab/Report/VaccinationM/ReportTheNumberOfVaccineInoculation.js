$(function () {
    $(document).on("click", "#addBtn", function (e) {
        location.href = "/Vaccination/ParameterM/LocationSetting_Add.aspx";
        e.preventDefault();
        return false;
    });
});

angular.module("MyApp", [])
         .controller("MyController", ["$scope","$http", function ($scope,$http) {

             $scope.VM = {};
             $scope.VM.locationObj = [];


             $scope.VM.CountyAry = [{ I: '0', N: "縣市" }];
             $scope.VM.TownAry = [{ I: '0', N: "鄉鎮" }];

             $scope.VM.SelectCounty = "0";
             $scope.VM.SelectTown = "0";


             $scope.BindCounty = function () {
                 var postData = {};
                 postData["a"] = "County";


                     $http({
                         method: 'POST',
                         url: "/Ashx/SystemAreaCodeOP.ashx",
                         data: $.param(postData),
                         headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                     })
                   .success(function (data, status, headers, config) {
                       data = data || [];
                       if (data.length > 0) {
                           $scope.VM.CountyAry = [{ I: '0', N: "縣市" }].concat(data);
                           $scope.VM.SelectCounty = "0";
                       }
                       else {
                           $scope.VM.CountyAry = [{ I: '0', N: "縣市" }];
                           $scope.VM.SelectCounty = "0";
                       }
                   })
                   .error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                   });
             };

             $scope.BindCounty();



             $scope.SelectCountyChange = function () {
                 var postData = {};
                 postData["a"] = "Town";
                 postData["p"] = $("#SelectCounty").val();


                 $http({
                     method: 'POST',
                     url:  "/Ashx/SystemAreaCodeOP.ashx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                     })
                  .success(function (data, status, headers, config) {
                      data = data || [];
                      if (data.length > 0) {
                          $scope.VM.TownAry = [{ I: '0', N: "鄉鎮" }].concat(data);;
                          $scope.VM.SelectTown = "0";
                      }
                      else {
                          $scope.VM.TownAry = [{ I: '0', N: "鄉鎮" }];
                          $scope.VM.SelectTown = "0";
                      }
                  })
                  .error(function (data, status, headers, config) {
                      // called asynchronously if an error occurs
                      // or server returns response with an error status.
                  });
                 
             };

             $scope.openOrgs = function (record) {
                 popUpWindow("/SelectOrgs.aspx", "SelectOrgs", 820, 450);

             };

             $scope.openSelectAgency = function (record) {
                 popUpWindow("/Vaccination/ParameterM/LocationSetting_SelectAgency.aspx", "SelectAgency", 930, 450);

             };

             var popUpWindow = function (url, title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }



         }]);


var getCodes = function (code) {
    var TextAry = [];
    $.each(code, function (index, item) {
        TextAry = TextAry.concat(item.TextAry);
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