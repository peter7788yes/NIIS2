$(function () {

    $(document).on("click", "#SearchBtn", function (e) {

        if (Page_ClientValidate()) { 
            angular.element('#MyController').scope().changePage(1);
            angular.element('#MyController').scope().$apply();

        }
        e.preventDefault();
        return false;
    });

    $(document).on("click", "#cancel", function (e) {
         window.close();
        e.preventDefault();
        return false;
    }); 

});
 
function  LoadContactList() {
    window.opener.LoadContactList();
    window.close();
}
function  AddContactTr(iContactID) {
    window.opener.AddContactTr(iContactID);
    window.close();
}



angular.module("MyApp", ["PageM", "TableM", "FilterM"])
         .controller("MyController", ["$scope", "PageProvider", "TableProvider", function ($scope, PageProvider, TableProvider, hotkeys) {

             $scope.PM = PageProvider;
             $scope.TM = {};
             $scope.changePage = function (pageIndex) {
                 $("#tmBlock").show();
                 var pgData = $scope.PM.genPageData(pageIndex);
                 var postData = {}; 
                 postData.NameOrIdNo = $(".NameOrIdNo").val();
                 postData = $scope.PM.filterPageData(pgData, postData);

                 $scope.PM.changePage("/CaseMaintain/ChooseUserContactListOP.aspx", postData, function (data) {
                     $scope.TM.data = data.message;
                     $scope.$apply(function () { });
                 });
             };


             // $scope.changePage(1);

             $scope.goDetail = function (record) {

                 OpenWindowWithPostOptions("/CaseMaintain/UserContact.aspx", 605, 405, "UserContract", { c: record["C"], i: CaseID });
                 
             };



         } ]);

      