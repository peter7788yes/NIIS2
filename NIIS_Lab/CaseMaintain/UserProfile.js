$(function () {


    $(document).on("click", "#imgCapacity", function (e) {

        OpenWindowWithPostOptions("/CaseMaintain/CapacityHistory.aspx", 600, 508, "CapacityHistory", { i: CaseID });


    });


    $(document).on("click", "#btnSameRes", function (e) {
        //同戶籍

        $(".ConCounty").html($(".ResCounty").html()).val($(".ResCounty").val());
        $(".ConTown").html($(".ResTown").html()).val($(".ResTown").val());
        $(".ConVillage").html($(".ResVillage").html()).val($(".ResVillage").val());
        $(".ConNei").val($(".ResNei").val());
        $(".ConAddr").val($(".ResAddr").val());

    });

    $(document).on("click", "#ModifyLogLink", function (e) {
        $("#DivUserContent").hide();
        $("#DivModifyLog").show();
        angular.element('#MyController').scope().changePage(1);

    });
    $(document).on("click", "#UserContentLink", function (e) {
        $("#DivModifyLog").hide();
        $("#DivUserContent").show();

    });

    //    $(document).on("click", ".DelPS", function (e) {
    //        $(this).parent().html('');
    //    });
    //    $(document).on("click", ".AddPS", function (e) {
    //        $(".CommentAreaDIV").append("<div>" + $("#CommentSample").html() + "</div>");
    //    });
    $(document).on("click", ".AddContract", function (e) {
        OpenWindowWithPostOptions("/CaseMaintain/ChooseUserContactList.aspx", 608, 508, "CapacityHistory", { i: CaseID });

    });
    $(document).on("click", ".BackToList", function (e) {
        // location.href = "UserProfileList.aspx";

        history.go(-1);

    });


    // $(".CommentAreaDIV").append("<div>" + $("#CommentSample").html() + "</div>");

    $(".ConCounty").val(CountyInival);
    $(".ConTown").val(TownInival);
    $(".ConVillage").val(VillageInival);

    $(".ResCounty").val(ResCountyInival);
    $(".ResTown").val(ResTownInival);
    $(".ResVillage").val(ResVillageInival);
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




    $(document).on("change", ".SelectMainContact", function (e) {
        //取得  

        GetMainContactInfo();

    });

    $(document).on("change", ".tbTelDayArea,.tbTelDayNo,.tbTelNightExt,.tbTelNightArea,.tbTelNightNo,.tbTelNightExt,.tbMobile,.tbEmail", function (e) {

        GetMainContactInfo();

    });

    // alert(MainContactInival);




    //取得聯絡人列表
    LoadContactList();

    //angular.element('#MyController').scope().ReloadMainContactSelect();



});


function CheckSubmit() {

    Page_ClientValidate();
    //聯絡人有沒有填父or 母
    var CanSubmit = true;
    if (Page_IsValid) {

        if (CaseID != 0) {

            if (!IsHaveParent()) {
                CanSubmit = false;
                alert('聯絡人父或母必填!')

            }
        }
        else {
            if (!IsHaveParentWithContactIDs()) {
                CanSubmit = false;
                alert('聯絡人父或母必填!')
            }
        }
        // ID 有沒有重覆

        if (CanSubmit) {
            //最後再問
            if ($(".tbIdNo").val() != '') {
                if (IsIdNoReapeat()) {
                    {
                        if (!confirm('身分證字號重覆,確定送出?')) {
                            CanSubmit = false;
                        }
                    }

                }
            }
        }
    } else {
        
        CanSubmit =  false;
    }
    return CanSubmit;
}
function IsIdNoReapeat( ) {
    var IsRepeat = true;
    $.ajax({
        cache: false,
        type: "POST",
        url: "/CaseMaintain/UserProfileOP.aspx",
        data: { 'action': 'IsRepeatNo', 'CaseID': CaseID, 'IdNo': $(".tbIdNo").val() },
        async: false
    })
        .done(function (response) {
            var reply = eval(response);
            if (reply.RetCode == '1') {
                IsRepeat = false; 
            }

        });

        return IsRepeat;
}
function IsHaveParentWithContactIDs( ) {
    var HaveParent = false;
    $.ajax({
        cache: false,
        type: "POST",
        url: "/CaseMaintain/UserProfileContactListOP.aspx",
        data: { 'action': 'IsHaveParentWithContactIDs', 'ContactIDs': $("#NewCaseUserContactIDs").val() },
        async: false
    })
        .done(function (response) {
            var reply = eval(response);
            if (reply.RetCode == '1') {
                HaveParent = true; 
            }

        });

    return HaveParent;
}
function IsHaveParent( ) {
    var HaveParent = false;
    $.ajax({
        cache: false,
        type: "POST",
        url: "/CaseMaintain/UserProfileContactListOP.aspx",
        data: { 'action': 'isHaveParent', 'CaseID': CaseID },
        async: false
    })
        .done(function (response) {
            var reply = eval(response);
            if (reply.RetCode == '1') {
                HaveParent = true; 
            }

        });

        return HaveParent;
}


