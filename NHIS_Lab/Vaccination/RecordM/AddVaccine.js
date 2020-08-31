$(function () {
    $(document).on("click", "#closeBtn", function (e) {
        window.close();
        e.preventDefault();
        return false;
    });
});

angular.module("MyApp", [])
         .controller("MyController", ["$scope","$http", function ($scope,$http) {
             $scope.TM = {};
             $scope.TM.data = tbAry;

             $scope.goAdd = function (record) {
                 var postData = {};
                 postData["C"] = C;
                 postData["R"] = record.VI;

                 $http({
                     method: 'POST',
                     url: "/Vaccination/RecordM/AddVaccineOP.aspx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
                .success(function (data, status, headers, config) {
                    if (data.chk > 0) {
                        //console.log(data);
                        //{chk: 1, message: "30"}
                        var rtnData={};
                        rtnData["I"] = data.message;
                        rtnData["R"] = record.VI;
                        window.opener.getNewVaccine(rtnData);
                        window.close();
                        return false;
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

}]);

