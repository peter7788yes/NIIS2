$(function () {
 

});

angular.module("MyApp", ["PageM"])
         .controller("MyController", ["$scope", "PageProvider","$http", function ($scope, PageProvider, $http) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};

             $scope.VM.CountyAry = [{ I: '0', N: "全部" }];
             $scope.VM.SelectCounty = "0";

             $scope.VM.TownAry = [{ I: '0', N: "全部" }];
             $scope.VM.SelectTown = "0";


             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};

                 postData.BirthDateS = $("#BirthDateS").val();
                 postData.BirthDateE = $("#BirthDateE").val();
                 postData.CaseName = $("#CaseName").val();
                 postData.CaseIdNo = $("#CaseIdNo").val();
                 postData.HouseNo = $("#HouseNo").val();

                 postData.ContactName = $("#ContactName").val();
                 postData.ContactIdNo = $("#ContactIdNo").val();
                 postData.ContactBirthDate = $("#ContactBirthDate").val();

                 postData = $scope.PM.filterPageData(pgData, postData);
                 //console.log(postData);
                 $scope.PM.changePage("/Vaccination/CertificateM/PrintCertificate.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };

             $scope.changePage(1);


             $scope.goDetail = function (record) {
                 location.href = "/Vaccination/RecordM/RegisterData_Detail.aspx?i=" + record["I"];
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
                })
                 .error(function (data, status, headers, config) {
                     // called asynchronously if an error occurs
                     // or server returns response with an error status.
                 });
             };



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



             $scope.openOrgs = function (record) {

                 popUpWindow("/SelectSingleOrg.aspx", "SelectSingleOrg", 930, 450);

             };


             $scope.refresh = function () {
                 var location = $('#tbLocation').val();
                 var locationID = $('#hfLocationID').val();
                 setTimeout(function () {
                     $scope.$apply(function () {
                         $scope.VM.location = location;
                         $scope.VM.locationID = locationID;
                     });
                 }, 1);

             };

             var popUpWindow = function (url, target, title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }



         }]);



var getCode = function (code) {
    $("#tbLocation").val(code.text);
    $("#hfLocationID").val(code.id);
    $('#refreshBtn').trigger('click');
};


