/*! NIIS 2016-05-09 */

angular.module("TableM",[]).provider("TableProvider",function(){this.$get=["$http",function(a){var b=this;return b.tbHeadArray=[],b.tbData=[],b.isHtml=!1,b}]}).directive("tableGrid",["$window",function(a){var b=function(a,b){return b.templateUrl},c=["$scope",function(b){b.Math=a.Math,b.IsDate=function(a){var b=/\d{4}-\d{2}-\d{2}T\d{2}\:\d{2}\:\d{2}(\.\d{0,3})?/g;return b.test(a)}}],d=function(a,b,c){};return{require:["ngModel"],restrict:"E",scope:{VMD:"=ngModel"},transclude:!0,templateUrl:b,controller:c,link:d}}]);
//# sourceMappingURL=TableM.js.map