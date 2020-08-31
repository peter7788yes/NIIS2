$(function () {
    $(document).on("click", "#addBtn", function (e) {
        location.href = "/Vaccination/RecordM/RegisterData_Add.aspx";
        e.preventDefault();
        return false;
    });
});

angular.module("MyApp", ["FilterM"])
        //.directive('watchChange', function() {
        //    return {
        //        scope: {
        //            onchange: '&watchChange'
        //        },
        //        link: function(scope, element, attrs) {
        //            element.on('input', function() {
        //                scope.onchange();
        //            });
        //        }
        //    };
        //})
         .controller("MyController", ["$scope", "$http", function ($scope, $http) {

             $scope.VM = {};
             //$scope.VM.location = "";
             //$scope.VM.locationID = 0;
             $scope.VM.locationObj = {};
             $scope.VM.locationObj.text = "";
             $scope.VM.locationObj.id =  0;
             $scope.VM.VaccineSelect = [{ I: "0", V: "請選擇" }];
             $scope.VM.selectCheck = "0";
             $scope.TM = [];
             $scope.TM.tbData1 = [];
             $scope.TM.tbData2 = [];
             
             $scope.openOrgs = function (record) {
                 popUpWindow("/SelectSingleOrg.aspx", "SelectSingleOrg", 930, 450);
           
             };

            

             $scope.changeVaccine = function () {

                 var postData = {};
                 postData["i"] = $scope.VM.locationObj.id;

                 //$http.post("/Vaccination/ParameterM/BatchSetting_VaccineOP.aspx", postData)
                 $http({
                     method: 'POST',
                     url: "/Vaccination/ParameterM/BatchSetting_VaccineOP.aspx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
                 .success(function (data, status, headers, config) {
                   
                                   

                                     data.list1 = data.list1 || [];
                                     if (data.list1.length > 0) {
                                          $scope.VM.VaccineSelect = data.list1;
                                          $scope.VM.selectCheck = data.list1[0].I;
                                          $("#dv").show();
                                     }
                                      else {
                                          $scope.VM.VaccineSelect = [{ I: "0", V: "請選擇" }];
                                          $scope.VM.selectCheck = "0";
                                          $scope.TM.tbData1 = [];
                                          $("#dv").hide();
                                     }
                                  
                                     $scope.TM.tbData1 = [];
                                     $scope.TM.tbData2 = [];
                                   
                                     $scope.TM.tbData1 = data.list2;

                                     angular.forEach($scope.TM.tbData1, function (value, key) {
                                         if(value.DBVID>0)
                                         {
                                             $scope.TM.tbData2.push(value);
                                         }
                                     });

                                   
                                  
                  })
                  .error(function (data, status, headers, config) {
                      // called asynchronously if an error occurs
                      // or server returns response with an error status.
                  });
               

              
             };

             $scope.changeSelect = function () {

                 $scope.VM.selectCheck = $scope.VM.selectCheck || 0;
                 var postData = {};
                 postData["i"] = $scope.VM.selectCheck;


                 //$http.post("/Vaccination/ParameterM/BatchSetting_xGetDefaultBatchVaccineOP.aspx", postData)
                 $http({
                     method: 'POST',
                     url: "/Vaccination/ParameterM/BatchSetting_xGetDefaultBatchVaccineOP.aspx",
                     data: $.param(postData),
                     headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                 })
                 .success(function (data, status, headers, config) {
                     $scope.TM.tbData1 = data;
                     //$("#trs1").show();
                     //$("#trs2").show();
                 })
                 .error(function (data, status, headers, config) {
                     // called asynchronously if an error occurs
                     // or server returns response with an error status.
                 });

                 // $.ajax({
                 //     cache: false,
                 //     type: "POST",
                 //     url: "/Vaccination/ParameterM/BatchSetting_xGetDefaultBatchVaccineOP.aspx",
                 //     data: postData
                 // })
                 //.done(function (data) {
                 //    data = data || [];
                 //    setTimeout(function () {
                 //        $scope.$apply(function () {
                 //            $scope.TM.tbData1 = data;
                 //            //console.log($scope.TM.tbData1);
                 //            $("#trs1").show();
                 //            $("#trs2").show();
                 //        });
                 //    }, 1);
                 // })
                 //  .fail(function (jqXHR, textStatus) {
                 //  });
             };

             $scope.goSetting = function (record) {

                 if (record.DBVID == 0) {

                     var postData = {};
                     postData["b"] = record.VBID;
                     postData["d"] = record.I;

                     $http({
                         method: 'POST',
                         url: "/Vaccination/ParameterM/BatchSetting_AddOP.aspx",
                         data: $.param(postData),
                         headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                     })
                    .success(function (data, status, headers, config) {
                        data.chk = data.chk || 0;
                        if (data.chk > 0) {
                            alert('儲存成功');
                            record.DBVID = data.chk;
                            $scope.TM.tbData2.push(record);
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
                     var postData = {};
                     postData["d"] = record.DBVID;

                     $http({
                         method: 'POST',
                         url: "/Vaccination/ParameterM/BatchSetting_RemoveOP.aspx",
                         data: $.param(postData),
                         headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                     })
                    .success(function (data, status, headers, config) {
                        data.chk = data.chk || 0;
                        if (data.chk > 0) {
                            alert('儲存成功');

                            angular.forEach($scope.TM.tbData2, function (value, key) {
                                if (value.DBVID == record.DBVID) {
                                    $scope.TM.tbData2.splice(key, 1);
                                }
                            });

                            record.DBVID = 0;
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
               
             };


             $scope.openOrgs = function (record) {
                 popUpWindow("/SelectSingleOrg.aspx", "SelectSingleOrg", 930, 450);

             };

             

             $scope.goRemove = function (record) {

                     var postData = {};
                     postData["d"] = record.DBVID;

                     $http({
                         method: 'POST',
                         url: "/Vaccination/ParameterM/BatchSetting_RemoveOP.aspx",
                         data: $.param(postData),
                         headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                     })
                    .success(function (data, status, headers, config) {
                        data.chk = data.chk || 0;
                        if (data.chk > 0) {
                            alert('儲存成功');

                            angular.forEach($scope.TM.tbData2, function (value, key) {
                                if (value.DBVID == record.DBVID) {
                                    $scope.TM.tbData2.splice(key, 1);
                                }
                            });

                            record.DBVID = 0;
                        }
                        else {
                            alert('儲存失敗');
                        }
                    })
                    .error(function (data, status, headers, config) {
                        // called asynchronously if an error occurs
                        // or server returns response with an error status.
                    });

             };

             var popUpWindow = function (url,target , title, w, h) {
                 var left = (screen.width / 2) - (w / 2);
                 var top = (screen.height / 2) - (h / 2);
                 return window.open(url, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
             }
}]);

var getCode = function (code) {
    var element = document.querySelector('#tbLocation');
    element.value = code.text;
    element.focus();
    var controllerElement = document.querySelector('section');
    var controllerScope = angular.element(controllerElement).scope();
    controllerScope.$apply(function () {
        controllerScope.VM.locationObj = code;
        controllerScope.changeVaccine();
    });
};
