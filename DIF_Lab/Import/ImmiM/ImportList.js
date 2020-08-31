function SetDate() {
    var scope = angular.element($("#StartDate")).scope();
    var scope = angular.element($("#EndDate")).scope();
    scope.$apply(function () {
        scope.VM.StartDate = document.getElementById("StartDate").value;
        scope.VM.EndDate = document.getElementById("EndDate").value;
    });
};
angular.module("MyApp", ["PageM", "TableM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider) {

             $scope.PM = PageProvider;
             $scope.PM.pgSize = 10;
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;
             //查詢頁面的物件
             $scope.VM = {};
             $scope.VM.StartDate = "";
             $scope.VM.EndDate = "";
             $scope.VM.Status = "2";

             $scope.goDetail = function (record) {
                 location.href = "ImportLogDetail.aspx?id=" + record["ID"] + "&ImportDate=" + record["CreateDate"] + "&DataCnt=" + record["DataCount"];
             }
             //西元年轉換民國年
             $scope.TransformROCDate = function (Data) {
                 var TempDate = [];
                 var returnDate = "";
                 TempDate = Data.split("-");
                 TempDate[0] = TempDate[0] - 1911;
                 if (TempDate[0] < 100) {
                     TempDate[0] = "0" + TempDate[0];
                 }
                 returnDate = TempDate[0] + TempDate[1] + TempDate[2];
                 return returnDate;
             }
             //民國年轉換西元年,例子(1040916->20150916)
             $scope.TransformADDate = function (Data) {
                 var TempDate = 0;
                 TempDate = (parseInt(Data.substring(0, 3)) + 1911);
                 TempDate = TempDate + Data.substring(3, 7)
                 return TempDate;
             }
             //發佈訊息頁面查詢按鈕的功能
             $scope.Search = function (pageIndex) {
                 $scope.VM.StartDate = document.getElementById("StartDate").value;
                 $scope.VM.EndDate = document.getElementById("EndDate").value;
                 if ($scope.VM.StartDate.trim() == "" && $scope.VM.EndDate.trim() == "") {
                     alert("請填寫介接日期");
                     return false;
                 }
                 if ($scope.VM.StartDate.trim() != "" && $scope.VM.EndDate.trim() != "") {
                     if ($scope.VM.StartDate.trim() > $scope.VM.EndDate.trim()) {
                         alert("起始日期不可晚於結束日期");
                         return false;
                     }
                 }
                 if ($scope.VM.StartDate.trim() != "" && ($scope.VM.StartDate.trim() > getToday())) {
                     alert("起始日期不可晚於今日");
                     return false;
                 }
                 if ($scope.VM.EndDate.trim() != "" && ($scope.VM.EndDate.trim() > getToday())) {
                     alert("結束日期不可晚於今日");
                     return false;
                 }
                 //更改頁碼，PageProvider物件
                 var pgData = $scope.PM.genPageData(pageIndex)
                 //因應查詢條件
                 var postData = {};
                 if ($scope.VM.StartDate.trim() != "")
                     postData.StartDate = $scope.TransformADDate($scope.VM.StartDate);
                 else
                     postData.StartDate = "";
                 if ($scope.VM.EndDate.trim() != "")
                     postData.EndDate = $scope.TransformADDate($scope.VM.EndDate);
                 else
                     postData.EndDate = "";
                 postData.Status = $scope.VM.Status;

                 //取得PostData，PostData物件
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("ImportListOP.aspx", postData, function (data) {
                     $(".listTb").show();
                     $scope.TM.tbData = data.message;
                     $scope.$apply(function () {
                     });
                 });
             };
         }]);
angular.bootstrap(document.getElementById("MyApp"), ["MyApp"]);