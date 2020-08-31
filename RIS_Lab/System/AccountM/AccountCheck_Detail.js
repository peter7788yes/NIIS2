$(function () {

    $(document).on("click", "#lastBtn", function (e) {
        location.href = "/System/AccountM/AccountCheck.aspx";
        e.preventDefault();
        return false;
    });
    
   
    $(document).on("click", "#downloadBtn", function (e) {
        $('#formid').submit();
        e.preventDefault();
        return false;
    });
});


angular.module("MyApp", ["PageM", "FilterM","cfp.hotkeys"])
         .controller("MyController", ["$scope", "PageProvider", "hotkeys", "$http", "$filter", function ($scope, PageProvider, hotkeys, $http, $filter) {


             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};
             $scope.VM.Uname = "";
             $scope.VM.Oname = "";
             $scope.VM.fList = [];

             $scope.VM.fAry = fAry;
             $scope.VM.iAry = iAry;

             var i = 0;
             angular.forEach($scope.VM.fAry, function (item, index) {
                 var obj = {};
                 obj.F = item;
                 obj.I = $scope.VM.iAry[i];
                 $scope.VM.fList.push(obj);
                 i++;
             });
             
             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};
                 postData["an"] = $scope.VM.Uname;
                 postData["on"] = $scope.VM.Oname;
                 postData["cs"] = $scope.VM.selectCheck;

                 postData = $scope.PM.filterPageData(pgData, postData);
                 $scope.PM.changePage("/System/AccountM/AccountCheckOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };

             $scope.changePage(1);

             hotkeys.add({
                 combo: 'enter',
                 description: 'Description goes here',
                 allowIn: ['INPUT', "BUTTON", 'SELECT', 'TEXTAREA'],
                 callback: function (event, hotkey) {
                     var focusType = document.activeElement.type;
                     if (focusType == "text" || focusType == "select-one") {
                         $scope.changePage(1);
                         //event.preventDefault();
                         //return false;
                         //$("input[name='addBtn']").trigger("click");
                     }
                 }
             });

             $scope.openOrgs = function (record) {
                 popUpWindow("/SelectSingleOrg.aspx", "SelectSingleOrg", 620, 450);

             };

             $scope.getCheck = function (record) {
                 var rtn = "";
                 $.each($scope.VM.checkAry, function (index, item) {
                     if (item.EV == record.C) {
                         rtn = item.EN;
                     }
                 });
                 return rtn;
             };

    

             var popUpWindow = function (url, title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }


             $scope.goDelete = function (item, index) {

                 if ($scope.VM.fList.length <= 1) {
                     alert("最少需要一個附檔，所以無法刪除");
                     return;
                 }


                 var postData = {};
                 postData["i"] = item.I;
                 postData["ui"] = i;


                 $http({
                     method: 'POST',
                     url: "/System/AccountM/AccountCheck_Detail_DeleteDocFileOP.aspx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
                 .success(function (data, status, headers, config) {
                     data.FileCount = data.FileCount || 0;
                     data.Chk = data.Chk || 0;
                     if (data.FileCount == 0) {
                         alert("無附檔，刪除失敗");
                     }
                     else if (data.FileCount == 1) {
                         alert("最少需要一個附檔，所以無法刪除");
                     }
                     else {
                         if (data.Chk > 0) {
                             alert("刪除成功");
                             $scope.VM.fList = $filter('filter')($scope.VM.fList, { I: '!' + item.I });
                             $scope.$apply(function () { });
                         }
                         else {
                             alert("刪除失敗");
                         }
                     }
                 })
                  .error(function (data, status, headers, config) {
                      // called asynchronously if an error occurs
                      // or server returns response with an error status.
                  });

             };
}]);

