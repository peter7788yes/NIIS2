angular.module("SystemParametersApp", ["TableM"])
         .controller("SystemParametersController", ["$scope", "TableProvider", function ($scope, TableProvider) {
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             $scope.VM = {};
             $scope.VM.Para = "";

             //參數設定頁面查詢按鈕的功能
             $scope.Search = function () {
                 var postData = {};
                 postData.Para = $scope.VM.Para;

                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/System/SystemSettingsM/SystemParameters/SystemParametersOP.aspx",
                     data: postData
                 })
                .done(function (data) {
                    $scope.TM.tbData = data.message;
                    $scope.$apply(function () {
                    });
                })
                .fail(function (jqXHR, textStatus) {

                });
             }
             $scope.Search();
             //儲存參數值
             $scope.SaveData = function (record) {
                 var postData = {};
                 postData.ID = record["c2"];
                 postData.ParaValue = record["c5"];

                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/System/SystemSettingsM/SystemParameters/Update_SystemParametersOP.aspx",
                     data: postData
                 })
                .done(function (data) {
                    if (data.Success > 0) {
                        //alert("儲存成功!");
                    }
                    else {
                        //alert("儲存失敗!");
                    }
                    $scope.$apply(function () {
                    });
                })
                .fail(function (jqXHR, textStatus) {

                });
             }
         }]);
angular.bootstrap(document.getElementById("SystemParametersApp"), ["SystemParametersApp"]);

