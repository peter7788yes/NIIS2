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
             $scope.VM.tbStudent = tbStudent;
             $scope.VM.tbCard = tbCard;
             if ($scope.VM.tbStudent > 0) {
                 $scope.VM.percent = ((parseFloat($scope.VM.tbCard) / parseFloat($scope.VM.tbStudent)) * 100).toFixed(2);
             }
             else
             {
                 $scope.VM.percent = 0;
             }


             $scope.VM.Iary = [];
             $scope.VM.Vary = [];
             $scope.VM.Sary = [];

             $scope.TM = {};

             //console.log(tbAry);

             angular.forEach(tbAry, function (value1, key1) {
                 value1.EV = value1.EV || 0;

                 angular.forEach(tbAry2, function (value2, key2) {
                   
                     value2.VT = value2.VT || 0;

                     if (value1.EV > 0 && value2.VT>0&&value1.EV == value2.VT)
                     {
                         value1.Number = value2.IN;
                         value1.sNumber = value2.SIN;

                         value1.Percent = ((parseFloat(value1.Number) / parseFloat(value1.sNumber)) * 100).toFixed(2);
                     }
                 });

                 if (value1.Number == undefined || value1.Number < 1)
                 {
                     //console.log(111);
                     value1.Number = 0;
                     //value1.sNumber = 0;
                     value1.Percent = 0;
                 }
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
                     angular.forEach(tbAry2, function (value2, key2) {
                         value2.VT = value2.VT || 0;
                         if (value.EV == value2.VT) {
                             $scope.VM.Vary.push(value2.I);
                         }
                     });
                     $scope.VM.Iary.push(value.Number);
                     $scope.VM.Sary.push(value.sNumber);
                 });

                 //console.log($scope.VM.Vary);
                 //console.log($scope.VM.Iary);
                 //console.log($scope.VM.Sary);
             };
            
}]);
