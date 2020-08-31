$(function () {


    $(document).on("click", "#SearchBtn", function (e) {

        e.preventDefault();
        return false;
    });

    getCode(UserOrg);

});



angular.module("MyApp", ["PageM", "TableM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};
              
              



             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};

                 postData.BirthDateS = $("#BirthDateS").val();
                 postData.BirthDateE = $("#BirthDateE").val();
                 postData.CaseName = $("#CaseName").val();
                 postData.CaseIdNo = $("#CaseIdNo").val();
                 postData.HouseNo = $("#HouseNo").val();

                 postData.ContactName = $("#ContactName").val();
                 postData.ContactIdNo = $("#ContactIdNo").val();
                 postData.ContactBirthDate = $("#ContactBirthDate").val();

                 postData = $scope.PM.filterPageData(pgData, postData);
                 //console.log(postData);
                 $scope.PM.changePage("/CaseMaintain/MergedUserListOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { 
                     
                     
                     });
                 });
             };

             $scope.changePage(1);

             $scope.goDetail = function (record) { 
                 post_to_url("/CaseMaintain/MergedUserDiffData.aspx", { i: record["C"] }, "post");
         
             };

              



             $scope.openOrgs = function (record) {

                 popUpWindow("/SelectSingleOrg.aspx", "SelectSingleOrg", 930, 450);

             };


             $scope.refresh = function () {
                 var location = $('#tbLocation').val();
                 var locationID = $('#hfLocationID').val();
                 setTimeout(function () {
                     $scope.$apply(function () {
                         $scope.VM.location = location;
                         $scope.VM.locationID = locationID;
                     });
                 }, 1);

             };

             var popUpWindow = function (url, target, title, w, h) {
                 var iWidth = w;  //視窗的寬度;
                 var iHeight = h; //視窗的高度;
                 var iTop = (window.screen.availHeight - 30 - iHeight) / 2;  //視窗的垂直位置;
                 var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;   //視窗的水平位置;
                 window.open(url, title, "width=542,height=508,top=" + iTop + ",left=" + iLeft + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes");

             }


         } ]);



         var getCode = function (code) {
             $("#tbLocation").val(code.text);
             $("#hfLocationID").val(code.id);
             $('#refreshBtn').trigger('click');
         };