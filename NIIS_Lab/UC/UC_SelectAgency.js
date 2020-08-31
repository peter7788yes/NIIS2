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
             $scope.VM.Vaccine = "";
             $scope.VM.VaccineIDs = "";
             $scope.VM.AN = "";

             $scope.VM.CountyAry = [{ I: "0", N: "全部縣市" }];
             $scope.VM.SelectCounty = "0";

             $scope.VM.TownAry = [{ I: "0", N: "全部鄉鎮市區" }];
             $scope.VM.SelectTown = "0";


             $scope.BindCounty = function () {
                 var postData = {};
                 postData["a"] = "County";
                 postData["o"] = OrgID;

                 $http({
                     method: 'POST',
                     url: "/Ashx/SystemAreaCodeOP.ashx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
                .success(function (data, status, headers, config) {
                    data = data || [];
                    if (data.length == 1) {
                        $scope.VM.CountyAry = data;
                        $scope.VM.SelectCounty = data[0].I.toString();
                        $scope.SelectDefaultCounty(data[0].I.toString());
                    }
                    else if (data.length > 1) {
                        $scope.VM.CountyAry = [{ I: "0", N: "全部縣市" }].concat(data);
                        $scope.VM.SelectCounty = "0";
                    }
                    else {
                        $scope.VM.CountyAry = [{ I: "-1", N: "無可選縣市" }].concat(data);
                        $scope.VM.SelectCounty = "-1";
                    }
                })
                .error(function (data, status, headers, config) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });
             };

             $scope.SelectDefaultCounty = function (county) {

                 var postData = {};
                 postData["a"] = "Town";
                 postData["p"] = county;
                 postData["o"] = OrgID;
                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/Ashx/SystemAreaCodeOP.ashx",
                     data: postData
                 })
                    .done(function (data) {
                        data = data || [];
                        setTimeout(function () {
                            $scope.$apply(function () {
                                if (data.length == 1) {
                                    $scope.VM.TownAry = data;
                                    $scope.VM.SelectTown = data[0].I.toString();
                                }
                                else if (data.length > 1) {
                                    $scope.VM.TownAry = [{ I: "0", N: "全部鄉鎮市區" }].concat(data);
                                    $scope.VM.SelectTown = "0";
                                }
                            });
                        }, 1);



                    })
                    .fail(function (jqXHR, textStatus) {

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
                        $scope.VM.TownAry = [{ I: "0", N: "全部鄉鎮市區" }].concat(data);
                    }
                    else {
                        $scope.VM.TownAry = [{ I: "0", N: "全部鄉鎮市區" }];
                    }
                    $scope.VM.SelectTown = "0";
                    $scope.VM.SelectVillage = "0";
                })
                .error(function (data, status, headers, config) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });
             };

             $("#tmBlock").show();
             $scope.PM.changePage("", tbData, function (data) {
                 $scope.TM.data = data.message;
                 history.replaceState({ "pageIndex": 1 }, document.title, location.href);
             });
             //$scope.changePage(1);
             $scope.changePage = function (pageIndex) {
                 //$("#pmBlock").show();
                 //$("#tmBlock").show();

                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};

                 var postData = {};
                 postData["an"] = $scope.VM.AN;
                 postData["ac"] = $scope.VM.SelectCounty;
                 postData["at"] = $scope.VM.SelectTown;
                 postData["p"] = p;
                 postData["as"] = agencyState;
                 postData["hf"] = hasFilter;
                 //postData["addlv4"] = AddOrgLevel4;

                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("/UC/UC_SelectAgencyOP.aspx", postData, function (data) {
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


             $scope.close = function (record) {
                 var rtn = {};
                 rtn["I"] = record.I;
                 rtn["AN"] = record.AN;
                 window.opener.getAgencyUC(rtn);
                 window.close();
             };

             var popUpWindow = function (url, title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }
         }]);


