angular.module("BatchListApp", ["PageM", "InputM", "TableM", "FilterM", "ngSanitize"])
         .controller("BatchListController", ["$scope", "TableProvider", function ($scope, TableProvider) {
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             $scope.VM = {};
             $scope.VM.Vaccine = VaccineData;
             $scope.VM.VaccineSelect = $scope.VM.Vaccine[0].ID;

             if (url('?Page') == "In") {
                 $scope.VM.PageName = "撥入疫苗：";
             }
             else if (url('?Page') == "Out") {
                 $scope.VM.PageName = "撥出疫苗：";
             }
             else if (url('?Page') == "Use") {
                 $scope.VM.PageName = "領用疫苗：";
             }
             else if (url('?Page') == "Dam") {
                 $scope.VM.PageName = "損毀疫苗：";
             }
             else if (url('?Page') == "Return") {
                 $scope.VM.PageName = "退貨疫苗：";
             }

             //轉換到新增疫苗批號頁面
             $scope.TransferNewListData = function (record) {
                 var Page = url('?Page');
                 location.href = "/Vaccine/StockManagementM/Vaccine" + Page + "/New_Vaccine" + Page + "Batch.aspx?BI=" + record["c1"] + "&I=" + url('?ID');
             };
             //取得疫苗批號資料
             $scope.GetVaccineBatch = function () {
                 //因應查詢條件
                 var postData = {};
                 postData.Page = url('?Page');
                 postData.ID = url('?ID');
                 postData.SelectID = $scope.VM.VaccineSelect;
                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/Vaccine/StockManagementM/StockCommonPage/BatchListOP.aspx",
                     data: postData
                 })
                .done(function (data) {
                    $scope.TM.tbData = data.message;
                    $scope.$apply(function () {
                    });
                })
                .fail(function (jqXHR, textStatus) {

                });
             };
             $scope.GetVaccineBatch();
         }]);
angular.bootstrap(document.getElementById("BatchListApp"), ["BatchListApp"]);