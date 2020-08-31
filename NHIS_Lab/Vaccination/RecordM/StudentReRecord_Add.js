$(function () {
    $(document).on("click", "#lastBtn", function (e) {
        location.href = "/Vaccination/RecordM/StudentReRecord.aspx";
        e.preventDefault();
        return false;
    });

});

angular.module("MyApp", [])
         .controller("MyController", ["$scope", function ($scope) {

             $scope.VM = {};
             $scope.VM.tbStudent = 0;
             $scope.VM.tbCard = 0;
             $scope.VM.percent = 0;

             $scope.VM.sAry = [{ "I": 0, "N": "請選擇學校名稱" }].concat(sAry);
             $scope.VM.selectSchool = "0";

             $scope.VM.Iary = [];
             $scope.VM.Vary = [];
             $scope.VM.Sary = [];

             $scope.TM = {};

             angular.forEach(tbAry, function (value, key) {
                 value.sNumber = 0;
                 value.Number = 0;
                 value.Percent = 0;
                 $scope.VM.Vary.push(value.EV);
                 $scope.VM.Iary.push(0);
                 $scope.VM.Sary.push(0);
             });


             //console.log(tbAry);
             $scope.TM.data = tbAry;


             $scope.changeAll = function () {
                 $scope.changePercent();
                 if ($scope.VM.tbStudent > 0) {
                     angular.forEach($scope.TM.data, function (value, key) {
                         value.Percent = ((parseFloat(value.Number) / parseFloat($scope.VM.tbStudent)) * 100).toFixed(2);
                     });
                 }
                 //getVI();
             };


             $scope.changePercent = function () {

                 if ($scope.VM.tbStudent > 0) {
                     $scope.VM.percent = ((parseFloat($scope.VM.tbCard) / parseFloat($scope.VM.tbStudent)) * 100).toFixed(2);
                 }

             };


             $scope.changePercent2 = function (record) {

                 if ($scope.VM.tbStudent > 0) {
                     record.Percent = ((parseFloat(record.Number) / parseFloat($scope.VM.tbStudent)) * 100).toFixed(2);
                 }
                 //getVI();
             };


             $scope.goAdd = function () {
                 getVI();
             };



             var getVI = function () {

                 $scope.VM.Vary = [];
                 $scope.VM.Iary = [];
                 $scope.VM.Sary = [];

                 angular.forEach($scope.TM.data, function (value, key) {
                     $scope.VM.Vary.push(value.EV);
                     $scope.VM.Iary.push(value.Number);
                     $scope.VM.Sary.push(value.sNumber);
                 });

                 //console.log($scope.VM.Vary);
                 //console.log($scope.VM.Iary);
                 //console.log($scope.VM.Sary);
             };

         }]);
