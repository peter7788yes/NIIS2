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

                 postData.NameOrIdNo = $("#NameOrIdNo").val();
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("/CaseMaintain/ChooseUserContractListOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };


             // $scope.changePage(1);

             $scope.goDetail = function (record) {
                 
                 OpenWindowWithPostOptions("/CaseMaintain/UserContract.aspx", 605, 405, "UserContract", { c: record["C"], i: CaseID });

             };



         } ]);

 