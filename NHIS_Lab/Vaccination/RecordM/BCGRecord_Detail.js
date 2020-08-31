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

             $scope.VM.tbBirthNumber = tbBirthNumber;
             $scope.VM.tbKid = tbKid;
             $scope.VM.tbBaby = tbBaby;

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

             $scope.TM = {};
             $scope.TM.data = tbAry;

             
             angular.forEach(tbAry, function (value, key) {
                 switch(value.KT)
                 {
                     case 1:
                         switch(value.TT)
                         {
                             case 1:
                                 $scope.VM.tbBabyNoScar1 = value.KN;
                                 break;
                             case 2:
                                 $scope.VM.tbBabyNoScar2 = value.KN;
                                 break;
                             case 3:
                                 $scope.VM.tbBabyNoScar3 = value.KN;
                                 break;
                         }
                         break;
                     case 2:
                         switch (value.TT) {
                             case 1:
                                 $scope.VM.tbKidNoScar1 = value.KN;
                                 break;
                             case 2:
                                 $scope.VM.tbKidNoScar2 = value.KN;
                                 break;
                             case 3:
                                 $scope.VM.tbKidNoScar3 = value.KN;
                                 break;
                         }
                         break;
                     case 3:
                         switch (value.TT) {
                             case 1:
                                 $scope.VM.tbOtherNoScar1 = value.KN;
                                 break;
                             case 2:
                                 $scope.VM.tbOtherNoScar2 = value.KN;
                                 break;
                             case 3:
                                 $scope.VM.tbOtherNoScar3 = value.KN;
                                 break;
                         }
                         break;
                     case 4:
                         switch (value.TT) {
                             case 1:
                                 $scope.VM.tbOtherHasScar1 = value.KN;
                                 break;
                             case 2:
                                 $scope.VM.tbOtherHasScar2 = value.KN;
                                 break;
                         }
                         break;
                        

                 }
             });


             $scope.changeAll = function () {

                 $scope.VM.all1 = ($scope.VM.tbBabyNoScar1 * 1) + ($scope.VM.tbKidNoScar1 * 1) + ($scope.VM.tbOtherNoScar1 * 1) + ($scope.VM.tbOtherHasScar1 * 1);
                 $scope.VM.all2 = ($scope.VM.tbBabyNoScar2 * 1) + ($scope.VM.tbKidNoScar2 * 1) + ($scope.VM.tbOtherNoScar2 * 1) + ($scope.VM.tbOtherHasScar2 * 1);
                 $scope.VM.all3 = ($scope.VM.tbKid * 1) + ($scope.VM.tbBaby * 1) + ($scope.VM.tbBabyNoScar3 * 1) + ($scope.VM.tbKidNoScar3 * 1) + ($scope.VM.tbOtherNoScar3 * 1);
             };
           
             $scope.changeAll();
}]);
