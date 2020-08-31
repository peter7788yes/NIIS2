$(function () {
    $(document).on("click", ".UploadIdNoFiles", function (e) {
      
        OpenWindowWithPostOptions("/CaseMaintain/GetIdNoFromUploadFile.aspx", 502, 230, "GetIdNoFromUploadFile", "");
        e.preventDefault();
    });


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

                 postData.CaseIdNo = $("#CaseIdNo").val();
                 postData.IsSearch = "1";
                 postData.SearchReason = $("#SearchReason").val();

                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("/CaseMaintain/ParentChildListOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };


             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};

                 postData.CaseIdNo = $("#CaseIdNo").val();
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("/CaseMaintain/UserProfileListSimpleOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };

             $scope.goDetail = function (record) {
              
                 OpenWindowWithPostOptions("/CaseMaintain/ParentChildDetail.aspx", 502, 230, "ParentChildDetail", { ParentID: record["ParentID"] });
             

             };






         } ]);


         var getIdNo = function (ids) {
             $("#CaseIdNo").val(ids);
         };
                  