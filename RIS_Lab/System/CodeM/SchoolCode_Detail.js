$(function () {
    $(document).on("click", "#lastBtn", function (e) {
        history.go(-1);
        e.preventDefault();
        return false;
    });

    $(document).on("click", "#cancelBtn", function (e) {
        window.close();
        e.preventDefault();
        return false;
    });

    if (UP == false) {
        $("#SelectCounty").prop("disabled", true);
        $("#SelectTown").prop("disabled", true);
        $("#SelectVillage").prop("disabled", true);
    }


    var aryLengh = stateAry.length;
    var aryNumber = "";
    $.each(stateAry.reverse(), function (index, item) {
        var $input = $("<input class='cbs' type='checkbox'/>").val(item.EV);
        var $label = $("<label/>");
        $label.append($input).append(item.EN);
        $("#td").prepend($label);
    });
    $("#td").append("<br/><label><input id='cbOther'  type='checkbox'/>其他(請說明)：<input id='tbOther'  name='tbOther' class='text02' type='text'/>科</label>");


    $(document).on("change", "#tbOther", function (e) {
        if ($(this).val() == "") {
            $("#cbOther").prop('checked', false);
        }
        else {
            $("#cbOther").prop('checked', true);
        }
        e.preventDefault();
        return false;
    });

    $.each(tbOtherIDs.split(","), function (index, item) {
        $("input:checkbox[value='" + item + "']").prop("checked", true);
    });

    $("#tbOther").val(tbOther);

    if ($("#tbOther").val() == "") {
        $("#cbOther").prop('checked', false);
    }
    else {
        $("#cbOther").prop('checked', true);
    }



    $(document).on("submit", "form", function (e) {
        var ary = [];
        $.each($(".cbs"), function (index, item) {
            if ($(item).prop("checked") == true) {
                ary.push($(item).val());
            }
        });
        $("#did").val(ary);
    });

    //$(document).on("change", "#ddAgState", function (e) {
    //    if($(this).val()=="2")
    //    {
    //        $('.tbDates').show();
    //    }
    //    else
    //    {
    //       $('.tbDates').hide();
    //    }
    //});
});

angular.module("MyApp", [])
         .controller("MyController", ["$scope", "$http", function ($scope, $http) {
             $scope.VM = {};


             $scope.VM.CountyAry = [{ I: '0', N: "縣市" }].concat(countyJson);
             $scope.VM.TownAry = [{ I: '0', N: "鄉鎮" }].concat(townJson);
             $scope.VM.VillageAry = [{ I: "0", N: "村里" }].concat(villageJson);

             $scope.VM.SelectCounty = county;
             $scope.VM.SelectTown = town;
             $scope.VM.SelectVillage = village;;



             $scope.BindCounty = function () {
                 var postData = {};
                 postData["a"] = "County";


                 $http({
                     method: 'POST',
                     url: "/Ashx/SystemAreaCodeOP.ashx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
                .success(function (data, status, headers, config) {
                    data = data || [];
                    if (data.length > 0) {
                        $scope.VM.CountyAry = [{ I: '0', N: "全部" }].concat(data);
                        $scope.VM.SelectCounty = "0";
                    }
                    else {
                        $scope.VM.CountyAry = [{ I: '0', N: "全部" }];
                        $scope.VM.SelectCounty = "0";
                    }
                })
                 .error(function (data, status, headers, config) {
                     // called asynchronously if an error occurs
                     // or server returns response with an error status.
                 });
             };




             $scope.SelectCountyChange = function () {
                 var postData = {};
                 postData["a"] = "Town";
                 postData["p"] = $("#SelectCounty").val();


                 $http({
                     method: 'POST',
                     url: "/Ashx/SystemAreaCodeOP.ashx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
               .success(function (data, status, headers, config) {
                   data = data || [];
                   if (data.length > 0) {
                       $scope.VM.TownAry = [{ I: '0', N: "全部" }].concat(data);;
                       $scope.VM.SelectTown = "0";
                   }
                   else {
                       $scope.VM.TownAry = [{ I: '0', N: "全部" }];
                       $scope.VM.SelectTown = "0";
                   }

                   $scope.VM.VillageAry = [{ I: "0", N: "全部" }];
                   $scope.VM.SelectVillage = "0";
               })
                .error(function (data, status, headers, config) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });

             };


             $scope.SelectTownChange = function () {
                 var postData = {};
                 postData["a"] = "Village";
                 postData["p"] = $("#SelectTown").val();


                 $http({
                     method: 'POST',
                     url: "/Ashx/SystemAreaCodeOP.ashx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
               .success(function (data, status, headers, config) {
                   data = data || [];
                   if (data.length > 0) {
                       $scope.VM.VillageAry = [{ I: "0", N: "全部" }].concat(data);

                   }
                   else {
                       $scope.VM.VillageAry = [{ I: "0", N: "全部" }];

                   }
                   $scope.VM.SelectVillage = "0";

               })
                .error(function (data, status, headers, config) {
                    // called asynchronously if an error occurs
                    // or server returns response with an error status.
                });

             };


             $scope.openOrgs = function () {
                 popUpWindow("/SelectSingleOrg.aspx", "SelectSingleOrg", 930, 450);

             };

             $scope.refresh = function () {
                 var location = $('#tbLocation').val();
                 var Vaccine = $('#tbVaccine').val();
                 var VaccineIDs = $('#hfVaccineIDs').val();

                 setTimeout(function () {
                     $scope.$apply(function () {
                         $scope.VM.location = location;
                         $scope.VM.Vaccine = Vaccine;
                         $scope.VM.VaccineIDs = VaccineIDs;
                     });
                 }, 1);
             };



             $scope.openAddVaccine = function (i) {
                 popUpWindow("/Vaccination/ParameterM/LocationSetting_AddVaccine.aspx?i=" + i + "&is=" + $('#hfVaccineIDs').val(), "AddVaccine", "AddVaccine", 910, 700)
             };

             var popUpWindow = function (url, title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }
         }]);



