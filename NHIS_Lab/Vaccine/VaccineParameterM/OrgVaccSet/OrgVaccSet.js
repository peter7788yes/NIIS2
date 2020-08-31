angular.module("OrgVaccSetApp", ["TableM"])
         .controller("OrgVaccSetController", ["$scope", "TableProvider", function ($scope, TableProvider) {
             //這裡是放JS控制項
             //沒有智慧控制是因為我用NoDEBUG包起來了
             $scope.TM = TableProvider;
             $scope.TM.isHtml = false;

             $scope.VM = {};
             $scope.VM.Status = [{ EV: '0', EN: "全部" }].concat(StatusData);
             $scope.VM.SelectStatus = "0";
             //var Data = new Array();
             var RexNumber = /^\d*$/;
             //取得疫苗狀態名稱
             $scope.Status = function (record) {
                 var rtn = "";
                 $.each($scope.VM.Status, function (index, item) {
                     if (item.EV == record.c5) {
                         rtn = item.EN;
                     }
                 });
                 return rtn;
             };
             //儲存安全庫存量設定頁面資料
             $scope.SaveData = function (record) {
                 var msg = "";
                 if (RexNumber.test(record.c6) == false) {
                     msg += "安全庫存量:請輸入正整數!\n";
                 }
                 if (RexNumber.test(record.c7) == false) {
                     msg += "效期提醒天數:請輸入正整數!\n";
                 }
                 if (msg.length != 0) {
                     alert(msg);
                     $scope.GetData();
                 }
                 else {
                     var postData = {};
                     postData.ID = record["c2"];
                     postData.SafeNum = record["c6"];
                     postData.AvaPeriod = record["c7"];

                     $.ajax({
                         cache: false,
                         type: "POST",
                         url: "/Vaccine/VaccineParameterM/OrgVaccSet/Modify_OrgVaccSetOP.aspx",
                         data: postData
                     })
                    .done(function (data) {
                        if (data.CheckNum == 1) {
                            alert("需大於等於原預設值!");
                            $scope.GetData();
                        }
                        else if (data.CheckNum == 2) {
                            alert("需大於等於原預設值!");
                            $scope.GetData();
                        }
                    })
                    .fail(function (jqXHR, textStatus) {

                    });
                 }
             }
             //安全庫存量設定頁面取得資料
             $scope.GetData = function () {

                 var postData = {};
                 postData.Status = $scope.VM.SelectStatus;
                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/Vaccine/VaccineParameterM/OrgVaccSet/OrgVaccSetOP.aspx",
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
             $scope.GetData();
         }]);
//angular.bootstrap(document.getElementById("OrgVaccSetApp"), ["OrgVaccSetApp"]);

