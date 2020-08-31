$(function () {
    $(document).on("click", "#closeBtn", function (e) {
        window.close();
        e.preventDefault();
        return false;
    });
});


angular.module("MyApp", ["FilterM"])

         .controller("MyController", ["$scope", "$http", function ($scope, $http) {
             $scope.TM = {};
             //$scope.TM.ApplyEffect = ApplyEffect;

             //$scope.RemoveApplyHealth = function (record) {

             //    var postData = {};
             //    postData["I"] = record.I;

             //    $http({
             //        method: 'POST',
             //        url: "/Vaccination/RecordM/ApplyHealth_RemoveOP.aspx",
             //        data: $.param(postData),
             //        headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
             //    })
             //   .success(function (data, status, headers, config) {
             //       if (data.chk > 0) {
             //           alert('儲存成功');

             //           angular.forEach($scope.TM.ApplyHealth, function (value, key) {
             //               if (value.I == record.I) {
             //                   $scope.TM.ApplyHealth.splice(key, 1);
             //               }
             //           });

             //       }
             //       else {
             //           alert('儲存失敗');
             //       }
             //   })
             //   .error(function (data, status, headers, config) {
             //       // called asynchronously if an error occurs
             //       // or server returns response with an error status.
             //   });
             //};
}]);