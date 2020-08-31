$(function () {
 

    $(document).on("click", "#SearchBtn", function (e) {

        if (Page_ClientValidate()) {

            angular.element('#MyController').scope().Search(1);
        } 
    });
     

     
});



angular.module("MyApp", ["PageM", "TableM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};

             $scope.Search = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};
                  
                 postData.CaseName = $("#CaseName").val();
                 postData.CaseIdNo = $("#CaseIdNo").val(); 
                 postData.IsSearch = "1";
                 postData.SearchReason = $(".SearchReason").val();
                 postData.SearchKind = "2";
            
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("/CaseMaintain/UserProfileListOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };


             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};
                  
                 postData.CaseName = $("#CaseName").val();
                 postData.CaseIdNo = $("#CaseIdNo").val();
                 postData.SearchKind = "2";
                 postData.IsSearch = "0"; 
                 postData = $scope.PM.filterPageData(pgData, postData);
                // console.log(postData);
                 $scope.PM.changePage("/CaseMaintain/UserProfileListOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };

           //  $scope.changePage(1);

             $scope.goDetail = function (record) { 

                 OpenWindowWithPostOptions("/CaseMaintain/UserProfileDetail.aspx", 820, 508, "CapacityHistory", { i: record["CaseID"] });
  
             };
             

           
               


         } ]);


           