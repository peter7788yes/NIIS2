$(function () {
    $(document).on("click", "#lastBtn", function (e) {
        location.href = "/System/AccountM/AccountMaintain.aspx";
        e.preventDefault();
        return false;
    });
});


angular.module("MyApp", ["PageM",'FilterM', "cfp.hotkeys"])
         .controller("MyController", ["$scope", "PageProvider", "hotkeys", function ($scope, PageProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};
             $scope.VM.NL = NL;
             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};
                 postData['i'] = i;
                 postData = $scope.PM.filterPageData(pgData, postData);
                 $scope.PM.changePage("/System/AccountM/AccountMaintain_DetailOP.aspx", postData, function (data) {
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
                     var focusType = document.activeElement.type;
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
                 var key = 2;
                 if (record.IE == true)
                     key = 1;
                 var rtn = "";
                 $scope.VM.enableAry = $scope.VM.enableAry || [];
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
                 $scope.VM.checkAry = $scope.VM.checkAry || [];
                 $.each($scope.VM.checkAry, function (index, item) {
                     if (item.EV == record.C) {
                         rtn = item.EN;
                     }
                 });
                 return rtn;
             };
}]);
