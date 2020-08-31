$(function () {
    $(document).on("click", "#lastBtn", function (e) {
        location.href = "/Vaccination/CertificateM/PrintCertificate.aspx";
        e.preventDefault();
        return false;
    });


});

angular.module("MyApp", ["PageM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", function ($scope, PageProvider) {

             $scope.PM = PageProvider;
             $scope.TM = {};

             $scope.VM = {};
             $scope.VM.u = uJson;
             $scope.VM.CountyAry = [{ I: '0', N: "全部" }];
             $scope.VM.SelectCounty = "0";

             $scope.VM.TownAry = [{ I: '0', N: "全部" }];
             $scope.VM.SelectTown = "0";


             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};
                 postData["i"]=i;
                 postData = $scope.PM.filterPageData(pgData, postData);
                 $scope.PM.changePage("/Vaccination/CertificateM/ApplyDataRecordOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     
                     angular.forEach(data.message, function (item, index) {
                         item.ary=[];
                         angular.forEach(data.message2, function (item2, index2) {
                             if(item.I == item2.AI)
                             {
                                 item.ary.push(item2);
                             }
                         });
                     });

                     //console.log($scope.TM.data);
                    
                     $scope.$apply(function () { });
                 });
             };

             $scope.changePage(1);


             $scope.goDetail = function (record) {
                 location.href = "/Vaccination/RecordM/RegisterData_Detail.aspx?i=" + record["I"];
             };


            
             $scope.goDownload = function (record) {
                 console.log(record);
                 var keys = [];
                 var values = [];
                 keys[0] = "i";
                 values[0] = record["FI"];
                 doPOST("/Vaccination/CertificateM/ApplyDataRecord_DownloadOP.aspx", keys, values);
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



var getCode = function (code) {
    $("#tbLocation").val(code.text);
    $("#hfLocationID").val(code.id);
    $('#refreshBtn').trigger('click');
};


