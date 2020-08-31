﻿$(function () {
    $(document).on("click", "#AddBtn", function (e) {
        location.href = "/CaseMaintain/UserProfile.aspx";
        e.preventDefault();
        return false;
    });

    $(document).on("click", "#SearchBtn", function (e) {


        if (Page_ClientValidate()) {

            angular.element('#MyController').scope().changePage(1);
        }

    });
     

     
});



angular.module("MyApp", ["PageM", "TableM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};




             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
              
                     var pgData = $scope.PM.genPageData(pageIndex);
                     var postData = {}; 
                     postData.IdNo = $(".IdNo").val();
                     // postData.LogItem = $(".cblLogItem>input:checkbox:checked").map(function () { return $(this).val(); }).get();
                     postData = $scope.PM.filterPageData(pgData, postData);
                     //console.log(postData);
                     $scope.PM.changePage("/LogManage/LogIdFilterListOP.aspx", postData, function (data) {
                         $scope.TM.data = data.message;
                         $scope.$apply(function () { });
                     });
               


             };

             $scope.changePage(1);

             $scope.goDetail = function (record) {
                // alert(record['ItemID']);alert(record['ID']);
                 location.href = "/LogManage/LogDetail.aspx?ItemID=" + record['ItemID'] + "&LogID=" + record['ID'];  

             };




             var popUpWindow = function (url, target, title, w, h) {
                 var iWidth = w;  //視窗的寬度;
                 var iHeight = h; //視窗的高度;
                 var iTop = (window.screen.availHeight - 30 - iHeight) / 2;  //視窗的垂直位置;
                 var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;   //視窗的水平位置;
                 window.open(url, title, "width=542,height=508,top=" + iTop + ",left=" + iLeft + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes");

             }


         } ]);


          