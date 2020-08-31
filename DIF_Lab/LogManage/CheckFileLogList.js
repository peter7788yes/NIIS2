$(function () {
    

    $(document).on("click", "#SearchBtn", function (e) {
        
        e.preventDefault();
        return false;
    });
    $(document).on("click", "#btnExe", function (e) {
     
        $.post("ReadFileToLog.aspx", function () {
            alert("success");
        })
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

                 postData.CreateDateS = $("#CreateDateS").val();
                 postData.CreateDateE = $("#CreateDateE").val();
                 postData.LogStatus = $("#LogStatus").val();
                 //postData.LogItem = $(".cblLogItem>input:checkbox:checked").map(function () { return $(this).val(); }).get();

                 postData = $scope.PM.filterPageData(pgData, postData);
                 //console.log(postData);
                 $scope.PM.changePage("/LogManage/CheckFileLogListOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };

             $scope.changePage(1);

             $scope.goDetail = function (record) {
              
                 post_to_url("/LogManage/LogMainList.aspx", { f: record["ID"] }, "post");
             };





         } ]);


          