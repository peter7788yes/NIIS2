function SetDate() {
    var scope = angular.element($("#DealDate")).scope();
    scope.$apply(function () {
        scope.VM.DealDate = document.getElementById("DealDate").value;
    });
};
function SetSearchDate() {
    var scope = angular.element($("#StartDeal")).scope();
    var scope = angular.element($("#EndDeal")).scope();
    scope.$apply(function () {
        scope.VM.StartDeal = document.getElementById("StartDeal").value;
        scope.VM.EndDeal = document.getElementById("EndDeal").value;
    });
};
var ChangePath = function (NewPathName) {
    var myHtml = sessionStorage.getItem("menuPath");
    var parent = document.getElementsByClassName('path')[0];
    parent.innerHTML = myHtml.replace(/領用登錄/, '<a href="javascript:void(0);">領用登錄</a>' + NewPathName);
};
var Page = "Use";
angular.module("VaccineUseApp", ["PageM", "InputM", "TableM", "FilterM", "ngCookies"])
         .controller("VaccineUseController", ["$scope", "$http", "$cookieStore", "PageProvider", "TableProvider", function ($scope, $http, $cookieStore, PageProvider, TableProvider) {
             //改變新的路徑
             ChangePath("查詢紀錄");
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.PM = PageProvider;
             $scope.PM.pgSize = 12;
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             $scope.VM = {};
             $scope.VM.StartDeal = "";
             $scope.VM.EndDeal = "";
             $scope.VM.OrgName = OrgData;
             $scope.VM.Staff = [{ EV: '0', EN: "請選擇" }];
             $scope.VM.SelectStaff = "0";
             $scope.VM.VaccineID = [{ EV: '0', EN: "請選擇" }];
             $scope.VM.SelectVaccineID = "0";
             $scope.VM.UseType = [{ EV: '0', EN: "請選擇" }].concat(UseTypeData);
             $scope.VM.SelectUseType = "0";
             $scope.VM.BatchID = [{ EV: '0', EN: "請選擇" }];
             $scope.VM.SelectBatchID = "0";
             $scope.VM.Sort = "0";

             //轉換到修改頁面
             $scope.TransferModifyBatch = function (record) {
                 location.href = "/Vaccine/StockManagementM/VaccineUse/Modify_VaccineUseBatch.aspx?VaccUseBatchDataID=" + record["c2"];
             };
             //回新增頁面
             $scope.TransferNewData = function () {
                 location.href = "/Vaccine/StockManagementM/VaccineUse/New_VaccineUseData.aspx";
             };
             //取得領用單位的工作人員和此單位有的疫苗
             $scope.GetSVDataByOrg = function () {

                 $http({
                     method: 'GET',
                     url: "/Vaccine/StockManagementM/VaccineUse/GetUseSVDataByOrgOP.aspx",
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
                .success(function (data, status, headers, config) {
                    if (data.StaffInfo != null) {
                        $scope.VM.Staff = [{ EV: '0', EN: "請選擇" }].concat(data.StaffInfo);
                        $scope.VM.SelectStaff = "0";
                    }
                    else {
                        $scope.VM.Staff = [{ EV: '0', EN: "請選擇" }];
                        $scope.VM.SelectStaff = "0";
                    }
                    if (data.VaccineIDInfo != null) {
                        $scope.VM.VaccineID = [{ EV: '0', EN: "請選擇" }].concat(data.VaccineIDInfo);
                        $scope.VM.SelectVaccineID = "0";
                    }
                    else {
                        $scope.VM.VaccineID = [{ EV: '0', EN: "請選擇" }];
                        $scope.VM.SelectVaccineID = "0";
                    }
                    $scope.VM.BatchID = [{ EV: '0', EN: "請選擇" }];
                    $scope.VM.SelectBatchID = "0";
                })
                .error(function (data, status, headers, config) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });
             };
             $scope.GetSVDataByOrg();
             //取得此單位有的疫苗批號
             $scope.GetBDataByOrg = function () {
                 var postData = {};
                 postData.VaccineID = $scope.VM.SelectVaccineID;

                 $http({
                     method: 'POST',
                     url: "/Vaccine/StockManagementM/VaccineUse/GetUseBDataByOrgOP.aspx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
                .success(function (data, status, headers, config) {
                    if (data.BatchIDInfo != null) {
                        $scope.VM.BatchID = [{ EV: '0', EN: "請選擇" }].concat(data.BatchIDInfo);
                        $scope.VM.SelectBatchID = "0";
                    }
                    else {
                        $scope.VM.BatchID = [{ EV: '0', EN: "請選擇" }];
                        $scope.VM.SelectBatchID = "0";
                    }
                })
                .error(function (data, status, headers, config) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });
             };
             //刪除批號資料
             $scope.DeleteBatch = function (record, index) {
                 if (confirm("你確定要刪除，此筆領用批號!")) {
                     var postData = {};
                     postData.BatchID = record.c2;

                     $http({
                         method: 'POST',
                         url: "/Vaccine/StockManagementM/VaccineUse/Delete_VaccineUseBatchOP.aspx",
                         data: $.param(postData),
                         headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                     })
                     .success(function (data, status, headers, config) {
                         if (data.Success > 0) {
                             alert("刪除成功!");
                             $scope.Search(1);
                         }
                         else {
                             alert("刪除失敗!");
                         }
                     })
                     .error(function (data, status, headers, config) {
                         // called asynchronously if an error occurs
                         // or server returns response with an error status.
                     });
                 }
             };
             //民國年轉換西元年,例子(1040916->20150916)
             $scope.TransformADDate = function (Data) {
                 var TempDate = 0;
                 TempDate = (parseInt(Data.substring(0, 3)) + 1911);
                 TempDate = TempDate + Data.substring(3, 7)
                 return TempDate;
             };
             //清空查詢條件
             $scope.ClearSearch = function () {
                 $scope.VM.StartDeal = "";
                 $scope.VM.EndDeal = "";
                 $scope.VM.OrgName = OrgData;
                 $scope.VM.SelectStaff = "0";
                 $scope.VM.SelectVaccineID = "0";
                 $scope.VM.SelectUseType = "0";
                 $scope.VM.BatchID = [{ EV: '0', EN: "請選擇" }];
                 $scope.VM.SelectBatchID = "0";
                 $scope.VM.Sort = "0";
                 $scope.GetSVDataByOrg();
                 $cookieStore.remove('StartDeal');
                 $cookieStore.remove('EndDeal');
                 $cookieStore.remove('OrgName');
                 $cookieStore.remove('SelectStaff');
                 $cookieStore.remove('SelectVaccineID');
                 $cookieStore.remove('SelectUseType');
                 $cookieStore.remove('SelectBatchID');
                 $cookieStore.remove('Sort');
             };
             //頁面查詢的功能
             $scope.Search = function (pageIndex) {
                 //記錄起查詢條件的值
                 $cookieStore.put('StartDeal', $scope.VM.StartDeal);
                 $cookieStore.put('EndDeal', $scope.VM.EndDeal);
                 $cookieStore.put('OrgName', $scope.VM.OrgName);
                 $cookieStore.put('SelectStaff', $scope.VM.SelectStaff);
                 $cookieStore.put('SelectVaccineID', $scope.VM.SelectVaccineID);
                 $cookieStore.put('SelectUseType', $scope.VM.SelectUseType);
                 $cookieStore.put('SelectBatchID', $scope.VM.SelectBatchID);
                 $cookieStore.put('Sort', $scope.VM.Sort);
                 //檢查日期起訖有沒有問題
                 var msg = "";
                 var StarDate = $scope.TransformADDate($scope.VM.StartDeal);
                 var EndDate = $scope.TransformADDate($scope.VM.EndDeal);
                 if ($scope.form1.StartDeal.$error.required != true && $scope.form1.EndDeal.$error.required != true) {
                     if (EndDate < StarDate) {
                         msg += "領用日期:起迄日期有問題!\n";
                     }
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
                     postData.Staff = $scope.VM.SelectStaff;
                     postData.VaccineID = $scope.VM.SelectVaccineID;
                     postData.UseType = $scope.VM.SelectUseType;
                     postData.BatchID = $scope.VM.SelectBatchID;
                     postData.Sort = $scope.VM.Sort;
                     //取得PostData，PostData物件
                     postData = $scope.PM.filterPageData(pgData, postData);

                     $scope.PM.changePage("VaccineUseOP.aspx", postData, function (data) {
                         $scope.TM.tbData = data.message;
                         $scope.$apply(function () {
                         });
                     });
                 }
             };
             //判斷查詢結果的頁面是第幾頁
             if (url('?P') == null) {
                 //若P=null代表是第一次進入此頁面或是從新增頁面回來的
                 $cookieStore.remove('StartDeal');
                 $cookieStore.remove('EndDeal');
                 $cookieStore.remove('OrgName');
                 $cookieStore.remove('SelectStaff');
                 $cookieStore.remove('SelectVaccineID');
                 $cookieStore.remove('SelectUseType');
                 $cookieStore.remove('SelectBatchID');
                 $cookieStore.remove('Sort');
                 //$scope.Search(1);
             }
             else {
                 //若P=3代表是從下面的查詢條件的結果第3頁來的
                 $scope.VM.StartDeal = $cookieStore.get('StartDeal');
                 $scope.VM.EndDeal = $cookieStore.get('EndDeal');
                 $scope.VM.OrgName = $cookieStore.get('OrgName');
                 $scope.VM.SelectStaff = $cookieStore.get('SelectStaff');
                 $scope.VM.SelectVaccineID = $cookieStore.get('SelectVaccineID');
                 $scope.VM.SelectUseType = $cookieStore.get('SelectUseType');
                 $scope.VM.SelectBatchID = $cookieStore.get('SelectBatchID');
                 $scope.VM.Sort = $cookieStore.get('Sort');
                 //$scope.Search(url('?P'));
             }
         }])
         .controller("NewVaccineUseDataController", ["$scope", "$http", "PageProvider", "TableProvider", function ($scope, $http, PageProvider, TableProvider) {
             //改變新的路徑
             ChangePath("新增");
             $scope.VM = {};
             $scope.VM.DealDate = "";
             $scope.VM.UseOrgName = "";
             $scope.VM.TotalCost = "";
             $scope.VM.Remark = "";

             //取得今天時間
             $scope.GetDate = function () {
                 var d = new Date();
                 var TodayYear = d.getFullYear() - 1911;
                 var TodayMonth = d.getMonth() + 1;
                 var TodayDate = d.getDate();
                 if (TodayYear < 100) {
                     TodayYear = "0" + TodayYear;
                 }
                 if (TodayMonth < 10) {
                     TodayMonth = "0" + TodayMonth;
                 }
                 if (TodayDate < 10) {
                     TodayDate = "0" + TodayDate;
                 }
                 return TodayYear + "" + TodayMonth + "" + TodayDate;
             };
             //民國年轉換西元年,例子(1040916->20150916)
             $scope.TransformADDate = function (Data) {
                 var TempDate = 0;
                 TempDate = (parseInt(Data.substring(0, 3)) + 1911);
                 TempDate = TempDate + Data.substring(3, 7)
                 return TempDate;
             };
             //轉換到領用頁面
             $scope.TransferVaccineUse = function () {
                 location.href = "/Vaccine/StockManagementM/VaccineUse/VaccineUse.aspx";
             };
             //轉換到新增頁面
             $scope.TransferNewDataList = function () {
                 var msg = "";
                 if ($scope.form1.DealDate.$error.required == true) {
                     msg += "領用日期:必填!\n";
                 }
                 if (msg.length != 0) {
                     alert(msg);
                 }
                 else {
                     var postData = {};
                     postData.DealDate = $scope.TransformADDate($scope.VM.DealDate);
                     postData.Remark = $scope.VM.Remark;

                     $http({
                         method: 'POST',
                         url: "/Vaccine/StockManagementM/VaccineUse/New_VaccineUseDataOP.aspx",
                         data: $.param(postData),
                         headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                     })
                     .success(function (data, status, headers, config) {
                         if (data.Success > 0) {
                             alert("儲存成功!");
                             location.href = "/Vaccine/StockManagementM/VaccineUse/New_VaccineUseDataList.aspx?ID=" + data.NewID;
                         }
                     })
                     .error(function (data, status, headers, config) {
                         // called asynchronously if an error occurs
                         // or server returns response with an error status.
                     });
                 }
             };
         }])
         .controller("NewVaccineUseDataListController", ["$scope", "$http", "PageProvider", "TableProvider", function ($scope, $http, PageProvider, TableProvider) {
             //改變新的路徑
             ChangePath("新增");
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             $scope.VM = {};
             $scope.VM.ID = url('?ID');
             $scope.VM.DealDate = "";
             $scope.VM.UseOrgName = "";
             $scope.VM.TotalCost = 0;
             $scope.VM.Remark = "";

             //確認完回領用登錄頁面
             $scope.ConfirmVaccineUse = function () {
                 var TempData = $scope.TM.tbData
                 if (TempData.length == 0) {
                     alert("此領用紀錄無領用批號!");
                 }
                 else {
                     var postData = {};
                     postData.ID = $scope.VM.ID;

                     $http({
                         method: 'POST',
                         url: "/Vaccine/StockManagementM/VaccineUse/VaccineUse_ConfirmDataOP.aspx",
                         data: $.param(postData),
                         headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                     })
                     .success(function (data, status, headers, config) {
                         if (data.Success > 0) {
                             alert("儲存成功!");
                             location.href = "/Vaccine/StockManagementM/VaccineUse/VaccineUse.aspx";
                         }
                         else {
                             if (data.CheckStorage > 0) {
                                 data.LackBatch = data.LackBatch.replace(/,/g, '庫存不足!\n');
                                 alert(data.LackBatch);
                             }
                             else {
                                 alert("儲存失敗!");
                             }
                         }
                     })
                     .error(function (data, status, headers, config) {
                         // called asynchronously if an error occurs
                         // or server returns response with an error status.
                     });
                 }
             };
             //取消回領用登錄頁面
             $scope.TransferVaccineUse = function () {
                 location.href = "/Vaccine/StockManagementM/VaccineUse/VaccineUse.aspx";
             };
             //轉換到新增疫苗批號頁面
             $scope.TransferNewList = function () {
                 location.href = "/Vaccine/StockManagementM/VaccineUse/VaccineUse_BatchList.aspx?ID=" + url('?ID');
             };
             //轉換到修改頁面
             $scope.TransferModifyListData = function (record) {
                 location.href = "/Vaccine/StockManagementM/VaccineUse/Modify_VaccineUseBatchList.aspx?ID=" + url('?ID') + "&VaccUseBatchDataID=" + record["c2"];
             };
             //加總批號的總金額
             $scope.CountTotalCost = function () {
                 var TempData = $scope.TM.tbData;
                 var TempTotalCost = $scope.VM.TotalCost;
                 for (var i = 0; i < TempData.length; i++) {
                     TempTotalCost = TempTotalCost + TempData[i].c10 * TempData[i].c11;
                 }
                 return TempTotalCost
             };
             //刪除批號資料
             $scope.DeleteBatch = function (record, index) {
                 if (confirm("你確定要刪除，此筆領用批號!")) {
                     var postData = {};
                     postData.BatchID = record.c2;

                     $http({
                         method: 'POST',
                         url: "/Vaccine/StockManagementM/VaccineUse/Delete_VaccineUseBatchOP.aspx",
                         data: $.param(postData),
                         headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                     })
                     .success(function (data, status, headers, config) {
                         if (data.Success > 0) {
                             alert("刪除成功!");
                             $scope.GetVaccineUseDataList();
                         }
                         else {
                             alert("刪除失敗!");
                         }
                     })
                     .error(function (data, status, headers, config) {
                         // called asynchronously if an error occurs
                         // or server returns response with an error status.
                     });
                 }
             };
             //取得領用資料和領用批號資料
             $scope.GetVaccineUseDataList = function () {
                 var postData = {};
                 postData.ID = $scope.VM.ID

                 $http({
                     method: 'POST',
                     url: "/Vaccine/StockManagementM/VaccineUse/New_VaccineUseDataListOP.aspx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
                .success(function (data, status, headers, config) {
                    try {
                        $scope.VM.Getdata = data.DataInfo[0];
                        $scope.TM.tbData = data.ListInfo;
                        $scope.VM.DealDate = $scope.VM.Getdata.c5;
                        $scope.VM.UseOrgName = $scope.VM.Getdata.c4;
                        $scope.VM.Remark = $scope.VM.Getdata.c6;
                    }
                    catch (exception) {
                        alert("資料取得失敗!");
                        location.href = "/Vaccine/StockManagementM/VaccineUse/VaccineUse.aspx";
                    }
                })
                .error(function (data, status, headers, config) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });
             };
             $scope.GetVaccineUseDataList();
         }])
         .controller("VaccineUseBatchListController", ["$scope", "$http", "TableProvider", function ($scope, $http, TableProvider) {
             //改變新的路徑
             ChangePath("新增");
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             $scope.VM = {};
             $scope.VM.Vaccine = VaccineData;
             $scope.VM.VaccineSelect = $scope.VM.Vaccine[0].ID;

             //轉換到新增疫苗批號頁面
             $scope.TransferNewListData = function (record) {
                 location.href = "/Vaccine/StockManagementM/VaccineUse/New_VaccineUseBatch.aspx?BI=" + record["c1"] + "&I=" + url('?ID');
             };
             //取得疫苗批號資料
             $scope.GetVaccineBatch = function () {
                 //因應查詢條件
                 var postData = {};
                 postData.Page = Page;
                 postData.ID = url('?ID');
                 postData.SelectID = $scope.VM.VaccineSelect;

                 $http({
                     method: 'POST',
                     url: "/Vaccine/StockManagementM/VaccineUse/VaccineUse_BatchListOP.aspx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
                .success(function (data, status, headers, config) {
                    $scope.TM.tbData = data.message;
                })
                .error(function (data, status, headers, config) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });
             };
             $scope.GetVaccineBatch();
         }]);
//angular.bootstrap(document.getElementById("VaccineUseApp"), ["VaccineUseApp"]);

