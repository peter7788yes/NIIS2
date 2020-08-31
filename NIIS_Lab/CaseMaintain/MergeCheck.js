$(function () {



    $(document).on("click", "#SearchBtn", function (e) {

        $(".tab > ul > li.here > a").click();

    });
    $(document).on("click", ".taba", function (e) {

        //     alert($(this).attr("id").replace("li", ""));
        $("#hfSearchKind").val($(this).attr("id").replace("li", ""));

        angular.element('#MyController').scope().changePage(1);

    });





    $(document).on("click", "#TipMerge", function (e) {

        alert("請等候排程執行");


        var postData = {};

        postData["action"] = "Merge";

        $.ajax({
            cache: false,
            type: "POST",
            url: "/CaseMaintain/MergeCheckDoMergeOP.aspx",
            data: postData,
            async: false
        })
                   .done(function (response) {
                       var reply = eval(response);
                       if (reply.RetCode == '1') {
                           //alert('設定完成');

                           angular.element('#MyController').scope().changePage(1);
                       }


                   })
                    .fail(function (jqXHR, textStatus) {

                    });





        return false;
    });


    $(document).on("click", "#ExportCSV", function (e) {

        var NowTab = $(".tab > ul > li.here > a").attr("id").replace("li", "");
         
        var postData = {};

        postData.BirthDateS = $("#BirthDateS").val();
        postData.BirthDateE = $("#BirthDateE").val();
        postData.OrgID = $("#hfLocationID").val();
        postData.SearchKind = NowTab;

       
        post_to_url("/CaseMaintain/MergeCheckDownloadOP.aspx", postData, "post");
        return false;
    });


    $(document).on("click", ".OrgSelect", function (e) {

        popUpWindow("/SelectSingleOrg.aspx", "SelectSingleOrg", 930, 450);

    });


    getCode(UserOrg);
});



angular.module("MyApp", ["PageM", "TableM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};

             $scope.changePage = function (pageIndex) {

                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};

                 postData.BirthDateS = $("#BirthDateS").val();
                 postData.BirthDateE = $("#BirthDateE").val();
                 postData.OrgID = $("#hfLocationID").val();

                 postData.SearchKind = $("#hfSearchKind").val();
                 postData = $scope.PM.filterPageData(pgData, postData);
                 $scope.PM.changePage("/CaseMaintain/MergeCheckOP.aspx", postData, function (data) {

                     $scope.TM.data = data.message;
                     $scope.$apply(function () {
                         //更新時間
                         if ($scope.TM.data.length > 0)
                             $("#LastUpdateTime").html("更新時間：" + $scope.TM.data[0]["UpdateDate"]);

                         $(".tab > ul > li").removeClass("here");
                         $("#li" + postData.SearchKind).parent("li").addClass("here");
                     });
                 });
                 // alert($scope.TM.data[0]["UpdateDate"]);

                 $(".HightLightTh").removeClass("redcolor")
                 $("#HightLightTh" + $("#hfSearchKind").val()).addClass("redcolor");



             };




             $scope.changePage(1);

             $scope.goDetail = function (record) {
                 post_to_url("/CaseMaintain/UserProfile.aspx", { i: record["C"] }, "post");

             };

             $scope.goMotherDetail = function (record) {
                 post_to_url("/CaseMaintain/UserProfile.aspx", { i: record["MC"] }, "post");

             };

             $scope.goDetail99 = function (record) {
                 post_to_url("/CaseMaintain/UserProfile.aspx", { i: record["C99"] }, "post");

             };


         } ]);


         var getCode = function (code) {
             $("#tbLocation").val(code.text);
             $("#hfLocationID").val(code.id); 
         };
         var popUpWindow = function (url, target, title, w, h) {
                 var iWidth = w;  //視窗的寬度;
                 var iHeight = h; //視窗的高度;
                 var iTop = (window.screen.availHeight - 30 - iHeight) / 2;  //視窗的垂直位置;
                 var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;   //視窗的水平位置;
                 window.open(url, title, "width=542,height=508,top=" + iTop + ",left=" + iLeft + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes");

             };