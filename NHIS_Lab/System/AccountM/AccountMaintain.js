$(function () {
    $(document).on("click", "#addBtn", function (e) {
        location.href = "/System/AccountM/AccountMaintain_Add.aspx";
        e.preventDefault();
        return false;
    });
   
});


angular.module("MyApp", ["PageM", "cfp.hotkeys"])
         .controller("MyController", ["$scope", "PageProvider", "hotkeys", function ($scope, PageProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};
             $scope.VM.Uname="";
             $scope.VM.Oname="";
             $scope.VM.roleAry = [].concat(roleData);
             $scope.VM.enableAry = [].concat(enableData);
             $scope.VM.checkAry = [].concat(checkData);
             $scope.VM.logoutAry = [].concat(logoutData);
             $scope.VM.selectRole = "0";
             $scope.VM.selectEnable = "0";
             $scope.VM.selectCheck = "0";
             $scope.VM.selectLogout = "0";



             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};
                 postData["an"] = $scope.VM.Uname;
                 postData["on"] = $scope.VM.Oname;
                 postData["es"] = $scope.VM.selectEnable;
                 postData["cs"] = $scope.VM.selectCheck;
                 postData["lp"] = $scope.VM.selectLogout;

                 postData = $scope.PM.filterPageData(pgData, postData);
                 $scope.PM.changePage("/System/AccountM/AccountMaintainOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };

             $scope.changePage(1);

             hotkeys.add({
                 combo: 'enter',
                 description: 'Description goes here',
                 allowIn: ['INPUT',"BUTTON", 'SELECT', 'TEXTAREA'],
                 callback: function (event, hotkey) {
                     var focusType = document.activeElement.type;
                     if (focusType == "text" || focusType == "select-one")
                     {
                         $scope.changePage(1);
                         //event.preventDefault();
                         //return false;
                         //$("input[name='addBtn']").trigger("click");
                     }
                 }
             });


             

             $scope.getEnable = function (record) {
                 var key = 2;
                 if (record.IE == true)
                     key = 1;
                 var rtn = "";
                 $.each($scope.VM.enableAry, function (index, item) {
                     if (item.EV == key)
                     {
                         rtn = item.EN;
                     }
                 });
                 return rtn;
             };

             $scope.getCheck = function (record) {
                 var rtn = "";
                 $.each($scope.VM.checkAry, function (index, item) {
                     if (item.EV == record.C) {
                         rtn = item.EN;
                     }
                 });
                 return rtn;
             };


             $scope.goDetail = function (record) {
                 var keys = [];
                 var values = [];
                 keys[0] = "i";
                 values[0] = record["I"];
                 doPOST("/System/AccountM/AccountMaintain_Detail.aspx", keys, values);
                 //location.href = "/System/AccountM/AccountMaintain_Detail.aspx?i=" + record["I"];
             };

             var doPOST = function (url, keys, values) {
                 keys = keys || [];
                 values = values || [];
                 var html = "";
                 html += "<html><head></head><body><form id='formid' method='post' action='" + url + "' autocomplete='off'>";
                 if (keys && values && (keys.length == values.length))
                     for (var i = 0; i < keys.length; i++)
                         html += "<input type='hidden' name='" + keys[i] + "' value='" + values[i] + "'/>";
                 html += "</form><script type='text/javascript'>document.getElementById(\"formid\").submit()</script></body></html>";
                 document.write(html);
             };


             $scope.openOrgs = function (record) {
                 popUpWindow("/SelectSingleOrg.aspx", "SelectSingleOrg", 620, 450);

             };


             var popUpWindow = function (url, title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }
}]);


var getCode = function (code) {
    var element = document.querySelector('#tbLocation');
    element.value = code.text;
    element.focus();
    var controllerElement = document.querySelector('section');
    var controllerScope = angular.element(controllerElement).scope();
    controllerScope.$apply(function () {
        controllerScope.VM.locationObj = code;
        controllerScope.VM.Oname = code.text;
    });
};
