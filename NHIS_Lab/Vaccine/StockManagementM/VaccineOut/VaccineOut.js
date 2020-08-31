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
var getCode = function (code) {
    $("#InOrgName").val(code.text);
    $("#InOrgID").val(code.id);
    $('#refreshBtn').trigger('click');
};
var getCodes = function (code) {
    if (code[0].RoleIDs != "") {
        $("#InOrgType").val(code[0].RoleType);
        $("#InOrgName").val(code[0].TextAry);
        $("#InOrgID").val(code[0].RoleIDs);
        $('#InrefreshBtn').trigger('click');
    }
    else {
        $("#InOrgType").val(code[1].RoleType);
        $("#InOrgName").val(code[1].TextAry);
        $("#InOrgID").val(code[1].RoleIDs);
        $('#InrefreshBtn').trigger('click');
    }    
};
var getAgency = function (code) {
    $("#DealHospitalName").val(code.AN);
    $("#DealHospitalID").val(code.I);
    $('#refreshBtn1').trigger('click');
};
var popUpWindow = function (url, title, w, h) {
    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
};
var ChangePath = function (NewPathName) {
    var myHtml = sessionStorage.getItem("menuPath");
    var parent = document.getElementsByClassName('path')[0];
    parent.innerHTML = myHtml.replace(/撥出登錄/, '<a href="javascript:void(0);">撥出登錄</a>' + NewPathName);
};
var Page = "Out";
angular.module("VaccineOutApp", ["PageM", "InputM", "TableM", "FilterM", "ngCookies"])
         .controller("VaccineOutController", ["$scope", "$http", "$cookieStore", "PageProvider", "TableProvider", function ($scope, $http, $cookieStore, PageProvider, TableProvider) {
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
             $scope.VM.InOrgType = "";
             $scope.VM.InOrgName = "";
             $scope.VM.InOrgID = "";
             $scope.VM.OutOrgType = "2";
             $scope.VM.OutOrgName = OrgData;
             $scope.VM.OutOrgID = "-1";
             $scope.VM.Staff = [{ EV: '0', EN: "請選擇" }];
             $scope.VM.SelectStaff = "0";
             $scope.VM.VaccineID = [{ EV: '0', EN: "請選擇" }];
             $scope.VM.SelectVaccineID = "0";
             $scope.VM.BatchType = [{ EV: '0', EN: "全部" }].concat(BatchTypeData);
             $scope.VM.SelectBatchType = "0";
             $scope.VM.BatchID = [{ EV: '0', EN: "請選擇" }];
             $scope.VM.SelectBatchID = "0";
             $scope.VM.DealStatus = [{ EV: '0', EN: "全部" }].concat(DealStatusData);
             $scope.VM.SelectDealStatus = "0";
             $scope.VM.Sort = "0";
             $scope.VM.DeleteWord = "---";

             //開起組織單位
             $scope.openOrgs = function () {
                popUpWindow("/SelectOrgs.aspx", "SelectOrgs", 930, 450);
             };
             //設定撥入組織單位
             $scope.Inrefresh = function () {
                 var OrgType = $('#InOrgType').val();
                 var OrgName = $('#InOrgName').val();
                 var OrgID = $('#InOrgID').val();
                 setTimeout(function () {
                     $scope.$apply(function () {
                         $scope.VM.InOrgType = OrgType;
                         $scope.VM.InOrgName = OrgName;
                         $scope.VM.InOrgID = OrgID;
                     });
                 }, 1);
             };
             //取得處理狀態名稱
             $scope.GetDealStatus = function (record) {
                 var rtn = "";
                 $.each($scope.VM.DealStatus, function (index, item) {
                     if (item.EV == record.c16) {
                         rtn = item.EN;
                     }
                 });
                 return rtn;
             };
             //檢查目前的處理狀態是否可以修改
             $scope.CheckModify = function (record) {
                 if (record.c16 == 1) {
                     return true;
                 }
                 else if (record.c16 == 2) {
                     return false;
                 }
                 else if (record.c16 == 3) {
                     return true;
                 }
                 else if (record.c16 == 4) {
                     return false;
                 }
                 else if (record.c16 == 5) {
                     return true;
                 }
                 else if (record.c16 == 6) {
                     return false;
                 }
             };
             //檢查目前的處理狀態是否被退回
             $scope.CheckReturn = function (record) {
                 if (record.c16 == 1) {
                     return false;
                 }
                 else if (record.c16 == 2) {
                     return false;
                 }
                 else if (record.c16 == 3) {
                     return false;
                 }
                 else if (record.c16 == 4) {
                     return false;
                 }
                 else if (record.c16 == 5) {
                     return true;
                 }
                 else if (record.c16 == 6) {
                     return false;
                 }
             };
             //轉換到修改頁面
             $scope.TransferModifyBatch = function (record) {
                 if (record.c16 == 1) {
                     location.href = "/Vaccine/StockManagementM/VaccineOut/Modify_VaccineOutBatch.aspx?VaccOutBatchDataID=" + record["c2"];
                 }
                 else {
                     location.href = "/Vaccine/StockManagementM/VaccineOut/View_VaccineOutBatch.aspx?VaccOutBatchDataID=" + record["c2"];
                 }
             };
             //轉換到新增頁面
             $scope.TransferNewData = function () {
                 location.href = "/Vaccine/StockManagementM/VaccineOut/New_VaccineOutData.aspx";
             };
             //開啟退回原因視窗
             $scope.TransferBack = function (record) {
                 popUpWindow("/Vaccine/StockManagementM/VaccineOut/Return_VaccineOut.aspx?VaccineOutBatchID=" + record["c2"], "Return_VaccineOut", 400, 200);
             };
             //取得撥出單位的工作人員和此單位有的疫苗
             $scope.GetSVDataByOrg = function () {

                 $http({
                     method: 'GET',
                     url: "/Vaccine/StockManagementM/VaccineOut/GetOutSVDataByOrgOP.aspx",
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
                     url: "/Vaccine/StockManagementM/VaccineOut/GetOutBDataByOrgOP.aspx",
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
                 if (confirm("你確定要刪除，此筆撥出批號!")) {
                     var postData = {};
                     postData.BatchID = record.c2;

                     $http({
                         method: 'POST',
                         url: "/Vaccine/StockManagementM/VaccineOut/Delete_VaccineOutBatchOP.aspx",
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
                 $scope.VM.InOrgType = "";
                 $scope.VM.InOrgName = "";
                 $scope.VM.InOrgID = "";
                 $scope.VM.OutOrgName = OrgData;
                 $scope.VM.SelectStaff = "0";
                 $scope.VM.SelectVaccineID = "0";
                 $scope.VM.SelectBatchType = "0";
                 $scope.VM.BatchID = [{ EV: '0', EN: "請選擇" }];
                 $scope.VM.SelectBatchID = "0";
                 $scope.VM.SelectDealStatus = "0";
                 $scope.VM.Sort = "0";
                 $scope.GetSVDataByOrg();
                 $cookieStore.remove('StartDeal');
                 $cookieStore.remove('EndDeal');
                 $cookieStore.remove('InOrgType');
                 $cookieStore.remove('InOrgID');
                 $cookieStore.remove('InOrgName');
                 $cookieStore.remove('OutOrgName');
                 $cookieStore.remove('SelectStaff');
                 $cookieStore.remove('SelectVaccineID');
                 $cookieStore.remove('SelectBatchType');
                 $cookieStore.remove('SelectBatchID');
                 $cookieStore.remove('SelectDealStatus');
                 $cookieStore.remove('Sort');
             };
             //頁面查詢的功能
             $scope.Search = function (pageIndex) {
                 //記錄起查詢條件的值
                 $cookieStore.put('StartDeal', $scope.VM.StartDeal);
                 $cookieStore.put('EndDeal', $scope.VM.EndDeal);
                 $cookieStore.put('OutOrgName', $scope.VM.OutOrgName);
                 $cookieStore.put('InOrgType', $scope.VM.InOrgType);
                 $cookieStore.put('InOrgID', $scope.VM.InOrgID);
                 $cookieStore.put('InOrgName', $scope.VM.InOrgName);
                 $cookieStore.put('SelectStaff', $scope.VM.SelectStaff);
                 $cookieStore.put('SelectVaccineID', $scope.VM.SelectVaccineID);
                 $cookieStore.put('SelectBatchType', $scope.VM.SelectBatchType);
                 $cookieStore.put('SelectBatchID', $scope.VM.SelectBatchID);
                 $cookieStore.put('SelectDealStatus', $scope.VM.SelectDealStatus);
                 $cookieStore.put('Sort', $scope.VM.Sort);
                 //檢查日期起訖有沒有問題
                 var msg = "";
                 var StarDate = $scope.TransformADDate($scope.VM.StartDeal);
                 var EndDate = $scope.TransformADDate($scope.VM.EndDeal);
                 if ($scope.form1.StartDeal.$error.required != true && $scope.form1.EndDeal.$error.required != true) {
                     if (EndDate < StarDate) {
                         msg += "撥出日期:起迄日期有問題!\n";
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
                     postData.InOrgType = $scope.VM.InOrgType;
                     postData.InOrgID = $scope.VM.InOrgID;                     
                     postData.Staff = $scope.VM.SelectStaff;
                     postData.VaccineID = $scope.VM.SelectVaccineID;
                     postData.BatchType = $scope.VM.SelectBatchType;
                     postData.BatchID = $scope.VM.SelectBatchID;
                     postData.DealStatus = $scope.VM.SelectDealStatus;
                     postData.Sort = $scope.VM.Sort;
                     //取得PostData，PostData物件
                     postData = $scope.PM.filterPageData(pgData, postData);

                     $scope.PM.changePage("VaccineOutOP.aspx", postData, function (data) {
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
                 $cookieStore.remove('InOrgType');
                 $cookieStore.remove('InOrgID');
                 $cookieStore.remove('InOrgName');
                 $cookieStore.remove('OutOrgName');
                 $cookieStore.remove('SelectStaff');
                 $cookieStore.remove('SelectVaccineID');
                 $cookieStore.remove('SelectBatchType');
                 $cookieStore.remove('SelectBatchID');
                 $cookieStore.remove('SelectDealStatus');
                 $cookieStore.remove('Sort');
                 //$scope.Search(1);
             }
             else {
                 //若P=3代表是從下面的查詢條件的結果第3頁來的
                 $scope.VM.StartDeal = $cookieStore.get('StartDeal');
                 $scope.VM.EndDeal = $cookieStore.get('EndDeal');
                 $scope.VM.InOrgType = $cookieStore.get('InOrgType');
                 $scope.VM.InOrgID = $cookieStore.get('InOrgID');
                 $scope.VM.InOrgName = $cookieStore.get('InOrgName');
                 $scope.VM.OutOrgName = $cookieStore.get('OutOrgName');
                 $scope.VM.SelectStaff = $cookieStore.get('SelectStaff');
                 $scope.VM.SelectVaccineID = $cookieStore.get('SelectVaccineID');
                 $scope.VM.SelectBatchType = $cookieStore.get('SelectBatchType');
                 $scope.VM.SelectBatchID = $cookieStore.get('SelectBatchID');
                 $scope.VM.SelectDealStatus = $cookieStore.get('SelectDealStatus');
                 $scope.VM.Sort = $cookieStore.get('Sort');
                 //$scope.Search(url('?P'));
             }
         }])
         .controller("NewVaccineOutDataController", ["$scope", "$http", function ($scope, $http) {
             //改變新的路徑
             ChangePath("新增");
             $scope.VM = {};
             $scope.VM.DealDate = "";
             $scope.VM.OutOrgName = OrgData;
             $scope.VM.InOrgName = "";
             $scope.VM.InOrgID = "";
             $scope.VM.TotalCost = "";
             $scope.VM.Remark = "";
             $scope.VM.DealType = DealTypeData;
             $scope.VM.DealTypeSelect = $scope.VM.DealType[0].EV;
             $scope.VM.DealHospitalName = "";
             $scope.VM.DealHospitalID = "";

             //開起組織單位
             $scope.openOrgs = function () {
                 popUpWindow("/Vaccine/StockManagementM/StockCommonPage/SelectOrgForStockManagementM.aspx", "SelectOrgForStockManagementM", 930, 450);

             };
             //設定組織單位
             $scope.refresh = function () {
                 var OrgName = $('#InOrgName').val();
                 var OrgID = $('#InOrgID').val();

                 setTimeout(function () {
                     $scope.$apply(function () {
                         $scope.VM.InOrgName = OrgName;
                         $scope.VM.InOrgID = OrgID;
                     });
                 }, 1);
             };
             //開啟合約院所選單
             $scope.openSelectAgency = function () {
                 popUpWindow("/Vaccine/StockManagementM/VaccineOut/SelectAgency.aspx", "SelectAgency", 930, 450);
             };
             //設定合約院所
             $scope.refresh1 = function () {
                 var DealHospitalName = $('#DealHospitalName').val();
                 var DealHospitalID = $('#DealHospitalID').val();
                 setTimeout(function () {
                     $scope.$apply(function () {
                         $scope.VM.DealHospitalName = DealHospitalName;
                         $scope.VM.DealHospitalID = DealHospitalID;
                     });
                 }, 1);
             };
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
             //轉換到查詢頁面
             $scope.TransferVaccineOut = function () {
                 location.href = "/Vaccine/StockManagementM/VaccineOut/VaccineOut.aspx";
             };
             //轉換到新增頁面
             $scope.TransferNewDataList = function () {
                 var msg = "";
                 if ($scope.form1.DealDate.$error.required == true) {
                     msg += "撥出日期:必填!\n";
                 }
                 if ($scope.form1.InOrgName.$error.required == true && $scope.form1.InOrgID.$error.required == true) {
                     msg += "撥入單位:必填!\n";
                 }
                 if ($scope.form1.DealTypeSelect.$error.required == true) {
                     msg += "撥出類別:必填!\n";
                 }
                 else if ($scope.VM.DealTypeSelect == "4") {
                     if ($scope.form1.DealHospitalName.$error.required == true || $scope.form1.DealHospitalID.$error.required == true) {
                         msg += "撥出類別:合約醫院:必填!\n";
                     }
                 }
                 if (msg.length != 0) {
                     alert(msg);
                 }
                 else {
                     var postData = {};
                     postData.DealDate = $scope.TransformADDate($scope.VM.DealDate);
                     postData.OrgID = $scope.VM.InOrgID;
                     postData.Remark = $scope.VM.Remark;
                     postData.DealType = $scope.VM.DealTypeSelect;
                     postData.DealHospitalID = $scope.VM.DealHospitalID;

                     $http({
                         method: 'POST',
                         url: "/Vaccine/StockManagementM/VaccineOut/New_VaccineOutDataOP.aspx",
                         data: $.param(postData),
                         headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                     })
                      .success(function (data, status, headers, config) {
                          if (data.Success > 0) {
                              alert("儲存成功!");
                              location.href = "/Vaccine/StockManagementM/VaccineOut/New_VaccineOutDataList.aspx?ID=" + data.NewID;
                          }
                      })
                      .error(function (data, status, headers, config) {
                          // called asynchronously if an error occurs
                          // or server returns response with an error status.
                      });
                 }
             };
         }])
         .controller("NewVaccineOutDataListController", ["$scope", "$http", "TableProvider", function ($scope, $http, TableProvider) {
             //改變新的路徑
             ChangePath("新增");
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             $scope.VM = {};
             $scope.VM.ID = url('?ID');
             $scope.VM.DealDate = "";
             $scope.VM.OutOrgName = "";
             $scope.VM.InOrgName = "";
             $scope.VM.TotalCost = 0;
             $scope.VM.Remark = "";
             $scope.VM.DealType = "";
             $scope.VM.DealHospitalName = "";

             //確認完回撥出登錄頁面
             $scope.ConfirmVaccineOut = function () {
                 var TempData = $scope.TM.tbData
                 if (TempData.length == 0) {
                     alert("此撥出紀錄無撥出批號!");
                 }
                 else {
                     var postData = {};
                     postData.ID = $scope.VM.ID;

                     $http({
                         method: 'POST',
                         url: "/Vaccine/StockManagementM/VaccineOut/VaccineOut_ConfirmDataOP.aspx",
                         data: $.param(postData),
                         headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                     })
                     .success(function (data, status, headers, config) {
                         if (data.Success > 0) {
                             alert("儲存成功!");
                             location.href = "/Vaccine/StockManagementM/VaccineOut/VaccineOut.aspx";
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
             //取消回撥出登錄頁面
             $scope.TransferVaccineOut = function () {
                 location.href = "/Vaccine/StockManagementM/VaccineOut/VaccineOut.aspx";
             };
             //轉換到新增疫苗批號頁面
             $scope.TransferNewList = function () {
                 location.href = "/Vaccine/StockManagementM/VaccineOut/VaccineOut_BatchList.aspx?ID=" + url('?ID');
             };
             //轉換到修改頁面
             $scope.TransferModifyListData = function (record) {
                 location.href = "/Vaccine/StockManagementM/VaccineOut/Modify_VaccineOutBatchList.aspx?I=" + url('?ID') + "&BI=" + record["c2"];
             };
             //加總批號的總金額
             $scope.CountTotalCost = function () {
                 var TempData = $scope.TM.tbData;
                 var TempTotalCost = $scope.VM.TotalCost;
                 for (var i = 0; i < TempData.length; i++) {
                     TempTotalCost = TempTotalCost + TempData[i].c7 * TempData[i].c8;
                 }
                 return TempTotalCost
             };
             //刪除批號資料
             $scope.DeleteBatch = function (record, index) {
                 if (confirm("你確定要刪除，此筆撥出批號!")) {
                     var postData = {};
                     postData.BatchID = record.c2;

                     $http({
                         method: 'POST',
                         url: "/Vaccine/StockManagementM/VaccineOut/Delete_VaccineOutBatchOP.aspx",
                         data: $.param(postData),
                         headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                     })
                     .success(function (data, status, headers, config) {
                         if (data.Success > 0) {
                             alert("刪除成功!");
                             $scope.GetVaccineOutDataList();
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
             //取得撥出資料和撥出批號資料
             $scope.GetVaccineOutDataList = function () {
                 var postData = {};
                 postData.ID = $scope.VM.ID

                 $http({
                     method: 'POST',
                     url: "/Vaccine/StockManagementM/VaccineOut/New_VaccineOutDataListOP.aspx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
                .success(function (data, status, headers, config) {
                    try {
                        $scope.VM.Getdata = data.DataInfo[0];
                        $scope.TM.tbData = data.ListInfo;
                        $scope.VM.DealDate = $scope.VM.Getdata.c5;
                        $scope.VM.OutOrgName = $scope.VM.Getdata.c3;
                        $scope.VM.InOrgName = $scope.VM.Getdata.c4;
                        $scope.VM.Remark = $scope.VM.Getdata.c6;
                        $scope.VM.DealType = $scope.VM.Getdata.c7;
                        $scope.VM.DealHospitalName = $scope.VM.Getdata.c8;
                    }
                    catch (exception) {
                        alert("資料取得失敗!");
                        location.href = "/Vaccine/StockManagementM/VaccineOut/VaccineOut.aspx";
                    }
                })
                .error(function (data, status, headers, config) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });
             };
             $scope.GetVaccineOutDataList();
         }])
         .controller("VaccineOutBatchListController", ["$scope", "$http", "TableProvider", function ($scope, $http, TableProvider) {
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
                 location.href = "/Vaccine/StockManagementM/VaccineOut/New_VaccineOutBatch.aspx?BI=" + record["c1"] + "&I=" + url('?ID');
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
                     url: "/Vaccine/StockManagementM/VaccineOut/VaccineOut_BatchListOP.aspx",
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
         }])
         .controller("ReturnVaccineOutDataController", ["$scope", "$http", function ($scope, $http) {

             $scope.VM = {};
             $scope.VM.VaccReturn = [{ EV: '0', EN: "請選擇" }].concat(VaccReturnData);
             $scope.VM.VaccReturnSelect = "0";
             $scope.VM.ReturnOther = "";

             //關閉視窗
             $scope.CloseWin = function () {
                 window.close();
             }
             //取得退回原因
             $scope.GetVaccReturnData = function () {
                 var postData = {};
                 postData.ID = url('?VaccineOutBatchID');

                 $http({
                     method: 'POST',
                     url: "/Vaccine/StockManagementM/VaccineOut/Return_VaccineOutOP.aspx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
                .success(function (data, status, headers, config) {
                    try{
                        $scope.VM.Getdata = data.message[0];
                        $scope.VM.VaccReturnSelect = $scope.VM.Getdata.VaccReturn;
                        $scope.VM.ReturnOther = $scope.VM.Getdata.ReturnOther;
                    }
                    catch (exception) {
                        alert("資料取得失敗!");
                        window.close();
                    }
                })
                .error(function (data, status, headers, config) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });
             }
             $scope.GetVaccReturnData();
         }])
         .controller("ModifyVaccineOutDataController", ["$scope", function ($scope) {

             //開啟合約院所選單
             $scope.openSelectAgency = function () {
                 popUpWindow("/Vaccine/StockManagementM/VaccineOut/SelectAgency.aspx", "SelectAgency", 930, 450);
             }

         }]);
//angular.bootstrap(document.getElementById("VaccineOutApp"), ["VaccineOutApp"]);