var menuCallback = function (selectedNode) {
    window.parent.document.getElementById('cpIframe').src = selectedNode.pg;//"/LogM/log.aspx";
    ////alert(url);
    //var element = window.parent.document.getElementById('cpIframe');
    ////alert(1);

};

//var doOP = function (url, postData, callback) {
//    $.ajax({
//        type: "POST",
//        cache: false,
//        url: url,
//        data: postData
//    })
//    .done(function (data) {
//        callback(data);
//    })
//    .fail(function (jqXHR, textStatus) {

//    });
//};

var GenMenuRecursive = function (nowNode, innerNode) {
    var myParent;
    innerNode.c = innerNode.c || [];
    for (var i = 0; i <= innerNode.c.length - 1 ; i++) {
        if (nowNode.P == innerNode.c[i].i) {
            myParent = innerNode.c[i];
        }
    }

    if (myParent != null) {
        myParent.c = myParent.c || [];
        myParent.c.push(nowNode);
    }
    else {
        innerNode.c.forEach(function (item) {
            GenMenuRecursive(nowNode, item);
        });
    }
}

angular.module("MyApp", ["angularTreeview"])
        .controller("MyController", ["$scope", function ($scope) {

            var leftMenuOrigin = localStorage.getItem("leftMenuOrigin");
            //var leftMenuOriginObj = JSON.parse(leftMenu);
            var leftMenu = localStorage.getItem("leftMenu");
            var leftMenuObj = JSON.parse(leftMenu);

            if (leftMenu != undefined && myTreeDataString == leftMenu) {
                $scope.treedata = leftMenuObj;
            }
            else {
                var lastNode = {};
                var rtn = [];

                localStorage.setItem("leftMenuOrigin", myTreeDataString);

                myTreeDataObj.forEach(function (item) {
                    if (item.P == 0) {
                        rtn.push(item);
                        lastNode = item;
                        lastNode.c = lastNode.c || [];
                    }
                    else {
                        if (lastNode != null) {
                            if (lastNode.i == item.P) {
                                lastNode.c = lastNode.c || [];
                                lastNode.c.push(item);
                            }
                            else {
                                GenMenuRecursive(item, lastNode);
                            }
                        }
                    }

                });

                $scope.treedata = rtn;
                localStorage.setItem("leftMenu", JSON.stringify(rtn));
            }



            //$scope.treedata = [];
            //doOP("/LeftMenuOP.aspx", {}, function (data) {
            //    //alert(data);
            //    //console.log(base64.decode(data));
            //    //base64.encode('Base64 string to decode');
            //    //JSON.parse(base64.decode(data));
            //    //var myData = [];
            //    //angular.copy(data, myData);

            //    //var ary = [];
            //    //angular.forEach(myData, function (value, key) {
            //    //    ary.push(value);
            //    //});

            //    //angular.forEach(ary, function (value, key) {
            //    //    value.children = [];
            //    //});

            //    //$scope.treedata = ary;
            //    //$scope.$apply(function () {
            //    //});

            //    $scope.treedata = data;
            //    $scope.$apply(function () {
            //    });

            //});


            //[
            //    {
            //        "label": "User", "id": "role1", "children": [
            //          { "label": "subUser1", "id": "role11", "children": [] },
            //          {
            //              "label": "subUser2", "id": "role12", "children": [
            //                {
            //                    "label": "subUser2-1", "id": "role121", "children": [
            //                      { "label": "subUser2-1-1", "id": "role1211", "children": [] },
            //                      { "label": "subUser2-1-2", "id": "role1212", "children": [] }
            //                    ]
            //                }
            //              ]
            //          }
            //        ]
            //    },
            //    { "label": "Admin", "id": "role2", "children": [] },
            //    { "label": "Guest", "id": "role3", "children": [] }
            //];



        }]);


angular.bootstrap(document.getElementById("MyApp"), ["MyApp"]);


