angular.module("StockQueryApp", ["PageM", "TableM", "ngCookies"])
         .controller("StockQueryController", ["$scope", "$cookieStore", "PageProvider", "TableProvider", function ($scope, $cookieStore, PageProvider, TableProvider) {
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.PM = PageProvider;
             $scope.PM.pgSize = 12;
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             $scope.VM = {};
             $scope.VM.OrgName = OrgData;
             $scope.VM.VaccineID = [{ EV: '0', EN: "請選擇" }].concat(VaccineData);
             $scope.VM.SelectVaccineID = "0";

             //轉換到各單位批號庫存頁面
             $scope.VeiwStockQuery = function (record) {
                 location.href = "/Vaccine/StockManagementM/StockQuery/View_StockQuery.aspx?BI=" + record["c2"] + "&P=" + $scope.PM.pgNow;
             }
             //頁面查詢的功能
             $scope.Search = function (pageIndex) {
                 //記錄起查詢條件的值
                 $cookieStore.put('OrgName', $scope.VM.OrgName);
                 $cookieStore.put('SelectVaccineID', $scope.VM.SelectVaccineID);
                 //更改頁碼，PageProvider物件
                 var pgData = $scope.PM.genPageData(pageIndex);
                 //因應查詢條件
                 var postData = {};
                 postData.VaccineID = $scope.VM.SelectVaccineID;
                 //取得PostData，PostData物件
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("StockQueryOP.aspx", postData, function (data) {
                     $scope.TM.tbData = data.message;
                     $scope.$apply(function () {
                     });
                 });
             };
             //判斷查詢結果的頁面是第幾頁
             if (url('?P') == null) {
                 //若P=null代表是第一次進入此頁面或是從新增頁面回來的
                 $cookieStore.remove('OrgName');
                 $cookieStore.remove('SelectVaccineID');
                 $scope.Search(1);
             }
             else {
                 //若P=3代表是從下面的查詢條件的結果第3頁來的
                 $scope.VM.OrgName = $cookieStore.get('OrgName');
                 $scope.VM.SelectVaccineID = $cookieStore.get('SelectVaccineID');
                 $scope.Search(url('?P'));
             }
         }])
         .controller("ViewStockQueryController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider) {
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.PM = PageProvider;
             $scope.PM.pgSize = 12;
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             $scope.VM = {};
             $scope.VM.VaccineID = "";
             $scope.VM.BatchID = "";
             $scope.VM.BatchType = "";
             $scope.VM.DosePer = "";
             $scope.VM.AvaDate = "";
             $scope.VM.Remaining = "";

             //轉換到待撥入清單頁面
             $scope.Return = function () {
                 location.href = "/Vaccine/StockManagementM/StockQuery/StockQuery.aspx?P=" + url('?P');
             }
             //取得批號資料
             $scope.GetBatchData = function () {
                 var postData = {};
                 postData.BatchID = url('?BI');

                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/Vaccine/StockManagementM/StockQuery/GetStockQueryBatchOP.aspx",
                     data: postData
                 })
                .done(function (data) {
                    try{
                        $scope.VM.Getdata = data.message[0];
                        $scope.VM.VaccineID = $scope.VM.Getdata.VaccineID;
                        $scope.VM.BatchID = $scope.VM.Getdata.BatchID;
                        $scope.VM.BatchType = $scope.VM.Getdata.BatchTypeName;
                        $scope.VM.DosePer = $scope.VM.Getdata.DosePer;
                        $scope.VM.AvaDate = $scope.VM.Getdata.AvaDate;
                        $scope.VM.Remaining = $scope.VM.Getdata.Remaining;
                        $scope.VM.Storage = $scope.VM.Getdata.Storage;
                        $scope.VM.DoseStorage = $scope.VM.Getdata.DoseStorage;
                    }
                    catch (exception) {
                        alert("資料取得失敗!");
                        location.href = "/Vaccine/StockManagementM/StockQuery/StockQuery.aspx?P=" + url('?P');
                    }
                    $scope.$apply(function () {
                    });
                })
                .fail(function (jqXHR, textStatus) {

                });
             }
             //頁面查詢的功能
             $scope.Search = function (pageIndex) {
                 $scope.GetBatchData();
                 var BatchID = url('?BI');
                 //更改頁碼，PageProvider物件
                 var pgData = $scope.PM.genPageData(pageIndex);
                 //因應查詢條件
                 var postData = {};
                 postData.BatchID = BatchID;
                 //取得PostData，PostData物件
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("View_StockQueryOP.aspx", postData, function (data) {
                     $scope.TM.tbData = data.message;
                     $scope.$apply(function () {
                     });
                 });
             };
             $scope.Search(1);
         }]);
//angular.bootstrap(document.getElementById("StockQueryApp"), ["StockQueryApp"]);