function GetMainContactInfo() {
   // alert('GetMainContactInfo');
    var iContactID = $(".SelectMainContact").val();

 //   alert(iContactID);
    if (iContactID == '-1') {
        //選本人

        var sb = "";
        sb += "<table>";
        sb += "<tr><td style='width:90px'>電話(日)：</td><td>" + $(".tbTelDayArea").val() + ' ' + $(".tbTelDayNo").val() + ($(".tbTelDayExt").val() != '' ? "分機" + $(".tbTelDayExt").val() : '')  + "</td></tr>";
        sb += "<tr><td>電話(夜)：</td><td>" + $(".tbTelNightArea").val() + ' ' + $(".tbTelNightNo").val() + ($(".tbTelNightExt").val() !=''  ?  "分機" + $(".tbTelNightExt").val() : '' ) + "</td></tr>";
        sb += "<tr><td>行動電話：</td><td>" + $('.tbMobile').map(function () { return '<div>' + $(this).val() + '</div>'; }).get().join(' ') + "</td></tr>";
        sb += "<tr><td>電子郵件：</td><td>" + $('.tbEmail').map(function () { return '<div>' + $(this).val() + '</div>'; }).get().join(' ') + "</td></tr>";
        sb += "</table>";
        $("#MainContactInfo").html(sb);

  
    } else if (iContactID != '0') {
        var postData = {};
        postData["ContactID"] = iContactID;


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

        });
    } else {
            $("#MainContactInfo").html('');
    }
}

 


function LoadContactList() {

    //$("#NewCaseUserRemarkIDs").val($("#NewCaseUserRemarkIDs").val() + "," + iRemarkID);

    var ContactData = {};
    ContactData["action"] = "LoadContactList";
    ContactData["CaseID"] = CaseID;

    $.ajax({
        cache: false,
        type: "POST",
        url: "/CaseMaintain/UserProfileContactListOP.aspx",
        data: ContactData
    })
                .done(function (data) {

                    var reply = eval(data);
                    if (reply.RetCode == '1') {
                        $("#CaseContactList").html(reply.Content);

                        //event;

                        $(".ModifyContact").click(function () {
                            var ContactID = $(this).attr("id").replace("ModifyContact_", "");

                            OpenWindowWithPostOptions("/CaseMaintain/UserContact.aspx", 600, 508, "UserContact", { "ContactID": ContactID });

                        });
                        $(".DeleteContact").click(function () {
                            if (confirm('確定刪除?')) {
                               
                                deleteContact($(this));

                            }
                        });

                        angular.element('#MyController').scope().ReloadMainContactSelect();

                    }


                }) ;

                
                

};

