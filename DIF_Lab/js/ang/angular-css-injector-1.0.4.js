'use strict';
/*
* angular-css-injector v1.0.4
* Written by Gabriel Delépine
* Special thanks to (github users) : @kleiinnn
* License: MIT
* https://github.com/Yappli/angular-css-injector/
*/
angular.module('angular.css.injector', [])
//$interpolateProvider插入服務
//$interpolateProvider.startSymbol() 是指 " {{ " 符號
//$interpolateProvider.endSymbol() 是指 " {{ " 符號
.provider('cssInjector', ['$interpolateProvider', function($interpolateProvider) {

	var singlePageMode = false;

	function CssInjector($compile, $rootScope, $rootElement){
        // Variables
        var head = angular.element(document.getElementsByTagName('head')[0]);
        var scope;

        // Capture the event `locationChangeStart` when the url change. If singlePageMode===TRUE, call the function `removeAll`
        $rootScope.$on('$locationChangeStart', function()
        {
            if(singlePageMode === true)
                removeAll();
        });

        // Always called by the functions `addStylesheet` and `removeAll` to initialize the variable `scope`
        var _initScope = function()
        {
            if(scope === undefined)
            {
                scope = $rootElement.scope();
            }
        };

        // Used to add a CSS files in the head tag of the page
        var addStylesheet = function(href)
        {
            _initScope();

            if(scope.injectedStylesheets === undefined)
            {
                scope.injectedStylesheets = [];
                head.append($compile("<link ng-repeat='stylesheet in injectedStylesheets' ng-href='" + $interpolateProvider.startSymbol() + "stylesheet.href" + $interpolateProvider.endSymbol() + "' rel='stylesheet' />")(scope)); // Found here : http://stackoverflow.com/a/11913182/1662766
            }
            else
            {
                for(var i in scope.injectedStylesheets)
                {
                    if(scope.injectedStylesheets[i].href == href) // An url can't be added more than once. I use a loop FOR, not the function indexOf to make the code IE < 9 compatible
                        return;
                }
            }

            scope.injectedStylesheets.push({href: href});
        };

	    //移除直接移除$scope.injectedStylesheets陣列就可以移除直接ng-repeat產生的檔案
        //因為是MVVM
		var remove = function(href){
			_initScope();

			if(scope.injectedStylesheets){
				for(var i = 0; i < scope.injectedStylesheets.length; i++){
					if(scope.injectedStylesheets[i].href === href){
						scope.injectedStylesheets.splice(i, 1);
						return;
					}
				}
			}
		};

	    //因為是MVVM
	    //移除直接移除$scope.injectedStylesheets陣列就可以移除直接ng-repeat產生的檔案
        // Used to remove all of the CSS files added with the function `addStylesheet`
        var removeAll = function()
        {
            _initScope();

            if(scope.injectedStylesheets !== undefined)
                scope.injectedStylesheets = []; // Make it empty
        };

        return {
            add: addStylesheet,
			remove: remove,
            removeAll: removeAll
        };
	}

	this.$get = ['$compile', '$rootScope', '$rootElement', function($compile, $rootScope, $rootElement){
		return new CssInjector($compile, $rootScope, $rootElement);
	}];

	this.setSinglePageMode = function(mode){
		singlePageMode = mode;
		return this;
	}
}]);