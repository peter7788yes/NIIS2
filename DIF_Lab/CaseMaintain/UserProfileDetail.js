$(function () {


    $(document).on("click", ".btnBack", function (e) {
        location.href = document.referrer;
        e.preventDefault();
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

                 postData.CaseID = CaseID;
            
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("/CaseMaintain/UserProfileDetailOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };


             $scope.changePage(1);

         } ]);


          