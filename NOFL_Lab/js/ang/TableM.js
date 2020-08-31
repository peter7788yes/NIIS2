angular.module("TableM", [])
.provider("TableProvider", function () {
        //$http不能壓縮,angular會失敗
        this.$get = ["$http", function ($http) {
            var self = this;
            self.tbHeadArray = [];
            self.tbData = [];
            self.isHtml = false;
            return self;
        }];
})
.directive("tableGrid", ["$window", function ($window) {

    var templateUrlF = function (elem, attrs) {
        return attrs.templateUrl;
    };

    var controllerF = ["$scope", function ($scope) {
        $scope.Math = $window.Math;
        $scope.IsDate = function (obj) {
            var pattern = /\d{4}-\d{2}-\d{2}T\d{2}\:\d{2}\:\d{2}(\.\d{0,3})?/g;
            return pattern.test(obj);;
            //return obj instanceof Date;
        }
    }];

    var linkF = function (scope, element, attrs) {
        // DO SOMETHING
        //對pageModel屬性做一點事情

        //scope.$watch("tableModelD", function (newValue, oldValue) {
        //    if (newValue)
        //        console.log("I see a data change!");
        //}, true);

        //如果填false，每次都會執行function寫log，因為引用來源每次都不一樣
        //如果填true，如果值相同就不執行function了
        //Compare for object equality using angular.equals instead of comparing for reference equality.
    };

    return {
        require: ["ngModel"],
        restrict: "E",
        scope: {
            // same as '=info'
            //info:"="
            VMD: "=ngModel"
        },
        transclude: true,//,transclude:'element'//用element的時候是否包含原來的子元素，用element的時候是否包含原來的本身元素和子元素
        templateUrl: templateUrlF,
        controller: controllerF,
        link: linkF
    };
}]);

