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

                 postData.LogCheckFileID = LogCheckFileID; 
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("/LogManage/LogItemErrorListOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;  
                     $scope.$apply(function () { });


                 });
             };

             $scope.changePage(1);


         } ]);


          