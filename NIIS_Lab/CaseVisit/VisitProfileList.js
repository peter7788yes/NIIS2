$(function () {
    $(document).on("click", "#AddBtn", function (e) {
        location.href = "/CaseMaintain/UserProfile.aspx";
 
    });

    $(document).on("click", "#SearchBtn", function (e) {
     
    });


    $(document).on("click", ".thOrderCol", function (e) {

        var OrderColId = $(this).attr("id").replace("thOrderCol_", "");
    
        $("#OrderCol").val(OrderColId);
        if ($("#OrderAsc").val() == "0")
            $("#OrderAsc").val("1");
        else
            $("#OrderAsc").val("0");
             
        angular.element('#MyController').scope().changePage(1);
    
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
                 postData.NumberType = $("#NumberType").val();

                 postData.HouseNo = $("#HouseNo").val();

                 postData.ContactName = $("#ContactName").val();
                 postData.ContactIdNo = $("#ContactIdNo").val();
                 postData.ContactBirthDate = $("#ContactBirthDate").val();
                 //    postData.OrgID = $("#hfLocationID").val();
                 postData.CountyID = $("#SelectCounty").val();
                 postData.TownID = $("#SelectTown").val();
                 postData.AddrKind = $("#SelectAddrKind").val();
                 postData.OrderCol = $("#OrderCol").val();
                 postData.OrderAsc = $("#OrderAsc").val();


                 postData = $scope.PM.filterPageData(pgData, postData);
                 //console.log(postData);
                 $scope.PM.changePage("/CaseVisit/VisitProfileListOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                     //排序
                     var OrderSign = "▲";
                     if (postData.OrderAsc == "0") OrderSign = "▼";
                     $(".OrderAscSign").html("");
                     $("#OrderAscSign_" + postData.OrderCol).html(OrderSign);

                 });
             };



             $scope.goDetail = function (record) {
                 post_to_url("/CaseMaintain/UserProfile.aspx", { i: record["C"] }, "post");
             };
             $scope.goVisit = function (record) {
                 post_to_url("/CaseVisit/VisitCaseList.aspx", { i: record["C"] }, "post");
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



                    })
                    .fail(function (jqXHR, textStatus) {

                    });

             };



             $scope.BindCounty();
             $scope.changePage(1);


              
               

         } ]);


          