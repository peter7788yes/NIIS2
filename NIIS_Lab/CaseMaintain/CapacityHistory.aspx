<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="CapacityHistory.aspx.cs" Inherits="CaseMaintain_CapacityHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
<link href="/css/design.css" rel="stylesheet" type="text/css">
</asp:Content> 
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
<section class="Content2" ng-app="MyApp" ng-controller="MyController" ng-cloak>
  <h2>身分別歷史</h2>
  
  <!-- form start-->
  <form id="form1" runat ="server" >
    <div class="formTb formTb2">
      <table>
        <tr>
          <th>身分別：</th>
          <td>
          <asp:DropDownList ID="ddlCapacity" CssClass ="CapacityID" runat="server"></asp:DropDownList>
            <input type="button" value="查詢"  id="SearchBtn" ng-click="changePage()" class="btn"></td>
        </tr>
      </table>
    </div>
    <!--表格 start -->
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
    
  <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
      <table>
        <tr>
          <th scope="col" >序號</th>
          <th scope="col" >工作人員</th>
          <th scope="col" >工作人員單位</th>
          <th scope="col">狀態</th>
          <th scope="col">時間</th>
        </tr> 
        <tr ng-repeat="record in TM.data track by $index">
     
              <td class="aCenter" ng-bind='record["S"]'></td>
                  <td class="aCenter" ng-bind='record["U"]'></td>
          <td class="aCenter" ng-bind='record["O"]' > </td>
               <td class="aCenter" ng-bind='record["K"]' > </td>
         <td class="aCenter" ng-bind='record["D"] '></td>
        </tr> 
      </table>
    </div>
  <div class="formBtn">
     <input type="button" name="ok" id="ok" value="確認" class="btn" />
      <input type="button" name="cancel" id="cancel" value="取消" class="btn" />
      <div id="CaseIdDiv" runat ="server" class="CaseID" style="display:none"></div>
    </div>
  </form>
  <!-- form end--> 
  
</section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">
<script >
    var CaseID = '<%=CaseID.ToString() %>';
</script>
    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="../js/ang/angular-1.4.3.js" type="text/javascript"></script> 
    <script src="../js/ang/PageM.js" type="text/javascript"></script>
    <script src="../js/ang/TableM.js" type="text/javascript"></script>
     <script src="../js/ang/FilterM.js" type="text/javascript"></script>
    <script src="../js/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js"></script>

    <script src="CapacityHistory.js" type="text/javascript"></script>

</asp:Content>

