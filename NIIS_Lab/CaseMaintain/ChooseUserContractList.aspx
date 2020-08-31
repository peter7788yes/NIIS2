<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ChooseUserContractList.aspx.cs" Inherits="CaseMaintain_ChooseUserContractList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
<link href="/css/design.css" rel="stylesheet" type="text/css">
</asp:Content> 
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
<section class="Content2" ng-app="MyApp" ng-controller="MyController" ng-cloak>
  <h2>新增聯絡人</h2>
  
  <!-- form start-->
  <form>
    <div class="formTb formTb2">
      <table>
        <tr>
          <th>證號或姓名：</th>
          <td><input name="NameOrIdNo" id="NameOrIdNo" type="text"  class="text03">
            <input type="button" value="查詢"  id="SearchBtn" ng-click="changePage()" class="btn"></td>
        </tr>
      </table>
    </div>
    <!--表格 start -->
        <page-nav  ng-model="PM" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
    
  <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
      <table>
        <tr>
          <th scope="col" >選入</th>
          <th scope="col" >姓名</th>
          <th scope="col" >身分證號</th>
          <th scope="col">出生日期</th>
        </tr>
        <tr ng-repeat="record in TM.data track by $index">
          <td class="aCenter"><input type="submit" name="Choose" id="Choose" value="確定" class="btn" ng-click="goDetail(record)"></td>
              <td class="aCenter" ng-bind='record["N"]'></td>
          <td class="aCenter" ng-bind='record["I"]' > </td>
         <td class="aCenter" ng-bind='record["MBD"] | SimpleTaiwanDate '></td>
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
<script>
    var CaseID = '<%=CaseID  %>';

</script>
    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="../js/ang/angular-1.4.3.js" type="text/javascript"></script> 
    <script src="../js/ang/PageM.js" type="text/javascript"></script>
    <script src="../js/ang/TableM.js" type="text/javascript"></script>
     <script src="../js/ang/FilterM.js" type="text/javascript"></script>
    <script src="../js/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js"></script>
         <script src="../js/other/commUtil.js" type="text/javascript"></script>
    <script src="ChooseUserContractList.js" type="text/javascript"></script>

</asp:Content>

