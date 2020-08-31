//(function (window, angular) {
    //'use strict'  
 angular.module("TreeM", [])
    .directive('treeView', [function () {

        return {
            restrict: 'E',
            templateUrl: '/html/ang_template/TreeView.html',
            scope: {
                selectedItem: '=',
                treeData: '=',
                canChecked: '=',
                textField: '@',
                itemClicked: '&',
                itemCheckedChanged: '&',
                itemTemplateUrl: '@'
            },
            controller: ['$scope', function ($scope) {
                $scope.itemExpended = function (item, $event) {
                    item.$$isExpend = !item.$$isExpend;
                    $event.stopPropagation();
                };

                $scope.getItemIcon = function (item) {
                    var isLeaf = $scope.isLeaf(item);

                    if (isLeaf) {
                        //return 'fa fa-leaf';
                        return 'glyphicon glyphicon-leaf';
                    }

                    //return item.$$isExpend ? 'fa fa-minus' : 'fa fa-plus';
                    return item.$$isExpend ? 'glyphicon glyphicon-minus' : 'glyphicon glyphicon-plus';
                };

                $scope.isLeaf = function (item) {
                    return !item.children || !item.children.length;
                };

                $scope.warpCallback = function (callback, item, $event) {
                    ($scope[callback] || angular.noop)({
                        $item: item,
                        $event: $event
                    });
                };

               
            }]
        };
}]);
//})(window,angular);