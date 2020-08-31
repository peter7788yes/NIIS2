/*
	@license Angular Treeview version 0.1.6
	ⓒ 2013 AHN JAE-HA http://github.com/eu81273/angular.treeview
	License: MIT


	[TREE attribute]
	angular-treeview: the treeview directive
	tree-id : each tree's unique id.
	tree-model : the tree model on $scope.
	node-id : each node's id
	node-label : each node's label
	node-children: each node's children

	<div
		data-angular-treeview="true"
		data-tree-id="tree"
		data-tree-model="roleList"
		data-node-id="roleId"
		data-node-label="roleName"
		data-node-children="children" 
        data-node-callback="callback">
	</div>
*/

(function (angular) {
    'use strict';

    angular.module('angularTreeview', []).directive('treeModel', ['$compile', function ($compile) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                //tree id
                var treeId = attrs.treeId;

                //tree model
                var treeModel = attrs.treeModel;

                //node id ,給預設值
                var nodeId = attrs.nodeId || 'id';

                //node label ,給預設值
                var nodeLabel = attrs.nodeLabel || 'label';

                //children ,給預設值
                var nodeChildren = attrs.nodeChildren || 'children';

                //children ,給預設值
                //var nodeLevel = attrs.nodeLevel || 'level';
                //alert(nodeLevel);

                //children ,給預設值
                var nodeCallback = attrs.nodeCallback || function () {
                    //alert(999);
                };

                //tree template
                var template =
                	'<ul>' +
						'<li data-ng-repeat="node in ' + treeModel + '">' +
							'<i class="collapsed" data-ng-show="node.' + nodeChildren + '.length && node.collapsed" data-ng-click="' + treeId + '.selectNodeHead(node)"></i>' +
							'<i class="expanded" data-ng-show="node.' + nodeChildren + '.length && !node.collapsed" data-ng-click="' + treeId + '.selectNodeHead(node)"></i>' +
							'<i class="normal" data-ng-hide="node.' + nodeChildren + '.length"></i> ' +
							'<span data-ng-class="node.selected" data-ng-click="' + treeId + '.selectNodeLabel(node)">{{node.' + nodeLabel + '}}</span>' +
							'<div data-ng-hide="node.collapsed" data-tree-id="' + treeId + '" data-tree-model="node.' + nodeChildren + '" data-node-id=' + nodeId + ' data-node-label=' + nodeLabel + ' data-node-children=' + nodeChildren + '></div>' +
						'</li>' +
                	'</ul>';

                //var template =
                //  '<ul  calss=" here' + treeModel + '.c.ulclass" ng-show="' + treeModel.root + '!=undefined">' +
                //      '<li  data-ng-repeat="node in ' + treeModel + '.'+nodeChildren+'.cc">' +
                //          //'<i class="collapsed" data-ng-show="node.' + nodeChildren + '.length && node.collapsed" data-ng-click="' + treeId + '.selectNodeHead(node)"></i>' +
                //          //'<i class="expanded" data-ng-show="node.' + nodeChildren + '.length && !node.collapsed" data-ng-click="' + treeId + '.selectNodeHead(node)"></i>' +
                //          //'<i class="normal" data-ng-hide="node.' + nodeChildren + '.length"></i> ' +
                //          '<a href="#" data-ng-click="' + treeId + '.selectNodeLabel(node)">{{node.' + nodeLabel + '}}</a>' +
                //          //下面是遞迴
                //          '<div data-ng-hide="node.collapsed" data-tree-id="' + treeId + '" data-tree-model="node" data-node-id=' + nodeId + ' data-node-label=' + nodeLabel + ' data-node-children=' + nodeChildren + ' ></div>' +
                //      '</li>' +
                //  '</ul>';

                //template +=
                // '<ul  calss="' + treeModel + '.ulclass" ng-show="' + treeModel.root + '==undefined">' +
                //     '<li  data-ng-repeat="node in ' + treeModel + '.' + nodeChildren + '">' +
                //         //'<i class="collapsed" data-ng-show="node.' + nodeChildren + '.length && node.collapsed" data-ng-click="' + treeId + '.selectNodeHead(node)"></i>' +
                //         //'<i class="expanded" data-ng-show="node.' + nodeChildren + '.length && !node.collapsed" data-ng-click="' + treeId + '.selectNodeHead(node)"></i>' +
                //         //'<i class="normal" data-ng-hide="node.' + nodeChildren + '.length"></i> ' +
                //         '<a href="#" data-ng-click="' + treeId + '.selectNodeLabel(node)">{{node.' + nodeLabel + '}}</a>' +
                //         //下面是遞迴
                //         '<div data-ng-hide="node.collapsed" data-tree-id="' + treeId + '" data-tree-model="node.' + nodeChildren + '" data-node-id=' + nodeId + ' data-node-label=' + nodeLabel + ' data-node-children=' + nodeChildren + ' ></div>' +
                //     '</li>' +
                // '</ul>';


                //check tree id, tree model
                //檢查有id和model
                if (treeId && treeModel) {

                    //root node
                    if (attrs.angularTreeview) {

                        //增加abc物件
                        //create tree object if not exists
                        scope[treeId] = scope[treeId] || {};


                        //增加abc.selectNodeHead方法
                        //if node head clicks,
                        scope[treeId].selectNodeHead = scope[treeId].selectNodeHead || function (selectedNode) {
                            //Collapse or Expand
                            selectedNode.collapsed = !selectedNode.collapsed;
                        };

                        //增加abc.selectNodeLabel方法
                        //if node label clicks,
                        scope[treeId].selectNodeLabel = scope[treeId].selectNodeLabel || function (selectedNode) {

                            //remove highlight from previous node
                            if (scope[treeId].currentNode && scope[treeId].currentNode.selected) {
                                scope[treeId].currentNode.selected = undefined;
                            }

                            //set highlight to selected node
                            selectedNode.selected = 'selected';

                            //set currentNode
                            scope[treeId].currentNode = selectedNode;

                            //alert(selectedNode.id);
                            //alert(0);
                            //scope[treeId].nodeCallback();
                            //alert(1);
                            //console.log(JSON.stringify(selectedNode));
                            menuCallback(selectedNode);
                        };
                    }

                    //Rendering template.
                    element.html('').append($compile(template)(scope));
                    //var e = $compile(html)(scope);
                    //element.replaceWith($compile(template)(scope));
                }
            }
        };
    }]);
})(angular);
