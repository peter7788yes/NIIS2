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
           
            
}]);
