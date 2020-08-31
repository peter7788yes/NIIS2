function SetSearchDate() {
    var scope = angular.element($("#StartDeal")).scope();
    var scope = angular.element($("#EndDeal")).scope();
    scope.$apply(function () {
        scope.VM.StartDeal = document.getElementById("StartDeal").value;
        scope.VM.EndDeal = document.getElementById("EndDeal").value;
    });
};
var getAgency = function (code) {
    $("#DealHospitalName").val(code.AN);
    $("#DealHospitalID").val(code.I);
    $('#refreshBtn').trigger('click');
};
var popUpWindow = function (url, title, w, h) {
    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
};
var ChangePath = function (NewPathName) {
    var myHtml = sessionStorage.getItem("menuPath");
    var parent = document.getElementsByClassName('path')[0];
    parent.innerHTML = myHtml.replace(/撥入登錄/, '<a href="javascript:void(0);">撥入登錄</a>' + NewPathName);
};
var Page = "In";
angular.module("VaccineInApp", ["PageM", "InputM", "TableM", "FilterM", "ngCookies"])
         .controller("VaccineInController", ["$scope", "$cookieStore", "PageProvider", "TableProvider", function ($scope, $cookieStore, PageProvider, TableProvider) {
             //改變新的路徑
             ChangePath("待撥入清單");
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.PM = PageProvider;
             $scope.PM.pgSize = 12;
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             $scope.VM = {};
             $scope.VM.OrgName = OrgData;

             //轉換到查詢頁面
             $scope.TransferSearch = function () {
                 location.href = "/Vaccine/StockManagementM/VaccineIn/Search_VaccineIn.aspx";
             };
             //轉換到登錄頁面
             $scope.TransferLogin = function (record) {
                 location.href = "/Vaccine/StockManagementM/VaccineIn/Login_VaccineIn.aspx?VaccineOutID=" + record["c2"];
             };
             //清空查詢條件
             $scope.ClearSearch = function () {
                 $scope.VM.OrgName = OrgData;
                 $scope.VM.OrgID = "-1";
                 $cookieStore.remove('OrgName');
                 $cookieStore.remove('OrgID');
             };
             //頁面查詢的功能
             $scope.Search = function (pageIndex) {
                 //記錄起查詢條件的值
                 $cookieStore.put('OrgName', $scope.VM.OrgName);
                 //更改頁碼，PageProvider物件
                 var pgData = $scope.PM.genPageData(pageIndex);
                 //因應查詢條件
                 var postData = {};
                 //alert(postData.OrgID);
                 //取得PostData，PostData物件
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("VaccineInOP.aspx", postData, function (data) {
                     $scope.TM.tbData = data.message;
                     $scope.$apply(function () {
                     });
                 });
             };
             //判斷查詢結果的頁面是第幾頁
             if (url('?P') == null) {
                 //若P=null代表是第一次進入此頁面或是從新增頁面回來的
                 $cookieStore.remove('OrgName');
                 $scope.Search(1);
             }
             else {
                 //若P=3代表是從下面的查詢條件的結果第3頁來的
                 $scope.VM.OrgName = $cookieStore.get('OrgName');
                 $scope.Search(url('?P'));
             }
         }])
         .controller("LoginVaccineInController", ["$scope", "$http", "TableProvider", function ($scope, $http, TableProvider) {
             //改變新的路徑
             ChangePath("待撥入清單");
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             $scope.VM = {};
             $scope.VM.DealDate = "";
             $scope.VM.OutOrgName = "";
             $scope.VM.TotalCost = "";
             $scope.VM.DealTypeName = "";
             $scope.VM.DealStatus = [{ EV: '0', EN: "全部" }].concat(DealStatusData);

             //取得處理狀態名稱
             $scope.GetDealStatus = function (record) {
                 var rtn = "";
                 $.each($scope.VM.DealStatus, function (index, item) {
                     if (item.EV == record.c10) {
                         rtn = item.EN;
                     }
                 });
                 return rtn;
             };
             //檢查目前的處理狀態
             $scope.CheckStatus = function (record) {
                 if (record.c10 == 4) {
                     return true;
                 }
                 else {
                     return false;
                 }
             };
             //轉換到待撥入清單頁面
             $scope.TransferVaccineIn = function () {
                 location.href = "/Vaccine/StockManagementM/VaccineIn/VaccineIn.aspx";
             };
             //轉換到撥入頁面
             $scope.LoginConfirm = function (record) {
                 location.href = "/Vaccine/StockManagementM/VaccineIn/LoginConfirm_VaccineIn.aspx?VaccineOutID=" + url('?VaccineOutID') + "&VaccineOutBatchID=" + record["c2"];
             };
             //轉換到退回頁面
             $scope.Return = function (record) {
                 location.href = "/Vaccine/StockManagementM/VaccineIn/Return_VaccineIn.aspx?VaccineOutID=" + url('?VaccineOutID') + "&VaccineOutBatchID=" + record["c2"];
             };
             //取得登錄資料
             $scope.GetLoginData = function () {
                 var ID = url('?VaccineOutID');
                 var postData = {};
                 postData.ID = ID;

                 $http({
                     method: 'POST',
                     url: "/Vaccine/StockManagementM/VaccineIn/Login_VaccineInOP.aspx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
               .success(function (data, status, headers, config) {
                   try {
                       $scope.VM.Getdata = data.DataInfo[0];
                       $scope.TM.tbData = data.ListInfo;
                       $scope.VM.DealDate = $scope.VM.Getdata.c4;
                       $scope.VM.OutOrgName = $scope.VM.Getdata.c3;
                       $scope.VM.TotalCost = $scope.VM.Getdata.c6;
                       $scope.VM.DealTypeName = $scope.VM.Getdata.c8;
                   }
                   catch (exception) {
                       alert("資料取得失敗!");
                       location.href = "/Vaccine/StockManagementM/VaccineIn/VaccineIn.aspx";
                   }
               })
               .error(function (data, status, headers, config) {
                   // called asynchronously if an error occurs
                   // or server returns response with an error status.
               });

                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/Vaccine/StockManagementM/VaccineIn/Login_VaccineInOP.aspx",
                     data: postData
                 })
                .done(function (data) {
                    $scope.VM.Getdata = data.DataInfo[0];
                    $scope.TM.tbData = data.ListInfo;
                    $scope.VM.DealDate = $scope.VM.Getdata.c4;
                    $scope.VM.OutOrgName = $scope.VM.Getdata.c3;
                    $scope.VM.TotalCost = $scope.VM.Getdata.c6;
                    $scope.VM.DealTypeName = $scope.VM.Getdata.c8;
                    $scope.$apply(function () {
                    });
                })
                .fail(function (jqXHR, textStatus) {

                });
             };
             $scope.GetLoginData();
         }])
         .controller("SearchVaccineInController", ["$scope", "$http", "$cookieStore", "PageProvider", "TableProvider", function ($scope, $http, $cookieStore, PageProvider, TableProvider) {
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
             $scope.VM.BatchType = [{ EV: '0', EN: "全部" }].concat(BatchTypeData);
             $scope.VM.SelectBatchType = "0";
             $scope.VM.BatchID = [{ EV: '0', EN: "請選擇" }];
             $scope.VM.SelectBatchID = "0";
             $scope.VM.Sort = "0";
             $scope.VM.DeleteWord = "---";

             //轉換到修改頁面
             $scope.TransferModifyBatch = function (record) {
                 if (record.c17 <= 0) {
                     location.href = "/Vaccine/StockManagementM/VaccineIn/Modify_VaccineInBatch.aspx?BI=" + record["c2"];
                 }
                 else {
                     location.href = "/Vaccine/StockManagementM/VaccineIn/View_VaccineInBatch.aspx?BI=" + record["c2"];
                 }
             };
             //回撥入登錄頁面
             $scope.TransferVaccineIn = function () {
                 location.href = "/Vaccine/StockManagementM/VaccineIn/VaccineIn.aspx";
             };
             //取得撥入單位的工作人員和此單位有的疫苗
             $scope.GetSVDataByOrg = function () {

                 $http({
                     method: 'GET',
                     url: "/Vaccine/StockManagementM/VaccineIn/GetInSVDataByOrgOP.aspx",
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
                 postData.BatchType = $scope.VM.SelectBatchType;

                 $http({
                     method: 'POST',
                     url: "/Vaccine/StockManagementM/VaccineIn/GetInBDataByOrgOP.aspx",
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
                 if (confirm("你確定要刪除，此筆撥入批號!")) {
                     var postData = {};
                     postData.BatchID = record.c2;

                     $http({
                         method: 'POST',
                         url: "/Vaccine/StockManagementM/VaccineIn/Delete_VaccineInBatchOP.aspx",
                         data: $.param(postData),
                         headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                     })
                     .success(function (data, status, headers, config) {
                         if (data.Success > 0) {
                             alert("刪除成功!");
                             $scope.Search(1);
                         }
                         else {
                             if (data.CheckStorage > 0) {
                                 alert("庫存量不足!無法刪除!");
                             }
                             else {
                                 alert("刪除失敗!");
                             }
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
                 $scope.VM.SelectBatchType = "0";
                 $scope.VM.BatchID = [{ EV: '0', EN: "請選擇" }];
                 $scope.VM.SelectBatchID = "0";
                 $scope.VM.Sort = "0";
                 $scope.GetSVDataByOrg();
                 $cookieStore.remove('StartDeal');
                 $cookieStore.remove('EndDeal');
                 $cookieStore.remove('OrgName');
                 $cookieStore.remove('SelectStaff');
                 $cookieStore.remove('SelectVaccineID');
                 $cookieStore.remove('SelectBatchType');
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
                 $cookieStore.put('SelectBatchType', $scope.VM.SelectBatchType);
                 $cookieStore.put('SelectBatchID', $scope.VM.SelectBatchID);
                 $cookieStore.put('Sort', $scope.VM.Sort);
                 //檢查日期起訖有沒有問題
                 var msg = "";
                 var StarDate = $scope.TransformADDate($scope.VM.StartDeal);
                 var EndDate = $scope.TransformADDate($scope.VM.EndDeal);
                 if ($scope.form1.StartDeal.$error.required != true && $scope.form1.EndDeal.$error.required != true) {
                     if (EndDate < StarDate) {
                         msg += "撥入日期:起迄日期有問題!\n";
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
                     postData.Saff = $scope.VM.SelectSaff;
                     postData.VaccineID = $scope.VM.SelectVaccineID;
                     postData.BatchType = $scope.VM.SelectBatchType;
                     postData.BatchID = $scope.VM.SelectBatchID;
                     postData.Sort = $scope.VM.Sort;
                     //取得PostData，PostData物件
                     postData = $scope.PM.filterPageData(pgData, postData);

                     $scope.PM.changePage("Search_VaccineInOP.aspx", postData, function (data) {
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
                 $cookieStore.remove('SelectBatchType');
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
                 $scope.VM.SelectBatchType = $cookieStore.get('SelectBatchType');
                 $scope.VM.SelectBatchID = $cookieStore.get('SelectBatchID');
                 $scope.VM.Sort = $cookieStore.get('Sort');
                 //$scope.Search(url('?P'));
             }
         }])
         .controller("ModifyVaccineInDataController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider) {
             //改變新的路徑
             ChangePath("維護");
             //開啟合約院所選單
             $scope.openSelectAgency = function () {
                 popUpWindow("/Vaccine/StockManagementM/VaccineIn/SelectAgency.aspx", "SelectAgency", 930, 450);
             };
         }]);
//angular.bootstrap(document.getElementById("VaccineInApp"), ["VaccineInApp"]);

