<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrgVaccSet.aspx.cs" Inherits="Vaccine_VaccineParameterM_OrgVaccSet_OrgVaccSet" MasterPageFile="~/MasterPage/MasterPage.master" %>
<%@ Import Namespace="System.Web.Optimization" %>
<asp:Content ID="headJsCT" ContentPlaceHolderID="headJsCP" Runat="Server">
      <%=HeadScript %>
</asp:Content> 
<asp:Content ID="cssCT" ContentPlaceHolderID="cssCP" Runat="Server">
      <%:Styles.Render("~/bundles/Common_CSS")%>
</asp:Content> 
<asp:Content ID="bodyClassCT" ContentPlaceHolderID="bodyClassCP" Runat="Server" >
     <%=BodyClass %>
</asp:Content> 
<asp:Content ID="cpCT" ContentPlaceHolderID="ctCP" Runat="Server">
<div class="path"></div>
    <section class="Content" ng-app="OrgVaccSetApp" ng-controller="OrgVaccSetController" ng-cloak>
        <%if(SearchPower.HasPower){ %>
        <div class="choose">
            <label for="">
            <span>疫苗狀態</span>
                <select ng-model="VM.SelectStatus" ng-change="GetData()">
                <option ng-repeat="option in VM.Status" value="{{option.EV}}" ng-bind="option.EN"></option>
                </select>
            </label>
         </div>
         <%} %>
         <div class="listTb">
            <table  border="1" ng-cloak>
                <tr>
                    <th scope="col" width="5%">序號</th>
                    <th scope="col" width="15%">疫苗代碼</th>
                    <th scope="col" width="40%">疫苗名稱</th>
                    <th scope="col" width="10%">疫苗狀態</th>
                    <th scope="col" width="15%">安全庫存量</th>
                    <th scope="col" width="15%">效期提醒天數</th>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ng-bind='record["c1"]'></td>
                    <td class="aCenter" style="white-space:nowrap;" ng-bind='record["c3"]'></td>
                    <td ng-bind='record["c4"]'></td>
                    <td class="aCenter" ng-bind='Status(record)'></td>
                    <%if(ModeifyPower.HasPower){ %>
                    <td class="aCenter" ><input ng-model='record["c6"]' type="text" value="" size="8" ng-blur="SaveData(record)" /></td>
                    <td class="aCenter" ><input ng-model='record["c7"]' type="text" value="" size="8" ng-blur="SaveData(record)" /></td>
                    <%}else{ %>
                    <td class="aCenter" ng-bind='record["c6"]'></td>
                    <td class="aCenter" ng-bind='record["c7"]'></td>
                    <%} %>
                </tr>
            </table>
         </div>
    </section>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <script>
         var StatusData = <%=VaccineStatus %>;
     </script>
     <%:Scripts.Render("~/bundles/OrgVaccSet_JS")%>
</asp:Content> 
