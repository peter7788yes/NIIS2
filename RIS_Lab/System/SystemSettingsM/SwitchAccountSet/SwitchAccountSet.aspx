<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SwitchAccountSet.aspx.cs" Inherits="System_SystemSettingsM_SwitchAccountSet_SwitchAccountSet" MasterPageFile="~/MasterPage/MasterPage.master" %>
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
    <div id="SwitchAccountSetApp" ng-controller="SwitchAccountSetController" ng-cloak>
         <form name="form1" id="form1" >
            <div class="formTb formTb6">
                <table>
                  <tr>
                    <td colspan="2">
                        <%if(SearchPower.HasPower){ %>
                        <input ng-init="VM.OrgName='<%=OrgName%>'" id="OrgName" ng-model="VM.OrgName" name="OrgName" type="text" value="" class="text03" ng-click="openOrgs()" readonly/>
                        <input ng-init="VM.OrgID='<%=OrgID%>'" id="OrgID" ng-model="VM.OrgID" name="OrgID" type="hidden" />
                        <img  src="/images/location.png" ng-click="openOrgs()" />
                        <a href="javascript:void(0);" id="refreshBtn" ng-click="refresh()"></a>    
                        <%}else{ %>
                        <input ng-init="VM.OrgName='<%=OrgName%>'" id="OrgName" ng-model="VM.OrgName" name="OrgName" type="text" value="" class="text03" readonly/>
                        <%} %>
                    </td>
                  </tr>
                </table>
            </div>
         </form>
         <section class="page">
            <page-nav ng-model="PM" on-change-page-d="Search(pgIndex)" template-url="/html/ang_template/PageNav_Common.html"></page-nav>
         </section>
         <div class="listTb">
            <table  border="1" ng-cloak>
                <tr>
                    <th scope="col" width="10%">序號</th>
                    <th scope="col" width="80%">單位名稱</th>
                    <th scope="col" width="10%">設定</th>
                </tr>
                <tr ng-repeat="record in TM.tbData track by $index">
                    <td class="aCenter" ng-bind='record["c1"]'></td>
                    <td class="aCenter" ng-bind='record["c4"]'></td>
                    <td class="aCenter"><a href="javascript:void(0);" ng-click="TransferSetMonth(record)"><img ng-src="/images/icon_tick.png"></a></td>
                </tr>
            </table>
         </div>
    </div>   
</asp:Content>
   
<asp:Content ID="jsCT" ContentPlaceHolderID="jsCP" Runat="Server">
     <script>
        var OrgID = <%=OrgID%>;
     </script>
     <%:Scripts.Render("~/bundles/SwitchAccountSet_JS")%>
</asp:Content>