$(function () {
    $(document).on("click", "#lastBtn", function (e) {
        history.go(-1);
        e.preventDefault();
        return false;
    });
    
});

angular.module("MyApp", ["PageM", "cfp.hotkeys"])
         .controller("MyController", ["$scope", "PageProvider", "hotkeys", function ($scope, PageProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};
             $scope.VM.enState = "0";
             $scope.VM.title = "";
             $scope.VM.publishState = "0";

             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};
                 postData["i"] = I;
                 postData["en"] = $scope.VM.enState;
                 postData = $scope.PM.filterPageData(pgData, postData);
                 $scope.PM.changePage("/System/CodeM/CodeSetting_DetailOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () {});
                 });
             };

             hotkeys.add({
                 combo: 'enter',
                 description: 'Description goes here',
                 allowIn: ['INPUT',"BUTTON", 'SELECT', 'TEXTAREA'],
                 callback: function (event, hotkey) {
                     var focusType = document.activeElement.type;
                     if (focusType == "text" || focusType == "select-one")
                     {
                         //$scope.changePage(1);
                         //event.preventDefault();
                         //return false;
                         //$("input[name='addBtn']").trigger("click");
                     }
                 }
             });


             $scope.changePage(1);


             $scope.goAdd = function () {
                 var keys = [];
                 var values = [];
                 keys[0] = "i";
                 values[0] = $('#i').val();
                 doPOST("/System/CodeM/CodeSetting_Add.aspx", keys, values);
             };

             var doPOST = function (url, keys, values) {
                 keys = keys || [];
                 values = values || [];
                 var html = "";
                 html += "<html><head></head><body><form id='formid' method='post' action='" + url + "' autocomplete='off'>";
                 if (keys && values && (keys.length == values.length))
                     for (var i = 0; i < keys.length; i++)
                         html += "<input type='hidden' name='" + keys[i] + "' value='" + values[i] + "'/>";
                 html += "</form><script type='text/javascript'>document.getElementById(\"formid\").submit()</script></body></html>";
                 //console.log(html);
                 document.write(html);
             };
             
}]);

