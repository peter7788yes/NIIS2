$(function () {
    $(document).on("click", "#addBtn", function (e) {
        location.href = "/Vaccination/RecordM/RegisterData_Add.aspx";
        e.preventDefault();
        return false;
    });

   
});

angular.module("MyApp", ["PageM"])
         .controller("MyController", ["$scope", "PageProvider", function ($scope, PageProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             //$scope.TM.isHide = true;
             $scope.VM = {};
             $scope.VM.title = "";
             $scope.VM.publishState = "0";

             $scope.changePage = function (pageIndex) {
                 //$("#pmBlock").show();
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};
                 postData.D = $scope.VM.title;
                 postData.p = $scope.VM.publishState;
                 postData = $scope.PM.filterPageData(pgData, postData);
                 $scope.PM.changePage("/Vaccination/RecordM/RegisterDataOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () {});
                 });
             };

             $scope.changePage(1);

             $scope.goAddYellowCard = function () {
                 popUpWindow("/Vaccination/RecordM/YellowCard_Add.aspx", "YellowCard_Add", 610, 300);
                 //window.open("/Vaccination/RecordM/YellowCard_Add.aspx", "YellowCard_Add", "width=610,height=300,toolbar=no,menubar=no,scrollbars=yes,resizable=yes");
             };

             var popUpWindow = function (url, title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }
}]);