$(function () {
    
    $(document).on("click", "#SearchBtn", function (e) {
         
    });
     

     
});



angular.module("MyApp", ["PageM", "TableM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};

             $scope.VM.OrgAry = [{ I: '0', O: "全部"}].concat(OrgData);
             $scope.VM.SelectOrg = "0";



             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};

                 postData.SearchDateS = $("#SearchDateS").val();
                 postData.SearchDateE = $("#SearchDateE").val();
                 postData.SearchKind = $("#SearchKind").val();
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("/SearchCheck/HistoryAuditLogListOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };



             $scope.changePage(1);

             $scope.goDetail = function (record) {

                // post_to_url("/SearchCheck/SearchLogDetailList.aspx", { i: record["UserID"] }, "post");
   post_to_url("/SearchCheck/SearchLogDetailList.aspx", { i: record["UserID"] , k: record["SearchKind"] , d: record["YearMonth"] }, "post");
			
             };


             $scope.goHistory = function (record) {
                 post_to_url("/SearchCheck/HistoryAuditLogListByUser.aspx", { i: record["UserID"] }, "post");

             };



             $scope.goExport = function (record) {

                 location.href = "/SearchCheck/SearchLogPdf.aspx?a=" + record["AuditID"];
             };


         } ]);


          