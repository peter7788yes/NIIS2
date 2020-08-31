$(function () {
    $(document).on("click", "#addBtn", function (e) {
        location.href = "/System/AccountM/AccountMaintain_Add.aspx";
        e.preventDefault();
        return false;
    });
   
});


angular.module("MyApp", ["PageM", "TableM", "cfp.hotkeys"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", "hotkeys", function ($scope, PageProvider, TableProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};
             $scope.VM.roleAry = [{ I: '0', R: "全部" }].concat(roleData);
             $scope.VM.enableAry = [{ EV: '0', EN: "全部" }].concat(enableData);
             $scope.VM.checkAry = [{ EV: '0', EN: "全部" }].concat(checkData);
             $scope.VM.logoutAry = [{ EV: '0', EN: "全部" }].concat(logoutData);
             $scope.VM.selectRole = "0";
             $scope.VM.selectEnable = "0";
             $scope.VM.selectCheck = "0";
             $scope.VM.selectLogout = "0";



             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};
                 postData.D = $scope.VM.title;
                 postData.p = $scope.VM.publishState;
                 postData = $scope.PM.filterPageData(pgData, postData);
                 $scope.PM.changePage("/System/AccountM/AccountMaintainOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };

             $scope.changePage(1);

             hotkeys.add({
                 combo: 'enter',
                 description: 'Description goes here',
                 allowIn: ['INPUT',"BUTTON", 'SELECT', 'TEXTAREA'],
                 callback: function (event, hotkey) {
                     var focusType = document.activeElement.type
                     if (focusType == "text" || focusType == "select-one")
                     {
                         //$scope.changePage(1);
                         //event.preventDefault();
                         //return false;
                         //$("input[name='addBtn']").trigger("click");
                     }
                 }
             });


             

             $scope.getEnable = function (record) {
                 var key = 1;
                 if (record.IE == true)
                     key = 2;
                 var rtn = "";
                 $.each($scope.VM.enableAry, function (index, item) {
                     if (item.EV == key)
                     {
                         rtn = item.EN;
                     }
                 });
                 return rtn;
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

}]);


//開起子視窗=================================================================================================================================
//function PopWin(targetURL) {
//    newWINwidth = 600;
//    newWINheight = 800;
//    window.open(targetURL, 'tarWin', "width=" + newWINwidth + ",height=" + newWINheight + ",toolbar=1,resizable=0,scrollbars=yes,status=yes");
//}
//function PopWin(targetURL, newWINwidth, newWINheight) {
//    window.open(targetURL, 'tarWin', "width=" + newWINwidth + ",height=" + newWINheight + ",toolbar=1,resizable=0,scrollbars=yes,status=yes");
//}

//function PopWin(targetURL, newWINwidth, newWINheight, targetID) {
//    window.open(targetURL, targetID, "width=" + newWINwidth + ",height=" + newWINheight + ",toolbar=1,resizable=0,scrollbars=yes,status=yes");
//}
//開起子視窗(END)=============================================================================================================================