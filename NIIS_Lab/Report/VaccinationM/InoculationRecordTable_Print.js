//$(function () {
//    $("#divReport").load("/Report/VaccinationM/InoculationRecordTable_Print.html", function () {
//    });
//});

$(function () {
});

angular.module("MyApp", ["FilterM"])
         .controller("MyController", ["$scope", function ($scope) {
             $scope.TM = {};
             $scope.TM.data = tbData;
             $("#tmBlock").show();
}]);


