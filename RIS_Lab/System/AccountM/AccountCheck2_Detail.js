$(function () {

    $(document).on("click", "#lastBtn", function (e) {
        location.href = "/System/AccountM/AccountCheck2.aspx";
        e.preventDefault();
        return false;
    });

    $("#on_cy").text(sessionStorage.getItem("AccountCheck2_on") + " - " + sessionStorage.getItem("AccountCheck2_cy"));
   
});

angular.module("MyApp", ["PageM", "cfp.hotkeys"])
         .controller("MyController", ["$scope", "PageProvider", "hotkeys", function ($scope, PageProvider, hotkeys) {


             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};

             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};
                 postData['i'] = i;
                 postData = $scope.PM.filterPageData(pgData, postData);
                 $scope.PM.changePage("/System/AccountM/AccountCheck2_DetailOP.aspx", postData, function (data) {
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
                     }
                 }
             });
       
}]);


