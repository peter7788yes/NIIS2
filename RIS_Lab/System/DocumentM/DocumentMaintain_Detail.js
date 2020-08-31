$(function () {
    $(document).on("click", "#lastBtn", function (e) {
        history.go(-1);
        e.preventDefault();
        return false;
    });
});


angular.module("MyApp", [])
         .controller("MyController", ["$scope", '$filter' ,'$http', function ($scope, $filter,$http) {

             $scope.VM = {};
             $scope.VM.i = i;
             $scope.VM.fileList = [];
             $scope.VM.fileList = fileList;

             $scope.goDelete = function (item,index) {

                 if ($scope.VM.fileList.length <=1) {
                     alert("最少需要一個附檔，所以無法刪除");
                     return;
                 }


                 var postData = {};
                 postData["i"] = item.F;
                 postData["d"] = $scope.VM.i; 


                 $http({
                     method: 'POST',
                     url: "/System/DocumentM/DocumentMaintain_DeleteDocFileOP.aspx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
                 .success(function (data, status, headers, config) {
                     data.FileCount = data.FileCount||0;
                     data.Chk =data.Chk||0;
                     if(data.FileCount==0){
                         alert("無附檔，刪除失敗");
                     }
                     else if (data.FileCount == 1) {
                         alert("最少需要一個附檔，所以無法刪除");
                     }
                     else
                     {
                         if(data.Chk>0)
                         {
                             alert("刪除成功");
                             $scope.VM.fileList = $filter('filter')($scope.VM.fileList, { F: '!' + item.F });
                             $scope.$apply(function () { });
                         }
                         else
                         {
                             alert("刪除失敗");
                         }
                     }
                 })
                  .error(function (data, status, headers, config) {
                      // called asynchronously if an error occurs
                      // or server returns response with an error status.
                  });
               


             };

}]);