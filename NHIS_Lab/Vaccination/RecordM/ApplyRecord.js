//change opener to '/Vaccination/RecordM/RegisterData_Detail.aspx'
//if (window.opener.location.pathname != '"/Vaccination/RecordM/RegisterData_Detail.aspx"') {
//    window.opener = window.opener.window.opener;
//}

//var rootWin = window.opener;

$(function () {

    $(document).on("click", "#closeBtn", function (e) {
        window.close();
        e.preventDefault();
        return false;
    });
});

angular.module("MyApp", ["FilterM"])
         .controller("MyController", ["$scope", "$http", function ($scope, $http) {

             $scope.TM = {};
             //$scope.TM.isHide = true;
             $scope.VM = {};

             $scope.VM.agency = agency;
             $scope.VM.agencyID = agencyID;
             $scope.VM.SelectVacc = "0";
             $scope.VM.picVM = [{ VI: '0', BI: "無", AD: null, BB: "", DP: 0, FD: "" }];
             $scope.VM.VaccAry = [{ VI: '0', BI: "無",AD:null,BB:"",DP:0,FD:"" }].concat(tbAry);

             $scope.goAddYellowCard = function () {
                 popUpWindow("/Vaccination/RecordM/YellowCard_Add.aspx", "YellowCard_Add", 610, 300);
                 //window.open("/Vaccination/RecordM/YellowCard_Add.aspx", "YellowCard_Add", "width=610,height=300,toolbar=no,menubar=no,scrollbars=yes,resizable=yes");
             };

             $scope.openSelectAgency = function (record) {
                 popUpWindow("/Vaccination/RecordM/ApplyRecord_SelectAgency.aspx", "SelectAgency", 930, 450);

             };


             $scope.refresh = function () {
                 var agency = $('#tbAgency').val();
                 var agencyID = $('#hfAgencyID').val();

                 setTimeout(function () {
                     $scope.$apply(function () {
                         $scope.VM.agency = agency;
                         $scope.VM.agencyID = agencyID;
                     });
                 }, 1);

             };

             $scope.chagneSelect = function () {
                 //console.log($("#SelectVacc")[0].selectedIndex);
                 $scope.VM.picVM = $scope.VM.VaccAry[$("#SelectVacc")[0].selectedIndex];
             };



             var popUpWindow = function (url, title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }

             var openWindowWithPost = function (url, title, w, h, keys, values) {
                 var newWindow = popUpWindow(url, title, w, h);
                 if (!newWindow) return false;
                 var html = "";
                 html += "<html><head></head><body><form id='formid' method='post' action='" + url + "'>";
                 keys = keys || [];
                 values = values || [];
                 if (keys && values && (keys.length == values.length))
                     for (var i = 0; i < keys.length; i++)
                         html += "<input type='hidden' name='" + keys[i] + "' value='" + values[i] + "'/>";
                 html += "</form><script type='text/javascript'>document.getElementById(\"formid\").submit()</script></body></html>";
                 newWindow.document.write(html);
                 return newWindow;
             }


             $scope.openAddVaccine = function () {
                 var keys = [];
                 var values = [];
                 openWindowWithPost("/Vaccination/RecordM/ApplyRecord_AddVaccine.aspx", "ApplyRecord_AddVaccine", 920, 450, keys, values);
             };
         }]);



var getAgency = function (code) {
    $("#tbAgency").val(code.AN);
    $("#hfAgencyID").val(code.I);
    $('#refreshBtn').trigger('click');
};



window.onbeforeunload = function () {
    window.opener.popOpenedWindows("ApplyRecord");
};