<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LocationSetting_AddVaccine.aspx.cs" Inherits="Vaccination_ParameterM_LocationSetting_AddVaccine" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" runat="Server">
    <%=HeadScript %>
</asp:Content>


<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" runat="Server">
    <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content>


<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" runat="Server">
    <%=BodyClass %>
</asp:Content>

<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" runat="Server">
<section class="Content3" ng-app="MyApp" ng-controller="MyController" ng-cloak>
  <h2>新增接種疫苗</h2>
  <div class="formBtn  formBtnright">
        <input type="button"  value="確定" class="btn" ng-click="goEnter()" />
        <input type="button"  value="取消" id="cancelBtn" class="btn" />
  </div>
  <div class="listTb">

      <table>
                <tr>
                    <th scope="col">選擇</th>
                    <th scope="col">劑別代號</th>
                    <th scope="col">疫苗名稱</th>
                </tr>
                <tr ng-repeat="record in TM.data track by $index">
                      <td class="aCenter"><input type="checkbox"  class="btn" ng-model="record['IC']" /></td>
                      <td class="aCenter" ng-bind='record["VC"]'></td>
                      <td class="aCenter" ng-bind='record["VE"]'></td>
                </tr>
            </table>
</div>
  </section>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="jsCP" runat="Server">
    <script>
        var ary = <%=ListJson%>;
    </script>
     <%:Scripts.Render("~/bundles/LocationSetting_AddVaccine_JS")%>
</asp:Content>
