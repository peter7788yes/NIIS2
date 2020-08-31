$(function () {



    $(document).on("click", "#cancel", function (e) {
        window.close();
    });
    $(document).on("click", "#ok", function (e) {
        var VacCodes = $('input:checkbox:checked[name="vc"]').map(function () { return $(this).val(); }).get();
        var VacIDs = $('input:checkbox:checked[name="vc"]').map(function () { return $(this).prop("id"); }).get();
        window.opener.document.getElementById(openertbid).value = VacCodes;
        window.opener.document.getElementById(openerhdid).value = VacIDs;

        var VacData = {};
        VacData["VacCodes"] = VacCodes;
        VacData["VacIDs"] = VacIDs;
        window.opener.UpdateVal(VacData);

        window.close();
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

                 // postData.NameOrIdNo = $("#NameOrIdNo").val(); 

                 postData = $scope.PM.filterPageData(pgData, postData);
                 //console.log(postData);
                 $scope.PM.changePage("/CaseVisit/ChooseVacListOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });

                 });
             };

             $scope.changePage(1);

             $scope.goDetail = function (record) {

                 var iWidth = 542;  //視窗的寬度;
                 var iHeight = 318; //視窗的高度;
                 var iTop = (window.screen.availHeight - 30 - iHeight) / 2;  //視窗的垂直位置;
                 var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;   //視窗的水平位置;
                 window.open("/CaseMaintain/UserContract.aspx?c=" + record["C"] + "&i=" + $(".CaseID").html(), "UserContract", "width=" + iWidth + ",height=" + iHeight + ",top=" + iTop + ",left=" + iLeft + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes");

             };


            
         } ]);

 