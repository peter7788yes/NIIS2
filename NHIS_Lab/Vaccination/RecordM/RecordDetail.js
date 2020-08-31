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
                $scope.TM.ApplyRecord = [];
                $scope.TM.ApplyHealth = [];
                $scope.TM.ApplyEffect = [];
                $scope.TM.ApplyRecord = ApplyRecord;
                $scope.TM.ApplyHealth = ApplyHealth;
                $scope.TM.ApplyEffect = ApplyEffect;


                $scope.RemoveApplyRecord = function (record) {
                    var postData = {};
                    postData["I"] = record.I;

                    return;

                    $http({
                        method: 'POST',
                        url: "/Vaccination/RecordM/ApplyHealth_RemoveOP.aspx",
                        data: $.param(postData),
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                    })
                   .success(function (data, status, headers, config) {
                       data.chk = data.chk || 0;
                       if (data.chk > 0) {
                           alert('儲存成功');

                           angular.forEach($scope.TM.ApplyHealth, function (value, key) {
                               if (value.I == record.I) {
                                   $scope.TM.ApplyHealth.splice(key, 1);
                               }
                           });

                       }
                       else {
                           alert('儲存失敗');
                       }
                   })
                   .error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                   });
                };


                $scope.RemoveApplyHealth = function (record) {
                    var postData = {};
                    postData["I"] = record.I;



                    $http({
                        method: 'POST',
                        url: "/Vaccination/RecordM/ApplyHealth_RemoveOP.aspx",
                        data: $.param(postData),
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                    })
                   .success(function (data, status, headers, config) {
                       data.chk = data.chk || 0;
                       if (data.chk > 0) {
                           alert('儲存成功');

                           angular.forEach($scope.TM.ApplyHealth, function (value, key) {
                               if (value.I == record.I) {
                                   $scope.TM.ApplyHealth.splice(key, 1);
                               }
                           });

                       }
                       else {
                           alert('儲存失敗');
                       }
                   })
                   .error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                   });
                };


                $scope.RemoveApplyEffect = function (record) {
                    var postData = {};
                    postData["i"] = record.I;
                    console.log(record.I);
                    $http({
                        method: 'POST',
                        url: "/Vaccination/RecordM/ApplyEffect_RemoveOP.aspx",
                        data: $.param(postData),
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                    })
                   .success(function (data, status, headers, config) {
                       data.chk = data.chk || 0;
                       if (data.chk > 0) {
                           alert('儲存成功');

                           angular.forEach($scope.TM.ApplyEffect, function (value, key) {
                               if (value.I == record.I) {
                                   $scope.TM.ApplyEffect.splice(key, 1);
                               }
                           });

                       }
                       else {
                           alert('儲存失敗');
                       }
                   })
                   .error(function (data, status, headers, config) {
                       // called asynchronously if an error occurs
                       // or server returns response with an error status.
                   });
                };

                $("#dvHealth").show();
                $("#dvEffect").show();
         }]);