$(function () {
    $(document).on("click", "input[name='lastBtn']", function (e) {
        history.go(-1);
        e.preventDefault();
        return false;
    });
});
angular.module("MessageViewApp", ["PageM", "TableM"])
         .controller("MessageViewController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider) {
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.PM = PageProvider;
             $scope.PM.pgSize = 12;
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             //查詢頁面的物件
             $scope.VM = {};
             $scope.VM.SearchDate = [{ EV: '0', EN: "全部日期" }].concat(SearchDateData);
             $scope.VM.SelectDateStatus = "0";
             $scope.VM.SearchContentFile = [{ EV: '0', EN: "全部" }].concat(SearchContentFileData);
             $scope.VM.SelectContentFileStatus = "0";

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
             //轉換到瀏覽頁面
             $scope.TransferView = function (record) {
                 location.href = "/System/ElectronBulletinM/MessageView/View_MessageView.aspx?CheckID=" + record["c2"];
             }
             //訊息檢視頁面查詢功能
             $scope.Search = function (pageIndex) {
                 //更改頁碼，PageProvider物件
                 var pgData = $scope.PM.genPageData(pageIndex)
                 //因應查詢條件
                 var postData = {};
                 postData.SelectDateStatus = $scope.VM.SelectDateStatus;
                 postData.SelectContentFileStatus = $scope.VM.SelectContentFileStatus;
                 //取得PostData，PostData物件
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("MessageViewOP.aspx", postData, function (data) {
                     $scope.TM.tbData = data.message;
                     $scope.$apply(function () {
                     });
                 });
             };
         }])
         .controller("ViewMessageViewController", ["$scope", function ($scope) {
             //瀏覽頁面的物件
             $scope.VM = {};
             $scope.VM.CheckID = url('?CheckID');
             $scope.VM.ReleaseDate = "";
             $scope.VM.OrgName = "";
             $scope.VM.Keynote = "";
             $scope.VM.Content = "";
             $scope.VM.Getdata = [];
             $scope.VM.filelist = [];

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
             //讀取發佈訊息的資料
             $scope.LoadMessageViewData = function () {
                 var postData = {};
                 postData.CheckID = $scope.VM.CheckID;
                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/System/ElectronBulletinM/MessageView/GetMessageViewOP.aspx",
                     data: postData
                 })
                .done(function (data) {
                    $scope.VM.Getdata = data.Datalist[0];
                    $scope.VM.CreateInfo = data.CreateInfo[0];
                    $scope.VM.ReleaseDate = $scope.VM.Getdata.c9;
                    $scope.VM.OrgName = $scope.VM.CreateInfo.c2;
                    $scope.VM.Keynote = $scope.VM.Getdata.c4;
                    $scope.VM.Content = $scope.VM.Getdata.c5;
                    $scope.VM.filelist = data.Filelist;
                    $scope.$apply(function () {
                    });
                })
                .fail(function (jqXHR, textStatus) {

                });
             }
         }]);
angular.bootstrap(document.getElementById("MessageViewApp"), ["MessageViewApp"]);