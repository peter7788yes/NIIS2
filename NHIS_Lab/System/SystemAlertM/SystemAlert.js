$(function () {
    $(document).on("click", "input[name='lastBtn']", function (e) {
        history.go(-1);
        e.preventDefault();
        return false;
    });
});
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
function SetDate() {
    var scope = angular.element($("#StartDate")).scope();
    var scope = angular.element($("#EndDate")).scope();
    scope.$apply(function () {
        scope.VM.StartDate = document.getElementById("StartDate").value;
        scope.VM.EndDate = document.getElementById("EndDate").value;
    });
};
angular.module("SystemAlertApp", ["PageM", "InputM", "TableM", "FilterM", "ngSanitize"])
         .controller("SystemAlertController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider) {
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.PM = PageProvider;
             $scope.PM.pgSize = 12;
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             $scope.VM = {};
             $scope.VM.OrgName = "";
             $scope.VM.OrgID = OrgID;
             $scope.VM.AlertType = [{ EV: '0', EN: "全部" }].concat(AlertTypeData);
             $scope.VM.SelectAlertType = "0";
             $scope.VM.StartDate = "";
             $scope.VM.EndDate = "";

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
             //轉換到訊息頁面
             $scope.TransferMessage = function () {
                 location.href = "/Vaccine/VaccineInformationM/Disease/New_Disease.aspx";
             }
             //民國年轉換西元年,例子(1040916->20150916)
             $scope.TransformADDate = function (Data) {
                 var TempDate = 0;
                 TempDate = (parseInt(Data.substring(0, 3)) + 1911);
                 TempDate = TempDate + Data.substring(3, 7)
                 return TempDate;
             }
             //疾病名稱頁面查詢按鈕的功能
             $scope.Search = function (pageIndex) {
                 //更改頁碼，PageProvider物件
                 var pgData = $scope.PM.genPageData(pageIndex);
                 //因應查詢條件
                 var postData = {};
                 postData.OrgID = $scope.VM.OrgID;
                 postData.AlertType = $scope.VM.SelectAlertType;
                 postData.StartDate = $scope.TransformADDate($scope.VM.StartDate);
                 postData.EndDate = $scope.TransformADDate($scope.VM.EndDate);
                 //取得PostData，PostData物件
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("SystemAlertOP.aspx", postData, function (data) {
                     $scope.TM.tbData = data.message;
                     $scope.$apply(function () {
                     });
                 });
             };
         }]);
angular.bootstrap(document.getElementById("SystemAlertApp"), ["SystemAlertApp"]);

