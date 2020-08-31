angular.module("PageM", [])
.config(["$httpProvider", function ($httpProvider) {
        delete $httpProvider.defaults.headers.common["X-Requested-With"];
        //$httpProvider.defaults.headers.post['Accept'] = 'application/json, text/javascript';
        //$httpProvider.defaults.headers.post['Content-Type'] = 'application/json; charset=utf-8';
        //$httpProvider.defaults.headers.post['Access-Control-Max-Age'] = '1728000';
        //$httpProvider.defaults.headers.common['Access-Control-Max-Age'] = '1728000';
        //$httpProvider.defaults.headers.common['Accept'] = 'application/json, text/javascript';
        $httpProvider.defaults.headers.common["Content-Type"] = "application/x-www-form-urlencoded; charset=UTF-8";
        $httpProvider.defaults.useXDomain = true;
        //$httpProvider.defaults.transformRequest = function (data) {
        //   return data != undefined ? $.param(data) : null;
        //}
}])
.provider("PageProvider", function () {
    //$http不能壓縮,angular會失敗
    this.$get = ["$http", function ($http) {
        var self = this;
        self.rdCount =1;
        self.pgNow = 1;
        self.pgCount = 1;
        self.pgSize = 5;
        self.pgArray = [];
        self.pgStart = 1;
        self.pgRange = 5;
        self.pgEnd = self.pgRange;//10

        self.getDataCallback = function (data, callback) {
            self.rdCount = data.rdCount;
            self.pgNow = data.pgNow;
            self.pgCount = data.pgCount == 0 ? 1 : data.pgCount;
            self.pgSize = data.pgSize;
     
            if(self.pgNow == 1)
            {
                self.pgStart = 1;
            }
            else if (self.pgNow % self.pgRange==0 ) {
                    self.pgStart = self.pgNow - self.pgRange + 1;
            }
            else {
                self.pgStart = (Math.floor(data.pgNow / self.pgRange) * self.pgRange) + 1;
            }

            if (self.pgNow == self.pgCount) {
                self.pgEnd = self.pgNow;
            }
            else {

                if ((self.pgStart + self.pgRange) >= self.pgCount)
                    self.pgEnd = self.pgCount;
                else
                    self.pgEnd = self.pgStart + self.pgRange - 1;
            }
           
            self.pgArray = [];
            for (var i = self.pgStart; i <= self.pgEnd; i++) {
                self.pgArray.push(i);
            }
            callback(data);

            
        };

        //var scope = angular.element("body").scope();
        //$scope.$apply(function () {
        //    //scope.EnTraceCode = response.EnTraceCode;
        //    //scope.action = "VitaeInfo";
        //    $scope.PM = PageProvider;
        //});
        ///因為是用ajax方式，所以要在controller自己寫觸發$scope的改變
        self.changePage = function (url, postData, callback) {
            $.ajaxSetup({ cache: false });
            $.ajax({
              type: "POST",
              //url: '/LogOP.aspx',
              url: url,
              data: postData
            })
            .done(function (data) {
                self.getDataCallback(data, callback);
            })
            .fail(function (jqXHR, textStatus) {

            });

            //$http.post(url, postData)
            //.success(function (data, status, headers, config) {
            //    self.getDataCallback(data, callback);
            //})
            //.error(function (data, status, headers, config) {
            //    // called asynchronously if an error occurs
            //    // or server returns response with an error status.
            //});
        };
        self.genPageData = function (pageIndex) {
            var pgData = {};
            pgData.pgNow = pageIndex;
            return angular.extend(self, pgData);

        };

        self.filterPageData = function ($pgData) {
            var postData = {};
            postData.pgNow = $pgData.pgNow;
            postData.pgSize = $pgData.pgSize;
            return postData;
        };
        return self;
    }]; 
})
.directive("pageNav", ["$window",function ($window) {

    var templateUrlF = function (elem, attrs) {
        return attrs.templateUrl;
    };

    var controllerF = ["$scope",function ($scope) {
        $scope.Math = $window.Math;
    }];

    var linkF = function (scope, element, attrs) {
        //對pageModel屬性做一點事情
        scope.$watch("pageModelD", function (newValue, oldValue) {
            if (newValue)
                console.log("I see a data change!");
        }, true);
        //如果填false，每次都會執行function寫log，因為引用來源每次都不一樣
        //如果填true，如果值相同就不執行function了
        //Compare for object equality using angular.equals instead of comparing for reference equality.
    };

    //Example of a directive that optionally requires a form controller from an ancestor:
    //myApp.directive('myDirective', function() {
    //    return {
    //        require: '^?form'
    //    }
    //}
    return {
        //The ^ prefix means that this directive searches for the controller on its parents (without the ^ prefix, the directive would look for the controller on just its own element)
        //require是需求在什麼controller裡面或是什麼tag裡面
        require: ["ngModel"],//Example of a directive that requires ngModel controller:
        restrict: "E",
        scope: {
            // same as '=info'
            //info:"="
            VMD: "=ngModel",
            changePageD: "&onChangePageD"
        },
        transclude: true,//,transclude:'element'//用element的時候是否包含原來的子元素，用element的時候是否包含原來的本身元素和子元素
        //templateUrl: "js/template/PageNav.html",
        templateUrl: templateUrlF,
        controller: controllerF,
        link: linkF
    };
}]);

