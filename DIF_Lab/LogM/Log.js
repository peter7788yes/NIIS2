angular.module("MyApp", ["PageM", "InputM", "TableM", "FilterM", "ngSanitize"])
         .controller("MyController",["$scope","PageProvider","TableProvider", function ($scope, PageProvider, TableProvider) {
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.PM = PageProvider;

             $scope.PM.pgSize = 10;
             $scope.PM.pgRange = 10;
       
             

             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             $scope.x = {};
             console.log(url('?q'));
             $scope.x.h2 = url('?q');
             $scope.x.rocidF = "A123456789";
             $scope.x.numberF = 9;
             $scope.x.numberF2 = 999;
             $scope.numberF2 = 102;

             $scope.x.nowDate = new Date();

             $scope.onclick = function (message) {
                 alert(message);
             };

             $scope.changePage = function (pageIndex) {
                 //alert(pageIndex);
                 //更改頁碼，PageProvider物件
                 var pgData = $scope.PM.genPageData(pageIndex);

                 //因應查詢條件
                 var postData = {};
                 postData.sex = "Female";
                 postData.birthdYear = '2002';
                 //取得PostData，PostData物件
                 postData = $scope.PM.filterPageData(pgData, postData);
                 //var postData = pgData;

                 $scope.PM.changePage("/LogM/LogOP.aspx", postData, function (data) {
                     $scope.TM.tbHeadArray = ["序號", "ThreadID", "MachineName", "LogName", "LogLevel", "LogMessage", "CallSite", "Exception", "Stacktrace", "CreateDateTime"];
                     $scope.TM.tbFieldArray = ["c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8", "c9", "c10"];
                     $scope.TM.tbData = data.message;

                     //alert(JSON.stringify($scope.message));
                     //var scope = angular.element("body").scope();
                     $scope.$apply(function () {
                         //scope.EnTraceCode = response.EnTraceCode;
                         //scope.action = "VitaeInfo";
                         //$scope.PM = PageProvider;
                         //$scope.TM = TableProvider;
                     });
                 });
             };
}]);

//angular.bootstrap(document.getElementById("MyApp"), ["MyApp"]);




var queryString = location.search.substring(1, location.search.length);
var queryObject = {};
var temp = queryString.split('&');
$.each(temp, function (index, item) {
    var ary = item.split('=');
    queryObject[ary[0]] = ary[1];
});

console.log(queryObject);

//$($('h2')[0]).text(queryObject.q);
//$($('h2')[0]).text(url('?q'));
