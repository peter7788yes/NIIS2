$(function () {




    $(document).on("click", "#TipMerge", function (e) { 
        alert("請等候排程執行");
        return false;
    });


});



angular.module("MyApp", ["PageM", "TableM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};

             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};

                 postData.BirthDateS = $("#BirthDateS").val();
                 postData.BirthDateE = $("#BirthDateE").val();
                 postData.SearchKind = 1; 

                 postData = $scope.PM.filterPageData(pgData, postData);
                  console.log(postData);
                 $scope.PM.changePage("/CaseMaintain/MergeCheckOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };

             $scope.changePage(1);

             $scope.goDetail = function (record) {

                 location.href = "/CaseMaintain/UserProfile.aspx?i=" + record["C"];
             };




             //List2
             $scope.PM2 = {};
             angular.copy($scope.PM, $scope.PM2);
             $scope.TM2 = {};
             angular.copy($scope.TM, $scope.TM2); 

             $scope.changePage2 = function (pageIndex) {
                 $("#tmBlock2").show();
                 var pgData = $scope.PM2.genPageData(pageIndex);
                 var postData = {};

                 postData.BirthDateS = $("#BirthDateS").val();
                 postData.BirthDateE = $("#BirthDateE").val();
                 postData.SearchKind = 2;
                 postData = $scope.PM2.filterPageData(pgData, postData);
                 console.log(postData);

                 $scope.PM2.changePage("/CaseMaintain/MergeCheckOP.aspx", postData, function (data) {
                     $scope.TM2.data = data.message;
                     $scope.$apply(function () { });
                 });
             };

             $scope.goDetail2 = function (record) {

                 location.href = "/CaseMaintain/UserProfile.aspx?i=" + record["C"];
             };


             //List3
             $scope.PM3 = {};
             angular.copy($scope.PM, $scope.PM3);
             $scope.TM3 = {};
             angular.copy($scope.TM, $scope.TM3);

             $scope.changePage3 = function (pageIndex) {
                 $("#tmBlock3").show();
                 var pgData = $scope.PM3.genPageData(pageIndex);
                 var postData = {};

                 postData.BirthDateS = $("#BirthDateS").val();
                 postData.BirthDateE = $("#BirthDateE").val();
                 postData.SearchKind = 3;
                 postData = $scope.PM3.filterPageData(pgData, postData);
                 console.log(postData);

                 $scope.PM3.changePage("/CaseMaintain/MergeCheckOP.aspx", postData, function (data) {
                     $scope.TM3.data = data.message;
                     $scope.$apply(function () { });
                 });
             };

             $scope.goDetail3 = function (record) {

                 location.href = "/CaseMaintain/UserProfile.aspx?i=" + record["C"];
             };










         } ]);

 