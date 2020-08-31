$(function () {
    
    $(document).on("click", "#SearchBtn", function (e) {
         
    });
     

     
});

var PageNow = 1;

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
                 postData.UserName = $("#UserName").val();
                 postData.OrgID = $("#SelectOrg").val();
                 postData.SearchKind = $("#SearchKind").val();
                 PageNow = pgData["pgNow"];

                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("/SearchCheck/SearchLogListOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };



             $scope.changePage(1);

             $scope.goDetail = function (record) {
                 location.href = "/SearchCheck/SearchLogDetailList.aspx?i=" + record["CaseID"];
             };

             $scope.goAdd = function (record) {
                 var postData = {};
                 postData["UserID"] = record["UserID"];
                 postData["YearMonth"] = record["YearMonth"];
                 postData["SearchKind"] = record["SearchKind"];
                 postData["action"] = 0;
                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/SearchCheck/AddAuditOP.aspx",
                     data: postData,
                     async: false
                 })
                   .done(function (response) {
                       var reply = eval(response);
                       if (reply.RetCode == '1') {
                           alert('設定完成');
                           $scope.changePage(PageNow);
                       }

                   })
                    .fail(function (jqXHR, textStatus) {

                    });



             };



             $scope.goDel = function (record) {
             
                 var postData = {};
                 postData["UserID"] = record["UserID"];
                 postData["YearMonth"] = record["YearMonth"];
                 postData["SearchKind"] = record["SearchKind"];
                 postData["action"] = 2;
                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/SearchCheck/AddAuditOP.aspx",
                     data: postData,
                     async: false
                 })
                   .done(function (response) {
                       var reply = eval(response);
                       if (reply.RetCode == '1') {
                           alert('設定完成');
                           $scope.changePage(PageNow);
                       }

                   })
                    .fail(function (jqXHR, textStatus) {

                    });



             };



         } ]);


          