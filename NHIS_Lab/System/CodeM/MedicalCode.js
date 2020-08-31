$(function () {
    
    window.addEventListener("popstate", function () {
        var currentState = history.state;
        if (currentState != null) {
            var controllerElement = document.querySelector('section');
            var controllerScope = angular.element(controllerElement).scope();
            controllerScope.$apply(function () {
                controllerScope.changePage(currentState.pageIndex, 1);
            });
        }
    });

    if (location.search.length > 0) {
        setTimeout(function () {
            history.go(-1);
        }, 10);
    }
});

angular.module("MyApp", ["PageM", "cfp.hotkeys"])
         .controller("MyController", ["$scope", "PageProvider", "$http", "hotkeys", function ($scope, PageProvider, $http, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};
             $scope.VM.AN = "";

             $scope.VM.businessStateAry = [].concat(BgState);
             $scope.VM.CountyAry = [];
             $scope.VM.TownAry = [];
             $scope.VM.VillageAry = [];

             $scope.VM.businessState = "0";
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
                        $scope.VM.CountyAry = [].concat(data);
                        $scope.VM.SelectCounty = "0";
                    }
                    else {
                        $scope.VM.CountyAry = [];
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
                        $scope.VM.TownAry = [].concat(data);;
                        $scope.VM.SelectTown = "0";
                    }
                    else {
                        $scope.VM.TownAry = [];
                        $scope.VM.SelectTown = "0";
                    }

                    $scope.VM.VillageAry = [];
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
                       $scope.VM.VillageAry = [].concat(data);

                   }
                   else {
                       $scope.VM.VillageAry = [];

                   }
                   $scope.VM.SelectVillage = "0";
               })
               .error(function (data, status, headers, config) {
                   // called asynchronously if an error occurs
                   // or server returns response with an error status.
               });


             };

             $scope.changePage = function (pageIndex, noHistory) {
                 $("#tmBlock").show();

                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};

                 postData["ac"] = $scope.VM.SelectCounty;
                 postData["at"] = $scope.VM.SelectTown;
                 postData["av"] = $scope.VM.SelectVillage;
                 postData["an"] = $scope.VM.AN;
                 postData["bs"] = $scope.VM.businessState;

                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("/System/CodeM/MedicalCodeOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });

                     if (history.pushState && noHistory == undefined) {
                         if (pageIndex > 1 || location.hash.length > 0) {
                             history.pushState({ "pageIndex": pageIndex }, document.title, location.href.split("?")[0] + "?#h");
                         }
                         else {
                             history.replaceState({ "pageIndex": 1 }, document.title, location.href);
                         }
                     }
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

           

             $scope.goDetail = function (record) {
                 var keys = [];
                 var values = [];
                 keys[0] = 'i';
                 values[0] = record["I"];
                 doPOST("/System/CodeM/MedicalCode_Detail.aspx", keys, values);

             };



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

             var popUpWindow = function (url, title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }
         }]);

