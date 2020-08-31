$(function () {



    $(document).on("click", "#ModifyLogTab", function (e) {
        $scope.changePage();
        e.preventDefault();
        return false;
    });


    $(document).on("click", ".DelPS", function (e) {
        $(this).parent().html('');
    });
    $(document).on("click", ".AddPS", function (e) {

        $(".CommentAreaDIV").append("<div>" + $("#CommentSample").html() + "</div>");

    });
    $(document).on("click", ".AddContract", function (e) {
        var iWidth = 542;  //視窗的寬度;
        var iHeight = 508; //視窗的高度;
        var iTop = (window.screen.availHeight - 30 - iHeight) / 2;  //視窗的垂直位置;
        var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;   //視窗的水平位置;
        window.open("/CaseMaintain/ChooseUserContractList.aspx?i=" + $(".CaseID").html(), "UserContractList", "width=542,height=508,top=" + iTop + ",left=" + iLeft + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes");
        e.preventDefault();
        return false;
        // PopWin("/Disease/New_Disease.aspx", 450, 300, "New_Disease");
    });
    $(document).on("click", "#BackToList", function (e) {

        location.href = "UserProfileList.aspx";
    });


    $(".CommentAreaDIV").append("<div>" + $("#CommentSample").html() + "</div>");

    $(".ConCounty").val(CountyInival);
    $(".ConTown").val(TownInival);
    $(".ConVillage").val(VillageInival);

    //    GetMainContactInfo();

    $(document).on("click", ".DelMobile,.DelEmail", function (e) {

        $(this).parent().html('');
        GetMainContactInfo();
    });
    $(document).on("click", ".AddMobile", function (e) {
        $(".MobileDIV").append("<div>" + $("#MobileSample").html() + "</div>");
  
    });
    $(document).on("click", ".AddEmail", function (e) {
        $(".EmailDIV").append("<div>" + $("#EmailSample").html() + "</div>");
      
    });


    $(".MobileDIV").append("<div>" + $("#MobileSample").html() + "</div>");
    $(".EmailDIV").append("<div>" + $("#EmailSample").html() + "</div>");




    $(document).on("change", "#MainContact", function (e) {
        //取得  
        GetMainContactInfo();
        e.preventDefault();
        return false;


    });

    $(document).on("change", ".tbTelDayArea,.tbTelDayNo,.tbTelNightExt,.tbTelNightArea,.tbTelNightNo,.tbTelNightExt,.tbMobile,.tbEmail", function (e) {
      
        GetMainContactInfo();
        e.preventDefault();
        return false; 

    });

    $("#MainContact").val(MainContactInival);
});

function GetMainContactInfo() {
      var iCaseID = $(".CaseID").html();
      var iContactCaseID = $("#MainContact").val();
     // alert(iCaseID + iContactCaseID);
    if (iContactCaseID != '0') {
        var postData = {};
        postData["CaseID"] = iCaseID;
        postData["ContactCaseID"] = iContactCaseID;


        $.ajax({
            cache: false,
            type: "POST",
            url: "/CaseMaintain/CaseUserGetContactInfoOP.aspx",
            data: postData,
            async: false
        })
        .done(function (response) {
            var reply = eval(response);
            if (reply.RetCode == '1') {
                $("#MainContactInfo").html(reply.Content);
            }

        })
        .fail(function (jqXHR, textStatus) {

        });
    } else {
        //選本人

        var sb = "";
      sb += "<table>";
      sb += "<tr><td style='width:90px'>電話(日)：</td><td>" + $(".tbTelDayArea").val() + ' ' + $(".tbTelDayNo").val() + "分機" + $(".tbTelDayExt").val() + "</td></tr>";
      sb += "<tr><td>電話(夜)：</td><td>" + $(".tbTelNightArea").val() + ' ' + $(".tbTelNightNo").val() + "分機" + $(".tbTelNightExt").val() + "</td></tr>";
      sb += "<tr><td>行動電話：</td><td>" + $('.tbMobile').map(function () { return '<div>' + $(this).val() + '</div>'; }).get().join(' ') + "</td></tr>";
      sb += "<tr><td>電子郵件：</td><td>" + $('.tbEmail').map(function () { return '<div>' + $(this).val() + '</div>'; }).get().join(' ') + "</td></tr>"; 
      sb += "</table>"; 
      $("#MainContactInfo").html(sb);

  } 
}


angular.module("MyApp", ["PageM", "TableM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};

             $scope.VM = {};
              
             //alert(Countydata);
             $scope.VM.CountyAry = [{ I: "0", N: "縣市"}].concat(Countydata);
             $scope.VM.TownAry = [{ I: "0", N: "鄉鎮市區"}].concat(Towndata); ;
             $scope.VM.VillageAry = [{ I: "0", N: "村里"}].concat(Villagedata); ;

             $scope.VM.SelectCounty = "0";
             $scope.VM.SelectTown = "0";
             $scope.VM.SelectVillage = "0";

             $scope.VM.ResCountyAry = [{ I: "0", N: "縣市"}].concat(ResCountydata);
             $scope.VM.ResTownAry = [{ I: "0", N: "鄉鎮市區"}].concat(ResTowndata); ;
             $scope.VM.ResVillageAry = [{ I: "0", N: "村里"}].concat(ResVillagedata); ;

             $scope.VM.SelectResCounty = "0";
             $scope.VM.SelectResTown = "0";
             $scope.VM.SelectResVillage = "0";



             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};

                 postData.CaseID = $(".CaseID").html();

                 postData = $scope.PM.filterPageData(pgData, postData);
                 //   console.log(postData);
                 $scope.PM.changePage("/CaseMaintain/UserProfileModifyListOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };

             $scope.changePage(1);

             //List2
             $scope.PM2 = {};
             angular.copy($scope.PM, $scope.PM2);
             $scope.TM2 = {};
             angular.copy($scope.TM, $scope.TM2);


             $scope.changePage2 = function (pageIndex) {
                 var CaseID = $(".CaseID").html();
                 if (CaseID != "") {
                     $("#tmBlock2").show();
                     var postData = {};
                     postData.CaseID = CaseID;
                     $scope.PM2.changePage("/CaseMaintain/UserProfileContactListOP.aspx", postData, function (data) {
                         $scope.TM2.data = data.message;
                         $scope.$apply(function () {
                          
                         });
                     });
                 }

             };

             $scope.goDetail2 = function (record) {

                 var iWidth = 542;  //視窗的寬度;
                 var iHeight = 318; //視窗的高度;
                 var iTop = (window.screen.availHeight - 30 - iHeight) / 2;  //視窗的垂直位置;
                 var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;   //視窗的水平位置;
                 window.open("/CaseMaintain/UserContract.aspx?c=" + record["CC"] + "&i=" + record["C"] + "&ParentClick=UpdateContactList", "UserContract", "width=" + iWidth + ",height=" + iHeight + ",top=" + iTop + ",left=" + iLeft + ",toolbar=no,menubar=no,scrollbars=yes,resizable=yes");

             };

             $scope.changePage2(1);

             GetMainContactInfo();




             $scope.SelectResTownChange = function () {

                 var postData = {};
                 postData["a"] = "Village";
                 postData["p"] = $(".ResTown").val();
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
                                    $scope.VM.ResVillageAry = [{ I: "0", N: "村里"}].concat(data);
                                }
                                else {
                                    $scope.VM.ResVillageAry = [{ I: "0", N: "村里"}];
                                }
                                $scope.VM.SelectResVillage = "0";

                            });


                        }, 1);

                    })
                    .fail(function (jqXHR, textStatus) {

                    });



             };


             $scope.SelectResCountyChange = function () {

                 var postData = {};
                 postData["a"] = "Town";
                 postData["p"] = $(".ResCounty").val();
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
                                    $scope.VM.ResTownAry = [{ I: "0", N: "鄉鎮市區"}].concat(data);
                                    $scope.VM.ResVillageAry = [{ I: "0", N: "村里"}];
                                }
                                else {
                                    $scope.VM.ResTownAry = [{ I: "0", N: "鄉鎮市區"}];
                                    $scope.VM.ResVillageAry = [{ I: "0", N: "村里"}];
                                }
                                $scope.VM.SelectResTown = "0";
                                $scope.VM.SelectResVillage = "0";
                            });
                        }, 1);



                    })
                    .fail(function (jqXHR, textStatus) {

                    });



             };

             $scope.SelectConTownChange = function () {

                 var postData = {};
                 postData["a"] = "Village";
                 postData["p"] = $(".ConTown").val();
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
                                    $scope.VM.VillageAry = [{ I: "0", N: "村里"}].concat(data);
                                }
                                else {
                                    $scope.VM.VillageAry = [{ I: "0", N: "村里"}];
                                }
                                $scope.VM.SelectVillage = "0";

                            });


                        }, 1);

                    })
                    .fail(function (jqXHR, textStatus) {

                    });



             };


             $scope.SelectConCountyChange = function () {

                 var postData = {};
                 postData["a"] = "Town";
                 postData["p"] = $(".ConCounty").val();
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
                                    $scope.VM.TownAry = [{ I: "0", N: "鄉鎮市區"}].concat(data);
                                    $scope.VM.VillageAry = [{ I: "0", N: "村里"}];
                                }
                                else {
                                    $scope.VM.TownAry = [{ I: "0", N: "鄉鎮市區"}];
                                    $scope.VM.VillageAry = [{ I: "0", N: "村里"}];
                                }
                                $scope.VM.SelectTown = "0";
                                $scope.VM.SelectVillage = "0";
                            });
                        }, 1);



                    })
                    .fail(function (jqXHR, textStatus) {

                    });



             };







         } ]);

 