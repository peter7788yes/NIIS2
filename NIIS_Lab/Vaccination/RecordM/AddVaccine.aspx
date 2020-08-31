<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddVaccine.aspx.cs" Inherits="Vaccination_RecordM_AddVaccine" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
<section class="Content3" ng-app="MyApp" ng-controller="MyController">
<h2>新增接種疫苗</h2>
<div class="close">
    <input type="button" id="closeBtn" value="取消" class="btn" />
</div>
  <div id="tmBlock" style="display:none;" class="listTb">
    <table>
  <tr>
    <th scope="col">選擇</th>
    <th scope="col">劑別代號</th>
    <th scope="col">疫苗名稱</th>
  </tr>
        <tr ng-repeat="record in TM.data track by $index">
                      <td class="aCenter"><input type="button" value="選擇" ng-click="goAdd(record)" class="btn" /></td>
                      <td class="aCenter" ng-bind='record["VI"]'></td>
                      <td class="aCenter" ng-bind='record["VC"]'></td>
        </tr>
</table>
</div>
</section>
     <script>
        var C =<%:CaseUserID %>;
        var tbAry =<%=tbAry%>;
    </script>
</asp:Content>
