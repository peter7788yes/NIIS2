<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ChooseVacList.aspx.cs" Inherits="CaseVisit_ChooseVacList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headJsCP" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cssCP" Runat="Server">
<link href="/css/design.css" rel="stylesheet" type="text/css">
</asp:Content> 
<asp:Content ID="Content4" ContentPlaceHolderID="ctCP" Runat="Server">
<section class="Content2" ng-app="MyApp" ng-controller="MyController" ng-cloak>
  <h2>選擇疫苗</h2>
  
  <!-- form start-->
  <form>
  
    <!--表格 start -->
        <page-nav  ng-model="PM"  ng-hide="true" on-change-page-d="changePage(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
    
  <div id="tmBlock" style="display:none;" class="listTb"  ng-cloak>
      <table>
        <tr>
          <th scope="col" >選入</th>
          <th scope="col" >疫苗代碼</th>
          <th scope="col" >疫苗中文名稱</th>
          <th scope="col">疫苗英文名稱</th>
        </tr>
        <tr ng-repeat="record in TM.data track by $index">
          <td class="aCenter">
          <input name="vc" type="checkbox" id="{{record['VID']}}"  value="{{record['VC']}}"   />  
          </td>
              <td class="aLeft" ng-bind='record["VC"]'></td>
          <td class="aLeft" ng-bind='record["VCN"]' > </td>
           <td class="aLeft" ng-bind='record["VEN"]' > </td>
        </tr> 
      </table>
    </div>
  <div class="formBtn">
     <input type="button" name="ok" id="ok" value="確認" class="btn" />
      <input type="button" name="cancel" id="cancel" value="取消" class="btn" /> 
    </div>
  </form>
  <!-- form end--> 
  
</section>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="jsCP" Runat="Server">
<script>

    var openerhdid = '<%=hdid %>';
    var openertbid = '<%=tbid %>';
</script>
    <script src="../js/jq/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="../js/ang/angular-1.4.3.js" type="text/javascript"></script> 
    <script src="../js/ang/PageM.js" type="text/javascript"></script>
    <script src="../js/ang/TableM.js" type="text/javascript"></script>
     <script src="../js/ang/FilterM.js" type="text/javascript"></script>
    <script src="../js/sys/menuPath.js" type="text/javascript"></script> 
    <script src="../js/date/WdatePicker.js"></script> 
    <script src="ChooseVacList.js" type="text/javascript"></script>

</asp:Content>

