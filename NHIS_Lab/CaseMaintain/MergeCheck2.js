$(function () {

    $(document).on("click", "#SearchBtn", function (e) {

        $(".tab > ul > li.here > a ").click();
        e.preventDefault();
        return false;
    });


    $(document).on("click", "#TipMerge", function (e) {

        alert("請等候排程執行");
        return false;
    });


    $(document).on("click", "#ExportCSV", function (e) {

        var NowTab = $(".tab > ul > li.here > a ").attr("id").replace("li","");
       
        var postData = {};
       
        postData.BirthDateS = $("#BirthDateS").val();
        postData.BirthDateE = $("#BirthDateE").val();
        postData.SearchKind = NowTab;

        $.post('/CaseMaintain/MergeCheckDownloadOP.aspx', postData, function (retData) {
          
        }); 

//        $.ajax({
//            cache: false,
//            type: "POST",
//            url: "/CaseMaintain/MergeCheckDownloadOP.aspx",
//            data: postData
//        }).done(function () {
//            alert('OK');
//        })
//         .fail(function (jqXHR, textStatus) {

//         });



        return false;
    });




});



angular.module("MyApp", ["PageM", "TableM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", "PageProvider", "PageProvider", "PageProvider", function ($scope, PageProvider, TableProvider, PageProvider2, PageProvider3, PageProvider4) {

             $scope.PM = PageProvider;
             $scope.TM = {};

             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};

                 postData.BirthDateS = $("#BirthDateS").val();
                 postData.BirthDateE = $("#BirthDateE").val();
                 postData.SearchKind = 1;

                 postData = $scope.PM.filterPageData(pgData, postData);
                 $scope.PM.changePage("/CaseMaintain/MergeCheckOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };




             $scope.changePage(1);

             $scope.goDetail = function (record) {

                 location.href = "/CaseMaintain/UserProfile.aspx?i=" + record["C"];
             };

             $scope.goMotherDetail = function (record) {

                 location.href = "/CaseMaintain/UserProfile.aspx?i=" + record["MC"];
             };


             //List2
             $scope.PM2 = PageProvider2;
             //angular.copy($scope.PM, $scope.PM2);
             $scope.TM2 = {};
             angular.copy($scope.TM, $scope.TM2);

             $scope.changePage2 = function (pageIndex) {
                 $("#tmBlock2").show();
                 var pgData = $scope.PM2.genPageData(pageIndex);
                 var postData = {};

                 postData.BirthDateS = $("#BirthDateS").val();
                 postData.BirthDateE = $("#BirthDateE").val();
                 postData.SearchKind = 2;
                 postData = $scope.PM2.filterPageData(pgData, postData);
                 $scope.PM2.changePage("/CaseMaintain/MergeCheckOP.aspx", postData, function (data) {
                     $scope.TM2.data = data.message;
                      $scope.$apply(function () { });
                     
                 });
             };
 


             //List3
             $scope.PM3 = PageProvider3;
             $scope.TM3 = {};
             angular.copy($scope.TM, $scope.TM3);

             $scope.changePage3 = function (pageIndex) {
                 $("#tmBlock3").show();
                 var pgData = $scope.PM3.genPageData(pageIndex);
                 var postData = {};

                 postData.BirthDateS = $("#BirthDateS").val();
                 postData.BirthDateE = $("#BirthDateE").val();
                 postData.SearchKind = 3;
                 postData = $scope.PM3.filterPageData(pgData, postData);

                 $scope.PM3.changePage("/CaseMaintain/MergeCheckOP.aspx", postData, function (data) {
                     $scope.TM3.data = data.message;
                     $scope.$apply(function () { });
                 });
             };

 

             //List4
             $scope.PM4 = PageProvider4;
             $scope.TM4 = {};
             angular.copy($scope.TM, $scope.TM4);

             $scope.changePage4 = function (pageIndex) {
                 $("#tmBlock4").show();
                 var pgData = $scope.PM4.genPageData(pageIndex);
                 var postData = {};

                 postData.BirthDateS = $("#BirthDateS").val();
                 postData.BirthDateE = $("#BirthDateE").val();
                 postData.SearchKind = 4;
                 postData = $scope.PM4.filterPageData(pgData, postData);

                 $scope.PM4.changePage("/CaseMaintain/MergeCheckOP.aspx", postData, function (data) {
                     $scope.TM4.data = data.message;
                     $scope.$apply(function () { });
                 });
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