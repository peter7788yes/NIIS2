
var changeYellowCardUpdateRecord = function (info) {
    $("#YellowCardUpdateRecord").text(info);
};


var changeCaseUserRemark = function (info) {
    $("#CaseUserRemark").text(info);
};

$(function () {
    $(document).on("click", "#addBtn", function (e) {
        location.href = "/Vaccination/RecordM/RegisterData_Add.aspx";
        e.preventDefault();
        return false;
    });

    $(document).on("click", "#lastBtn", function (e) {
        history.go(-1);
        e.preventDefault();
        return false;
    });
});

angular.module("MyApp", ["PageM","FilterM"])
         .controller("MyController", ["$scope", "PageProvider", function ($scope, PageProvider) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.TM.data = tbAry;

             $scope.VM = {};
             $scope.VM.u = uJson;
             $scope.VM.title = "";
             $scope.VM.publishState = "0";
            
          
             $scope.goAddYellowCard = function (record) {
                 var keys = [];
                 var values = [];
                 keys[0] = "c";
                 values[0] = CC;
                 openWindowWithPost("/Vaccination/RecordM/YellowCard_Add.aspx", "YellowCard_Add", 610, 300, keys, values);
                 //popUpWindow("/Vaccination/RecordM/YellowCard_Add.aspx?i=1", "YellowCard_Add", 610, 300);
             };

             $scope.goAddRemark = function (record) {
                 var keys = [];
                 var values = [];
                 keys[0] = "c";
                 values[0] = CC;
                 openWindowWithPost("/Vaccination/RecordM/CaseUserRemark_Add.aspx", "CaseUserRemark_Add", 610, 300, keys, values);
                 //popUpWindow("/Vaccination/RecordM/CaseUserRemark_Add.aspx?i=1", "CaseUserRemark_Add", 610, 300);
             };

             ///////////
             $scope.goChooseCate = function (record) {
                 var keys = [];
                 var values = [];
                 keys[0] = "c";
                 keys[1] = "i";
                 keys[2] = "r";
                 keys[3] = "a";
                 values[0] = CC;
                 values[1] = record.RID;
                 values[2] = record.SRVC;
                 values[3] = record.AD;
                 openWindowWithPost("/Vaccination/RecordM/ChooseCate.aspx", "ChooseCate", 610, 300, keys, values);
                 //popUpWindow("/Vaccination/RecordM/ChooseCate.aspx?i=1", "ChooseCate", 610, 300);
             };

             $scope.goDetail = function (record) {
                 var keys = [];
                 var values = [];
                 keys[0] = "c";
                 keys[1] = "i";
                 keys[2] = "r";
                 keys[3] = "a";
                 keys[4] = "ae";
                 values[0] = CC;
                 values[1] = record.RID;
                 values[2] = record.SRVC;
                 values[3] = record.AD;
                 values[4] = encodeURIComponent(record.AE);
                 //var x = record.AE;
                 //console.log($('<div/>').text(x).html());
                 //values[4] = htmlEncode(record.AE);
                 openWindowWithPost("/Vaccination/RecordM/RecordDetail.aspx", "RecordDetail", 930, 450, keys, values);
                 //popUpWindow("/Vaccination/RecordM/RecordDetail.aspx?i=1", "RecordDetail", 930, 450);
             };

             $scope.goAddVaccine = function (record) {
                 //popUpWindow("/Vaccination/RecordM/AddVaccine.aspx?i=1", "AddVaccine", 930, 450);
                 var data = [];
                 angular.forEach($scope.TM.data, function (item, index) {
                     data.push(item.SRVC);
                 });
                 var keys = [];
                 var values = [];
                 keys[0] = "a";
                 values[0] = data;
                 keys[1] = "c";
                 values[1] = CC;
                 openWindowWithPost("/Vaccination/RecordM/AddVaccine.aspx", "AddVaccine", 930, 450,keys,values);
                 //window.open("/Vaccination/RecordM/YellowCard_Add.aspx", "YellowCard_Add", "width=610,height=300,toolbar=no,menubar=no,scrollbars=yes,resizable=yes");
             };

             var popUpWindow = function (url, title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }

             var openWindowWithPost =function (url, title,w,h, keys, values) {
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

             $scope.getClass = function (record) {
                 var rtn='';
                 switch (record.CC) {
                     case 1:
                         rtn = 'yellowcolor';
                       
                         break;
                     case 2:
                         rtn = 'pinkcolor';
                         break;
                 }
                 //console.log(rtn);

                 return rtn;
             };


             function htmlEncode(value) {
                 //create a in-memory div, set it's inner text(which jQuery automatically encodes)
                 //then grab the encoded contents back out.  The div never exists on the page.
                 return $('<div/>').text(value).html();
             }

             function htmlDecode(value) {
                 return $('<div/>').html(value).text();
             }


         }]);

var getRecordDataID = function (obj) {
    var controllerElement = document.querySelector('section');
    var controllerScope = angular.element(controllerElement).scope();
    controllerScope.$apply(function () {
        var keepGoing = true;
        angular.forEach(controllerScope.TM.data, function (item, index) {
            if (keepGoing) {
                if (obj.SCode == item.SRVC) {
                    item.RID = obj.RID;
                    item.VBID =VBID
                    keepGoing = false;
                }
            }
        });

        console.log(controllerScope.TM.data);
    });
};


var AddApplyRecord =function(obj)
{
    //console.log(obj);
    //alert(54321);
    if (obj == null)
        return;

    var controllerElement = document.querySelector('section');
    var controllerScope = angular.element(controllerElement).scope();
    controllerScope.$apply(function () {
        var keepGoing = true;
        angular.forEach(controllerScope.TM.data, function (item, index) {
            if (keepGoing) {
              
                if (item.RID == obj.RID) {
                    item.CD = obj.CD;
                    item.VB = obj.VB;
                    item.ID = obj.ID;
                    item.ON = obj.ON;
                }
            }
        });

        //console.log(controllerScope.TM.data);
    });
}
var getNewVaccine = function (obj) {

    var Vobj =
    {
        "YMID": 0,
        "RID": obj["I"],
        "AE": "",
        "P": 0,
        "SRVID": 0,
        "SRVC": obj["R"],
        "ID": null,
        "CD": null,
        "OI": 0,
        "CT": "",
        "VB": 0,
        "AD": null,
        "ON": ON,
        "CC": 0,
        "IR": false
    };

    var controllerElement = document.querySelector('section');
    var controllerScope = angular.element(controllerElement).scope();
    controllerScope.$apply(function () {
        var thisIndex = 0;
        angular.forEach(controllerScope.TM.data, function (item, index) {
            if(thisIndex ==0 && item.ID!=null){
                var thisIndex = index;
            }
        });

        controllerScope.TM.data.splice(thisIndex, 0, Vobj);
    });
   

};