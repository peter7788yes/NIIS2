$(function () {
    $(document).on("click", "#lastBtn", function (e) {
        location.href = "/Vaccination/RecordM/BCGRecord.aspx";
        e.preventDefault();
        return false;
    });

});

angular.module("MyApp", [])
         .controller("MyController", ["$scope", function ($scope) {
             $scope.VM = {};


             $scope.VM.tbBirthNumber = 0;
             $scope.VM.tbKid = 0;
             $scope.VM.tbBaby = 0;

             $scope.VM.tbBabyNoScar1 = 0;
             $scope.VM.tbKidNoScar1 = 0;
             $scope.VM.tbOtherNoScar1 = 0;
             $scope.VM.tbOtherHasScar1 = 0;

             $scope.VM.tbBabyNoScar2 = 0;
             $scope.VM.tbKidNoScar2 = 0;
             $scope.VM.tbOtherNoScar2 = 0;
             $scope.VM.tbOtherHasScar2 = 0;


             $scope.VM.tbBabyNoScar3 = 0;
             $scope.VM.tbKidNoScar3 = 0;
             $scope.VM.tbOtherNoScar3 = 0;
           
             $scope.VM.all1 = 0;
             $scope.VM.all2 = 0;
             $scope.VM.all3 = 0;


             $scope.changeAll = function () {

                 $scope.VM.all1 = ($scope.VM.tbBabyNoScar1 * 1) + ($scope.VM.tbKidNoScar1 * 1) + ($scope.VM.tbOtherNoScar1 * 1) + ($scope.VM.tbOtherHasScar1 * 1);
                 $scope.VM.all2 = ($scope.VM.tbBabyNoScar2 * 1) + ($scope.VM.tbKidNoScar2 * 1) + ($scope.VM.tbOtherNoScar2 * 1) + ($scope.VM.tbOtherHasScar2 * 1);
                 $scope.VM.all3 = ($scope.VM.tbKid * 1) + ($scope.VM.tbBaby * 1) + ($scope.VM.tbBabyNoScar3 * 1) + ($scope.VM.tbKidNoScar3 * 1) + ($scope.VM.tbOtherNoScar3 * 1);
             };
            

             $scope.changeAll();
}]);
