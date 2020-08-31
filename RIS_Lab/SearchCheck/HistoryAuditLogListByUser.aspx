<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="HistoryAuditLogListByUser.aspx.cs" Inherits="SearchCheck_HistoryAuditLogListByUser" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
  <link href="/css/design.css" rel="stylesheet" type="text/css" />  
 <link href="/css/common.css" rel="stylesheet" type="text/css" />
  <link href="/css/table.css" rel="stylesheet" type="text/css" />
  <link href="/css/page.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyClassCP" Runat="Server">
  <%=BodyClass %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
    
 <section class="Content" ng-app="MyApp" ng-controller="MyController" ng-cloak>
        <div class="path"></div>
      
        <!-- form start-->
  <form runat="server" id="form1">
  
   

  </form>
  <!-- form end-->  
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
        <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
            <table>
                <tr>
                    <th scope="col" style="width:5%">序號</th>
                    <th scope="col">年月</th> 
                     <th scope="col">類別</th>  
                    <th scope="col">筆數</th>     
                           <th scope="col">查核結果</th>   
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                    <td class="aCenter" ng-bind='record["S"]' ></td>
                       <td class="aCenter" ng-bind='record["YearMonth"]'></td>   
                   <td class="aCenter" ng-bind='record["SearchKindName"]'></td> 
                   
            <td class="aCenter"  ng-bind='record["SumCount"]' ></td>
             
                   <td class="aCenter"  ng-bind='record["AuditResultName"]' > </td>
              
                </tr>
            </table>
        </div>
         <div class="formBtn formBtncenter">
      <input type="button"   id="btnReturn" onclick ="javascript:history.go(-1);"  value="確定" class="btn" />
  
    </div>
    </section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">
  <script>
      var UserID = '<%=UserID.ToString() %>';
  </script>

    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="../js/ang/angular-1.4.3.js" type="text/javascript"></script> 
    <script src="../js/ang/PageM.js" type="text/javascript"></script>
    <script src="../js/ang/TableM.js" type="text/javascript"></script>
     <script src="../js/ang/FilterM.js" type="text/javascript"></script>
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js"></script>

    <script src="HistoryAuditLogListByUser.js" type="text/javascript"></script>
</asp:Content>

