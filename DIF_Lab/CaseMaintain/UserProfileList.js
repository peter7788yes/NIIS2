$(function () {
    $(document).on("click", "#AddBtn", function (e) {
        location.href = "/CaseMaintain/UserProfile.aspx";
        e.preventDefault();
    });
    $(document).on("click", "#SearchBtn", function (e) {


        if (Page_ClientValidate()) {

            angular.element('#MyController').scope().Search(1);
        } 

    });
});

 

angular.module("MyApp", ["PageM", "TableM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};

             $scope.VM.CountyAry = [{ I: '0', N: "全部"}];
             $scope.VM.SelectCounty = "0";

             $scope.VM.TownAry = [{ I: '0', N: "全部"}];
             $scope.VM.SelectTown = "0";





             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};

                 postData.BirthDateS = $("#BirthDateS").val();
                 postData.BirthDateE = $("#BirthDateE").val();
                 postData.CaseName = $("#CaseName").val();
                 postData.CaseIdNo = $("#CaseIdNo").val();
                 postData.TownID = $("#SelectTown").val();
                 postData.CountyID = $("#SelectCounty").val();
                 postData.IsSearch = "0";
                 postData.SearchKind = "1";



                 postData = $scope.PM.filterPageData(pgData, postData);
                 console.log(postData);
                 $scope.PM.changePage("/CaseMaintain/UserProfileListOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };

             $scope.Search = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};

                 postData.BirthDateS = $("#BirthDateS").val();
                 postData.BirthDateE = $("#BirthDateE").val();
                 postData.CaseName = $("#CaseName").val();
                 postData.CaseIdNo = $("#CaseIdNo").val();
                 postData.TownID = $("#SelectTown").val();
                 postData.CountyID = $("#SelectCounty").val();
                 postData.IsSearch = "1";
                 postData.SearchReason = $(".SearchReason").val();
                 postData.SearchKind = "1";
                 postData = $scope.PM.filterPageData(pgData, postData);
          
                 $scope.PM.changePage("/CaseMaintain/UserProfileListOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };


             //  $scope.changePage(1);

             $scope.goDetail = function (record) {


                 OpenWindowWithPostOptions("/CaseMaintain/UserProfileDetail.aspx", 820, 508, "CapacityHistory", { i: record["CaseID"] });
 

             };


             $scope.SelectCountyChange = function () {
                 var postData = {};
                 postData["a"] = "Town";
                 postData["p"] = $("#SelectCounty").val();
                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/Ashx/SystemAreaCodeOP.ashx",
                     data: postData
                 })
                    .done(function (data) {
                        data = data || [];
                        setTimeout(function () {
                            $scope.$apply(function () {
                                if (data.length > 0) {
                                    $scope.VM.TownAry = [{ I: '0', N: "全部"}].concat(data); ;
                                    $scope.VM.SelectTown = "0";
                                }
                                else {
                                    $scope.VM.TownAry = [{ I: '0', N: "全部"}];
                                    $scope.VM.SelectTown = "0";
                                }
                            });
                        }, 1);


                    })
                    .fail(function (jqXHR, textStatus) {

                    });


             };



             $scope.BindCounty = function () {
                 var postData = {};
                 postData["a"] = "County";
                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/Ashx/SystemAreaCodeOP.ashx",
                     data: postData
                 })
                    .done(function (data) {
                        data = data || [];
                        setTimeout(function () {
                            $scope.$apply(function () {
                                if (data.length > 0) {
                                    $scope.VM.CountyAry = [{ I: '0', N: "全部"}].concat(data);
                                    $scope.VM.SelectCounty = "0";
                                }
                                else {
                                    $scope.VM.CountyAry = [{ I: '0', N: "全部"}];
                                    $scope.VM.SelectCounty = "0";
                                }

                            });
                        }, 1);


                    })
                    .fail(function (jqXHR, textStatus) {

                    });

             };



             $scope.BindCounty();

               


         } ]);

          