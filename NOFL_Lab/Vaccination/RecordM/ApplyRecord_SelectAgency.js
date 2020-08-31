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

angular.module("MyApp", ["PageM"])
         .controller("MyController", ["$scope", "PageProvider","$http", function ($scope, PageProvider, $http) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};
             $scope.VM.Vaccine = "";
             $scope.VM.VaccineIDs = "";
             $scope.VM.AN;

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
                          // called asynchronously if an error occurs
                          // or server returns response with an error status.
                      });
                    
             };

             $scope.changePage = function (pageIndex) {
                 //$("#pmBlock").show();
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);

                 var postData = {};
                 postData.D = $scope.VM.title;
                 postData.p = $scope.VM.publishState;
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("/Vaccination/RecordM/ApplyRecord_SelectAgencyOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };


             $scope.goSearch = function () {

                 var postData = {};
                 postData["an"] = $scope.VM.AN;
                 postData["ac"] = $scope.VM.SelectCounty;
                 postData["at"] = $scope.VM.SelectTown;
                

                 $http({
                     method: 'POST',
                     url: "/Vaccination/RecordM/ApplyRecord_SelectAgencyOP.aspx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
                  .success(function (data, status, headers, config) {
                      $scope.TM.data = data.message;
                  })
                   .error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                   });
             };

             $scope.changePage(1);

             $scope.openOrgs = function () {
                 popUpWindow("/SelectSingleOrg.aspx", "SelectSingleOrg", 930, 450);
           
             };

            

             $scope.refresh = function () {
                 var location = $('#tbLocation').val();
                 var Vaccine = $('#tbVaccine').val();
                 var VaccineIDs = $('#hfVaccineIDs').val();

                 setTimeout(function () {
                     $scope.$apply(function () {
                         $scope.VM.location = location;
                         $scope.VM.Vaccine = Vaccine;
                         $scope.VM.VaccineIDs = VaccineIDs;
                     });
                 }, 1);
             };


             $scope.close = function (record) {
                 var rtn = {};
                 rtn["I"] = record.I;
                 rtn["AN"] = record.AN;
                 window.opener.getAgency(rtn);
                 window.close();
             };

             var popUpWindow = function (url,target , title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }
}]);



var getIds = function (rtn) {
    $("#tbVaccine").val(rtn.text);
    $("#hfVaccineIDs").val(rtn.ids);
    $('#refreshBtn').trigger('click');
};