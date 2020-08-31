function SetSearchDate() {
    var scope = angular.element($("#StartDeal")).scope();
    var scope = angular.element($("#EndDeal")).scope();
    scope.$apply(function () {
        scope.VM.StartDeal = document.getElementById("StartDeal").value;
        scope.VM.EndDeal = document.getElementById("EndDeal").value;
    });
};
angular.module("KnotStockQueryApp", ["PageM", "TableM"])
         .controller("KnotStockQueryController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider) {
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.PM = PageProvider;
             $scope.PM.pgSize = 12;
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             $scope.VM = {};
             $scope.VM.OrgName = OrgData;
             $scope.VM.StartDeal = "";
             $scope.VM.EndDeal = "";
             $scope.VM.Vaccine = VaccineData;
             $scope.VM.SelectVaccine = [];

             //全選疫苗資料
             $scope.AllSelect = function () {
                 $scope.VM.SelectVaccine = [];
                 angular.forEach($scope.VM.Vaccine, function (item) {
                     $scope.VM.SelectVaccine.push(item.EV.toString());
                 });
             };
             //取消選取疫苗資料
             $scope.AllCancel = function () {
                 $scope.VM.SelectVaccine = [];
             };
             //民國年轉換西元年,例子(1040916->20150916)
             $scope.TransformADDate = function (Data) {
                 var TempDate = 0;
                 TempDate = (parseInt(Data.substring(0, 3)) + 1911);
                 TempDate = TempDate + Data.substring(3, 7)
                 return TempDate;
             };
             //結存量查詢按鈕的功能
             $scope.Search = function (pageIndex) {
                 var msg = "";
                 var StarDate = $scope.TransformADDate($scope.VM.StartDeal);
                 var EndDate = $scope.TransformADDate($scope.VM.EndDeal);
                 if ($scope.form1.OrgName.$error.required == true && $scope.form1.OrgID.$error.required == true) {
                     msg += "結存單位:必填!\n";
                 }
                 if ($scope.form1.StartDeal.$error.required == true) {
                     msg += "結存區間:開始時間:必填!\n";
                 }
                 if ($scope.form1.EndDeal.$error.required == true) {
                     msg += "結存區間:結束時間:必填!\n";
                 }
                 if ($scope.form1.SelectVaccine.$error.required == true) {
                     msg += "疫苗:必填!\n";
                 }
                 if (EndDate < StarDate) {
                     msg += "結存區間:起迄日期有問題!\n";
                 }
                 if (msg.length != 0) {
                     alert(msg);
                 }
                 else {
                     //更改頁碼，PageProvider物件
                     var pgData = $scope.PM.genPageData(pageIndex);
                     //因應查詢條件
                     var postData = {};
                     postData.StartDeal = $scope.TransformADDate($scope.VM.StartDeal);
                     postData.EndDeal = $scope.TransformADDate($scope.VM.EndDeal);
                     postData.SelectVaccine = $scope.VM.SelectVaccine.toString();
                     //取得PostData，PostData物件
                     postData = $scope.PM.filterPageData(pgData, postData);

                     $scope.PM.changePage("KnotStockQueryOP.aspx", postData, function (data) {
                         $scope.TM.tbData = data.message;
                         $scope.$apply(function () {
                         });
                     });
                 }
             };
         }]);
//angular.bootstrap(document.getElementById("KnotStockQueryApp"), ["KnotStockQueryApp"]);

