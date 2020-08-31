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
                 postData.SearchKind = $("#SearchKind").val();
                 postData = $scope.PM.filterPageData(pgData, postData);
                 console.log(postData);
                 $scope.PM.changePage("/SearchCheck/AuditSearchLogListOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };



             $scope.changePage(1);

             $scope.goDetail = function (record) {
                 location.href = "/SearchCheck/SearchLogDetailList.aspx?i=" + record["UserID"];
             };


              

             $scope.goHistory = function (record) {
   
                 post_to_url("/SearchCheck/HistoryAuditLogListByUser.aspx", { i: record["UserID"] }, "post");
                 

             };

             $scope.goAudit = function (record) {
        
                 post_to_url("/SearchCheck/DoAudit.aspx", { a: record["AuditID"] }, "post");
               
             };

             $scope.goExport = function (record) {
                 //location.href = "/SearchCheck/ExportFileOP.aspx?UserID=" + record["UserID"] + "&SearchKind=" + record["SearchKind"] + "&YearMonth=" + record["YearMonth"];
                 location.href = "/SearchCheck/SearchLogPdf.aspx?a=" + record["AuditID"];
             }; 


         } ]);


          