$(function () {
    $(document).on("click", "#addBtn", function (e) {
        location.href = "/Vaccination/ParameterM/LocationSetting_Add.aspx";
        e.preventDefault();
        return false;
    });

    $(document).on("click", "#cancelBtn", function (e) {
        window.close();
        e.preventDefault();
        return false;
    });
});

angular.module("MyApp", [])
         .controller("MyController", ["$scope" ,  function ($scope) {
             $scope.TM = {};
             $scope.TM.data = ary;

             $scope.goEnter = function () {
                 var rtn = {};
                 rtn["text"] = "";
                 rtn["ids"] = [];

                 angular.forEach($scope.TM.data, function (value, key) {
                     value.IC = value.IC || false;
                     if (value.IC == true)
                     {
                         rtn["text"] += value.VC + ",";
                         rtn["ids"].push(value.I);
                     }
                    
                 });

                 rtn["text"] = rtn["text"].substring(0, rtn["text"].length - 1);
                 //console.log(rtn);
                 window.opener.getIds(rtn);
                 window.close();
             };


}]);

