angular.module("MasterApp", ["angularTreeview","ngCookies", "angular.css.injector"])
        .controller("MasterController",["$scope","$cookies","cssInjector",function ($scope,$cookies, cssInjector) {
            var size = $cookies["fontSize"];
            if (size === undefined) {
                cssInjector.add("/css/fontM.css");
                $cookies["fontSize"] = "fontM";
            }
            else {
                cssInjector.add("/css/" + size + ".css");
            }
}]);

angular.bootstrap(document.getElementById("MasterApp"), ["MasterApp"]);