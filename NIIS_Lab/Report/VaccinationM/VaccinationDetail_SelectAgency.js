$(function () {
    $(document).on("click", "#lastBtn", function (e) {
        location.href = "/Vaccination/ParameterM/LocationSetting.aspx";
        e.preventDefault();
        return false;
    });

    $(document).on("click", "#cancelBtn", function (e) {
        window.close();
        e.preventDefault();
        return false;
    });
});

angular.module("MyApp", ["PageM", "cfp.hotkeys"])
         .controller("MyController", ["$scope", "PageProvider", "$http", "hotkeys", function ($scope, PageProvider, $http, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};
             $scope.VM.Vaccine = "";
             $scope.VM.VaccineIDs = "";
             $scope.VM.AN = "";

             $scope.VM.CountyAry = [{ I: '0', N: "全部" }];
             $scope.VM.TownAry = [{ I: '0', N: "全部" }];

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
                      })
                      .error(function (data, status, headers, config) {
                      });
             };

             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var postData = {};
                 postData["an"] = $scope.VM.AN;
                 postData["ac"] = $scope.VM.SelectCounty;
                 postData["at"] = $scope.VM.SelectTown;


                 $http({
                     method: 'POST',
                     url: "/Report/VaccinationM/VaccinationDetail_SelectAgencyOP.aspx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
                  .success(function (data, status, headers, config) {
                      $scope.TM.data = data.message;
                  })
                   .error(function (data, status, headers, config) {
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

             $scope.changePage(1);

             $scope.openOrgs = function () {
                 popUpWindow("/SelectSingleOrg.aspx", "SelectSingleOrg", 930, 450);
           
             };

             $scope.openAddVaccine = function (i) {
                 popUpWindow("/Vaccination/ParameterM/LocationSetting_AddVaccine.aspx?i=" + i, "AddVaccine", "AddVaccine", 910, 700)
             };

             $scope.close = function (record) {
                 var rtn = {};
                 rtn["I"] = record.I;
                 rtn["AN"] = record.AN;
                 window.opener.getAgency(rtn);
                 window.close();
             };

             var popUpWindow = function (url, title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }
}]);



