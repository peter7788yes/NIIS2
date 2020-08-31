angular.module("InputM", [])
.provider("InputProvider", function () {
        //$http不能壓縮,angular會失敗
        this.$get = [function () {
            var self = this;
            return self;
        }];
})
.directive("checkRocid", function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ctrl) {   //ctrl= NgModelController
            console.log(attrs);
            //這裡的驗證要從寫
            var pattern = /[a-zA-Z][1-2]{1}\d{8}/g;
            ctrl.$parsers.unshift(function (viewValue) {
                //alert(viewValue);
                if (viewValue.length == 10 && pattern.test(viewValue) == true) {
                    ctrl.$setValidity('checkRocid', true);
                    return viewValue;
                } else {
                    ctrl.$setValidity('checkRocid', false);
                    return undefined;
                }
            });
        }
    };
})
.directive("inputRocid", function () {
    return {
        require: ["ngModel"],
        restrict: "E",
        scope: {
            // same as '=info'
            //info:"="
            //VMD: "=ngModel"
        },
        transclude: true,//,transclude:'element'//用element的時候是否包含原來的子元素，
        //template: "<input type='text' name='rocId'  ng-model='VMD.VM' check-roc-id/><span ng-show=form1.rocId.$error.checkRocId' ng-bind='VMD.errMsg'></span>",
        link: function (scope, elm, attrs, ctrl) { },
        compile: function (element, attrs) {
            var name = attrs.name;
            var formName = attrs.form;
            var errMsg = attrs.err;
            var required = attrs.required;
            var VM = attrs.vm;
            var html ="<input type='text' name='" + name + "'  ng-model='"+VM+"' check-rocid";
            if(required != undefined)
                html +=" required";
            html += "/>";
            if (required != undefined)
                html += "<span ng-show='" + formName + "." + name + ".$error.required'>必填</span>";
            html += "<span ng-show='" + formName + "." + name + ".$error.checkRocid'>" + errMsg + "</span>";

            element.html(html);
        }
    };
})
.directive("inputSubmit", function () {
    return {
        //require: ["ngModel"],
        restrict: "E",
        scope: {
            // same as '=info'
            //info:"="
            //VMD: "=ngModel"
        },
        transclude: true,//,transclude:'element'//用element的時候是否包含原來的子元素，
        //template: "<input type='text' name='rocId'  ng-model='VMD.VM' check-roc-id/><span ng-show=form1.rocId.$error.checkRocId' ng-bind='VMD.errMsg'></span>",
        link: function (scope, elm, attrs, ctrl) { },
        compile: function (element, attrs) {
            var value = attrs.value;
            var name = attrs.name;
            var formName = attrs.form;
            var errMsg = attrs.err;
            element.html("<input type='button' name='" + name + "' value='"+value+"' ng-disabled ='" + formName + ".$invaild'/>");
        }
    };
})
.directive("inputNumber", ["$http", "$templateCache", "$compile", function ($http, $templateCache, $compile) {
    //var getTemlateUrl = function (url) {
    //    var data = "";
    //    $http.get(url)
    //         .then(function (response) {
    //             //alert(response.data);
    //             data = response.data;
    //             //element.html($compile(response.data)(scope));
    //         });
    //    return data;
    //}
    return {
        require: ["ngModel"],
        //scope: true, //false => 與controller 的scope同步,true 就是 isolate scope
        restrict: "E",
        //scope: {
        //    // same as '=info'
        //    //info:"="
        //    VMD: "=ngModel"
        //},
        transclude: true,//,transclude:'element'//用element的時候是否包含原來的子元素，
        //template: "<input type='text' name='rocId'  ng-model='VMD.VM' check-roc-id/><span ng-show=form1.rocId.$error.checkRocId' ng-bind='VMD.errMsg'></span>",
        //link: function (scope, element, attrs, ctrl) {
        //link: function(scope, element){
        //    $templateRequest("template.html").then(function(html){
        //        element.append($compile(html)(scope));
        //    });
        //};
        //compile:  function(element, attrs) {
        //    $http.get(attrs.templateUrl, {cache: $templateCache}).success(function(html) {

        //      t.Element.html(html);
        //}
        compile: function (tElement, tAttrs) {
            var min = tAttrs.min;
            var max = tAttrs.max;
            var name = tAttrs.name;
            var formName = tAttrs.form;
            var errMsg = tAttrs.err;
            var VM = tAttrs.vm;
            var templateUrl = tAttrs.templateUrl;

            tElement.html("<input type='number' min='" + min + "' max='" + max + "' name='" + name + "' ng-model='" + VM + "' /><span ng-show='" + formName + "." + name + ".$valid == false'>" + errMsg + "</span>");
            //var dict =
            //    {
            //        "@min": min,
            //        "@max": max,
            //        "@name": name,
            //        "@VM": VM,
            //        "@ng-show": formName + "." + name + ".$valid == false",
            //        "@errMsg": errMsg
            //    };
            //var tag = '<input type="number" min="@min" max="@max" name="@name" ng-model="@VM" /><span ng-show="@ng-show">@errMsg</span>';
            //angular.forEach(dict, function (value, key) {
            //    tag = tag.replace(key, value);
            //});
            //tElement.html(tag);
            //var keyArray = ["@min", "@max", "@name", "@VM", "@ng-show","@errMsg"];
            //var valueArray = [min, max, name, VM, formName + "." + name + ".$valid == false", errMsg];
            //alert(templateUrl);
            //tElement.html("<input type='number' min='" + min + "' max='" + max + "' name='" + name + "' ng-model='" + VM + "' /><span ng-show='" + formName + "." + name + ".$valid == false'>" + errMsg + "</span>");
            //tElement.html("{{" + formName + "." + name + ".$valid == false" + "}}");
            //var tmp = "";
            //var templateLoader = $http.get(templateUrl, { cache: $templateCache })
            //                       .success(function (html) {
            //                           var data = html;
            //                           angular.forEach(dict, function (value, key) {
            //                               data = data.replace(key, value);
            //                           });
            //                           tmp = data;
            //                           tElement.html(data);
            //                       });
            // return function (scope, element, attrs) {
            //             templateLoader.then(function (templateText) {
            //                 element.html($compile(tElement.html())(scope));
            //             });
            //         };
            //element.html($sce.trustAsHtml(getTemlateUrl(templateUrl)));
        }
    };
}]);