$(function () {
    $(document).on("click", "#btnSave", function (e) {
        location.href = "/CaseCheck/CheckCaseUpdateList.aspx";
        e.preventDefault();
        return false;
    });

  

    $(document).on("click", "#btnBack", function (e) {
        location.href = "/CaseCheck/CheckCaseUpdateList.aspx";
        e.preventDefault();
        return false;
    });


});



angular.module("MyApp", ["PageM", "TableM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {}; 

              


         } ]);

          