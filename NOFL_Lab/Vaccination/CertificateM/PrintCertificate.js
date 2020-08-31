$(function () {
    $(document).on("click", "#AddBtn", function (e) {
        location.href = "/CaseMaintain/UserProfile.aspx";
        e.preventDefault();
        return false;
    });

    $(document).on("click", "#SearchBtn", function (e) {
        e.preventDefault();
        return false;
    });

});

angular.module("MyApp", ["PageM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", function ($scope, PageProvider) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};

             $scope.VM.CountyAry = [{ I: '0', N: "全部" }];
             $scope.VM.SelectCounty = "0";

             $scope.VM.TownAry = [{ I: '0', N: "全部" }];
             $scope.VM.SelectTown = "0";


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
                 $scope.PM.changePage("/Vaccination/CertificateM/PrintCertificateOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };

             $scope.changePage(1);


             $scope.goDetail = function (record) {
                 var keys = [];
                 var values = [];
                 keys[0] = "i";
                 values[0] = record["C"];
                 doPOST("/Vaccination/RecordM/RegisterData_Detail.aspx", keys, values);
                 //location.href = "/Vaccination/RecordM/RegisterData_Detail.aspx?i=" + record["C"];
             };

             $scope.goRecord = function (record) {
                 var keys = [];
                 var values = [];
                 keys[0] = "i";
                 values[0] = record["C"];
                 doPOST("/Vaccination/CertificateM/ApplyDataRecord.aspx", keys, values);
                 //location.href = "/Vaccination/RecordM/RegisterData_Detail.aspx?i=" + record["C"];
             };


             $scope.goApplyData = function (record) {
                 popUpWindow("/Vaccination/CertificateM/ApplyData.aspx?i=" + record["C"], "ApplyData", 930, 450);
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
                                    $scope.VM.TownAry = [{ I: '0', N: "全部" }].concat(data);;
                                    $scope.VM.SelectTown = "0";
                                }
                                else {
                                    $scope.VM.TownAry = [{ I: '0', N: "全部" }];
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
                                    $scope.VM.CountyAry = [{ I: '0', N: "全部" }].concat(data);
                                    $scope.VM.SelectCounty = "0";
                                }
                                else {
                                    $scope.VM.CountyAry = [{ I: '0', N: "全部" }];
                                    $scope.VM.SelectCounty = "0";
                                }

                            });
                        }, 1);


                    })
                    .fail(function (jqXHR, textStatus) {

                    });

             };



             $scope.BindCounty();



             $scope.openOrgs = function (record) {

                 popUpWindow("/SelectSingleOrg.aspx", "SelectSingleOrg", 930, 450);

             };


             var popUpWindow = function (url, target, title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }



             var doPOST = function (url, keys, values) {
                 keys = keys || [];
                 values = values || [];
                 var html = "";
                 html += "<html><head></head><body><form id='formid' method='post' action='" + url + "'>";
                 if (keys && values && (keys.length == values.length))
                     for (var i = 0; i < keys.length; i++)
                         html += "<input type='hidden' name='" + keys[i] + "' value='" + values[i] + "'/>";
                 html += "</form><script type='text/javascript'>document.getElementById(\"formid\").submit()</script></body></html>";
                 document.write(html);
             };
         }]);



