﻿angular.module("InputM", []).provider("InputProvider", function () { this.$get = [function () { var e = this; return e }] }).directive("checkRocid", function () { return { restrict: "A", require: "ngModel", link: function (e, n, r, t) { console.log(r); var i = /[a-zA-Z][1-2]{1}\d{8}/g; t.$parsers.unshift(function (e) { return 10 == e.length && 1 == i.test(e) ? (t.$setValidity("checkRocid", !0), e) : void t.$setValidity("checkRocid", !1) }) } } }).directive("inputRocid", function () { return { require: ["ngModel"], restrict: "E", scope: {}, transclude: !0, link: function (e, n, r, t) { }, compile: function (e, n) { var r = n.name, t = n.form, i = n.err, c = n.required, o = n.vm, u = "<input type='text' name='" + r + "'  ng-model='" + o + "' check-rocid"; void 0 != c && (u += " required"), u += "/>", void 0 != c && (u += "<span ng-show='" + t + "." + r + ".$error.required'>必填</span>"), u += "<span ng-show='" + t + "." + r + ".$error.checkRocid'>" + i + "</span>", e.html(u) } } }).directive("inputSubmit", function () { return { restrict: "E", scope: {}, transclude: !0, link: function (e, n, r, t) { }, compile: function (e, n) { var r = n.value, t = n.name, i = n.form; n.err; e.html("<input type='button' name='" + t + "' value='" + r + "' ng-disabled ='" + i + ".$invaild'/>") } } }).directive("inputNumber", ["$http", "$templateCache", "$compile", function (e, n, r) { return { require: ["ngModel"], restrict: "E", transclude: !0, compile: function (e, n) { var r = n.min, t = n.max, i = n.name, c = n.form, o = n.err, u = n.vm; n.templateUrl; e.html("<input type='number' min='" + r + "' max='" + t + "' name='" + i + "' ng-model='" + u + "' /><span ng-show='" + c + "." + i + ".$valid == false'>" + o + "</span>") } } }]);