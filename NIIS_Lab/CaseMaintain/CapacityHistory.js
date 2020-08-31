$(function () { 

    $(document).on("click", "#SearchBtn", function (e) {
        
        e.preventDefault();
        return false;
    });

    $(document).on("click", "#cancel", function (e) {
        window.opener.document.getElementById('UpdateContactList').click();
        window.close();
        e.preventDefault();
        return false;
    });
    $(document).on("click", "#ok", function (e) {
        window.opener.document.getElementById('UpdateContactList').click();
        window.close();
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
                 postData["CaseID"] = CaseID;
                 postData["CapacityID"] = $(".CapacityID").val();
                  
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("/CaseMaintain/CapacityHistoryOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };

             $scope.changePage(1);




         } ]);

 