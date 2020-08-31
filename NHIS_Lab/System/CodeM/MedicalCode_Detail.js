$(function () {
    $(document).on("click", "#lastBtn", function (e) {
        history.go(-1);
        e.preventDefault();
        return false;
    });


    //window.addEventListener("popstate", function () {
    //    var currentState = history.state;
    //    if (currentState != null) {
    //        var controllerElement = document.querySelector('section');
    //        var controllerScope = angular.element(controllerElement).scope();
    //        controllerScope.$apply(function () {
    //            controllerScope.changePage(currentState.pageIndex, 1);
    //        });
    //    }
    //});

    //if (location.search.length > 0) {
    //    setTimeout(function () {
    //        history.go(-1);
    //    }, 10);
    //}
});
angular.module("MyApp", ["PageM","FilterM"])
         .controller("MyController", ["$scope", "PageProvider", "$http", function ($scope, PageProvider, $http) {
             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.VM = {};

             $scope.changePage = function (pageIndex, noHistory) {
                 $("#tmBlock").show();

                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {};
                 postData["i"] = i;
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("/System/CodeM/MedicalCode_DetailOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });

                     //if (history.pushState && noHistory == undefined) {
                     //    if (pageIndex > 1 || location.hash.length > 0) {
                     //        history.pushState({ "pageIndex": pageIndex }, document.title, location.href.split("?")[0] + "?#h");
                     //    }
                     //    else {
                     //        history.replaceState({ "pageIndex": 1 }, document.title, location.href);
                     //    }
                     //}
                 });
             };

             $scope.changePage(1);

}]);

