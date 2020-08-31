$(function () {
   // $("img.lazy").lazyload();

    $(document).on("click", "#addBtn", function (e) {
        location.href = "/Vaccination/ParameterM/LocationSetting_Add.aspx";
        e.preventDefault();
        return false;
    });
});

angular.module("MyApp", ["PageM", "cfp.hotkeys"])
         .controller("MyController", ["$scope", "PageProvider", "$http", "hotkeys", function ($scope, PageProvider, $http, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             //$scope.TM.isHide = true;
             $scope.VM = {};
             //$scope.VM.location = "";
             //$scope.VM.locationID = 0;
             $scope.VM.locationObj = {};
             $scope.VM.locationObj.text = "";
             $scope.VM.locationObj.id = 0;
             $scope.VM.agencyObj = {};
             $scope.VM.agencyObj.AN = "";
             $scope.VM.agencyObj.I =  0;

             //$scope.VM.agency = "";
             //$scope.VM.agencyID = 0;
             $scope.VM.title = "";


             $scope.VM.publishStateAry = [{ EV: '0', EN: "請選擇" }].concat(AgState);
             $scope.VM.CountyAry = [{ I: '0', N: "全部" }];
             $scope.VM.TownAry = [{ I: '0', N: "全部" }];
             $scope.VM.VillageAry = [{ I: "0", N: "全部" }];

             $scope.VM.publishState = "0";
             $scope.VM.SelectCounty = "0";
             $scope.VM.SelectTown = "0";
             $scope.VM.SelectVillage = "0";
           

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
                        $scope.VM.CountyAry = [{ I: '0', N: "全部" }].concat(data);
                        $scope.VM.SelectCounty = "0";
                    }
                    else {
                        $scope.VM.CountyAry = [{ I: '0', N: "全部" }];
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
                     url: "/Ashx/SystemAreaCodeOP.ashx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
                .success(function (data, status, headers, config) {
                    data = data || [];
                    if (data.length > 0) {
                        $scope.VM.TownAry = [{ I: '0', N: "全部" }].concat(data);;
                        $scope.VM.SelectTown = "0";
                    }
                    else {
                        $scope.VM.TownAry = [{ I: '0', N: "全部" }];
                        $scope.VM.SelectTown = "0";
                    }

                    $scope.VM.VillageAry = [{ I: "0", N: "全部" }];
                    $scope.VM.SelectVillage = "0";
                })
                .error(function (data, status, headers, config) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });
                
             };


             $scope.SelectTownChange = function () {
                 var postData = {};
                 postData["a"] = "Village";
                 postData["p"] = $("#SelectTown").val();


                 $http({
                     method: 'POST',
                     url: "/Ashx/SystemAreaCodeOP.ashx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
               .success(function (data, status, headers, config) {
                   data = data || [];
                   if (data.length > 0) {
                       $scope.VM.VillageAry = [{ I: "0", N: "全部" }].concat(data);

                   }
                   else {
                       $scope.VM.VillageAry = [{ I: "0", N: "全部" }];

                   }
                   $scope.VM.SelectVillage = "0";
               })
               .error(function (data, status, headers, config) {
                   // called asynchronously if an error occurs
                   // or server returns response with an error status.
               });

                
             };

             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();

                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};
              
                 postData["oid"] = $scope.VM.locationObj.id;
                 postData["ac"] = $scope.VM.SelectCounty;
                 postData["at"] = $scope.VM.SelectTown;
                 postData["av"] = $scope.VM.SelectVillage;
                 postData["an"] = $scope.VM.agencyObj.AN;
                 postData["as"] = $scope.VM.publishState;

                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("/Vaccination/ParameterM/LocationSettingOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };

             $scope.changePage(1);

             hotkeys.add({
                 combo: 'enter',
                 description: 'Description goes here',
                 allowIn: ['INPUT', "BUTTON", 'SELECT', 'TEXTAREA'],
                 callback: function (event, hotkey) {
                     var focusType = document.activeElement.type;
                     if (focusType == "text" || focusType == "select-one") {
                         $scope.changePage(1);
                     }
                 }
             });

             $scope.openOrgs = function (record) {
                 popUpWindow("/SelectSingleOrg.aspx", "SelectSingleOrg", 620, 450);
           
             };

             $scope.openSelectAgency = function (record) {
                 popUpWindow("/Vaccination/ParameterM/LocationSetting_SelectAgency.aspx", "SelectAgency", 930, 450);

             };


             $scope.goUpdate = function (record) {
                 location.href = "/Vaccination/ParameterM/LocationSetting_Update.aspx?i=" + record["I"];
             };

           
             
             var popUpWindow = function (url, title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }
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

