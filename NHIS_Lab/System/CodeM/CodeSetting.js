$(function () {
    window.addEventListener("popstate", function () {
        var currentState = history.state;
        if (currentState != null) {
            var controllerElement = document.querySelector('section');
            var controllerScope = angular.element(controllerElement).scope();
            controllerScope.$apply(function () {
                controllerScope.changePage(currentState.pageIndex, 1);
            });
        }
    });

    if (location.search.length > 0) {
        setTimeout(function () {
            history.go(-1);
        }, 10);
    }
});


angular.module("MyApp", ["PageM", "cfp.hotkeys"])
         .controller("MyController", ["$scope", "PageProvider", "hotkeys", function ($scope, PageProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};
             $scope.VM.title = "";
             $scope.VM.publishState = "0";

             $scope.changePage = function (pageIndex, noHistory) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};
                 postData.D = $scope.VM.title;
                 postData.p = $scope.VM.publishState;
                 postData = $scope.PM.filterPageData(pgData, postData);
                 $scope.PM.changePage("/System/CodeM/CodeSettingOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });

                     if (history.pushState && noHistory == undefined) {
                         if (pageIndex > 1 || location.hash.length > 0) {
                             history.pushState({ "pageIndex": pageIndex }, document.title, location.href.split("?")[0] + "?#h");
                         }
                         else {
                             history.replaceState({ "pageIndex": 1 }, document.title, location.href);
                         }
                     }
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

             $scope.goDetail = function (record) {

                 if (history.pushState) {
                     history.pushState(null, document.title, location.href.split("?")[0] + "?b=1");
                 }

                 var keys = [];
                 var values = [];
                 keys[0] = "i";
                 values[0] = record["I"];
                 doPOST("/System/CodeM/CodeSetting_Detail.aspx", keys, values);

            
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
