﻿$(function () {
    $(document).on("click", "#addBtn", function (e) {
        location.href = "/Vaccination/RecordM/BCGRecord_Add.aspx";
        e.preventDefault();
        return false;
    });
});

angular.module("MyApp", ["PageM", "FilterM", "cfp.hotkeys"])
         .controller("MyController", ["$scope", "PageProvider", 'hotkeys', function ($scope, PageProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             //$scope.TM.isHide = true;
             $scope.VM = {};
             $scope.VM.locationObj = {};
             $scope.VM.locationObj.text = "";
             $scope.VM.locationObj.id = 0;
             $scope.VM.title = "";
             $scope.VM.publishState = "0";

             $scope.changePage = function (pageIndex) {
                 //$("#pmBlock").show();
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};
                 postData.S = "";
                 postData.E = "";
                 postData.LID = $scope.VM.locationObj.id;
                 postData.S = $("#tbDateStart").val();
                 postData.S = postData.S.replace(/年/g, "");
                 postData.E = $("#tbDateEnd").val();
                 postData.E = postData.E.replace(/年/g, "");
                 postData = $scope.PM.filterPageData(pgData, postData);
                 $scope.PM.changePage("/Vaccination/RecordM/BCGRecordOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };

             $scope.changePage(1);

             hotkeys.add({
                 combo: 'enter',
                 description: 'Description goes here',
                 allowIn: ['INPUT', "BUTTON", 'SELECT', 'TEXTAREA'],
                 callback: function (event, hotkey) {
                     var focusType = document.activeElement.type;
                     if (focusType == "text" || focusType == "select-one") {
                         $scope.changePage(1);
                     }
                 }
             });

             $scope.openOrgs = function (record) {
                 popUpWindow("/SelectSingleOrg.aspx", "SelectSingleOrg", 620, 450);
           
             };


             $scope.goDetail = function (record) {
                 location.href = "/Vaccination/RecordM/BCGRecord_Detail.aspx?i="+record["I"];
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
        controllerScope.changeVaccine();
    });
};