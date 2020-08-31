$(function () {
    $(document).on("click", "#lastBtn", function (e) {
        history.go(-1);
        e.preventDefault();
        return false;
    });
    $(document).on("click", "#addBtn", function (e) {
        location.href = "/System/CodeM/CodeSetting_Add.aspx?i=" + I;
        e.preventDefault();
        return false;
    });
    
});

angular.module("MyApp", ["PageM", "TableM", "cfp.hotkeys"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", "hotkeys", function ($scope, PageProvider, TableProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};
             $scope.VM.title = "";
             $scope.VM.publishState = "0";

             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};
                 postData.I = I;
                 postData = $scope.PM.filterPageData(pgData, postData);
                 $scope.PM.changePage("/System/CodeM/CodeSetting_DetailOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () {});
                 });
             };

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


             $scope.changePage(1);

             
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