$(function () {
    $(document).on("click", "#closeBtn", function (e) {
        window.close();
        e.preventDefault();
        return false;
    });
});

angular.module("MyApp", [])
         .controller("MyController", ["$scope", "$http", function ($scope, $http) {

             $scope.TM = {};
             //$scope.TM.isHide = true;
             $scope.VM = {};
             $scope.VM.title = "";
             $scope.VM.publishState = "0";

             $scope.VM.agency = "";
             $scope.VM.agencyID = 0;

             $scope.goAddYellowCard = function () {
                 popUpWindow("/Vaccination/RecordM/YellowCard_Add.aspx", "YellowCard_Add", 610, 300);
                 //window.open("/Vaccination/RecordM/YellowCard_Add.aspx", "YellowCard_Add", "width=610,height=300,toolbar=no,menubar=no,scrollbars=yes,resizable=yes");
             };

             $scope.openSelectAgency = function (record) {
                 popUpWindow("/Vaccination/RecordM/ApplyRecord_SelectAgency.aspx", "SelectAgency", 930, 450);

             };


             $scope.refresh = function () {
                 var agency = $('#tbAgency').val();
                 var agencyID = $('#hfAgencyID').val();

                 setTimeout(function () {
                     $scope.$apply(function () {
                         $scope.VM.agency = agency;
                         $scope.VM.agencyID = agencyID;
                     });
                 }, 1);

             };

             var popUpWindow = function (url, title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }
         }]);



var getAgency = function (code) {
    $("#tbAgency").val(code.AN);
    $("#hfAgencyID").val(code.I);
    $('#refreshBtn').trigger('click');
};