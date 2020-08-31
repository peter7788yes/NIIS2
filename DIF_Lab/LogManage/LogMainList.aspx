<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="LogMainList.aspx.cs" Inherits="LogManage_LogMainList" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
  <link href="/css/design.css" rel="stylesheet" type="text/css" />  
 <link href="/css/common.css" rel="stylesheet" type="text/css" />
  <link href="/css/table.css" rel="stylesheet" type="text/css" />
  <link href="/css/page.css" rel="stylesheet" type="text/css" />
</asp:Content> 
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
    
 <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
      
        <!-- form start-->
  <form id="form1" runat="server">
  
  

  </form>
  <!-- form end--> 
   
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
            <table>
                <tr> 
                    <th scope="col" style="width :5%">序號</th> 
                    <th scope="col">檔案日期</th> 
                    <th scope="col">檔案名稱</th>
                   <th scope="col">項目</th>
                      <th scope="col">應收筆數</th>
                         <th scope="col">實收筆數</th> 
                         <th scope="col">異動成功</th> 
                            <th scope="col">異動失敗</th> 
                 
                    <th scope="col">檢視內容(檔案)</th>
                  
                    
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                     <td class="aCenter" ng-bind='record["S"]'></td> 
                       <td class="aCenter" ng-bind='record["FileDate"]'></td> 
                         <td class="aCenter" ng-bind='record["FileName"]'></td> 
                          <td class="aCenter" ng-bind='record["LogItemName"]'></td> 
                                  <td class="aCenter" ng-bind='record["MustCount"]'></td> 
                          <td class="aCenter" ng-bind='record["ActuallyCount"]'></td> 
                            <td class="aCenter" ng-bind='record["Y"]'></td> 
                          <td class="aCenter" ><a ng-click='goErrorList(record)' ng-bind='record["N"]' href="javascript:void(0);"></a></td> 
                           
                   <td class="aCenter" ><a ng-click='goDetail(record)'><img src="../images/icon_file01.png" /></a></td>  
                 </tr>
            </table>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">

 <script type="text/javascript">

     var LogCheckMainID = '<%=LogCheckMainID.ToString() %>';

 </script>

    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="../js/ang/angular-1.4.3.js" type="text/javascript"></script> 
    <script src="../js/ang/PageM.js" type="text/javascript"></script>
    <script src="../js/ang/TableM.js" type="text/javascript"></script>
     <script src="../js/ang/FilterM.js" type="text/javascript"></script>
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js"></script>
      <script src="../js/other/commUtil.js" type="text/javascript"></script>
    <script src="LogMainList.js" type="text/javascript"></script>
</asp:Content>

