$(function () {
    $(document).on("click", "input[name='lastBtn']", function (e) {
        history.go(-1);
        e.preventDefault();
        return false;
    });
});
angular.module("QAViewApp", ["PageM", "TableM"])
         .controller("QAViewController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider) {
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.PM = PageProvider;
             $scope.PM.pgSize = 12;
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             //查詢頁面的物件
             $scope.VM = {};
             $scope.VM.Type = [{ ID: '0', Name: "請選擇" }].concat(TypeData);
             $scope.VM.QuestionType = "0";
             $scope.VM.Question = "";
             $scope.VM.Status = [{ EV: '0', EN: "全部" }].concat(StatusData);
             $scope.VM.QAViewDateStatus = "0";

             //取得問題類別名稱
             $scope.GetQuestionType = function (record) {
                 var rtn = "";
                 $.each($scope.VM.Type, function (index, item) {
                     if (item.ID == record.c3) {
                         rtn = item.Name;
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
             $scope.TransferView = function (record) {
                 var postData = {};
                 postData.CheckID = record.c2;
                 postData.ViewNum = record.c10;
                 $scope.PM.changePage("AddQAViewNumOP.aspx", postData, function (data) {
                     $scope.$apply(function () {
                     });
                 });
                 location.href = "/System/FrequentlyAskedQuestionM/QAView/View_QAView.aspx?CheckID=" + record["c2"];
             }
             //問答維護頁面查詢功能
             $scope.Search = function (pageIndex) {
                 //更改頁碼，PageProvider物件
                 var pgData = $scope.PM.genPageData(pageIndex)
                 //因應查詢條件
                 var postData = {};
                 postData.QuestionType = $scope.VM.QuestionType;
                 postData.Question = $scope.VM.Question;
                 postData.QAViewDateStatus = $scope.VM.QAViewDateStatus;
                 //取得PostData，PostData物件
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("QAViewOP.aspx", postData, function (data) {
                     $scope.TM.tbData = data.message;
                     $scope.$apply(function () {
                     });
                 });
             };
             $scope.Search(1);
         }])
         .controller("ViewQAViewController", ["$scope", function ($scope) {
             //查詢頁面的物件
             $scope.VEVM = {};
             $scope.VEVM.CheckID = url('?CheckID');
             $scope.VEVM.ReleaseDate = "";
             $scope.VEVM.Question = "";
             $scope.VEVM.Reply = "";
             $scope.VEVM.filelist = [];

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
             //讀取問題資料
             $scope.LoadQAViewData = function () {
                 var postData = {};
                 postData.CheckID = $scope.VEVM.CheckID;
                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/System/FrequentlyAskedQuestionM/QAView/GetQAViewDataOP.aspx",
                     data: postData
                 })
                .done(function (data) {
                    $scope.VEVM.Getdata = data.Datalist[0];
                    $scope.VEVM.ReleaseDate = $scope.TransformROCDate($scope.VEVM.Getdata.c11);
                    $scope.VEVM.Question = $scope.VEVM.Getdata.c4;
                    $scope.VEVM.Reply = $scope.VEVM.Getdata.c8;
                    $scope.VEVM.filelist = data.Filelist;
                    $scope.$apply(function () {
                    });
                })
                .fail(function (jqXHR, textStatus) {

                });
             }
         }]);
angular.bootstrap(document.getElementById("QAViewApp"), ["QAViewApp"]);