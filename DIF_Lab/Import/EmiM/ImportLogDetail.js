angular.module("MyApp", ["PageM", "TableM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider) {

             $scope.PM = PageProvider;
             $scope.PM.pgSize = 10;
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;
             //查詢頁面的物件
             $scope.VM = {};
             $scope.VM.MasterID = $("#MasterID").val();
             //發佈訊息頁面查詢按鈕的功能
             $scope.Search = function (pageIndex) {
                 //更改頁碼，PageProvider物件
                 var pgData = $scope.PM.genPageData(pageIndex)
                 //因應查詢條件
                 var postData = {};
                 postData.MasterID = $scope.VM.MasterID;
                 //取得PostData，PostData物件
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("ImportLogDetailOP.aspx", postData, function (data) {
                     $(".listTb").show();
                     $scope.TM.tbData = data.message;
                     $scope.$apply(function () {
                     });
                 });
             };
             $scope.Search(1);
         }]);
angular.bootstrap(document.getElementById("MyApp"), ["MyApp"]);