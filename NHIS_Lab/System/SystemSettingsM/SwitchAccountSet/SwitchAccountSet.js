var refresh = function (ID) {
    //location.href = "/System/SystemSettingsM/SwitchAccountSet/SwitchAccountSetMonth.aspx?OrgID=" + ID ;
    $('#li2').trigger('click');
};
var getCode = function (code) {
    $("#OrgName").val(code.text);
    $("#OrgID").val(code.id);
    $('#refreshBtn').trigger('click');
};
var popUpWindow = function (url, title, w, h) {
    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
};
$(function () {
    $("#tabs").tabs();
});
angular.module("SwitchAccountSetApp", ["PageM", "InputM", "TableM", "FilterM", "ngSanitize"])
         .controller("SwitchAccountSetController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider) {
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.PM = PageProvider;
             $scope.PM.pgSize = 12;
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             $scope.VM = {};
             $scope.VM.OrgName = "";
             $scope.VM.OrgID = OrgID;

             //開起組織單位
             $scope.openOrgs = function () {
                 popUpWindow("/SelectSingleOrg.aspx", "SelectSingleOrg", 930, 450);
             };
             //設定組織單位
             $scope.refresh = function () {
                 var OrgName = $('#OrgName').val();
                 var OrgID = $('#OrgID').val();

                 setTimeout(function () {
                     $scope.$apply(function () {
                         $scope.VM.OrgName = OrgName;
                         $scope.VM.OrgID = OrgID;
                         $scope.Search(1);
                     });
                 }, 1);
             };
             //轉換到設定月份頁面
             $scope.TransferSetMonth = function (record) {
                 location.href = "/System/SystemSettingsM/SwitchAccountSet/SwitchAccountSetMonth.aspx?OrgID=" + record["c3"];
             }
             //查詢底下所屬單位的功能
             $scope.Search = function (pageIndex) {
                 //更改頁碼，PageProvider物件
                 var pgData = $scope.PM.genPageData(pageIndex);
                 //因應查詢條件
                 var postData = {};
                 postData.OrgID = $scope.VM.OrgID;
                 //取得PostData，PostData物件
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("SwitchAccountSetOP.aspx", postData, function (data) {
                     $scope.TM.tbData = data.message;
                     $scope.$apply(function () {
                     });
                 });
             };
             $scope.Search(1);
         }])
         .controller("SwitchAccountSetMonthController", ["$scope", "PageProvider", "TableProvider", "$http", function ($scope, PageProvider, TableProvider, $http) {
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.PM = PageProvider;
             $scope.PM.pgSize = 12;
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;
             $scope.TM2 = {};
             angular.copy($scope.TM, $scope.TM2);

             $scope.VM = {};
             $scope.VM.OrgName = "";
             $scope.VM.OrgID = url('?OrgID');
             $scope.VM.Year = YearData;
             var Today = new Date();
             var TodatYear = Today.getFullYear();
             $scope.VM.SelectYear = TodatYear;
             $scope.VM.SetName = "設定";
             $scope.VM.NewFileName = "申請資料";

             //回到查詢各單位開關帳頁面
             $scope.Return = function () {
                 location.href = "/System/SystemSettingsM/SwitchAccountSet/SwitchAccountSet.aspx";
             }
             //新增申請資料
             $scope.NewData = function () {
                 popUpWindow("/System/SystemSettingsM/SwitchAccountSet/New_SwitchAccountFile.aspx?OrgID=" + $scope.VM.OrgID, "New_SwitchAccountFile", 620, 500);
             };
             //修改申請資料
             $scope.ModifyData = function (record) {
                 popUpWindow("/System/SystemSettingsM/SwitchAccountSet/Modify_SwitchAccountFile.aspx?OrgID=" + $scope.VM.OrgID + "&ID=" + record["c2"], "Modify_SwitchAccountFile", 620, 500);
             }
             //刪除申請資料
             $scope.DeleteData = function (record) {
                 if (confirm("你確定要刪除此筆申請資料!")) {
                     var postData = {};
                     postData.ID = record.c2;

                     $http({
                         method: 'POST',
                         url: "/System/SystemSettingsM/SwitchAccountSet/Delete_SwtichAccountFileOP.aspx",
                         data: $.param(postData),
                         headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                     })
                     .success(function (data, status, headers, config) {
                         if (data.Success > 0) {
                             alert("刪除成功!");
                             $scope.GetFile(1);
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
             }
             //交換狀態
             $scope.ChangStatus = function (record) {
                 var index = $scope.SelectIDs.indexOf(record.c2);
                 if (record.c6 == 1) {
                     if (confirm("你確定要修改!")) {
                         record.c6 = 0;
                         var postData = {};
                         postData.ID = record.c2;
                         postData.Status = record.c6;

                         $http({
                             method: 'POST',
                             url: "/System/SystemSettingsM/SwitchAccountSet/Modify_SwitchAccountStatusOP.aspx",
                             data: $.param(postData),
                             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                         })
                         .success(function (data, status, headers, config) {
                             if (data.Success > 0) {
                                 alert("修改成功!");
                             }
                             else {
                                 alert("修改失敗!");
                                 record.c6 = 1;
                             }
                         })
                          .error(function (data, status, headers, config) {
                              // called asynchronously if an error occurs
                              // or server returns response with an error status.
                          });
                     }
                     else {
                         record.c6 = 1;
                     }
                 }
                 else {
                     if (confirm("你確定要修改!")) {
                         record.c6 = 1;
                         var postData = {};
                         postData["ID"] = record.c2;
                         postData["Status"] = record.c6;

                         $http({
                             method: 'POST',
                             url: "/System/SystemSettingsM/SwitchAccountSet/Modify_SwitchAccountStatusOP.aspx",
                             data: $.param(postData),
                             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                         })
                         .success(function (data, status, headers, config) {
                             if (data.Success > 0) {
                                 alert("修改成功!");
                             }
                             else {
                                 alert("修改失敗!");
                                 record.c6 = 0;
                             }
                         })
                          .error(function (data, status, headers, config) {
                              // called asynchronously if an error occurs
                              // or server returns response with an error status.
                          });
                     }
                     else {
                         record.c6 = 0;
                     }
                 }
             };
             //取得月份資料
             $scope.GetMonth = function () {
                 $("#MonthBlock").show();
                 $("#NewFileBlock").hide();
                 $("#Newbtn").hide();

                 var postData = {};
                 postData.OrgID = $scope.VM.OrgID;
                 postData.Year = $scope.VM.SelectYear;
                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/System/SystemSettingsM/SwitchAccountSet/SwitchAccountSetMonthOP.aspx",
                     data: postData
                 })
                .done(function (data) {
                    $scope.TM.tbData = data.message;
                    $scope.$apply(function () {
                    });
                })
                .fail(function (jqXHR, textStatus) {

                });
             };
             $scope.GetMonth();
             //取得申請資料
             $scope.GetFile = function (pageIndex) {
                 $("#MonthBlock").hide();
                 $("#NewFileBlock").show();
                 $("#Newbtn").show();
                 //更改頁碼，PageProvider物件
                 var pgData = $scope.PM.genPageData(pageIndex);
                 //因應查詢條件
                 var postData = {};
                 postData.OrgID = $scope.VM.OrgID;
                 //取得PostData，PostData物件
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("SwitchAccountFileOP.aspx", postData, function (data) {
                     $scope.TM2.tbData = data.message;
                     $scope.$apply(function () {
                     });
                 });
             };
         }]);
angular.bootstrap(document.getElementById("SwitchAccountSetApp"), ["SwitchAccountSetApp"]);

