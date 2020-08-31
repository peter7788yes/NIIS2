var getCode = function (code) {
    $("#OrgName").val(code.text);
    $("#OrgID").val(code.id);
    $('#refreshBtn').trigger('click');
    location.href = "/System/SystemSettingsM/SendMessageAccountSet/SendMessageAccountSet.aspx?ID=" + code.id;
};
var popUpWindow = function (url, title, w, h) {
    var left = (screen.width / 2) - (w / 2);
    var top = (screen.height / 2) - (h / 2);
    return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
}
angular.module("SendMessageAccountSetApp", [])
         .controller("SendMessageAccountSetController", ["$scope", function ($scope) {

             $scope.VM = {};
             $scope.VM.MsgAccount = "";
             $scope.VM.MsgPassWord = "";
             $scope.VM.MsgStatus = 1;
             $scope.VM.OrgName = "";
             $scope.VM.OrgID = OrgID;
             if (url('?ID') != null) {
                 $scope.VM.OrgID = url('?ID');
                 $scope.VM.MsgStatus = 0;
             }
             
             //開起組織單位
             $scope.openOrgs = function () {
                 popUpWindow("/SelectSingleOrg.aspx", "SelectSingleOrg", 930, 450);
             };
             //開起測式發送簡訊
             $scope.openTestMessage = function () {
                 popUpWindow("/System/SystemSettingsM/SendMessageAccountSet/Test_SendMessageAccountSet.aspx?O=" + $scope.VM.OrgID, "Test_SendMessageAccountSet", 620, 500);
             }
             //取得此單位的帳號密碼
             $scope.GetData = function () {
                 var postData = {};
                 postData.OrgID = $scope.VM.OrgID;

                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/System/SystemSettingsM/SendMessageAccountSet/SendMessageAccountSetOP.aspx",
                     data: postData
                 })
                .done(function (data) {
                    if (data.message.length != 0) {
                        $scope.VM.OrgName = data.message[0].OrgName;
                        $scope.VM.OrgID = data.message[0].OrgID;
                        $scope.VM.MsgAccount = data.message[0].MsgAccount;
                        $scope.VM.MsgPassWord = data.message[0].MsgPassWord;
                        $scope.VM.MsgStatus = data.message[0].MsgStatus;
                    }
                    $scope.$apply(function () {
                    });
                })
                .fail(function (jqXHR, textStatus) {

                });
             }
             $scope.GetData();
             //儲存變更資料
             $scope.SaveData = function () {
                 var postData = {};
                 postData.ID = $scope.VM.OrgID;
                 postData.MsgAccount = $scope.VM.MsgAccount;
                 postData.MsgPassWord = $scope.VM.MsgPassWord;
                 postData.MsgStatus = $scope.VM.MsgStatus;
                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/System/SystemSettingsM/SendMessageAccountSet/Update_SendMessageAccountSetOP.aspx",
                     data: postData
                 })
                .done(function (data) {
                    if (data.Success > 0) {
                        alert("儲存成功!");
                    }
                    else {
                        alert("儲存失敗!");
                    }
                    $scope.$apply(function () {
                    });
                })
                .fail(function (jqXHR, textStatus) {

                });
             }
         }])
         .controller("TestSendMessageAccountSetController", ["$scope", "$http", function ($scope, $http) {

             $scope.VM = {};
             $scope.VM.Content = "";
             $scope.VM.Staff = "";
             $scope.VM.MsgAccount = "";
             $scope.VM.MsgPassWord = "";
             $scope.StaffNum = [];
             $scope.VM.Staffs = [];
             $scope.VM.Title = "測試發送簡訊設定";

             //檢查電話格式
             $scope.CheckPhone = /09[0-9]{8}/;
             //關閉開啟式窗頁面
             $scope.CloseWin = function () {
                 window.close();
             }
             //發送簡訊資料
             $scope.SendMessageData = function () {
                 var checkback = 0;
                 if (confirm("你確定要發送訊息?")) {
                     var postData = {};
                     postData.OrgID = url('?O');
                     postData.MsgAccount = $scope.VM.MsgAccount;
                     postData.MsgPassWord = $scope.VM.MsgPassWord;
                     $http({
                         method: 'POST',
                         url: "/System/SystemSettingsM/SendMessageAccountSet/Test_SendMessageAccountSetOP.aspx",
                         data: $.param(postData),
                         headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                     })
                     .success(function (data, status, headers, config) {
                         if (data.DataError == 0) {
                             if (data.CheckAccount == 1 && data.CheckPassWord == 1) {
                                 alert("發送成功!");
                                 checkback++;
                                 if (checkback == 1) {
                                     window.close();
                                 }
                             }
                             else if (data.CheckAccount == 0 && data.CheckPassWord == 0) {
                                 alert("帳號輸入錯誤!\n密碼輸入錯誤!\n發送失敗!");
                             }
                             else if (data.CheckAccount == 0) {
                                 alert("帳號輸入錯誤!\n發送失敗!");
                             }
                             else if (data.CheckPassWord == 0) {
                                 alert("密碼輸入錯誤!\n發送失敗!");
                             }
                             
                         }
                         else {
                             alert("取得資料失敗!");
                         }
                     })
                     .error(function (data, status, headers, config) {
                         // called asynchronously if an error occurs
                         // or server returns response with an error status.
                     });
                 }
             }
             
         }]);
angular.bootstrap(document.getElementById("SendMessageAccountSetApp"), ["SendMessageAccountSetApp"]);

