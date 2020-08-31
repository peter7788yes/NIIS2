$(function () {
     
      

     
});



angular.module("MyApp", ["PageM", "TableM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};

             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};

                 postData.LogCheckMainID = LogCheckMainID;

                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("/LogManage/LogMainListOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     
                     $scope.$apply(function () { });
                 });
             };

             $scope.changePage(1);

             $scope.goDetail = function (record) {
                 OpenWindow("/LogSample/" + record['FileName'], 800, 500, "RowFile");
             };

             $scope.goErrorList = function (record) {
                 post_to_url("/LogManage/LogItemErrorList.aspx", { f: record["ID"] }, "post");
             };



         } ]);


          