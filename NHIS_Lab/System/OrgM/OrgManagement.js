angular.module("MyApp", ["TreeM"])
         .controller("MyController", ["$scope", "$http", function ($scope, $http) {
          //var VM = this;
          $scope.VM = {};
          $scope.VM.selectedItemParent = "";
          $scope.VM.selectedItem = { selectedItem :0};
          $scope.VM.dataItem = { I: 0, AC: "", N: "", EN: "", SN: "", OL: 0, AS: 0, ON: 0 };
          $scope.VM.dataItem.IP=[{ IS: "", IE: "",i1:0,i2:0,i3:0,i4:0,i5:0,i6:0,i7:0,i8:0 }];
    
          var getIpPart = function () {
            
              angular.forEach($scope.VM.dataItem.IP, function (item, index) {
                  item.IS = item.IS || "";
                  item.IE = item.IE || "";
                 
                  item.i1 = 0;
                  item.i2 = 0;
                  item.i3 = 0;
                  item.i4 = 0;
                  item.i5 = 0;
                  item.i6 = 0;
                  item.i7 = 0;
                  item.i8 = 0;

                  try
                  {
                  

                      var ISary = item.IS.split('.');
                      var IEary = item.IE.split('.');
                      //console.log(ISary);
                      //console.log(IEary);
                      item.i1 = parseInt(ISary[0]);
                      item.i2 = parseInt(ISary[1]);
                      item.i3 = parseInt(ISary[2]);
                      item.i4 = parseInt(ISary[3]);

                      item.i5 = parseInt(IEary[0]);
                      item.i6 = parseInt(IEary[1]);
                      item.i7 = parseInt(IEary[2]);
                      item.i8 = parseInt(IEary[3]);

                     
                  }
                  catch(e)
                  {

                  }
              
                 
              });

              angular.forEach($scope.VM.dataItem.IP, function (item, index) {
                 
                  if (item.i1 == null || item.i1 == undefined) item.i1 = 0;
                  if (item.i2 == null || item.i2 == undefined) item.i2 = 0;
                  if (item.i3 == null || item.i3 == undefined) item.i3 = 0;
                  if (item.i4 == null || item.i4 == undefined) item.i4 = 0;
                  if (item.i5 == null || item.i5 == undefined) item.i5 = 0;
                  if (item.i6 == null || item.i6 == undefined) item.i6 = 0;
                  if (item.i7 == null || item.i7 == undefined) item.i7 = 0;
                  if (item.i8 == null || item.i8 == undefined) item.i8 = 0;

              });
          };

        
        

          getIpPart();
    

          var OrgsMenuOrigin = sessionStorage.getItem("OrgManagementOrigin");
          //console.log(OrgsMenuOrigin);

          //var OrgsMenuOrigin2 = sessionStorage.getItem("OrgManagement");
          //console.log(OrgsMenuOrigin2);
          //console.log(OrgsMenuOrigin == data);
          if (OrgsMenuOrigin == data) {
              var finalObj = sessionStorage.getItem("OrgManagement");
              $scope.VM.tree = JSON.parse(finalObj);
              //$("#ulRoot").html(myHtml);
              //$("a").removeClass("here");
              //$("li").removeClass("hereblock");
          }
          else {
              var lastNode = {};
              var rtn = [];

              $.each(JSON.parse(data), function (index, item) {
                  //console.log(item);
                  item.children = item.children || [];
                  if (item.P == 0) {
                      rtn.push(item);
                      lastNode = item;
                  }
                  else {
                      if (lastNode != null) {
                          if (lastNode.I == item.P) {
                              lastNode.children.push(item);
                          }
                          else {
                              GenMenuRecursive(item, lastNode);
                          }
                      }
                  }

              });
              //console.log(rtn);
              $scope.VM.tree = rtn;

              sessionStorage.setItem("OrgManagementOrigin", data);
              sessionStorage.setItem("OrgManagement", JSON.stringify(rtn));
          }


          $scope.getDetail = function ($item) {
              var postData = {};
              postData["i"] = $item.I;

              $http({
                  method: 'POST',
                  url: "/System/OrgM/OrgManagementOP.aspx",
                  data: $.param(postData),
                  headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
              })
             .success(function (data, status, headers, config) {
                 $scope.VM.dataItem = data;
                 $scope.VM.dataItem.IP = $scope.VM.dataItem.IP || [];
               
                 if ($scope.VM.dataItem.IP.length == 0) {
                     $scope.VM.dataItem.IP = [{ IS: "", IE: "", i1: 0, i2: 0, i3: 0, i4: 0, i5: 0, i6: 0, i7: 0, i8: 0 }];
                 }
                 else {
                     getIpPart();
                 }
   
                 //console.log($scope.VM.dataItem);
              })
              .error(function (data, status, headers, config) {
                  // called asynchronously if an error occurs
                  // or server returns response with an error status.
              });
          };

          $scope.goSave = function () {
              if($scope.VM.dataItem.I>0)
              {
                  var postData = {};
                  postData["i"] = $scope.VM.dataItem.I;
                  postData["ac"] = $scope.VM.dataItem.AC;
                  postData["n"] = $scope.VM.dataItem.N;
                  postData["en"] = $scope.VM.dataItem.EN;
                  postData["sn"] = $scope.VM.dataItem.SN;
                  postData["ol"] = $scope.VM.dataItem.OL;
                  postData["as"] = $scope.VM.dataItem.AS;
                  postData["on"] = $scope.VM.dataItem.ON;

                  $http({
                      method: 'POST',
                      url: "/System/OrgM/OrgManagement_UpdateOP.aspx",
                      data: $.param(postData),
                      headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                  })
                 .success(function (data, status, headers, config) {
                     if (data.chk > 0) {
                         alert('儲存成功');
                         //foreach ary
                         GetParentRecursive($scope.VM.dataItem.I);
                         node.O = $scope.VM.dataItem.N;
                     }
                     else {
                         alert('儲存失敗');
                     }
                 })
                  .error(function (data, status, headers, config) {
                      // called asynchronously if an error occurs
                      // or server returns response with an error status.
                  });
              }
              else
              {
                  alert("請選擇組織單位");
              }
          };

          $scope.VM.itemClicked = function ($item) {
              $item.heightLight=true;
              //console.log($item);
              $scope.VM.selectedItem = $item;
              $scope.getDetail($item);
              //console.log($item, 'item clicked');
              $scope.VM.selectedItemParent = "";
              GetParentRecursive($item.P);
              $scope.VM.selectedItemParent = nodeName;

          };

          $scope.VM.itemCheckedChanged = function ($item) {
              //$http.post('/url', $item);
              //console.log($item, 'item checked');
          };

          //return VM;
}]);

var node = {};
var nodeName = "";
var GetParentRecursive = function (id) {
    nodeName = "";
    var controllerElement = document.querySelector('section');
    var controllerScope = angular.element(controllerElement).scope();
    RecursiveObj(controllerScope.VM.tree[0], id);
};

var RecursiveObj = function (obj, id) {
    
    if (obj.I == id) {
        nodeName = obj.O;
        node = obj;
    }
    else {
        obj.children = obj.children || [];
        $.each(obj.children, function (index, item) {
            RecursiveObj(item, id);
        });
    }
}

var GenMenuRecursive = function (nowNode, innerNode) {
    var myParent;
    innerNode.children = innerNode.children || [];

    for (var i = 0; i <= innerNode.children.length - 1 ; i++) {
        if (nowNode.P == innerNode.children[i].I) {
            myParent = innerNode.children[i];
        }
    }
    //console.log(myParent);

    if (myParent != null) {
        myParent.children = myParent.children || [];
        myParent.children.push(nowNode);
    }
    else {
        innerNode.children.forEach(function (item) {
            GenMenuRecursive(nowNode, item);
        });
    }
}
