$(function () {


    $(document).on("click", ".btnBack", function (e) {
        history.go(-1);
        e.preventDefault();
        return false;
    });



});



angular.module("MyApp", ["PageM", "TableM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};


 


         } ]);


          