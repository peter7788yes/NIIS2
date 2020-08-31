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

    if(UP==false)
    {
        $("#SelectCounty").prop("disabled", true);
        $("#SelectTown").prop("disabled", true);
        $("#SelectVillage").prop("disabled", true);
    }
});

angular.module("MyApp", [])
         .controller("MyController", ["$scope","$http", function ($scope,$http) {
             $scope.VM = {};

             $scope.VM.Vaccine = "";
             $scope.VM.VaccineIDs = "";

             $scope.VM.CountyAry = [{ I: '0', N: "縣市" }].concat(countyJson);
             $scope.VM.TownAry = [{ I: '0', N: "鄉鎮" }].concat(townJson);
             $scope.VM.VillageAry = [{ I: "0", N: "村里" }].concat(villageJson);

             $scope.VM.SelectCounty = county;
             $scope.VM.SelectTown = town;
             $scope.VM.SelectVillage = village;;
             

             

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

            

             $scope.openAddVaccine = function (i) {
                 popUpWindow("/Vaccination/ParameterM/LocationSetting_AddVaccine.aspx?i=" + i + "&is=" + $('#hfVaccineIDs').val(), "AddVaccine", "AddVaccine", 910, 700)
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

