function SetDate() {
    var scope = angular.element($("#PublishedStarDate")).scope();
    scope.$apply(function () {
        scope.VM.PublishedStarDate = document.getElementById("PublishedStarDate").value;
    });
    var scope = angular.element($("#PublishedEndDate")).scope();
    scope.$apply(function () {
        scope.VM.PublishedEndDate = document.getElementById("PublishedEndDate").value;
    });
};
angular.module("QnADataApp", ["PageM", "TableM"])
         .controller("QnADataController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider) {
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.PM = PageProvider;
             $scope.PM.pgSize = 12;
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             //查詢頁面的物件
             $scope.VM = {};
             $scope.VM.Type = [{ EnumValue: '0', EnumName: "請選擇" }].concat(TypeData);
             $scope.VM.QuestionType = "0";
             $scope.VM.Question = "";
             $scope.VM.PublishedStarDate = "";
             $scope.VM.PublishedEndDate = "";
             $scope.VM.Status = [{ EV: '0', EN: "全部" }].concat(StatusData);
             $scope.VM.PublishedStatus = "0";

             //取得問題類別名稱
             $scope.GetQuestionType = function (record) {
                 var rtn = "";
                 $.each($scope.VM.Type, function (index, item) {
                     if (item.EnumValue == record.c3) {
                         rtn = item.EnumName;
                     }
                 });
                 return rtn;
             };
             //取得狀態的名稱
             $scope.GetStatus = function (record) {
                 var rtn = "";
                 $.each($scope.VM.Status, function (index, item) {
                     if (item.EV == record.c9) {
                         rtn = item.EN;
                     }
                 });
                 return rtn;
             };
             //民國年轉換西元年,例子(1040916->20150916)
             $scope.TransformADDate = function (Data) {
                 var TempDate = 0;
                 TempDate = (parseInt(Data.substring(0, 3)) + 1911);
                 TempDate = TempDate + Data.substring(3, 7)
                 return TempDate;
             }
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
             //轉換到新增問答頁面
             $scope.TransferNew = function () {
                 location.href = "/System/FrequentlyAskedQuestionM/QnAData/New_QnAData.aspx";
             }
             //轉換到問答類別頁面
             $scope.TransferType = function () {
                 location.href = "/System/FrequentlyAskedQuestionM/QnAData/QnAType.aspx";
             }
             //轉換到修改問答頁面
             $scope.TransferModiy = function (record) {
                 location.href = "/System/FrequentlyAskedQuestionM/QnAData/Modify_QnAData.aspx?I=" + record["c2"];
             }
             //檢查起迄日期
             $scope.CheckDate = function () {
                 var StarDate = $scope.TransformADDate($scope.VM.PublishedStarDate);
                 var EndDate = $scope.TransformADDate($scope.VM.PublishedEndDate);
                 if (EndDate < StarDate) {
                     alert("起迄日期有問題!");
                     return 0;
                 }
                 else{
                     return 1;
                 }
             }
             //問答維護頁面查詢功能
             $scope.Search = function (pageIndex) {
                 var CehckMsg = $scope.CheckDate();
                 if (CehckMsg == 1) {
                     //更改頁碼，PageProvider物件
                     var pgData = $scope.PM.genPageData(pageIndex)
                     //因應查詢條件
                     var postData = {};
                     postData.QuestionType = $scope.VM.QuestionType;
                     postData.Question = $scope.VM.Question;
                     postData.PublishedStarDate = $scope.TransformADDate($scope.VM.PublishedStarDate);
                     postData.PublishedEndDate = $scope.TransformADDate($scope.VM.PublishedEndDate);
                     postData.PublishedStatus = $scope.VM.PublishedStatus;
                     //取得PostData，PostData物件
                     postData = $scope.PM.filterPageData(pgData, postData);

                     $scope.PM.changePage("QnADataOP.aspx", postData, function (data) {
                         $scope.TM.tbData = data.message;
                         $scope.$apply(function () {
                         });
                     });
                 }
             };
             $scope.Search(1);
         }])
         .controller("ModifyQnADataController", ["$filter", "$scope", function ($filter, $scope) {
             //修改問題頁面的物件
             $scope.VM = {};
             $scope.VM.ID = url('?I');

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
             //讀取問題的資料
             $scope.LoadQnAData = function () {
                var postData = {};
                postData.ID = $scope.VM.ID;

                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "/System/FrequentlyAskedQuestionM/QnAData/GetQnADataOP.aspx",
                    data: postData
                })
                .done(function (data) {
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
             $scope.LoadQnAData();
             //刪除附件檔案
             $scope.DeleteFile = function (item, index) {
                 var postData = {};
                 postData.FileID = item.c3;

                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/System/FrequentlyAskedQuestionM/QAMaintenance/Delete_QnADataFileOP.aspx",
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
         }])
         .controller("QnATypeController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider) {
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.PM = PageProvider;
             $scope.PM.pgSize = 12;
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             //新增問題類別頁面的物件
             $scope.VM = {};
             $scope.VM.Status = [{ EV: '0', EN: "全部" }].concat(StatusData);
             $scope.VM.SelectStatus = "0";


             //轉換到新增問答類別頁面
             $scope.TransferNew = function () {
                 location.href = "/System/FrequentlyAskedQuestionM/QnAData/New_QnAType.aspx?PageView=add";
             }
             //轉換到修改問答類別頁面
             $scope.TransferModiy = function (record) {
                 location.href = "/System/FrequentlyAskedQuestionM/QnAData/Modify_QnAType.aspx?I=" + record["c2"];
             }
             //取得狀態的名稱
             $scope.GetStatus = function (record) {
                 var rtn = "";
                 $.each($scope.VM.Status, function (index, item) {
                     if (item.EV == record.c4) {
                         rtn = item.EN;
                     }
                 });
                 return rtn;
             };
             //訊息檢視頁面查詢功能
             $scope.Search = function (pageIndex) {
                 //更改頁碼，PageProvider物件
                 var pgData = $scope.PM.genPageData(pageIndex)
                 //因應查詢條件
                 var postData = {};
                 postData.Status = $scope.VM.SelectStatus;
                 //取得PostData，PostData物件
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("QnATypeOP.aspx", postData, function (data) {
                     $scope.TM.tbData = data.message;
                     $scope.$apply(function () {
                     });
                 });
             }
             $scope.Search(1);
         }])
         .controller("ModifyQnATypeController", ["$scope", function ($scope) {
             //修改問題類別頁面的物件
             $scope.VM = {};
             $scope.VM.ID = url('?I');

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
             //讀取問題類別的資料
             $scope.LoadQnATypeData = function () {
                var postData = {};
                postData.ID = $scope.VM.ID;
                $.ajax({
                    cache: false,
                    type: "POST",
                    url: "/System/FrequentlyAskedQuestionM/QnAData/GetQnATypeDataOP.aspx",
                    data: postData
                })
                .done(function (data) {
                    $scope.VM.CreateInfo = data.CreateInfo[0];
                    $scope.VM.ModifyInfo = data.ModifyInfo[0];
                    $scope.VM.CreateAccount = $scope.VM.CreateInfo.c1;
                    $scope.VM.CreateRole = $scope.VM.CreateInfo.c2;
                    $scope.VM.CreateDate = $scope.TransformDate($scope.VM.CreateInfo.c3);
                    $scope.VM.ModifyAccount = $scope.VM.ModifyInfo.c1;
                    $scope.VM.ModifyRole = $scope.VM.ModifyInfo.c2;
                    $scope.VM.ModifyDate = $scope.TransformDate($scope.VM.ModifyInfo.c3);
                    $scope.$apply(function () {
                    });
                })
                .fail(function (jqXHR, textStatus) {

                });
             }
             $scope.LoadQnATypeData();
         }]);
angular.bootstrap(document.getElementById("QnADataApp"), ["QnADataApp"]);