<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LocationSetting_AddVaccine.aspx.cs" Inherits="Vaccination_ParameterM_LocationSetting_AddVaccine" MasterPageFile="~/MasterPage/Custom/DecoratedMasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ContentPlaceHolderID="ctCP" runat="Server">
<section class="Content3" ng-app="MyApp" ng-controller="MyController">
  <h2>新增接種疫苗</h2>
  <div class="listTb">
            <table>
                <tr>
                    <th scope="col">選擇</th>
                    <th scope="col">疫苗代碼</th>
                    <th scope="col">疫苗中文名稱</th>
                    <th scope="col">疫苗英文名稱</th>
                </tr>
                <tr id="tmBlock" ng-repeat="record in TM.data track by $index">
                      <td class="aCenter"><input type="checkbox"  class="btn" ng-model="record['IC']" /></td>
                      <td class="aCenter" ng-bind='record["VI"]'></td>
                      <td ng-bind='record["VC"]'></td>
                      <td ng-bind='record["VE"]'></td>
                </tr>
            </table>
    </div>
    <div class="formBtn">
        <input type="button"  value="確定" class="btn" ng-click="goEnter()" />
        <input type="button"  value="取消" id="cancelBtn" class="btn" />
  </div>
  </section>
  <script>
        var ary = <%=ListJson%>;
  </script>
</asp:Content>