function AddContactTr(iContactID) {

    $("#NewCaseUserContactIDs").val($("#NewCaseUserContactIDs").val() + "," + iContactID);

    var ContactData = {};
    ContactData["action"] = "GetContactTr";
    ContactData["ContactID"] = iContactID;

    $.ajax({
        cache: false,
        type: "POST",
        url: "/CaseMaintain/UserProfileContactListOP.aspx",
        data: ContactData
    })
                .done(function (data) {
                    var reply = eval(data);
                    if (reply.RetCode == '1') {
                        $("#Contact_TB").append(reply.Content);

                        //event;

                        $(".ModifyContact").click(function () {
                            var ContactID = $(this).attr("id").replace("ModifyContact_", "");

                            OpenWindowWithPostOptions("/CaseMaintain/UserContact.aspx", 600, 508, "UserContact", { "ContactID": ContactID });

                        });
                        $(".DeleteContact").click(function () {
                            if (confirm('確定刪除?')) {
                                deleteContact($(this));
                            }
                        });

                    }
                    //alert('ReloadMainContactSelect');
                    angular.element('#MyController').scope().ReloadMainContactSelect();

                })
                .fail(function (jqXHR, textStatus) {

                });


};
function deleteContact(obj) {
    var iContactID = $(obj).attr("id").replace("DeleteContact_", "");
     
    $.ajax({
        cache: false,
        type: "POST",
        url: "/CaseMaintain/UserProfileContactListOP.aspx",
        data: { 'action': 'Delete', 'ContactID': iContactID } 
    }).done(function (data) {
        var reply = eval(data);
        if (reply.RetCode == '1') {
            $(obj).parent().parent().html('');
        }
    }); 
}
function checkID(source, args) {




    var idStr = args.Value; 
    //99證號 
    // 使用「正規表達式」檢驗格式
    if (idStr.search(/^(9)(9)\d{8}$/i) == 0) {
        // 基本格式錯誤 
        args.IsValid = true;

    }
    else {



        // 依照字母的編號排列，存入陣列備用。
        var letters = new Array('A', 'B', 'C', 'D',
      'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M',
      'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
      'X', 'Y', 'W', 'Z', 'I', 'O');
        // 儲存各個乘數
        var multiply = new Array(1, 9, 8, 7, 6, 5,
                           4, 3, 2, 1);
        var nums = new Array(2);
        var firstChar;
        var firstNum;
        var lastNum;
        var total = 0;
        // 撰寫「正規表達式」。第一個字為英文字母，
        // 第二個字為1或2，後面跟著8個數字，不分大小寫。
        var regExpID = /^[a-z](1|2)\d{8}$/i;
        // 使用「正規表達式」檢驗格式
        if (idStr.search(regExpID) == -1) {
            // 基本格式錯誤 
            args.IsValid = false;

        } else {
            // 取出第一個字元和最後一個數字。
            firstChar = idStr.charAt(0).toUpperCase();
            lastNum = idStr.charAt(9);

            // 找出第一個字母對應的數字，並轉換成兩位數數字。
            for (var i = 0; i < 26; i++) {
                if (firstChar == letters[i]) {
                    firstNum = i + 10;
                    nums[0] = Math.floor(firstNum / 10);
                    nums[1] = firstNum - (nums[0] * 10);
                    break;
                }
            }
            // 執行加總計算
            for (var i = 0; i < multiply.length; i++) {
                if (i < 2) {
                    total += nums[i] * multiply[i];
                } else {
                    total += parseInt(idStr.charAt(i - 1)) *
                               multiply[i];
                }
            }


            if ((parseInt(total) + parseInt(lastNum)) % 10 == 0)
                args.IsValid = true;
            else
                args.IsValid = false;



        }


    }
}


