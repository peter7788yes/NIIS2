
var PostDataALL = {};

$(function () {
    $(document).on("click", "#AddBtn", function (e) {
        location.href = "/CaseMaintain/UserProfile.aspx";

    });

    $(document).on("click", "#SearchBtn", function (e) {

    });



    $(document).on("click", "#btnClear", function (e) {

        $("#CaseName").val('');
        $("#CaseIdNo").val('');
        $("#BirthDateS").val('');
        $("#BirthDateE").val('');
        $("#HouseNo").val('');
        $("#ContactName").val('');
        $("#ContactIdNo").val('');
        $("#ContactBirthDate").val('');
        $("#SelectCounty").val('0').change();
        $("#NumberType").val('0');
        $("#SelectAddrKind").val('0');

        angular.element('#MyController').scope().clearList();
        SetLocalHash();

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




    GetLastPostData();

});

function GetLastPostData() {
   if (location.hash != '') 
   {

            var LastPageNo = 1;
            try {
        
                    PostDataALL = JSON.parse(decodeURIComponent(location.hash).substring(1));
                    LastPageNo = PostDataALL["CaseListNowPage"];

                    //把資料寫回去
                    if (LastPageNo != '') {
                        $("#BirthDateS").val() = PostDataALL.BirthDateS;
                        $("#BirthDateE").val() = PostDataALL.BirthDateE;
                        $("#CaseName").val() = PostDataALL.CaseName;
                        $("#CaseIdNo").val() = PostDataALL.CaseIdNo;
                        $("#NumberType").val() = PostDataALL.NumberType;
                        $("#HouseNo").val() = PostDataALL.HouseNo;
                        $("#ContactName").val() = PostDataALL.ContactName;
                        $("#ContactIdNo").val() = PostDataALL.ContactIdNo;
                        $("#ContactBirthDate").val() = PostDataALL.ContactBirthDate;
                        $("#SelectCounty").val() = PostDataALL.CountyID;
                        $("#SelectTown").val() = PostDataALL.TownID;
                        $("#SelectAddrKind").val() = PostDataALL.AddrKind;
                        $("#OrderCol").val() = PostDataALL.OrderCol;
                        $("#OrderAsc").val() = PostDataALL.OrderAsc;

                    }
                
       
            }
            catch (e) {
             //   alert(e.EndLine);
            } 
   
        if (LastPageNo>0)  angular.element('#MyController').scope().changePage(LastPageNo);
    }
  
  }
function SetLocalHash(  ) {

    location.hash = encodeURIComponent(JSON.stringify(PostDataALL));

}

function GetPostData( ) {

    PostDataALL.BirthDateS = $("#BirthDateS").val();
    PostDataALL.BirthDateE = $("#BirthDateE").val();
    PostDataALL.CaseName = $("#CaseName").val();
    PostDataALL.CaseIdNo = $("#CaseIdNo").val();
    PostDataALL.NumberType = $("#NumberType").val(); 
    PostDataALL.HouseNo = $("#HouseNo").val(); 
    PostDataALL.ContactName = $("#ContactName").val();
    PostDataALL.ContactIdNo = $("#ContactIdNo").val();
    PostDataALL.ContactBirthDate = $("#ContactBirthDate").val();
    PostDataALL.CountyID = $("#SelectCounty").val();
    PostDataALL.TownID = $("#SelectTown").val();
    PostDataALL.AddrKind = $("#SelectAddrKind").val();
    PostDataALL.OrderCol = $("#OrderCol").val();
    PostDataALL.OrderAsc = $("#OrderAsc").val();

}

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
                 $("page-nav").show();
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 //設頁數
                 GetPostData();
                 PostDataALL.CaseListNowPage = pageIndex; 
                  
                 var GoSearchData = PostDataALL;
                 GoSearchData = $scope.PM.filterPageData(pgData, PostDataALL);

                 $scope.PM.changePage("/CaseMaintain/UserProfileListOP.aspx", GoSearchData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                     //排序
                     var OrderSign = "▲";
                     if (GoSearchData.OrderAsc == "0") OrderSign = "▼";
                     $(".OrderAscSign").html("");
                     $("#OrderAscSign_" + GoSearchData.OrderCol).html(OrderSign);

                 });
             };
               $scope.clearList = function ( ) {

                $scope.TM.data = '';
                $scope.$apply(function () { });
                     
                   //$("#tmBlock").hide();
                   //$("page-nav").hide();
               };
             $scope.goDetail = function (record) {
                 SetLocalHash();
                 post_to_url("/CaseMaintain/UserProfile.aspx", { i: record["C"] }, "post");
             };
             $scope.goVisit = function (record) {
                 SetLocalHash();
                 post_to_url("/CaseVisit/VisitCaseList.aspx", { i: record["C"] }, "post");
             };
             $scope.goInject = function (record) {
                 SetLocalHash();
                 post_to_url("/Vaccination/RecordM/RegisterData_Detail.aspx", { i: record["C"] }, "post");
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
             //  $scope.changePage(1);





         } ]);


          