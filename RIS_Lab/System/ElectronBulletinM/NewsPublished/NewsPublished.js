var getCode = function (code) {
    $("#OrgName").val(code.text);
    $("#OrgID").val(code.id);
    $('#refreshBtn').trigger('click');
};
var getCodes = function (code) {
    if (code[0].RoleIDs != "") {
        $("#OrgType").val(code[0].RoleType);
        $("#OrgName").val(code[0].TextAry);
        $("#OrgID").val(code[0].RoleIDs);
        $('#refreshBtn').trigger('click');
    }
    else {
        $("#OrgType").val(code[1].RoleType);
        $("#OrgName").val(code[1].TextAry);
        $("#OrgID").val(code[1].RoleIDs);
        $('#refreshBtn').trigger('click');
    }
};
var popUpWindow = function (url, title, w, h) {
    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
};
angular.module("NewsPublishedApp", ["PageM", "TableM"])
         .controller("NewsPublishedController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider) {
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.PM = PageProvider;
             $scope.PM.pgSize = 12;
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             //查詢頁面的物件
             $scope.VM = {};
             $scope.VM.OrgName = "";
             $scope.VM.OrgID = "-1";
             $scope.VM.Subject = "";
             $scope.VM.Status = [{ EV: '0', EN: "全部" }].concat(StatusData);
             $scope.VM.PublishedStatus = "0";

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
             }
             //取得狀態的名稱
             $scope.GetStatus = function (record) {
                 var ToDate = $scope.GetDate();
                 var StarDate = $scope.TransformROCDate(record.c6);
                 var EndDate = $scope.TransformROCDate(record.c7);
                 if (ToDate < StarDate) {
                     var rtn = "";
                     $.each($scope.VM.Status, function (index, item) {
                         if (item.EV == 1) {
                             rtn = item.EN;
                         }
                     });
                     return rtn;
                 }
                 else if (ToDate >= StarDate && ToDate <= EndDate) {
                     var rtn = "";
                     $.each($scope.VM.Status, function (index, item) {
                         if (item.EV == 2) {
                             rtn = item.EN;
                         }
                     });
                     return rtn;
                 }
                 else if (ToDate > EndDate) {
                     var rtn = "";
                     $.each($scope.VM.Status, function (index, item) {
                         if (item.EV == 3) {
                             rtn = item.EN;
                         }
                     });
                     return rtn;
                 }
             };
             //西元年轉換民國年,例子(20150916->1040916)
             $scope.TransformROCDate = function (Data) {
                 var TempDate = [];
                 var returnDate = "";
                 TempDate = Data.split("/");
                 TempDate[0] = TempDate[0] - 1911;
                 if (TempDate[0] < 100) {
                     TempDate[0] = "0" + TempDate[0];
                 }
                 if (TempDate[1] < 10) {
                     TempDate[1] = "0" + TempDate[1];
                 }
                 TempDate[2] = TempDate[2].substring(0, 2);
                 if (TempDate[2] < 10) {
                     TempDate[2] = "0" + TempDate[2];
                 }
                 returnDate = TempDate[0] + TempDate[1] + TempDate[2];
                 return returnDate;
             }
             //新增發佈訊息頁面轉換按鈕
             $scope.TransferNew = function () {
                 location.href = "/System/ElectronBulletinM/NewsPublished/New_NewsPublished.aspx";
             }
             //轉換到修改頁面
             $scope.TransferModiy = function (record) {
                 location.href = "/System/ElectronBulletinM/NewsPublished/Modify_NewsPublished.aspx?CheckID=" + record["c2"];
             }
             //發佈訊息頁面查詢按鈕的功能
             $scope.Search = function (pageIndex) {
                 //更改頁碼，PageProvider物件
                 var pgData = $scope.PM.genPageData(pageIndex)
                 //因應查詢條件
                 var postData = {};
                 postData.OrgID = $scope.VM.OrgID;
                 postData.Subject = $scope.VM.Subject;
                 postData.Status = $scope.VM.PublishedStatus;
                 //取得PostData，PostData物件
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("NewsPublishedOP.aspx", postData, function (data) {
                     $scope.TM.tbData = data.message;
                     $scope.$apply(function () {
                     });
                 });
             };
             $scope.Search(1);
         }])
         .controller("NewNewsPublishedController", ["$scope", function ($scope) {
             //開起組織單位
             $scope.openOrgs = function () {
                 popUpWindow("/SelectOrgsByOrgID.aspx", "SelectOrgsByOrgID", 930, 450);
             };
         }])
         .controller("ModifyNewsPublishedController", ["$filter", "$scope", function ($filter, $scope) {
             //查詢頁面的物件
             $scope.VM = {};
             $scope.VM.ID = url('?CheckID');

             //開起組織單位
             $scope.openOrgs = function () {
                 popUpWindow("/SelectOrgsByOrgID.aspx", "SelectOrgsByOrgID", 930, 450);
             };
             //西元年轉換,例子(2015/09/16 下午 03:25:13->2015/09/16 15:25:13)
             $scope.TransformDate = function (Data) {
                 var TempDate = [];
                 var TempDate1 = [];
                 var ReturnDate = "";
                 var Temp;
                 TempDate = Data.split("/");
                 if (TempDate[2].match("上午") != null) {
                     TempDate[2] = TempDate[2].replace("上午", "");
                 }
                 else if (TempDate[2].match("下午") != null) {
                     TempDate1 = TempDate[2].split("下午");
                     Temp = parseInt(TempDate1[1].substring(1, 3)) + 12;
                     if (Temp == 24) {
                         Temp = 12;
                     }
                     TempDate1[1] = TempDate1[1].replace(TempDate1[1].substring(1, 3), Temp);
                     TempDate[2] = TempDate1[0] + TempDate1[1];
                 }
                 ReturnDate = TempDate[0] + "/" + TempDate[1] + "/" + TempDate[2];
                 return ReturnDate;
             }
             //讀取發佈訊息的資料
             $scope.LoadData = function () {
                var postData = {};
                postData.CheckID = $scope.VM.ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "/System/ElectronBulletinM/NewsPublished/GetNewsPublishedDataOP.aspx",
                    data: postData
                })
                .done(function (data) {
                    $scope.VM.Getdata = data.Datalist[0];
                    $scope.VM.CreateInfo = data.CreateInfo[0];
                    $scope.VM.ModifyInfo = data.ModifyInfo[0];
                    $scope.VM.CreateAccount = $scope.VM.CreateInfo.c1;
                    $scope.VM.CreateRole = $scope.VM.CreateInfo.c2;
                    $scope.VM.CreateDate = $scope.TransformDate($scope.VM.CreateInfo.c3);
                    $scope.VM.ModifyAccount = $scope.VM.ModifyInfo.c1;
                    $scope.VM.ModifyRole = $scope.VM.ModifyInfo.c2;
                    $scope.VM.ModifyDate = $scope.TransformDate($scope.VM.ModifyInfo.c3);
                    $scope.VM.filelist = data.Filelist;
                    $scope.$apply(function () {
                    });
                })
                .fail(function (jqXHR, textStatus) {

                });
             }
             $scope.LoadData();
             //刪除附件檔案
             $scope.DeleteFile = function (item, index) {
                 var postData = {};
                 postData.FileID = item.c3;

                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/System/ElectronBulletinM/NewsPublished/Delete_NewsPublishedFileOP.aspx",
                     data: postData
                 })
                .done(function (data) {
                    if (data.Success > 0) {
                        alert("刪除成功");
                        $scope.VM.filelist = $filter('filter')($scope.VM.filelist, { c3: '!' + item.c3 });
                        $scope.$apply(function () { });
                    }
                    else {
                        alert("刪除失敗");
                    }
                })
                .fail(function (jqXHR, textStatus) {

                });
             }
         }]);
angular.bootstrap(document.getElementById("NewsPublishedApp"), ["NewsPublishedApp"]);