function checkRes(source, args) {

    var idStr = args.Value;
    // 依照字母的編號排列，存入陣列備用。
    var letters = new Array('A', 'B', 'C', 'D',
      'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M',
      'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
      'X', 'Y', 'W', 'Z', 'I', 'O');
    // 儲存各個乘數
    var multiply = new Array(1, 9, 8, 7, 6, 5,
                           4, 3, 2, 1);
    var nums = new Array(3);
    var firstChar; var secChar;
    var firstNum; var secNum;
    var lastNum;
    var total = 0;
    // 撰寫「正規表達式」。第1.2個字為英文字母，
    // 第3個字為1或2，後面跟著7個數字，不分大小寫。
    var regExpID = /^[a-z][a-d](1|2)\d{7}$/i;
    // 使用「正規表達式」檢驗格式
    if (idStr.search(regExpID) == -1) {
        // 基本格式錯誤 
        args.IsValid = false;

    } else {
        // 取出第一個字元和最後一個數字。
        firstChar = idStr.charAt(0).toUpperCase();
        secChar = idStr.charAt(1).toUpperCase();
        lastNum = idStr.charAt(9);

        // 找出第一個字母對應的數字，並轉換成兩位數數字。
        for (var i = 0; i < 26; i++) {
            if (firstChar == letters[i]) {
                firstNum = i + 10;
                nums[0] = Math.floor(firstNum / 10);
                nums[1] = firstNum - (nums[0] * 10);
                break;
            }
        }
        // 找出第2個字母對應的數字，並轉換成兩位數數字 留個位數。
        for (var i = 0; i < 26; i++) {
            if (secChar == letters[i]) {
                secNum = i + 10;
                nums[2] = secNum - (Math.floor(secNum / 10) * 10);
                break;
            }
        }
        // 執行加總計算
        for (var i = 0; i < multiply.length; i++) {
            if (i < 3) {
                total += nums[i] * multiply[i];
            } else {
                total += parseInt(idStr.charAt(i - 1)) *
                               multiply[i];
            }
        }

      
        if ((parseInt(total) + parseInt(lastNum)) % 10 == 0)
            args.IsValid = true;
        else
            args.IsValid = false;



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
             $scope.VM.ResTownAry = [{ I: "0", N: "鄉鎮市區"}].concat(ResTowndata);
             $scope.VM.ResVillageAry = [{ I: "0", N: "村里"}].concat(ResVillagedata);

             $scope.VM.SelectResCounty = "0";
             $scope.VM.SelectResTown = "0";
             $scope.VM.SelectResVillage = "0";

             //             $scope.VM.MainContactAry = [{ I: "0", N: "請選擇"}];
             //             $scope.VM.SelectMainContact = "0";

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

             // $scope.changePage(1);

             //             //List2
             //             $scope.PM2 = {};
             //             angular.copy($scope.PM, $scope.PM2);
             //             $scope.TM2 = {};
             //             angular.copy($scope.TM, $scope.TM2);


             //             $scope.changePage2 = function (pageIndex) {
             //                 var CaseID = $(".CaseID").html();
             //                 if (CaseID != "") {
             //                     $("#tmBlock2").show();
             //                     var postData = {};
             //                     postData.CaseID = CaseID;
             //                     $scope.PM2.changePage("/CaseMaintain/UserProfileContactListOP.aspx", postData, function (data) {
             //                         $scope.TM2.data = data.message;
             //                         $scope.$apply(function () {

             //                         });
             //                     });
             //                 }

             //             };

             //             $scope.goDetail2 = function (record) {

             //                OpenWindowWithPostOptions("/CaseMaintain/UserContact.aspx", 600, 508, "CapacityHistory", { c: record["CC"], i: record["C"],ParentClick:"UpdateContactList" });

             //             };

             //             $scope.changePage2(1);

             // GetMainContactInfo();




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



             $scope.ReloadMainContactSelect = function () {
                 // alert(CaseID);
                 var postData = {};
                 postData["CaseID"] = CaseID;
                 $.ajax({
                     cache: false,
                     type: "POST",
                     url: "/CaseMaintain/UserProfileContactSelectOP.aspx",
                     data: postData
                 })
                    .done(function (data) {
                        // alert(data);
                        data = data || [];

                        var SelectVal = "0";


                        $scope.$apply(function () {

                            if (data.length > 0) {
                                $scope.VM.MainContactAry = [{ N: "請選擇", I: "0", S: "0"}].concat(data);


                                angular.forEach(data, function (data, key) {

                                    if (data.S == '1') SelectVal= data.I;

                                });

                            }
                            else {
                                $scope.VM.MainContactAry = [{ N: "請選擇", I: "0", S: "0" }, { N: "本人", I: "-1", S: "0"}];
                                 
                            }
                            // alert($scope.VM.SelectMainContact);
                            $scope.VM.SelectMainContact = SelectVal;
                            // $scope.VM.SelectMainContact = SelectVal;
                            
                            
                        });

                         GetMainContactInfo();
                    })
                    .fail(function (jqXHR, textStatus) {

                    });



             };



         } ]);

 